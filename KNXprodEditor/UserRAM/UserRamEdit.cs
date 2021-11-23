using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnxProd.Model;

namespace knxprod_ns
{
    public class UserRamEdit
    {
        public static void RearrangeComObjectsInUserRam()
        {
            uint dataPtrSize;
            if (ApplicationProgramGui.selectedApplicationProgram.MaskVersion.Substring(4, 2) == "01") // BCU1
            {
                dataPtrSize = 1;
            }
            else //BCU2, BIM112
            {
                dataPtrSize = 2;
            }

            byte[] data = new byte[1 + dataPtrSize];

            uint ramPointer = 0;

            // den Start des User RAM Bereichs festlegen (aus der sblib übernommen)
            if (ApplicationProgramGui.selectedApplicationProgram.MaskVersion.Substring(4, 2) == "01" || // BCU1
                ApplicationProgramGui.selectedApplicationProgram.MaskVersion.Substring(4, 2) == "02")   // BCU2
            {
                ramPointer = 0; // USER_RAM_START_DEFAULT bei BCU1, BCU2 = 0
            }
            else // BIM112
            {
                ramPointer = 0x5FC; // USER_RAM_START_DEFAULT bei BIM112 = 0x5FC
            }

            // wir legen die RAM-Flags-Table an den Anfang des User-RAMs
            if (dataPtrSize == 1) //BCU1
            {
                data[1] = (byte)ramPointer; // RAM-Flags-Table Pointer
            }
            else //BCU2, BIM112
            {
                data[1] = (byte)(ramPointer >> 8); // RAM-Flags-Table Pointer
                data[2] = (byte)ramPointer; // RAM-Flags-Table Pointer
            }

            // Die höchste Nummer der Kommunikationsobjekte holen (Die Anzal der Tabelleneinträge ist gleich der höchsten Nummer)
            // da das erste Kommunikationsobjekt die Nummer 0 hat, muss +1 addiert werden
            uint numberOfComObjects = ComObjectsGui.GetHighestComObjectNumber() + 1; 

            // den RAM Pointer um die Anzahl der Kommunikationsobjekte erhöhen, da die Flags-Table pro Kommunikationsobjekt ein Byte Platz benötigt
            ramPointer += numberOfComObjects;

            // die Größe der Tabelle steht im ersten Byte (Anzahl der ComObjekte)
            data[0] = (byte)(numberOfComObjects); 

            for (uint comObjectNumberCounter = 0; comObjectNumberCounter < numberOfComObjects; comObjectNumberCounter++)
            {
                ComObject_t comObject = ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject.ToList().Find(x => x.Number == comObjectNumberCounter);
                if (comObject != null)
                {
                    uint comObjectSize = HandleKnxDataTypes.GetComObjectByteSize(comObject.ObjectSize);
                    byte configByte = CreateConfigByte(comObject);
                    byte typeByte = CreateTypeByte(comObject);

                    // Berechnen der Stelle des GroupObject Descriptors im Data Segment
                    uint descriptorPointer = (comObjectNumberCounter * (dataPtrSize + 2)) + dataPtrSize + 1;

                    if (dataPtrSize == 1) // BCU1
                    {
                        Array.Resize(ref data, (int)descriptorPointer + 3);
                        data[descriptorPointer] = (byte)ramPointer;
                        data[descriptorPointer + 1] = configByte;
                        data[descriptorPointer + 2] = typeByte;
                    }
                    else // BCU2, BIM112
                    {
                        Array.Resize(ref data, (int)descriptorPointer + 4);
                        data[descriptorPointer] = (byte)(ramPointer >> 8);
                        data[descriptorPointer + 1] = (byte)ramPointer;
                        data[descriptorPointer + 2] = configByte;
                        data[descriptorPointer + 3] = typeByte;
                    }
                    ramPointer += comObjectSize;
                }
            }

            // das CodeSegment finden, in dem die ComObject Tabelle (GroupObject Table) hinterlegt werden soll und die neue Data Sektion dort einhängen
            if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment != null)
            {
                ApplicationProgramStatic_tCodeAbsoluteSegment absCodeSeg =
                    ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment.ToList().Find(x => x.Id == ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.CodeSegment);
                if (absCodeSeg != null)
                {
                    absCodeSeg.Data = data;
                }
            }
            if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment != null)
            {
                ApplicationProgramStatic_tCodeRelativeSegment relCodeSeg =
                    ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment.ToList().Find(x => x.Id == ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.CodeSegment);
                if (relCodeSeg != null)
                {
                    relCodeSeg.Data = data;
                }
            }
        }

        private static byte CreateConfigByte(ComObject_t comObject)
        {
            byte configByte = 0;
            configByte = 1 << 7; // Bit 7 shall be 1
            if (HandleKnxDataTypes.ReadKNXType(comObject.TransmitFlag))
            {
                configByte |= (1 << 6);
            }
            if (HandleKnxDataTypes.ReadKNXType(comObject.WriteFlag))
            {
                configByte |= (1 << 4);
            }
            if (HandleKnxDataTypes.ReadKNXType(comObject.ReadFlag))
            {
                configByte |= (1 << 3);
            }
            if (HandleKnxDataTypes.ReadKNXType(comObject.CommunicationFlag))
            {
                configByte |= (1 << 2);
            }
            if(comObject.Priority == ComObjectPriority_t.Alert)
            {
                configByte |= (1 << 1);
            }
            else if (comObject.Priority == ComObjectPriority_t.High)
            {
                configByte |=(1 << 0);
            }
            else if (comObject.Priority == ComObjectPriority_t.Low)
            {
                configByte |= (1 << 1);
                configByte |= (1 << 0);
            }
            return configByte;
        }

        private static byte CreateTypeByte(ComObject_t comObject)
        {
            byte typeByte = 0;
            switch (comObject.ObjectSize)
            {
                case ComObjectSize_t.Item1Bit:
                    typeByte = 0;
                    break;
                case ComObjectSize_t.Item2Bit:
                    typeByte = 1;
                    break;
                case ComObjectSize_t.Item3Bit:
                    typeByte = 2;
                    break;
                case ComObjectSize_t.Item4Bit:
                    typeByte = 3;
                    break;
                case ComObjectSize_t.Item5Bit:
                    typeByte = 4;
                    break;
                case ComObjectSize_t.Item6Bit:
                    typeByte = 5;
                    break;
                case ComObjectSize_t.Item7Bit:
                    typeByte = 6;
                    break;
                case ComObjectSize_t.Item1Byte:
                    typeByte = 7;
                    break;
                case ComObjectSize_t.Item2Bytes:
                    typeByte = 8;
                    break;
                case ComObjectSize_t.Item3Bytes:
                    typeByte = 9;
                    break;
                case ComObjectSize_t.Item4Bytes:
                    typeByte = 10;
                    break;
                case ComObjectSize_t.Item6Bytes:
                    typeByte = 11;
                    break;
                case ComObjectSize_t.Item8Bytes:
                    typeByte = 12;
                    break;
                case ComObjectSize_t.Item10Bytes:
                    typeByte = 13;
                    break;
                case ComObjectSize_t.Item14Bytes:
                    typeByte = 14;
                    break;
            }
            return typeByte;
        }
    }
}