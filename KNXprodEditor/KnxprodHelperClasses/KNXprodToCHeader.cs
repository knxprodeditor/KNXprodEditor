using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnxProd.Model;



namespace knxprod_ns
{
    public static class KNXprodToCHeader
    {
        public static void CreateSblibHeader(StreamWriter headerFile, string FilePath)
        {
            string FileName = Path.GetFileNameWithoutExtension(FilePath);

            string headerFileExtension = FileName.ToUpper();

            string[] headerFileLines = { 
                "#ifndef KNXPRODEDITORHEADER_" + headerFileExtension + "_H",
                "#define KNXPRODEDITORHEADER_" + headerFileExtension + "_H",
                "/**",
                " * Header File for DIY KNX Device",
                " * generated with " + Config.AppVersion,
                " * ",
                " * Format: ",
                " * MANUFACTURER = knxMaster->Manufacturer->KnxManufacturerId",
                " * DEVICETYPE = ApplicationProgram->ApplicationNumber",
                " * APPVERSION = ApplicationProgram->ApplicationVersion",
                " * ",
                " * Parameter: EE_...",
                " * Simple parameter: EE_PARAMETER_[Name]   [Address in hex] //Addr:[Address in hex], Size:[Size in hex], Description in language 1",
                " * Union Parameter: EE_UNIONPARAMETER_[Address in hex]   [Address in hex] //Addr:[Address in hex], Size:[Size in hex], Description Parameter 1 in language 1, Description Parameter 2 in language 1, ...",
                " * ",
                " * Communication objects: COMOBJ...",
                " * COMOBJ_[Description]_[Function text]   [Communication object number] //Com-Objekt number: [number] ,Description: [in language 1], Function [in language 1]",
                " *",
                " *",
                " *",
            };

            foreach (string line in headerFileLines)
            {
                headerFile.WriteLine(line);
            }

            FillGeneralInformationInHeaderFile(headerFile);
            headerFile.WriteLine("");
            headerFile.WriteLine("");
            FillEEPROMParameterInHeaderFile(headerFile);
            headerFile.WriteLine("");
            headerFile.WriteLine("");
            FillComObjectsInHeaderFile(headerFile);

            string[] bottomFileLines = {
                "",
                "#endif",
            };

            foreach (string line in bottomFileLines)
            {
                headerFile.WriteLine(line);
            }
        }

        private static void FillGeneralInformationInHeaderFile(StreamWriter headerFile)
        {
            headerFile.WriteLine(" * Device information:");
            headerFile.WriteLine(" * Device: " + CatalogGui.selectedCatalogItem.Name);
            headerFile.WriteLine(" * ApplicationProgram: " + ApplicationProgramGui.selectedApplicationProgram.Id);
            headerFile.WriteLine(" *");
            headerFile.WriteLine(" */");
            headerFile.WriteLine("");
            headerFile.WriteLine("");

            MasterData_tManufacturer masterManufacturer = KNXprodFile.knxprodKnxMaster.MasterData.Manufacturers.ToList().Find(x => x.Id == ApplicationProgramGui.selectedApplicationManufacturer.RefId);
            headerFile.WriteLine("#define MANUFACTURER " + masterManufacturer.KnxManufacturerId.ToString() + " //!< Manufacturer ID");
            headerFile.WriteLine("#define DEVICETYPE " + ApplicationProgramGui.selectedApplicationProgram.ApplicationNumber.ToString() + " //!< Device Type");
            headerFile.WriteLine("#define APPVERSION " + ApplicationProgramGui.selectedApplicationProgram.ApplicationVersion.ToString() + " //!< Application Version");
            headerFile.WriteLine("");
            ReadHardwareVersionFromLdCtrlCompareProp(headerFile);
            headerFile.WriteLine("");
        }

        private static void ReadHardwareVersionFromLdCtrlCompareProp(StreamWriter headerFile)
        {
            foreach(LoadProcedures_tLoadProcedure loadProcedure in ApplicationProgramGui.selectedApplicationProgram.Static.LoadProcedures)
            {
                if (loadProcedure.Items != null)
                {
                    foreach (object loadProcedureItem in loadProcedure.Items)
                    {
                        ReadLoadProcedureItem(headerFile, loadProcedureItem);
                    }
                }
            }
        }

        private static void ReadLoadProcedureItem(StreamWriter headerFile, object loadProcedureItem)
        {
            if (loadProcedureItem is LdCtrlCompareProp_t)
            {
                if ((loadProcedureItem as LdCtrlCompareProp_t).PropId == 78)
                {
                    // in dem Element LdCtrlCompareProp mit der PropId 78(dez) ist an der Byteposition 0-6 die hardwareVersion eingetragen
                    // siehe: https://selfbus.myxwiki.org/xwiki/bin/view/Entwicklung/ARM_sblib_Verwendung_LPC11xx
                    byte[] inlineData = (loadProcedureItem as LdCtrlCompareProp_t).InlineData;
                    string PropCode = "";
                    if (inlineData != null)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            PropCode = PropCode + inlineData[i].ToString("X2");
                        }
                    }

                    headerFile.WriteLine("const unsigned char hardwareVersion[6] = { 0x"
                        + PropCode.Substring(0, 2) + ", 0x"
                        + PropCode.Substring(2, 2) + ", 0x"
                        + PropCode.Substring(4, 2) + ", 0x"
                        + PropCode.Substring(6, 2) + ", 0x"
                        + PropCode.Substring(8, 2) + ", 0x"
                        + PropCode.Substring(10, 2) + " }; //!< The hardware identification number hardwareVersion");
                }
            }
            if (loadProcedureItem is LdCtrlBaseChoose_t)
            {
                foreach (LdCtrlBaseChoose_tWhen when in (loadProcedureItem as LdCtrlBaseChoose_t).when)
                {
                    foreach(object whenItem in when.Items)
                    {
                        ReadLoadProcedureItem(headerFile, whenItem); // Rekursive Auflösung der choose-when Struktur
                    }
                }
            }
        }

        private static void FillEEPROMParameterInHeaderFile(StreamWriter headerFile)
        {
            uint bitOffset = 0;
            uint offset = 0;
            uint sizeInBit = 0;
            string paraDescription = "";
            string headerFileLine = "";
            string paraName = "";
            string codeSegment = "";

            //Lese die Startadresse des EEPROM Bereichs der Parameter
            //BCU_EEPROM.ReadBCUEEprom(FormKNXprodEditor.selectedApplicationProgram.Static);

            // alle Parameter durchgehen und nach Memory Einträgen suchen
            // anschließend wird jeder Parameter in die EEPROM Table eingetragen
            foreach (var parametersItem in ApplicationProgramGui.selectedApplicationProgram.Static.Parameters)
            {
                sizeInBit = 0;
                paraDescription = "";
                codeSegment = null;   

                if (parametersItem is ApplicationProgramStatic_tParameter)
                { 
                    if ((parametersItem as ApplicationProgramStatic_tParameter).Item is MemoryParameter_t)
                    {
                        offset = ((parametersItem as ApplicationProgramStatic_tParameter).Item as MemoryParameter_t).Offset;
                        bitOffset = ((parametersItem as ApplicationProgramStatic_tParameter).Item as MemoryParameter_t).BitOffset;
                        codeSegment = ((parametersItem as ApplicationProgramStatic_tParameter).Item as MemoryParameter_t).CodeSegment;

                        paraDescription = ApplicationProgramGui.languageProcessingApplication.ReadTranslation((parametersItem as ApplicationProgramStatic_tParameter).Id);
                        if(paraDescription == null)
                        {
                            paraDescription = (parametersItem as ApplicationProgramStatic_tParameter).Name;
                        }
                        paraName = (parametersItem as ApplicationProgramStatic_tParameter).Name;

                        sizeInBit = ParameterHelper.GetParameterTypeSizeInBit((parametersItem as ApplicationProgramStatic_tParameter).ParameterType);
                    }

                    /*
                     * Property Parameter hat kein CodeSegement, daher auch keinen Speicherplatz, dafür aber einen Offset und BitOffset
                     * Parameter wurde bisher nicht in einer knxprod gefuden, daher keine Analyse möglich
                     */

                    /*else if ((parametersItem as ApplicationProgramStatic_tParameter).Item is PropertyParameter_t)
                    {
                       
                        offset = ((parametersItem as ApplicationProgramStatic_tParameter).Item as PropertyParameter_t).Offset;
                        bitOffset = ((parametersItem as ApplicationProgramStatic_tParameter).Item as PropertyParameter_t).BitOffset;

                        paraDescription = KNXprodViewer.languageProcessingApplication.ReadTranslation((parametersItem as ApplicationProgramStatic_tParameter).Id);
                        if(paraDescription == null)
                        {
                            paraDescription = (parametersItem as ApplicationProgramStatic_tParameter).Name;
                        }
                        paraName = (parametersItem as ApplicationProgramStatic_tParameter).Name;

                        ParameterType_t paramType = KNXprodViewer.selectedApplicationProgram.Static.ParameterTypes.ToList().Find(x => x.Id == (parametersItem as ApplicationProgramStatic_tParameter).ParameterType);
                        if (paramType.Item is ParameterType_tTypeRestriction)
                        {
                            sizeInBit = (paramType.Item as ParameterType_tTypeRestriction).SizeInBit;
                        }
                        else if (paramType.Item is ParameterType_tTypeNumber)
                        {
                            sizeInBit = (paramType.Item as ParameterType_tTypeNumber).SizeInBit;
                        }
                    }*/
                    if (codeSegment != null)
                    {

                        //einfacher Parameter: EE_PARAMETER_[Name] [Adresse in hex] //Addr:[Adresse in hex], Size:[Größe in hex], Beschreibung in Sprache 1
                        paraName = FormatDefineString(paraDescription);
                        paraDescription = FormatCommentString(paraDescription);

                        if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment != null)
                        {
                            ApplicationProgramStatic_tCodeAbsoluteSegment parameterCodeSegment = ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment.ToList().Find(x => x.Id == codeSegment);
                            uint paraAddress = parameterCodeSegment.Address + offset + bitOffset;
                            headerFileLine = "#define EE_PARAMETER_" + paraAddress.ToString("X4") + "_" + paraName + "     0x" + paraAddress.ToString("X4") + " //!< Addr: 0x" + paraAddress.ToString("X4") + ", Size: 0x" + sizeInBit.ToString("X4") + ", " + paraDescription;
                        }
                        if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment != null)
                        {
                            ApplicationProgramStatic_tCodeRelativeSegment parameterCodeSegment = ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment.ToList().Find(x => x.Id == codeSegment);
                            uint paraAddress = parameterCodeSegment.Offset + offset + bitOffset;
                            headerFileLine = "#define EE_PARAMETER_" + paraAddress.ToString("X4") + "_" + paraName + "     0x" + paraAddress.ToString("X4") + " //!< relative Addr: 0x" + paraAddress.ToString("X4") + ", Size: 0x" + sizeInBit.ToString("X4") + ", " + paraDescription;
                        }                
                    }
                }
                else if (parametersItem is ApplicationProgramStatic_tUnion)
                {
                    if ((parametersItem as ApplicationProgramStatic_tUnion).Item is MemoryUnion_t)
                    {
                        offset = ((parametersItem as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t).Offset;
                        bitOffset = ((parametersItem as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t).BitOffset;
                        codeSegment = ((parametersItem as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t).CodeSegment;

                        foreach (UnionParameter_t unionParameter in (parametersItem as ApplicationProgramStatic_tUnion).Parameter)
                        {
                            string tempParaDescription = ApplicationProgramGui.languageProcessingApplication.ReadTranslation(unionParameter.Id);
                            if(tempParaDescription == null)
                            {
                                tempParaDescription = unionParameter.Name;
                            }
                            paraDescription += tempParaDescription + " oder ";
                        }

                        paraDescription = paraDescription.Remove(paraDescription.Length - 6); //das letzte oder wieder löschen

                        sizeInBit = (parametersItem as ApplicationProgramStatic_tUnion).SizeInBit;
                    }


                    /*
                     * Property Parameter hat kein CodeSegement, daher auch keinen Speicherplatz, dafür aber einen Offset und BitOffset
                     * Parameter wurde bisher nicht in einer knxprod gefuden, daher keine Analyse möglich
                     */

                    /*else if ((parametersItem as ApplicationProgramStatic_tUnion).Item is PropertyUnion_t)
                    {
                        offset = ((parametersItem as ApplicationProgramStatic_tUnion).Item as PropertyUnion_t).Offset;
                        bitOffset = ((parametersItem as ApplicationProgramStatic_tUnion).Item as PropertyUnion_t).BitOffset;

                        paraDescription = "Property ID: " + ((parametersItem as ApplicationProgramStatic_tUnion).Item as PropertyUnion_t).PropertyId.ToString();

                        sizeInBit = (parametersItem as ApplicationProgramStatic_tUnion).SizeInBit;
                    }*/
                    if (codeSegment != null)
                    {
                        //EE_UNIONPARAMETER_[Adresse in hex] //Addr:[Adresse in hex], Size:[Größe in hex], Beschreibung Parameter 1 in Sprache 1, Beschreibung Parameter 2 in Sprache 1, ...
                        paraName = FormatDefineString(paraDescription);
                        paraDescription = FormatCommentString(paraDescription);

                        if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment != null)
                        {
                            ApplicationProgramStatic_tCodeAbsoluteSegment parameterCodeSegment = ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment.ToList().Find(x => x.Id == codeSegment);
                            uint unionAddress = parameterCodeSegment.Address + offset + bitOffset;
                            headerFileLine = "#define EE_UNIONPARAMETER_" + unionAddress.ToString("X4") + "     0x" + unionAddress.ToString("X4") + " //!< Addr: 0x" + unionAddress.ToString("X4") + ", Size: 0x" + sizeInBit.ToString("X4") + ", " + paraDescription;
                        }
                        if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment != null)
                        {
                            ApplicationProgramStatic_tCodeRelativeSegment parameterCodeSegment = ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment.ToList().Find(x => x.Id == codeSegment);
                            uint unionAddress = parameterCodeSegment.Offset + offset + bitOffset;
                            headerFileLine = "#define EE_UNIONPARAMETER_" + unionAddress.ToString("X4") + "     0x" + unionAddress.ToString("X4") + " //!< relative Addr: 0x" + unionAddress.ToString("X4") + ", Size: 0x" + sizeInBit.ToString("X4") + ", " + paraDescription;
                        }
                    }
                }
                if (sizeInBit > 0)
                {
                    headerFile.WriteLine(headerFileLine);
                }
            }
        }

        private static void FillComObjectsInHeaderFile(StreamWriter headerFile)
        {
            // CO_[Kommunikation Objekt Nummer]_FunctionText //Beschreibung in Sprache 1

            if (ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject != null)
            {
                foreach (ComObject_t comObject in ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject)
                {
                    string coFunctionText = ApplicationProgramGui.languageProcessingApplication.ReadTranslation(comObject.Id, "FunctionText");
                    string coDescription = ApplicationProgramGui.languageProcessingApplication.ReadTranslation(comObject.Id, "Text");
                    if (coDescription == null)
                    {
                        coDescription = comObject.Text;
                    }
                    if (coFunctionText == null)
                    {
                        coFunctionText = comObject.FunctionText;
                    }

                    string coDescriptionDefine = FormatDefineString(coDescription);
                    coDescription = FormatCommentString(coDescription);

                    string coFunctionTextDefine = FormatDefineString(coFunctionText);
                    // wenn der FonctionText existiert, ein Unterstrich davor einsetzen
                    if (coFunctionTextDefine.Length > 0)
                    {
                        coFunctionTextDefine = "_" + coFunctionTextDefine;
                    }
                    coFunctionText = FormatCommentString(coFunctionText);

                    headerFile.WriteLine("#define COMOBJ_" + comObject.Number + "_" + coDescriptionDefine + coFunctionTextDefine + "     " + comObject.Number + " //!< Com-Objekt number: " + comObject.Number + ", Description: " + coDescription + ", Function: " + coFunctionText);
                }
            }
        }


        private static string FormatDefineString(string replaceString)
        {
            if (replaceString.Length > 0)
            {
                while (replaceString.Substring(replaceString.Length - 1) == " ") // Leerzeichen am Ende eleminieren
                {
                    replaceString = replaceString.Substring(0, replaceString.Length - 1);
                }

                replaceString = replaceString.Replace("\r\n", "_");
                replaceString = replaceString.Replace(" ", "_");
                replaceString = replaceString.Replace("Ä", "AE");
                replaceString = replaceString.Replace("ä", "ae");
                replaceString = replaceString.Replace("Ü", "UE");
                replaceString = replaceString.Replace("ü", "ue");
                replaceString = replaceString.Replace("Ö", "OE");
                replaceString = replaceString.Replace("ö", "oe");
                replaceString = replaceString.Replace("/", "_");
                replaceString = replaceString.Replace("\\", "_");
                replaceString = replaceString.Replace("ß", "ss");
                replaceString = replaceString.Replace("-", "_");
                replaceString = replaceString.Replace("°", "_Grad_");
                replaceString = replaceString.Replace(",", "_");
                replaceString = replaceString.Replace(".", "_");
                replaceString = replaceString.Replace("%", "Prozent");

                foreach (char c in replaceString)
                {
                    int unicode = c;
                    // wenn Sonderzeichen (außer Unterstrich _ = 0x5F = 95 dez)
                    if (unicode < 48 || (unicode > 57 && unicode < 65) || (unicode > 90 && unicode < 95) || (unicode > 95 && unicode < 97) || unicode > 122)
                    {
                        replaceString = replaceString.Replace(c.ToString(), "");
                    }
                }

                replaceString = replaceString.ToUpper();

                //Mehrfache Underlines eleminieren
                while (replaceString.Contains("__"))
                {
                    replaceString = replaceString.Replace("__", "_");
                }

                if(replaceString.Substring(replaceString.Length - 1) == "_") //Unterstrich am Ende eleminieren
                {
                    replaceString = replaceString.Substring(0, replaceString.Length - 1);
                }

                if (replaceString.Substring(0,1) == "_") //Unterstrich am Anfang eleminieren
                {
                    replaceString = replaceString.Substring(1, replaceString.Length - 1);
                }
            }
            return replaceString;
        }

        private static string FormatCommentString(string replaceString)
        {
            replaceString = replaceString.Replace("\r\n", " ");
            return replaceString;
        }
    }
}
