using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnxProd.Model;

namespace knxprod_ns
{
    static class MemoryTable
    {
        /// <summary>
        /// Den dataGridView für die Eintragung der Speicherstellen vorbereiten
        /// </summary>
        /// <param name="comboBoxCodeSegment">die ComboBox, in der der anzugeigende Specherbereich steht</param>
        /// <param name="dataGridViewUserEeprom">das DataGridView in das die Daten gefüllt werden sollen</param>
        public static void CreateEEPROMViewContent(ComboBox comboBoxCodeSegment, DataGridView dataGridViewUserEeprom)
        {
            DataTable dt = new DataTable("UserEEPROM");
            // Tabelle und View für erneuten Einsatz vorbereiten
            dt.Clear();
            dt.Columns.Clear();
            dataGridViewUserEeprom.DataSource = null;
            dataGridViewUserEeprom.Rows.Clear();

            if ((comboBoxCodeSegment.SelectedItem as ComboBoxItem).Tag is ApplicationProgramStatic_tCodeAbsoluteSegment)
            {
                ApplicationProgramStatic_tCodeAbsoluteSegment codeSegment = (comboBoxCodeSegment.SelectedItem as ComboBoxItem).Tag as ApplicationProgramStatic_tCodeAbsoluteSegment;

                FillDataInEepromView(dataGridViewUserEeprom, dt, codeSegment.Size, codeSegment.Address, codeSegment.Id);
            }
            else if ((comboBoxCodeSegment.SelectedItem as ComboBoxItem).Tag is ApplicationProgramStatic_tCodeRelativeSegment)
            {
                ApplicationProgramStatic_tCodeRelativeSegment codeSegment = (comboBoxCodeSegment.SelectedItem as ComboBoxItem).Tag as ApplicationProgramStatic_tCodeRelativeSegment;

                FillDataInEepromView(dataGridViewUserEeprom, dt, codeSegment.Size, codeSegment.Offset, codeSegment.Id);
            }
        }

        private static void FillDataInEepromView(DataGridView dataGridViewUserEeprom, DataTable dt, uint size, uint address, string selectedCodeSegmentId)
        {
            if (size > 0)
            {
                // Spaltenbeschreibungen erstellen
                for (int i = 0; i < 8; i++)
                {
                    dt.Columns.Add(string.Format("Bit " + (7 - i).ToString() + "{0}2^" + (7 - i).ToString(), Environment.NewLine));
                    dt.Columns[i].DefaultValue = "";
                }

                // Anzahl der Bytes = Anzahl der Zeilen hinzufügen
                for (int i = 0; i <= size; i++)
                {
                    dt.Rows.Add();
                }

                // Make the DataGridView use the DataTable as its data source.
                dataGridViewUserEeprom.DataSource = dt;

                // Spaltenbreite festlegen
                for (int i = 0; i < 8; i++)
                {
                    dataGridViewUserEeprom.Columns[i].Width = 35;
                }

                // Die Adressen der Bytes in den Zeilenheader schreiben
                for (int i = (int)address; i <= (address + size); i++)
                {
                    dataGridViewUserEeprom.Rows[i - (int)address].HeaderCell.Value = "0x" + i.ToString("X4");
                }

                ReadParameterInEEPROMView(dt, selectedCodeSegmentId);
                ReadComObjectTableInEEPROMView(dt, selectedCodeSegmentId);
                ReadAddressTableInEEPROMView(dt, selectedCodeSegmentId);
                ReadAssociationTableInEEPROMView(dt, selectedCodeSegmentId);
            }
        }

        /// <summary>
        /// die Parameter durchgehen und alle relevanten Parameter zusammentragen
        /// </summary>
        /// <param name="dTable">die DataTable in der die Spciherdaten gesammelt werden sollen</param>
        /// <param name="selectedCodeSegment">der Speicherbereich der durchsucht werden soll</param>
        private static void ReadParameterInEEPROMView(DataTable dTable, string selectedCodeSegmentId)
        {
            uint bitOffset = 0;
            uint offset = 0;
            uint sizeInBit = 0;
            string paraDescription = "";

            // alle Parameter durchgehen und nach Memory Einträgen suchen
            // anschließend wird jeder Parameter in die EEPROM Table eingetragen
            foreach (var parametersItem in ApplicationProgramGui.selectedApplicationProgram.Static.Parameters)
            {
                // Byte size muss für jeden einzutrangenden Parameter größer 0 sein
                sizeInBit = 0;
                paraDescription = "";

                if (parametersItem is ApplicationProgramStatic_tParameter)
                {
                    if ((parametersItem as ApplicationProgramStatic_tParameter).Item is MemoryParameter_t)
                    {
                        if (((parametersItem as ApplicationProgramStatic_tParameter).Item as MemoryParameter_t).CodeSegment == selectedCodeSegmentId) // wenn der Parameter im gewählten Speicherbereich liegt
                        {
                            offset = ((parametersItem as ApplicationProgramStatic_tParameter).Item as MemoryParameter_t).Offset;
                            bitOffset = ((parametersItem as ApplicationProgramStatic_tParameter).Item as MemoryParameter_t).BitOffset;

                            paraDescription = ApplicationProgramGui.languageProcessingApplication.ReadTranslation((parametersItem as ApplicationProgramStatic_tParameter).Id);
                            if (paraDescription == null)
                            {
                                paraDescription = "Parameter hat keine Translation." + System.Environment.NewLine + "Parameter Name: " + (parametersItem as ApplicationProgramStatic_tParameter).Name;
                            }

                            sizeInBit = ParameterHelper.GetParameterTypeSizeInBit((parametersItem as ApplicationProgramStatic_tParameter).ParameterType);

                            /*
                            ParameterType_t paramType = selectedApplicationProgram.Static.ParameterTypes.ToList().Find(x => x.Id == (parametersItem as ApplicationProgramStatic_tParameter).ParameterType);
                            if (paramType != null)
                            {
                                if (paramType.Item is ParameterType_tTypeRestriction)
                                {
                                    sizeInBit = (paramType.Item as ParameterType_tTypeRestriction).SizeInBit;
                                }
                                else if (paramType.Item is ParameterType_tTypeNumber)
                                {
                                    sizeInBit = (paramType.Item as ParameterType_tTypeNumber).SizeInBit;
                                }
                                else if (paramType.Item is ParameterType_tTypeText)
                                {
                                    sizeInBit = (paramType.Item as ParameterType_tTypeText).SizeInBit;
                                }
                                else if (paramType.Item is ParameterType_tTypeTime)
                                {
                                    sizeInBit = (paramType.Item as ParameterType_tTypeTime).SizeInBit;
                                }
                            }*/
                        }
                    }


                    /*
                     * Property Parameter hat kein CodeSegement, daher auch keinen Speicherplatz, dafür aber einen Offset und BitOffset
                     * Parameter wurde bisher nicht in einer knxprod gefuden, daher keine Analyse möglich
                     */

                    /*else if ((parametersItem as ApplicationProgramStatic_tParameter).Item is PropertyParameter_t)
                    {
                        offset = ((parametersItem as ApplicationProgramStatic_tParameter).Item as PropertyParameter_t).Offset;
                        bitOffset = ((parametersItem as ApplicationProgramStatic_tParameter).Item as PropertyParameter_t).BitOffset;

                        paraDescription = languageProcessingApplication.ReadTranslation((parametersItem as ApplicationProgramStatic_tParameter).Id);
                        if (paraDescription == null)
                        {
                            paraDescription = "Parameter hat keine Translation." + System.Environment.NewLine + "Parameter Name: " + (parametersItem as ApplicationProgramStatic_tParameter).Name;
                        }

                        ParameterType_t paramType = selectedApplicationProgram.Static.ParameterTypes.ToList().Find(x => x.Id == (parametersItem as ApplicationProgramStatic_tParameter).ParameterType);
                        if (paramType.Item is ParameterType_tTypeRestriction)
                        {
                            sizeInBit = (paramType.Item as ParameterType_tTypeRestriction).SizeInBit;
                        }
                        else if (paramType.Item is ParameterType_tTypeNumber)
                        {
                            sizeInBit = (paramType.Item as ParameterType_tTypeNumber).SizeInBit;
                        }
                    }*/
                }
                else if (parametersItem is ApplicationProgramStatic_tUnion)
                {
                    if ((parametersItem as ApplicationProgramStatic_tUnion).Item is MemoryUnion_t)
                    {
                        if (((parametersItem as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t).CodeSegment == selectedCodeSegmentId) // wenn der Parameter im gewählten Speicherbereich liegt
                        {
                            offset = ((parametersItem as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t).Offset;
                            bitOffset = ((parametersItem as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t).BitOffset;

                            foreach (UnionParameter_t unionParameter in (parametersItem as ApplicationProgramStatic_tUnion).Parameter)
                            {
                                string tempParaDescription = ApplicationProgramGui.languageProcessingApplication.ReadTranslation(unionParameter.Id);
                                if (tempParaDescription == null)
                                {
                                    tempParaDescription = "Parameter hat keine Translation." + System.Environment.NewLine + "Parameter Name: " + unionParameter.Name;
                                }
                                paraDescription += tempParaDescription + System.Environment.NewLine;
                            }

                            sizeInBit = (parametersItem as ApplicationProgramStatic_tUnion).SizeInBit;
                        }
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
                }
                if (sizeInBit > 0)
                {
                    for (uint i = 0; i < sizeInBit; i++)
                    {
                        uint bitCount = i + bitOffset;
                        if ((bitCount > 0) && ((bitCount % 8) == 0)) //jedesmal wenn das Byte überläuft
                        {
                            offset++; //nächsten Byte anfangen
                        }
                        uint bitPosition = bitCount % 8; //über 8 Bytes wird nur die bitposition herausgerechnet

                        // Rows[Zeile][Spalte]
                        dTable.Rows[(int)offset][(int)bitPosition] = paraDescription;
                    }
                }
            }
        }

        private static void ReadComObjectTableInEEPROMView(DataTable dTable, string selectedCodeSegmentId)
        {
            // wenn die ComObject Table (oder in der KNX Spec 3/5/1 auch "Group Object Table" genannt) im gewählten Speicherbereich liegt
            if (ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.CodeSegment == selectedCodeSegmentId)
            {
                uint dataPointerSize;
                // prüfen, ob die MaskVersion zu einer BCU1 gehört -> dann 1 Byte DataPointer Größe
                if (ApplicationProgramGui.selectedApplicationProgram.MaskVersion.Substring(4, 2) == "01")
                {
                    dataPointerSize = 1;
                }
                else // bei BCU2 und BIM112 ist der DataPointer 2 Byte groß
                {
                    dataPointerSize = 2;
                }

                int comObjectsMemOffset = 0;

                if (ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.OffsetSpecified)
                {
                    comObjectsMemOffset = (int)ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.Offset;
                }

                // Die Tabellengröße ist zu Begin immer in dem ersten Byte
                for (int bitCount = 0; bitCount < 8; bitCount++)
                {
                    dTable.Rows[comObjectsMemOffset][bitCount] = "ComObject Table Size";
                }

                // anschließend wird ein RAM-Flags_Table Pointer geschrieben
                for (int bitCount = 0; bitCount < 8; bitCount++)
                {
                    dTable.Rows[comObjectsMemOffset + 1][bitCount] = "ComObject RAM-Flags-Table Pointer ";
                }

                // danach kommen die ComObjekte mit insgesamt 3 oder 4 Byte pro Objekt
                int MemoryCounter = 2;
                uint highestComObjectNumber = ComObjectsGui.GetHighestComObjectNumber();
                for (uint comObjCounter = 0; comObjCounter <= highestComObjectNumber; comObjCounter++)
                {
                    // Data Pointer Byte(s)
                    if (dataPointerSize == 1)
                    {
                        for (int bitCount = 0; bitCount < 8; bitCount++)
                        {
                            dTable.Rows[comObjectsMemOffset + MemoryCounter][bitCount] = "ComObject " + comObjCounter.ToString() + " Data Pointer";
                        }
                        MemoryCounter++;
                    }
                    else if (dataPointerSize == 2)
                    {
                        for (int byteCounter = 0; byteCounter < dataPointerSize; byteCounter++)
                        {
                            for (int bitCount = 0; bitCount < 8; bitCount++)
                            {
                                dTable.Rows[comObjectsMemOffset + MemoryCounter][bitCount] = "ComObject " + comObjCounter.ToString() + " Data Pointer";
                            }
                            MemoryCounter++;
                        }
                    }

                    // Config Byte
                    for (int bitCount = 0; bitCount < 8; bitCount++)
                    {
                        dTable.Rows[comObjectsMemOffset + MemoryCounter][bitCount] = "ComObject " + comObjCounter.ToString() + " Config";
                    }
                    MemoryCounter++;

                    // Type Byte
                    for (int bitCount = 0; bitCount < 8; bitCount++)
                    {
                        dTable.Rows[comObjectsMemOffset + MemoryCounter][bitCount] = "ComObject " + comObjCounter.ToString() + " Type";
                    }
                    MemoryCounter++;
                }
            }
        }

        private static void ReadAddressTableInEEPROMView(DataTable dTable, string selectedCodeSegmentId)
        {
            // wenn die Address Table (oder in der KNX Spec 3/5/1 auch "Group Address Table" genannt) im gewählten Speicherbereich liegt
            if (ApplicationProgramGui.selectedApplicationProgram.Static.AddressTable.CodeSegment == selectedCodeSegmentId)
            {
                int addressTableOffset = 0;
                if (ApplicationProgramGui.selectedApplicationProgram.Static.AddressTable.OffsetSpecified)
                {
                    addressTableOffset = (int)ApplicationProgramGui.selectedApplicationProgram.Static.AddressTable.Offset;
                }

                // Die Address Table beginnt immer mit der Längenangebe (1 Byte)
                for (int bitCount = 0; bitCount < 8; bitCount++)
                {
                    dTable.Rows[addressTableOffset][bitCount] = "AddressTable Length";
                }

                int MemoryCounter = 1;
                // anschließend wird eine 2 Byte Individual Address geschrieben
                for (int byteCounter = 0; byteCounter < 2; byteCounter++)
                {
                    for (int bitCount = 0; bitCount < 8; bitCount++)
                    {
                        dTable.Rows[addressTableOffset + MemoryCounter][bitCount] = "AddressTable Individual Address";
                    }
                    MemoryCounter++;
                }

                // danach werden die Gruppenadressen mit 2 Byte pro Adresse abgelegt
                int endOfAddressTable = (int)(ApplicationProgramGui.selectedApplicationProgram.Static.AddressTable.MaxEntries * 2) + MemoryCounter;
                for (; MemoryCounter <= endOfAddressTable; MemoryCounter++)
                {
                    for (int bitCount = 0; bitCount < 8; bitCount++)
                    {
                        dTable.Rows[addressTableOffset + MemoryCounter][bitCount] = "AddressTable Group Address";
                    }
                }
            }
        }

        private static void ReadAssociationTableInEEPROMView(DataTable dTable, string selectedCodeSegmentId)
        {
            // wenn die Associaation Table (oder in der KNX Spec 3/5/1 auch "Group Object Association Table" genannt) im gewählten Speicherbereich liegt
            if (ApplicationProgramGui.selectedApplicationProgram.Static.AssociationTable.CodeSegment == selectedCodeSegmentId)
            {
                int associationTableOffset = 0;
                if (ApplicationProgramGui.selectedApplicationProgram.Static.AssociationTable.OffsetSpecified)
                {
                    associationTableOffset = (int)ApplicationProgramGui.selectedApplicationProgram.Static.AssociationTable.Offset;
                }

                // Die Association Table beginnt immer mit der Größenangabe (1 Byte)
                for (int bitCount = 0; bitCount < 8; bitCount++)
                {
                    dTable.Rows[associationTableOffset][bitCount] = "AssociationTable Length";
                }

                int MemoryCounter = 1;

                // danach werden die Verknüpfungen mit 2 Byte pro Tabelleneintrag abgelegt
                int endOfAssociationTable = (int)(ApplicationProgramGui.selectedApplicationProgram.Static.AssociationTable.MaxEntries * 2) + MemoryCounter;
                for (; MemoryCounter <= endOfAssociationTable; MemoryCounter++)
                {
                    for (int bitCount = 0; bitCount < 8; bitCount++)
                    {
                        dTable.Rows[associationTableOffset + MemoryCounter][bitCount] = "AssociationTable Entry";
                    }
                }
            }
        }

        static bool IsTheSameCellValue(int column, int row, DataGridView dataGridViewUserEeprom)
        {
            DataGridViewCell cell1 = dataGridViewUserEeprom[column, row];
            DataGridViewCell cell2 = dataGridViewUserEeprom[column - 1, row];
            if (cell1.Value == null || cell2.Value == null || cell1.Value is "" || cell2.Value is "")
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }





        public static void DataGridViewUserEepromCellPainting(DataGridViewCellPaintingEventArgs e, DataGridView dataGridViewUserEeprom)
        {
            if (e.ColumnIndex >= 0)
            {
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 1)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex, dataGridViewUserEeprom))
            {
                e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Left = dataGridViewUserEeprom.AdvancedCellBorderStyle.Left;
            }
        }




        public static void DatagridViewUserEepromCellFormatting(DataGridViewCellFormattingEventArgs e, DataGridView dataGridViewUserEeprom)
        {
            if (!(e.Value is ""))
            {
                if (e.Value.ToString().Length >= 9 && e.Value.ToString().Substring(0, 9).Contains("ComObject"))
                {
                    e.CellStyle.BackColor = Color.LightBlue;
                }
                else if (e.Value.ToString().Length >= 12 && e.Value.ToString().Substring(0, 12).Contains("AddressTable"))
                {
                    e.CellStyle.BackColor = Color.LightPink;
                }
                else if (e.Value.ToString().Length >= 16 && e.Value.ToString().Substring(0, 16).Contains("AssociationTable"))
                {
                    e.CellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                }
            }
            if (e.ColumnIndex == 0)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex, dataGridViewUserEeprom))
            {
                e.CellStyle.ForeColor = Color.Transparent;
                e.CellStyle.SelectionForeColor = Color.Transparent;

                e.FormattingApplied = true;
            }
        }


        /// <summary>
        /// eine Zelle im DataGridView selektieren
        /// </summary>
        /// <param name="codeSegment">das CodeSegment, welches selektiert werden soll</param>
        /// <param name="offset">der Offset der Speicherzelle</param>
        /// <param name="bitOffset">der BitOffset der Speicherzelle</param>
        /// <param name="comboBoxCodeSegment">die ComboBox, in der die Speicherbereiche aufgelistet sind</param>
        /// <param name="dataGridViewUserEeprom">das DataGridView, in dem der Speicher dargestellt ist</param>
        public static void selectEEPROMTableCell(string codeSegment, uint offset, uint bitOffset, ComboBox comboBoxCodeSegment, DataGridView dataGridViewUserEeprom)
        {
            if ((comboBoxCodeSegment.SelectedItem as ComboBoxItem).Tag is ApplicationProgramStatic_tCodeAbsoluteSegment)
            {
                if (((comboBoxCodeSegment.SelectedItem as ComboBoxItem).Tag as ApplicationProgramStatic_tCodeAbsoluteSegment).Id != codeSegment)
                {
                    foreach (ComboBoxItem comboBoxCodeSegmentItem in comboBoxCodeSegment.Items)
                    {
                        if ((comboBoxCodeSegmentItem.Tag as ApplicationProgramStatic_tCodeAbsoluteSegment).Id == codeSegment)
                        {
                            comboBoxCodeSegment.SelectedItem = comboBoxCodeSegmentItem;
                            break;
                        }
                    }
                }
            }
            else if ((comboBoxCodeSegment.SelectedItem as ComboBoxItem).Tag is ApplicationProgramStatic_tCodeRelativeSegment)
            {
                if (((comboBoxCodeSegment.SelectedItem as ComboBoxItem).Tag as ApplicationProgramStatic_tCodeRelativeSegment).Id != codeSegment)
                {
                    foreach (ComboBoxItem comboBoxCodeSegmentItem in comboBoxCodeSegment.Items)
                    {
                        if ((comboBoxCodeSegmentItem.Tag as ApplicationProgramStatic_tCodeRelativeSegment).Id == codeSegment)
                        {
                            comboBoxCodeSegment.SelectedItem = comboBoxCodeSegmentItem;
                            break;
                        }
                    }
                }
            }

            if (offset < dataGridViewUserEeprom.Rows.Count)
            {
                dataGridViewUserEeprom.CurrentCell = dataGridViewUserEeprom.Rows[(int)offset].Cells[(int)bitOffset];
            }

        }
    }
}
