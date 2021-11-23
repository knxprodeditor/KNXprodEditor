using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnxProd.Model;

namespace knxprod_ns
{
    public partial class ParameterGui : UserControl
    {
        private static ParameterGui mParameterGui;

        public ParameterGui()
        {
            InitializeComponent();
            mParameterGui = this;
        }

        /*********************************************************************************************************************/
        // Parameter View

        public static void InitializeParameterTreeView()
        {
            mParameterGui.TreeViewParameter.SelectedNode = null;

            // alle Haupt-Tabs unter dem Reiter "Parameter" löschen
            while (mParameterGui.tabControlParameter.TabPages.Count > 0)
            {
                mParameterGui.tabControlParameter.TabPages.Clear();
            }

            // Workaround, da der DataGridView einen bekannten Fehler hat:
            // wenn er nicht angezeigt wird, lassen sich die Zeilen nicht korrekt füllen
            // daher hier einmal kurz anzeigen, füllen und wieder ausblenden

            ApplicationProgramGui.SelectAppParameterTab();
            mParameterGui.InitParameterEepromView();
            ApplicationProgramGui.SelectAppDynamicTab();

            // treeViewParameter füllen
            mParameterGui.FillTreeViewParameter();
        }

        private void FillTreeViewParameter()
        {
            TreeViewParameter.Nodes.Clear();

            foreach (var parameter in ApplicationProgramGui.selectedApplicationProgram.Static.Parameters)
            {
                if (parameter is ApplicationProgramStatic_tParameter)
                {
                    // nur Parameter mit Memory Eintrag einfügen -> neue Überlegung: alle Parameter einfügen
                    //if((parameter as ApplicationProgramStatic_tParameter).Item is MemoryParameter_t || (parameter as ApplicationProgramStatic_tParameter).Item is PropertyParameter_t)
                    //{
                    TreeNode childNode = TreeViewParameter.Nodes.Add((parameter as ApplicationProgramStatic_tParameter).Name);
                    childNode.Tag = parameter;
                    //}


                }
                else if (parameter is ApplicationProgramStatic_tUnion) //Unions bisher nur mit Memory Item gesehen 
                {
                    // einziges Unterscheidungsmerkmal für Unions mit Memory Eintrag: die Adresse im Speicher
                    if ((parameter as ApplicationProgramStatic_tUnion).Item is MemoryUnion_t)
                    {
                        MemoryUnion_t memoryUnion = (parameter as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t;
                        uint parameterAddress = 0;
                        if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment != null)
                        {
                            ApplicationProgramStatic_tCodeAbsoluteSegment absoluteSegment = ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment.ToList().Find(x => x.Id == memoryUnion.CodeSegment);
                            parameterAddress = absoluteSegment.Address + memoryUnion.Offset + memoryUnion.BitOffset;
                        }
                        if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment != null)
                        {
                            ApplicationProgramStatic_tCodeRelativeSegment relativeSegment = ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment.ToList().Find(x => x.Id == memoryUnion.CodeSegment);
                            parameterAddress = relativeSegment.Offset + memoryUnion.Offset + memoryUnion.BitOffset;
                        }
                        TreeNode childNode = TreeViewParameter.Nodes.Add("Union Adresse: 0x" + parameterAddress.ToString("X4"));
                        childNode.Tag = parameter;

                    }
                    // Unterscheidungsmerkmal für Unions mit Property Eintrag: objectIndex? (hab bisher noch nie eine Property gesehen)
                    else if ((parameter as ApplicationProgramStatic_tUnion).Item is PropertyUnion_t)
                    {
                        PropertyUnion_t propParameter = (parameter as ApplicationProgramStatic_tUnion).Item as PropertyUnion_t;
                        TreeNode childNode = TreeViewParameter.Nodes.Add("Property Union: " + propParameter.PropertyId.ToString());
                        childNode.Tag = parameter;
                    }
                }
            }
        }


        TreeNode lastSelectedTreeViewParameterNode;
        private void TreeViewParameter_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Highlight new node
            e.Node.BackColor = SystemColors.Highlight;
            e.Node.ForeColor = SystemColors.HighlightText;
            if (lastSelectedTreeViewParameterNode != null)
            {
                // Set colors to normal for old node
                lastSelectedTreeViewParameterNode.BackColor = SystemColors.Window;
                lastSelectedTreeViewParameterNode.ForeColor = SystemColors.WindowText;
            }
            lastSelectedTreeViewParameterNode = e.Node;

            while (tabControlParameter.TabPages.Count > 0)
            {
                tabControlParameter.TabPages.Clear();
            }

            // die Parameter Details im Union Reiter ausblenden (sonst falsche Informationen sichtbar)
            panelParUnionParameter.Visible = false;

            if (TreeViewParameter.SelectedNode.Tag is ApplicationProgramStatic_tParameter)
            {
                tabControlParameter.TabPages.Add(this.tabPageParParameter);
                ApplicationProgramStatic_tParameter parameter = TreeViewParameter.SelectedNode.Tag as ApplicationProgramStatic_tParameter;
                textParParId.Text = parameter.Id;
                textParParName.Text = parameter.Name;
                HandleKnxDataTypes.ReadKNXType(listBoxParParParameterTypeParams, parameter.ParameterTypeParams);
                textParParText.Text = parameter.Text;
                textParParSuffixText.Text = parameter.SuffixText;
                comboBoxParParAccess.Text = HandleKnxDataTypes.ReadKNXType(parameter.Access);
                textParParValue.Text = parameter.Value;
                textParParInitialValue.Text = parameter.InitialValue;
                checkBoxParParCustomerAdjustable.Checked = parameter.CustomerAdjustable;
                textParParInternalDescription.Text = parameter.InternalDescription;
                checkBoxParParLegacyPatchAlways.Checked = parameter.LegacyPatchAlways;

                //löschen der Tabs im Memory oder Propertry tabcontrol
                while (tabControlParParameterItem.TabCount > 0)
                {
                    tabControlParParameterItem.TabPages.Clear();
                }

                if (parameter.Item is MemoryParameter_t)
                {
                    checkBoxParParWithMemOrProp.Checked = true;
                    tabControlParParameterItem.TabPages.Add(this.tabParParMemory);
                    FillCodeSegementComboBox(comboBoxParParMemCodeSegment, (parameter.Item as MemoryParameter_t).CodeSegment);
                    numericParParMemOffset.Value = (parameter.Item as MemoryParameter_t).Offset;
                    numericParParMemBitOffset.Value = (parameter.Item as MemoryParameter_t).BitOffset;
                    if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment != null)
                    {
                        ApplicationProgramStatic_tCodeAbsoluteSegment absoluteSegment = ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment.ToList().Find(x => x.Id == (parameter.Item as MemoryParameter_t).CodeSegment);
                        if (absoluteSegment != null)
                        {
                            textParParMemBaseAddress.Text = absoluteSegment.Address.ToString() + " (dez) = 0x" + absoluteSegment.Address.ToString("X4");
                            uint memAddr = absoluteSegment.Address + (parameter.Item as MemoryParameter_t).Offset + (parameter.Item as MemoryParameter_t).BitOffset;
                            textParParMemParameterAddress.Text = memAddr.ToString() + " (dez) = 0x" + (memAddr).ToString("X4");
                            MemoryTable.selectEEPROMTableCell((parameter.Item as MemoryParameter_t).CodeSegment, (parameter.Item as MemoryParameter_t).Offset, (parameter.Item as MemoryParameter_t).BitOffset, comboBoxParaCodeSegment, dataGridViewParaUserEeprom); // die Adresse des Parameters in der EEPROM Tabelle auswählen
                        }
                    }
                    if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment != null)
                    {
                        ApplicationProgramStatic_tCodeRelativeSegment relativeSegment = ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment.ToList().Find(x => x.Id == (parameter.Item as MemoryParameter_t).CodeSegment);
                        if (relativeSegment != null)
                        {
                            textParParMemBaseAddress.Text = relativeSegment.Offset.ToString() + " (dez) = 0x" + relativeSegment.Offset.ToString("X4");
                            uint memAddr = relativeSegment.Offset + (parameter.Item as MemoryParameter_t).Offset + (parameter.Item as MemoryParameter_t).BitOffset;
                            textParParMemParameterAddress.Text = memAddr.ToString() + " (dez) = 0x" + (memAddr).ToString("X4");
                            MemoryTable.selectEEPROMTableCell((parameter.Item as MemoryParameter_t).CodeSegment, (parameter.Item as MemoryParameter_t).Offset, (parameter.Item as MemoryParameter_t).BitOffset, comboBoxParaCodeSegment, dataGridViewParaUserEeprom); // die Adresse des Parameters in der EEPROM Tabelle auswählen
                        }
                    }
                }
                else if (parameter.Item is PropertyParameter_t)
                {
                    checkBoxParParWithMemOrProp.Checked = true;
                    tabControlParParameterItem.TabPages.Add(this.tabParParProperty);
                    numericParParPropObjectIndex.Value = (parameter.Item as PropertyParameter_t).ObjectIndex;
                    numericParParPropObjectType.Value = (parameter.Item as PropertyParameter_t).ObjectType;
                    numericParParPropOccurence.Value = (parameter.Item as PropertyParameter_t).Occurrence;
                    numericParParPropPropertyId.Value = (parameter.Item as PropertyParameter_t).PropertyId;
                    numericParParPropOffset.Value = (parameter.Item as PropertyParameter_t).Offset;
                    numericParParPropBitOffset.Value = (parameter.Item as PropertyParameter_t).BitOffset;
                }
                else
                {
                    checkBoxParParWithMemOrProp.Checked = false;
                }
                LanguageProcessing.FillLanguageDataGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvParParTranslations, parameter.Id, "Text");
                ParameterHelper.FillParameterTypeSektion(comboBoxParParParametertypeName, parameter.ParameterType, listBoxParParSelectableParameters, numericParParParametertypeSizeInBit);

            }
            else if (TreeViewParameter.SelectedNode.Tag is ApplicationProgramStatic_tUnion)
            {
                tabControlParameter.TabPages.Add(this.tabPageParUnion);
                ApplicationProgramStatic_tUnion union = TreeViewParameter.SelectedNode.Tag as ApplicationProgramStatic_tUnion;
                numericParUnionSizeInBit.Value = union.SizeInBit;
                textParUnionInternalDescription.Text = union.InternalDescription;

                //löschen der Tabs im Memory oder Propertry tabcontrol
                while (tabControlParUnionParameterItem.TabCount > 0)
                {
                    tabControlParUnionParameterItem.TabPages.Clear();
                }

                if (union.Item is MemoryUnion_t)
                {
                    checkBoxParUnionWithMemOrProp.Checked = true;
                    tabControlParUnionParameterItem.TabPages.Add(this.tabParUnionMemory);
                    FillCodeSegementComboBox(comboBoxParUnionParUnionMemCodeSegment, (union.Item as MemoryUnion_t).CodeSegment);
                    numericParUnionParUnionMemOffset.Value = (union.Item as MemoryUnion_t).Offset;
                    numericParUnionParUnionMemBitOffset.Value = (union.Item as MemoryUnion_t).BitOffset;
                    if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment != null)
                    {
                        ApplicationProgramStatic_tCodeAbsoluteSegment absoluteSegment = ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment.ToList().Find(x => x.Id == (union.Item as MemoryUnion_t).CodeSegment);
                        if (absoluteSegment != null)
                        {
                            textParUnionParUnionMemBaseAddress.Text = absoluteSegment.Address.ToString() + " (dez) = 0x" + absoluteSegment.Address.ToString("X4");
                            uint memAddr = absoluteSegment.Address + (union.Item as MemoryUnion_t).Offset + (union.Item as MemoryUnion_t).BitOffset;
                            textParUnionParUnionMemParAddress.Text = memAddr.ToString() + " (dez) = 0x" + (memAddr).ToString("X4");
                            MemoryTable.selectEEPROMTableCell((union.Item as MemoryUnion_t).CodeSegment, (union.Item as MemoryUnion_t).Offset, (union.Item as MemoryUnion_t).BitOffset, comboBoxParaCodeSegment, dataGridViewParaUserEeprom); // die Adresse des Parameters in der EEPROM Tabelle auswählen
                        }
                    }
                    if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment != null)
                    {
                        ApplicationProgramStatic_tCodeRelativeSegment relativeSegment = ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment.ToList().Find(x => x.Id == (union.Item as MemoryUnion_t).CodeSegment);
                        if (relativeSegment != null)
                        {
                            textParUnionParUnionMemBaseAddress.Text = relativeSegment.Offset.ToString() + " (dez) = 0x" + relativeSegment.Offset.ToString("X4");
                            uint memAddr = relativeSegment.Offset + (union.Item as MemoryUnion_t).Offset + (union.Item as MemoryUnion_t).BitOffset;
                            textParUnionParUnionMemParAddress.Text = memAddr.ToString() + " (dez) = 0x" + (memAddr).ToString("X4");
                            MemoryTable.selectEEPROMTableCell((union.Item as MemoryUnion_t).CodeSegment, (union.Item as MemoryUnion_t).Offset, (union.Item as MemoryUnion_t).BitOffset, comboBoxParaCodeSegment, dataGridViewParaUserEeprom); // die Adresse des Parameters in der EEPROM Tabelle auswählen
                        }
                    }
                }
                else if (union.Item is PropertyUnion_t)
                {
                    checkBoxParUnionWithMemOrProp.Checked = true;
                    tabControlParUnionParameterItem.TabPages.Add(this.tabParUnionProperty);
                    numericParUnionParUnionPropObjectIndex.Value = (union.Item as PropertyUnion_t).ObjectIndex;
                    numericParUnionParUnionPropObjectType.Value = (union.Item as PropertyUnion_t).ObjectType;
                    numericParUnionParUnionPropOccurence.Value = (union.Item as PropertyUnion_t).Occurrence;
                    numericParUnionParUnionPropPropertyId.Value = (union.Item as PropertyUnion_t).PropertyId;
                    numericParUnionParUnionPropOffset.Value = (union.Item as PropertyUnion_t).Offset;
                    numericParUnionParUnionPropBitOffset.Value = (union.Item as PropertyUnion_t).BitOffset;
                }
                else
                {
                    checkBoxParUnionWithMemOrProp.Checked = false;
                }

                while (TreeViewParUnionParameters.Nodes.Count > 0)
                {
                    TreeViewParUnionParameters.Nodes.RemoveAt(0);
                }

                // die in der Union eingehängten Parameter in einen TreeView laden
                foreach (UnionParameter_t unionPara in union.Parameter)
                {
                    TreeNode childNode = TreeViewParUnionParameters.Nodes.Add(unionPara.Name);
                    childNode.Tag = unionPara;
                }

            }
        }

        private void FillCodeSegementComboBox(ComboBox codeSegmentComboBox, string selectCodeSegment)
        {
            codeSegmentComboBox.Items.Clear();
            if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment != null)
            {
                foreach (var absSegement in ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment)
                {
                    ComboBoxItem newAbsSemegemtItem = new ComboBoxItem(absSegement.Id, absSegement);
                    codeSegmentComboBox.Items.Add(newAbsSemegemtItem);
                    if (absSegement.Id == selectCodeSegment)
                    {
                        codeSegmentComboBox.SelectedItem = newAbsSemegemtItem;
                    }
                }
            }
            if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment != null)
            {
                foreach (var relSegement in ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment)
                {
                    ComboBoxItem newRelSemegemtItem = new ComboBoxItem(relSegement.Id, relSegement);
                    codeSegmentComboBox.Items.Add(newRelSemegemtItem);
                    if (relSegement.Id == selectCodeSegment)
                    {
                        codeSegmentComboBox.SelectedItem = newRelSemegemtItem;
                    }
                }
            }
            if (selectCodeSegment == null)
            {
                codeSegmentComboBox.SelectedIndex = -1;
            }
        }


        TreeNode lastSelectedTreeViewParUnionParametersNode;
        private void TreeViewParUnionParameters_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Highlight new node
            e.Node.BackColor = SystemColors.Highlight;
            e.Node.ForeColor = SystemColors.HighlightText;
            if (lastSelectedTreeViewParUnionParametersNode != null)
            {
                // Set colors to normal for old node
                lastSelectedTreeViewParUnionParametersNode.BackColor = SystemColors.Window;
                lastSelectedTreeViewParUnionParametersNode.ForeColor = SystemColors.WindowText;
            }
            lastSelectedTreeViewParUnionParametersNode = e.Node;

            panelParUnionParameter.Visible = true;
            UnionParameter_t unionPara = TreeViewParUnionParameters.SelectedNode.Tag as UnionParameter_t;
            textParUnionParId.Text = unionPara.Id;
            textParUnionParName.Text = unionPara.Name;
            HandleKnxDataTypes.ReadKNXType(listBoxParUnionParParameterTypeParams, unionPara.ParameterTypeParams);
            numericParUnionParOffset.Value = unionPara.Offset;
            numericParUnionParBitOffset.Value = unionPara.BitOffset;
            textParUnionParText.Text = unionPara.Text;
            textParUnionParSuffixText.Text = unionPara.SuffixText;
            comboBoxParUnionParAccess.SelectedItem = HandleKnxDataTypes.ReadKNXType(unionPara.Access);
            textParUnionParValue.Text = unionPara.Value;
            textParUnionParInitialValue.Text = unionPara.InitialValue;
            checkBoxParUnionParCustomerAdjustable.Checked = unionPara.CustomerAdjustable;
            checkBoxParUnionParDefaultUnionParameter.Checked = unionPara.DefaultUnionParameter;
            textParUnionParInternalDescription.Text = unionPara.InternalDescription;
            LanguageProcessing.FillLanguageDataGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvParUnionParTranslations, unionPara.Id, "Text");
            ParameterHelper.FillParameterTypeSektion(comboBoxParUnionParParametertypeName, unionPara.ParameterType, listBoxParUnionParSelectableParameters, numericParUnionParParametertypeSizeInBit);
        }


        public static ref TreeView GetParameterTreeView()
        {
            return ref mParameterGui.TreeViewParameter;
        }

        public static ref TreeView GetParUnionParametersTreeView()
        {
            return ref mParameterGui.TreeViewParUnionParameters;
        }

        public static TreeNode GetSelectedParameterTreeViewNode()
        {
            return mParameterGui.TreeViewParameter.SelectedNode;
        }


        /*********************************************************************************************************************/
        // Parameter Edit

        /// <summary>
        /// Für die Parameter Übersicht das contextMenu entsprechend der Position des Mauszeigers anpassen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeViewParameter_MouseDown(object sender, MouseEventArgs e)
        {
            ContextMenuHandler.contextMenuLastClickedTreeView = TreeViewParameter;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuHandler.ShowContextMenuAtMousePosition();
                TreeViewParameter.SelectedNode = TreeViewParameter.GetNodeAt(e.X, e.Y);

                ContextMenuHandler.SetToolStripMenuAddVisible(true);
                ContextMenuHandler.SettoolStripMenuPasteVisible(false);
                if (TreeViewParameter.SelectedNode == null)
                {
                    ContextMenuHandler.SettoolStripMenuDeleteVisible(false);
                    ContextMenuHandler.SettoolStripMenuCopyVisible(false);
                }
                else
                {
                    ContextMenuHandler.SettoolStripMenuDeleteVisible(true);
                    ContextMenuHandler.SettoolStripMenuCopyVisible(true);
                }
            }
        }

        /// <summary>
        /// bereitet alles vor, um einen neuen Parameter oder eine neue Union anzulegen
        /// </summary>
        public static void AddParameter()
        {
            mParameterGui.ClearParParameterPage();
            mParameterGui.ClearParUnionPage();
            if (!mParameterGui.tabControlParameter.TabPages.Contains(mParameterGui.tabPageParParameter))
            {
                mParameterGui.tabControlParameter.TabPages.Add(mParameterGui.tabPageParParameter);
            }
            if (!mParameterGui.tabControlParameter.TabPages.Contains(mParameterGui.tabPageParUnion))
            {
                mParameterGui.tabControlParameter.TabPages.Add(mParameterGui.tabPageParUnion);
            }


            if (mParameterGui.lastSelectedTreeViewParameterNode != null)
            {
                // Set colors to normal for old node
                mParameterGui.lastSelectedTreeViewParameterNode.BackColor = SystemColors.Window;
                mParameterGui.lastSelectedTreeViewParameterNode.ForeColor = SystemColors.WindowText;
                mParameterGui.lastSelectedTreeViewParameterNode = null;
            }

            //deselect existing ComObject Item
            mParameterGui.TreeViewParameter.SelectedNode = null;
        }

        /// <summary>
        /// Zum Kopieren eines Parameters wird das kopierte Element im TreeNode deselektiert
        /// Dadurch kann der Parameter als neues Object mit andere Nummer gespeichert werden
        /// </summary>
        public static void CopyParameter()
        {
            if (mParameterGui.lastSelectedTreeViewParameterNode != null)
            {
                // Set colors to normal for old node
                mParameterGui.lastSelectedTreeViewParameterNode.BackColor = SystemColors.Window;
                mParameterGui.lastSelectedTreeViewParameterNode.ForeColor = SystemColors.WindowText;
                mParameterGui.lastSelectedTreeViewParameterNode = null;
            }

            // deselect existing Parameter Item
            mParameterGui.TreeViewParameter.SelectedNode = null;

            // delete old Id
            mParameterGui.textParParId.Text = "";
        }

        /// <summary>
        /// löscht einen Parameter oder eine Union aus den Daten und aus dem TreeView
        /// </summary>
        public static void DeleteParameter()
        {
            DialogResult result = MessageBox.Show("Soll der Parameter wirklich gelöscht werden? Die Löschung wirkt sich auf alle Einträge im \"Parameter und KO\" Bereich aus!", "Parameter Löschung Sicherheitsabfrage",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            if (mParameterGui.TreeViewParameter.SelectedNode.Tag is ApplicationProgramStatic_tParameter)
            {
                // Zwischenspeichern der Parameter Referenzen zum löschen (TreeViews werden verändert und zeigen dann nicht mehr auf das richtige Objekt)
                string parameterId = (mParameterGui.TreeViewParameter.SelectedNode.Tag as ApplicationProgramStatic_tParameter).Id;
                int parameterIndex = 0;
                foreach (var parameter in ApplicationProgramGui.selectedApplicationProgram.Static.Parameters)
                {
                    if (parameter is ApplicationProgramStatic_tParameter)
                    {
                        if ((parameter as ApplicationProgramStatic_tParameter).Id == parameterId)
                        {
                            break;
                        }
                    }
                    parameterIndex++;
                }
                ApplicationProgramGui.selectedApplicationProgram.Static.Parameters =
                    TreeViewArrayFunctions.DeleteArrayTreeView(ApplicationProgramGui.selectedApplicationProgram.Static.Parameters, parameterIndex, mParameterGui.TreeViewParameter);

                //Löschen aller Translation Daten
                LanguageProcessing.DeleteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, parameterId);

                // Es sollen auch alle Verweise auf diesen Parameter gelöscht werden
                foreach (ParameterRef_t parameterRef in ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs)
                {
                    if (parameterRef.RefId == parameterId)
                    {
                        DynamicSectionEdit.DeleteIdOfDynamic(parameterRef.Id);
                    }
                }
            }
            else if (mParameterGui.TreeViewParameter.SelectedNode.Tag is ApplicationProgramStatic_tUnion)
            {
                ApplicationProgramStatic_tUnion deleteUnion = mParameterGui.TreeViewParameter.SelectedNode.Tag as ApplicationProgramStatic_tUnion;
                int unionIndex = 0;

                // alle UnionParameter durchgehen und aus der Dynamic Sektion löschen
                foreach (var unionParameter in deleteUnion.Parameter)
                {
                    foreach (ParameterRef_t parameterRef in ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs)
                    {
                        if (parameterRef.RefId == unionParameter.Id)
                        {
                            DynamicSectionEdit.DeleteIdOfDynamic(parameterRef.Id);
                        }
                    }
                    //Löschen aller Translation Daten
                    LanguageProcessing.DeleteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, unionParameter.Id);
                }

                // den Index finden, an dem die Union in der Parameters Sektion steht
                foreach (var Parameter in ApplicationProgramGui.selectedApplicationProgram.Static.Parameters)
                {
                    if (Parameter == deleteUnion)
                    {
                        break;
                    }
                    unionIndex++;
                }
                ApplicationProgramGui.selectedApplicationProgram.Static.Parameters =
                    TreeViewArrayFunctions.DeleteArrayTreeView(ApplicationProgramGui.selectedApplicationProgram.Static.Parameters, unionIndex, mParameterGui.TreeViewParameter);
            }
        }


        /// <summary>
        /// Der speichern Button im Parameter Tab wurde geklickt
        /// </summary>
        private void buttonParParSave_Click(object sender, EventArgs e)
        {
            string newAbsCodeSegmentId = "";
            if (tabControlParParameterItem.SelectedTab == tabParParMemory)
            {
                if (comboBoxParParMemCodeSegment.SelectedItem == null)
                {
                    MessageBox.Show("Es muss ein CodeSegement für den Parameter ausgewählt werden", "CodeSegment für Parameter Auswahl Fehler",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    newAbsCodeSegmentId = ((comboBoxParParMemCodeSegment.SelectedItem as ComboBoxItem).Tag as ApplicationProgramStatic_tCodeAbsoluteSegment).Id;
                    ApplicationProgramStatic_tParameter aktParameter = new ApplicationProgramStatic_tParameter();
                    if (TreeViewParameter.SelectedNode != null)
                    {
                        aktParameter = TreeViewParameter.SelectedNode.Tag as ApplicationProgramStatic_tParameter;
                    }
                    else
                    {
                        aktParameter = null;
                    }

                    if (CheckIfMemeoryUsed(newAbsCodeSegmentId, (uint)numericParParMemOffset.Value, (byte)numericParParMemBitOffset.Value, (uint)numericParParParametertypeSizeInBit.Value, aktParameter))
                    {
                        MessageBox.Show("Der Speicherbereich wird bereits von einem anderen Parameter verwendet", "Speicherbereichsüberschneidung",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            if (comboBoxParParParametertypeName.SelectedItem == null)
            {
                MessageBox.Show("Es muss ein ParameterType ausgewählt werden", "ParameterType Auswahl Fehler",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (((comboBoxParParParametertypeName.SelectedItem as ComboBoxItem).Tag as ParameterType_t).Item is ParameterType_tTypeRestriction)
            {
                if (textParParValue.Text == "")
                {
                    MessageBox.Show("Es muss bei einem gewähltem TypeRestriction ParameterType das Value Feld gefüllt sein.", "TypeRestriction ParameterType Value Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (((comboBoxParParParametertypeName.SelectedItem as ComboBoxItem).Tag as ParameterType_t).Item is ParameterType_tTypeNumber)
            {
                if (textParParValue.Text == "")
                {
                    MessageBox.Show("Es muss bei einem gewähltem TypeNumber ParameterType das Value Feld gefüllt sein.", "TypeNumber ParameterType Value Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (((comboBoxParParParametertypeName.SelectedItem as ComboBoxItem).Tag as ParameterType_t).Item is ParameterType_tTypeFloat)
            {
                if (textParParValue.Text == "")
                {
                    MessageBox.Show("Es muss bei einem gewähltem TypeFloat ParameterType das Value Feld gefüllt sein.", "TypeFloat ParameterType Value Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (textParParValue.Text.Contains(","))
                {
                    MessageBox.Show("Es darf bei einem gewähltem TypeFloat ParameterType kein Komma im Value Feld verwendet werden. \n" +
                        "Bitte als Dezimaltrennzeichen den Punkt verwenden.", "TypeFloat ParameterType Value Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ApplicationProgramStatic_tParameter newParameter = new ApplicationProgramStatic_tParameter();
            TreeNode childNode = new TreeNode();

            if (TreeViewParameter.SelectedNode != null) //bestehender Parameter wurde verändert
            {
                newParameter = TreeViewParameter.SelectedNode.Tag as ApplicationProgramStatic_tParameter;
                TreeViewParameter.SelectedNode.Text = textParParName.Text;
            }
            else
            {
                // den neuen Parameter anhängen
                ApplicationProgramGui.selectedApplicationProgram.Static.Parameters = HandleArrayFunctions.Add(ApplicationProgramGui.selectedApplicationProgram.Static.Parameters, newParameter);

                //einen neuen TreeView Knoten erzeugen
                childNode = TreeViewParameter.Nodes.Add(textParParName.Text);
                childNode.Tag = newParameter;

                // die Parameter ID besteht aus der ApplicationProgram ID, _P- und einer fortlaufenden Nummer
                int newParaNumber = HighestUsedParaNumber() + 1;
                newParameter.Id = ApplicationProgramGui.selectedApplicationProgram.Id + "_P-" + newParaNumber.ToString();
            }

            newParameter.Name = Extensions.NullIfEmpty(textParParName.Text);
            newParameter.ParameterTypeParams = HandleGuiDataTypes.ReadListBox(listBoxParParParameterTypeParams);
            newParameter.Text = Extensions.NullIfEmpty(textParParText.Text);
            newParameter.SuffixText = Extensions.NullIfEmpty(textParParSuffixText.Text);
            newParameter.Access = HandleGuiDataTypes.ReadAccess(comboBoxParParAccess.SelectedItem.ToString());
            newParameter.Value = textParParValue.Text; // Value muss immer gesetzt sein
            newParameter.InitialValue = Extensions.NullIfEmpty(textParParInitialValue.Text);
            newParameter.CustomerAdjustable = checkBoxParParCustomerAdjustable.Checked;
            newParameter.InternalDescription = Extensions.NullIfEmpty(textParParInternalDescription.Text);
            newParameter.LegacyPatchAlways = checkBoxParParLegacyPatchAlways.Checked;
            LanguageProcessing.WriteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvParParTranslations, newParameter.Id, "Text", ApplicationProgramGui.selectedApplicationProgram.Id);

            if (tabControlParParameterItem.SelectedTab == tabParParMemory)
            {
                MemoryParameter_t newMemPara = new MemoryParameter_t();
                newParameter.Item = newMemPara;
                newMemPara.CodeSegment = newAbsCodeSegmentId;
                newMemPara.Offset = (uint)numericParParMemOffset.Value;
                newMemPara.BitOffset = (byte)numericParParMemBitOffset.Value;
            }
            else if (tabControlParParameterItem.SelectedTab == tabParParProperty)
            {
                PropertyParameter_t newPropPara = new PropertyParameter_t();
                newParameter.Item = newPropPara;
                newPropPara.ObjectIndex = (byte)numericParParPropObjectIndex.Value;
                newPropPara.ObjectType = (ushort)numericParParPropObjectType.Value;
                newPropPara.Occurrence = (ushort)numericParParPropOccurence.Value;
                newPropPara.PropertyId = (ushort)numericParParPropPropertyId.Value;
                newPropPara.Offset = (uint)numericParParPropOffset.Value;
                newPropPara.BitOffset = (byte)numericParParPropBitOffset.Value;
            }

            newParameter.ParameterType = ((comboBoxParParParametertypeName.SelectedItem as ComboBoxItem).Tag as ParameterType_t).Id;

            // wenn ein neuer Parameter angelegt wurde, soll diese angewählt werden
            if (TreeViewParameter.SelectedNode == null)
            {
                TreeViewParameter.SelectedNode = childNode;
            }

            if (tabControlParParameterItem.SelectedTab == tabParParMemory)
            {
                // die EEPROM Tabellen neu füllen
                InitParameterEepromView();

                ApplicationProgramGui.SelectAppDynamicTab();
                DynamicMemoryTable.InitEEPROMView();
                ApplicationProgramGui.SelectAppParameterTab();

                // wieder die Adresse des Parameters in der EEPROM Tabelle auswählen
                MemoryTable.selectEEPROMTableCell((newParameter.Item as MemoryParameter_t).CodeSegment, (newParameter.Item as MemoryParameter_t).Offset, (newParameter.Item as MemoryParameter_t).BitOffset, comboBoxParaCodeSegment, dataGridViewParaUserEeprom);
            }
        }

        // checkBox, ob der Parameter ein Memory oder Union Item haben soll
        private void checkBoxParParWithMemOrProp_Click(object sender, EventArgs e)
        {
            if (checkBoxParParWithMemOrProp.Checked)
            {
                if (!tabControlParParameterItem.TabPages.Contains(this.tabParParMemory))
                {
                    tabControlParParameterItem.TabPages.Add(this.tabParParMemory);
                }
                if (!tabControlParParameterItem.TabPages.Contains(this.tabParParProperty))
                {
                    tabControlParParameterItem.TabPages.Add(this.tabParParProperty);
                }
            }
            else
            {
                //löschen der Tabs im Memory oder Propertry tabcontrol
                while (tabControlParParameterItem.TabCount > 0)
                {
                    tabControlParParameterItem.TabPages.Clear();
                }
            }
        }

        /// <summary>
        /// speichern Button im Union Tab
        /// </summary>
        private void buttonParUnionSave_Click(object sender, EventArgs e)
        {
            ApplicationProgramStatic_tCodeAbsoluteSegment newAbsCodeSegment = new ApplicationProgramStatic_tCodeAbsoluteSegment();
            if (tabControlParUnionParameterItem.SelectedTab == tabParUnionMemory)
            {
                if (comboBoxParUnionParUnionMemCodeSegment.SelectedItem == null)
                {
                    MessageBox.Show("Es muss ein CodeSegement für die Union ausgewählt werden", "CodeSegment für Union Auswahl Fehler",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    newAbsCodeSegment = (comboBoxParUnionParUnionMemCodeSegment.SelectedItem as ComboBoxItem).Tag as ApplicationProgramStatic_tCodeAbsoluteSegment;
                    ApplicationProgramStatic_tUnion aktUnion = new ApplicationProgramStatic_tUnion();
                    if (TreeViewParameter.SelectedNode != null)
                    {
                        aktUnion = TreeViewParameter.SelectedNode.Tag as ApplicationProgramStatic_tUnion;
                    }
                    else
                    {
                        aktUnion = null;
                    }

                    if (CheckIfMemeoryUsed(newAbsCodeSegment.Id, (uint)numericParUnionParUnionMemOffset.Value, (byte)numericParUnionParUnionMemBitOffset.Value, (uint)numericParUnionSizeInBit.Value, aktUnion))
                    {
                        MessageBox.Show("Der Speicherbereich wird bereits von einer anderen Union verwendet", "Speicherbereichsüberschneidung",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }
            }

            ApplicationProgramStatic_tUnion newUnion = new ApplicationProgramStatic_tUnion();
            TreeNode childNode = new TreeNode();
            uint unionAddress = newAbsCodeSegment.Address + (uint)numericParUnionParUnionMemOffset.Value + (uint)numericParUnionParUnionMemBitOffset.Value;
            if (TreeViewParameter.SelectedNode != null) //bestehender Parameter wurde verändert
            {
                newUnion = TreeViewParameter.SelectedNode.Tag as ApplicationProgramStatic_tUnion;
                TreeViewParameter.SelectedNode.Text = "Union Adresse: 0x" + unionAddress.ToString("X4");
            }
            else
            {
                // den neuen Parameter anhängen
                ApplicationProgramGui.selectedApplicationProgram.Static.Parameters = HandleArrayFunctions.Add(ApplicationProgramGui.selectedApplicationProgram.Static.Parameters, newUnion);

                //einen neuen TreeView Knoten erzeugen
                childNode = TreeViewParameter.Nodes.Add("Union Adresse: 0x" + unionAddress.ToString("X4"));
                childNode.Tag = newUnion;

                newUnion.Parameter = new UnionParameter_t[0];
            }

            newUnion.SizeInBit = (uint)numericParUnionSizeInBit.Value;
            newUnion.InternalDescription = textParUnionInternalDescription.Text;

            if (tabControlParUnionParameterItem.SelectedTab == tabParUnionMemory)
            {
                MemoryUnion_t newMemUnion = new MemoryUnion_t();
                newUnion.Item = newMemUnion;
                newMemUnion.CodeSegment = newAbsCodeSegment.Id;
                newMemUnion.Offset = (uint)numericParUnionParUnionMemOffset.Value;
                newMemUnion.BitOffset = (byte)numericParUnionParUnionMemBitOffset.Value;
            }
            else if (tabControlParUnionParameterItem.SelectedTab == tabParUnionProperty)
            {
                PropertyUnion_t newPropUnion = new PropertyUnion_t();
                newUnion.Item = newPropUnion;
                newPropUnion.ObjectIndex = (byte)numericParUnionParUnionPropObjectIndex.Value;
                newPropUnion.ObjectType = (ushort)numericParUnionParUnionPropObjectType.Value;
                newPropUnion.Occurrence = (ushort)numericParUnionParUnionPropOccurence.Value;
                newPropUnion.PropertyId = (ushort)numericParUnionParUnionPropPropertyId.Value;
                newPropUnion.Offset = (uint)numericParUnionParUnionPropOffset.Value;
                newPropUnion.BitOffset = (byte)numericParUnionParUnionPropBitOffset.Value;
            }

            // wenn eine neue Union angelegt wurde, soll diese angewählt werden
            if (TreeViewParameter.SelectedNode == null)
            {
                TreeViewParameter.SelectedNode = childNode;
            }

            if (tabControlParUnionParameterItem.SelectedTab == tabParUnionMemory)
            {
                // die EEPROM Tabellen neu füllen
                InitParameterEepromView();
                ApplicationProgramGui.SelectAppDynamicTab();
                DynamicMemoryTable.InitEEPROMView();
                ApplicationProgramGui.SelectAppParameterTab();

                // wieder die Adresse des Parameters in der EEPROM Tabelle auswählen
                MemoryTable.selectEEPROMTableCell((newUnion.Item as MemoryUnion_t).CodeSegment, (newUnion.Item as MemoryUnion_t).Offset, (newUnion.Item as MemoryUnion_t).BitOffset, comboBoxParaCodeSegment, dataGridViewParaUserEeprom);
            }
        }


        // checkBox, ob der Parameter ein Memory oder Union Item haben soll
        private void checkBoxParUnionWithMemOrProp_Click(object sender, EventArgs e)
        {
            if (checkBoxParUnionWithMemOrProp.Checked)
            {
                if (!tabControlParUnionParameterItem.TabPages.Contains(this.tabParUnionMemory))
                {
                    tabControlParUnionParameterItem.TabPages.Add(this.tabParUnionMemory);
                }
                if (!tabControlParUnionParameterItem.TabPages.Contains(this.tabParUnionProperty))
                {
                    tabControlParUnionParameterItem.TabPages.Add(this.tabParUnionProperty);
                }
            }
            else
            {
                //löschen der Tabs im Memory oder Propertry tabcontrol
                while (tabControlParUnionParameterItem.TabCount > 0)
                {
                    tabControlParUnionParameterItem.TabPages.Clear();
                }
            }
        }

        /// <summary>
        /// der Button "Parameter sortieren" im Parameters Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSortParameters_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Es wird die Parameters Table nach Namen und Adressen im Speicher sortiert. Diese Reihenfolge wird auch in den knxprod Daten abgelegt.",
                "Parameter Sortierung Hinweis", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.Cancel)
            {
                return;
            }

            SortParametersTable();
        }

        /// <summary>
        /// Sortieren der Parameters Table anhand des Speicherplatzes oder des Namens
        /// -> Wenn der Parameter einen Speicherplatz im EEPROM hat, soll anhand dessen sortiert werden,
        ///    wenn kein Speicherplatz zugewiesen wurde, sollen die Parameter am Anfang der Liste stehen und alphabetisch sortiert sein
        /// </summary>
        private void SortParametersTable()
        {
            object[] parametersTable = ApplicationProgramGui.selectedApplicationProgram.Static.Parameters;

            // zuerst die Parameter mit und ohne Memory Eintrag in verschiedene Array auslagern, damit die Sortierung unterschiedlich laufen kann
            object[] parametersWithMemoryTable = new object[0];
            object[] parametersWithoutMemoryTable = new object[0];

            foreach (object para in parametersTable)
            {
                if (para is ApplicationProgramStatic_tParameter)
                {
                    if ((para as ApplicationProgramStatic_tParameter).Item is MemoryParameter_t)
                    {
                        parametersWithMemoryTable = HandleArrayFunctions.Add(parametersWithMemoryTable, para);
                    }
                    else
                    {
                        parametersWithoutMemoryTable = HandleArrayFunctions.Add(parametersWithoutMemoryTable, para);
                    }
                }
                else if (para is ApplicationProgramStatic_tUnion)
                {
                    if ((para as ApplicationProgramStatic_tUnion).Item is MemoryUnion_t)
                    {
                        parametersWithMemoryTable = HandleArrayFunctions.Add(parametersWithMemoryTable, para);
                    }
                    else
                    {
                        parametersWithoutMemoryTable = HandleArrayFunctions.Add(parametersWithoutMemoryTable, para);
                    }
                }
            }


            Array.Sort(parametersWithMemoryTable, delegate (object para1, object para2) {

                uint memStartOfPara1 = 0;
                uint memStartOfPara2 = 0;

                if (para1 is ApplicationProgramStatic_tParameter)
                {
                    memStartOfPara1 = CalculateStartAddressOfParameter(((para1 as ApplicationProgramStatic_tParameter).Item as MemoryParameter_t).CodeSegment,
                        ((para1 as ApplicationProgramStatic_tParameter).Item as MemoryParameter_t).Offset,
                        ((para1 as ApplicationProgramStatic_tParameter).Item as MemoryParameter_t).BitOffset);

                }
                else if (para1 is ApplicationProgramStatic_tUnion)
                {
                    memStartOfPara1 = CalculateStartAddressOfParameter(((para1 as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t).CodeSegment,
                        ((para1 as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t).Offset,
                        ((para1 as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t).BitOffset);
                }

                if (para2 is ApplicationProgramStatic_tParameter)
                {
                    memStartOfPara2 = CalculateStartAddressOfParameter(((para2 as ApplicationProgramStatic_tParameter).Item as MemoryParameter_t).CodeSegment,
                        ((para2 as ApplicationProgramStatic_tParameter).Item as MemoryParameter_t).Offset,
                        ((para2 as ApplicationProgramStatic_tParameter).Item as MemoryParameter_t).BitOffset);
                }
                else if (para2 is ApplicationProgramStatic_tUnion)
                {
                    memStartOfPara2 = CalculateStartAddressOfParameter(((para2 as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t).CodeSegment,
                        ((para2 as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t).Offset,
                        ((para2 as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t).BitOffset);
                }

                return memStartOfPara1.CompareTo(memStartOfPara2);
            });


            // anschließend die Parameter ohne Memory Eintrag nach dem Namen sortieren

            Array.Sort(parametersWithoutMemoryTable, delegate (object para1, object para2)
            {
                string namePara1 = "";
                string namePara2 = "";

                if (para1 is ApplicationProgramStatic_tParameter)
                {
                    namePara1 = (para1 as ApplicationProgramStatic_tParameter).Name;
                }

                if (para2 is ApplicationProgramStatic_tParameter)
                {
                    namePara2 = (para2 as ApplicationProgramStatic_tParameter).Name;
                }

                return namePara1.CompareTo(namePara2);
            });

            // nun die beiden Array aneinanderhängen, zuerst die Parameter ohne Memory Eintrag, aschließend die Parameter mit Memory Eintrag
            int newLength = parametersWithoutMemoryTable.Length + parametersWithMemoryTable.Length;

            object[] newParametersTable = new object[newLength];


            for (int i = 0; i < parametersWithoutMemoryTable.Length; i++)
                newParametersTable[i] = parametersWithoutMemoryTable[i];

            for (int i = parametersWithoutMemoryTable.Length; i < parametersWithoutMemoryTable.Length + parametersWithMemoryTable.Length; i++)
                newParametersTable[i] = parametersWithMemoryTable[i - parametersWithoutMemoryTable.Length];

            ApplicationProgramGui.selectedApplicationProgram.Static.Parameters = newParametersTable;

            // zuletzt noch den TreeView des Parameters Tab wieder neu aufbauen
            RefillTreeViewParameter();
        }

        // <summary>
        /// Die Auflistung der Parameter wird neu aus den Daten gefüllt und der bisher markierte Eintrag wird wieder gewählt
        /// </summary>
        private void RefillTreeViewParameter()
        {
            int index = 0;
            object parameterMem = null;
            if (TreeViewParameter.SelectedNode != null)
            {
                //den bisher gewählten Parameter ablegen, um ihn nach der neuen Befüllung des Treeviews wieder zu aktivieren
                parameterMem = TreeViewParameter.SelectedNode.Tag;
            }
            else
            {
                index = TreeViewParameter.Nodes.Count; // neuer Index ist die alte Anzahl von TreeView Elementen
            }

            // neu befüllen des TreeView
            FillTreeViewParameter();

            if (parameterMem != null)
            {
                foreach (TreeNode parameterTreeNode in TreeViewParameter.Nodes)
                {
                    if (parameterTreeNode.Tag == parameterMem)
                    {
                        TreeViewParameter.SelectedNode = parameterTreeNode;
                        break;
                    }
                }
            }
            else
            {
                if (index >= TreeViewParameter.Nodes.Count)
                {
                    index = TreeViewParameter.Nodes.Count - 1;
                }
                TreeViewParameter.SelectedNode = TreeViewParameter.Nodes[index];
            }
        }

        private uint CalculateStartAddressOfParameter(string CodeSegmentId, uint offset, byte bitOffset)
        {
            // CodeSegment Adresse herausfinden
            if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment != null)
            {
                ApplicationProgramStatic_tCodeAbsoluteSegment absCodeSegment =
                ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment.ToList().Find(x => x.Id == CodeSegmentId);
            }
            if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment != null)
            {
                ApplicationProgramStatic_tCodeRelativeSegment relCodeSegment =
                ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment.ToList().Find(x => x.Id == CodeSegmentId);
            }
            return (offset * 8) + bitOffset;
        }

        /*******************************/
        // Ab hier für die Parameter in dem Union Tab

        private void TreeViewParUnionParameters_MouseDown(object sender, MouseEventArgs e)
        {
            ContextMenuHandler.contextMenuLastClickedTreeView = TreeViewParUnionParameters;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuHandler.ShowContextMenuAtMousePosition();
                TreeViewParUnionParameters.SelectedNode = TreeViewParUnionParameters.GetNodeAt(e.X, e.Y);

                ContextMenuHandler.SetToolStripMenuAddVisible(true);
                ContextMenuHandler.SettoolStripMenuPasteVisible(false);
                if (TreeViewParameter.SelectedNode == null)
                {
                    ContextMenuHandler.SettoolStripMenuDeleteVisible(false);
                    ContextMenuHandler.SettoolStripMenuCopyVisible(false);
                }
                else
                {
                    ContextMenuHandler.SettoolStripMenuDeleteVisible(true);
                    ContextMenuHandler.SettoolStripMenuCopyVisible(true);
                }
            }
        }

        public static void AddUnionParameter()
        {
            mParameterGui.ClearParUnionParameterPage();

            if (mParameterGui.lastSelectedTreeViewParUnionParametersNode != null)
            {
                // Set colors to normal for old node
                mParameterGui.lastSelectedTreeViewParUnionParametersNode.BackColor = SystemColors.Window;
                mParameterGui.lastSelectedTreeViewParUnionParametersNode.ForeColor = SystemColors.WindowText;
                mParameterGui.lastSelectedTreeViewParUnionParametersNode = null;
            }

            //deselect existing ComObject Item
            mParameterGui.TreeViewParUnionParameters.SelectedNode = null;
        }

        public static void CopyUnionParameter()
        {
            if (mParameterGui.lastSelectedTreeViewParUnionParametersNode != null)
            {
                // Set colors to normal for old node
                mParameterGui.lastSelectedTreeViewParUnionParametersNode.BackColor = SystemColors.Window;
                mParameterGui.lastSelectedTreeViewParUnionParametersNode.ForeColor = SystemColors.WindowText;
                mParameterGui.lastSelectedTreeViewParUnionParametersNode = null;
            }

            // deselect existing ComObject Item
            mParameterGui.TreeViewParUnionParameters.SelectedNode = null;

            // delete old Id
            mParameterGui.textParUnionParId.Text = "";
        }

        public static void DeleteUnionParameter()
        {
            DialogResult result = MessageBox.Show("Soll der Parameter wirklich gelöscht werden? Die Löschung wirkt sich auf alle Einträge im \"Parameter und KO\" Bereich aus!", "Parameter Löschung Sicherheitsabfrage",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }

            ApplicationProgramStatic_tUnion aktUnion = mParameterGui.TreeViewParameter.SelectedNode.Tag as ApplicationProgramStatic_tUnion;
            UnionParameter_t deleteUnionParameter = mParameterGui.TreeViewParUnionParameters.SelectedNode.Tag as UnionParameter_t;

            // die Referenzen auf den Parameter löschen
            foreach (ParameterRef_t parameterRef in ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs)
            {
                if (parameterRef.RefId == deleteUnionParameter.Id)
                {
                    DynamicSectionEdit.DeleteIdOfDynamic(parameterRef.Id);
                }
            }

            //Löschen aller Translation Daten
            LanguageProcessing.DeleteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, deleteUnionParameter.Id);

            // den Parameter löschen
            int unionParameterIndex = 0;
            foreach (var Parameter in aktUnion.Parameter)
            {
                if (Parameter == deleteUnionParameter)
                {
                    break;
                }
                unionParameterIndex++;
            }
            aktUnion.Parameter = TreeViewArrayFunctions.DeleteArrayTreeView(aktUnion.Parameter, unionParameterIndex, mParameterGui.TreeViewParUnionParameters);
        }

        /// <summary>
        /// speichern Button im ParameterFeld des Union Tabs
        /// </summary>
        private void buttonParUnionParSave_Click(object sender, EventArgs e)
        {
            if (comboBoxParUnionParParametertypeName.SelectedItem == null)
            {
                MessageBox.Show("Es muss ein ParameterType ausgewählt werden", "ParameterType Auswahl Fehler",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TreeNode childNode = new TreeNode();
            UnionParameter_t newUnionParameter = new UnionParameter_t();
            if (TreeViewParUnionParameters.SelectedNode != null) // bestehender Parameter wurde verändert
            {
                newUnionParameter = TreeViewParUnionParameters.SelectedNode.Tag as UnionParameter_t;
                TreeViewParUnionParameters.SelectedNode.Text = textParUnionParName.Text;
            }
            else
            {
                // den neuen Parameter an die Union hängen
                (TreeViewParameter.SelectedNode.Tag as ApplicationProgramStatic_tUnion).Parameter =
                    HandleArrayFunctions.Add((TreeViewParameter.SelectedNode.Tag as ApplicationProgramStatic_tUnion).Parameter, newUnionParameter);

                //einen neuen TreeView Knoten erzeugen
                childNode = TreeViewParUnionParameters.Nodes.Add(textParUnionParName.Text);
                childNode.Tag = newUnionParameter;

                // die Parameter ID besteht aus der ApplicationProgram ID, _P- und einer fortlaufenden Nummer
                int newUnionParaNumber = HighestUnionParaNumber() + 1;
                newUnionParameter.Id = ApplicationProgramGui.selectedApplicationProgram.Id + "_UP-" + newUnionParaNumber.ToString();
            }

            newUnionParameter.Name = Extensions.NullIfEmpty(textParUnionParName.Text);
            newUnionParameter.ParameterTypeParams = HandleGuiDataTypes.ReadListBox(listBoxParUnionParParameterTypeParams);
            newUnionParameter.Offset = (uint)numericParUnionParOffset.Value;
            newUnionParameter.BitOffset = (byte)numericParUnionParBitOffset.Value;
            newUnionParameter.Text = Extensions.NullIfEmpty(textParUnionParText.Text);
            newUnionParameter.SuffixText = Extensions.NullIfEmpty(textParUnionParSuffixText.Text);
            newUnionParameter.Access = HandleGuiDataTypes.ReadAccess(comboBoxParUnionParAccess.SelectedItem.ToString());
            newUnionParameter.Value = Extensions.NullIfEmpty(textParUnionParValue.Text);
            newUnionParameter.InitialValue = Extensions.NullIfEmpty(textParUnionParInitialValue.Text);
            newUnionParameter.CustomerAdjustable = checkBoxParUnionParCustomerAdjustable.Checked;
            newUnionParameter.DefaultUnionParameter = checkBoxParUnionParDefaultUnionParameter.Checked;
            newUnionParameter.InternalDescription = Extensions.NullIfEmpty(textParUnionParInternalDescription.Text);
            LanguageProcessing.WriteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvParUnionParTranslations, newUnionParameter.Id, "Text", ApplicationProgramGui.selectedApplicationProgram.Id);
            newUnionParameter.ParameterType = ((comboBoxParUnionParParametertypeName.SelectedItem as ComboBoxItem).Tag as ParameterType_t).Id;

            if (TreeViewParUnionParameters.SelectedNode == null)
            {
                TreeViewParUnionParameters.SelectedNode = childNode;
            }
        }


        /*************************************/
        // Ab hier Hilfsfunktionen

        /// <summary>
        /// Der ParameterType im Parameter Tab wurde verändert
        /// </summary>
        private void comboBoxParParParametertypeName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ParameterHelper.FillParameterTypeSektion(comboBoxParParParametertypeName, ((comboBoxParParParametertypeName.SelectedItem as ComboBoxItem).Tag as ParameterType_t).Id, listBoxParParSelectableParameters, numericParParParametertypeSizeInBit);
        }

        /// <summary>
        /// Der ParameterType im Union Tab wurde verändert
        /// </summary>
        private void comboBoxParUnionParParametertypeName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ParameterHelper.FillParameterTypeSektion(comboBoxParUnionParParametertypeName, ((comboBoxParUnionParParametertypeName.SelectedItem as ComboBoxItem).Tag as ParameterType_t).Id, listBoxParUnionParSelectableParameters, numericParUnionParParametertypeSizeInBit);
        }

        /// <summary>
        /// Findet die höchste bisher verwendete Nummer für Parameter
        /// </summary>
        /// <returns>höchste bisher verwendete Nummer</returns>
        private int HighestUsedParaNumber()
        {
            int highestId = 0;
            foreach (var parameter in ApplicationProgramGui.selectedApplicationProgram.Static.Parameters)
            {
                if (parameter is ApplicationProgramStatic_tParameter)
                {
                    if ((parameter as ApplicationProgramStatic_tParameter).Id != null)
                    {
                        int parId = int.Parse((parameter as ApplicationProgramStatic_tParameter).Id.Remove(0, ApplicationProgramGui.selectedApplicationProgram.Id.Count() + 3)); // App Id + _P-
                        if (parId > highestId)
                        {
                            highestId = parId;
                        }
                    }
                }
            }
            return highestId;
        }

        /// <summary>
        /// Findet die höchste bisher verwendete Nummer für UnionParameter
        /// </summary>
        /// <returns>höchte bisher verwendete Nummer</returns>
        private int HighestUnionParaNumber()
        {
            int highestId = 0;
            foreach (var parameter in ApplicationProgramGui.selectedApplicationProgram.Static.Parameters)
            {
                if (parameter is ApplicationProgramStatic_tUnion)
                {
                    foreach (UnionParameter_t unionParameter in (parameter as ApplicationProgramStatic_tUnion).Parameter)
                    {
                        if (unionParameter.Id != null)
                        {
                            int parId = int.Parse(unionParameter.Id.Remove(0, ApplicationProgramGui.selectedApplicationProgram.Id.Count() + 4)); // App Id + _UP-
                            if (parId > highestId)
                            {
                                highestId = parId;
                            }
                        }
                    }
                }
            }
            return highestId;
        }

        /// <summary>
        /// Überprüft, ob der angegebene Speicherbereich noch nicht von einem anderen Parameter verwendet wurde
        /// </summary>
        /// <param name="codeSegment">das CodeSegement, welches überprüft werden soll</param>
        /// <param name="offset">der Byte Offset, welcher überprüft werden soll</param>
        /// <param name="bitOffset"> der Bit Offset, der überprüft werden soll</param>
        /// <returns></returns>
        private bool CheckIfMemeoryUsed(string newCodeSegment, uint newOffset, byte newBitOffset, uint newSizeInBit, object aktParameter)
        {
            //ApplicationProgramStatic_tCodeAbsoluteSegment absoluteSegment = ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment.ToList().Find(x => x.Id == codeSegment);
            foreach (var parameter in ApplicationProgramGui.selectedApplicationProgram.Static.Parameters)
            {
                if (parameter == aktParameter) // den aktuellen Parameter nicht mit sich selber abgleichen
                {
                    break;
                }
                string parCodeSegment = "";
                uint parOffset = 0;
                byte parBitOffset = 0;
                uint parSizeInBit = 0;
                if (parameter is ApplicationProgramStatic_tParameter)
                {
                    if ((parameter as ApplicationProgramStatic_tParameter).Item is MemoryParameter_t)
                    {
                        parCodeSegment = ((parameter as ApplicationProgramStatic_tParameter).Item as MemoryParameter_t).CodeSegment;
                        parOffset = ((parameter as ApplicationProgramStatic_tParameter).Item as MemoryParameter_t).Offset;
                        parBitOffset = ((parameter as ApplicationProgramStatic_tParameter).Item as MemoryParameter_t).BitOffset;
                        parSizeInBit = ParameterHelper.GetParameterTypeSizeInBit((parameter as ApplicationProgramStatic_tParameter).ParameterType);
                    }
                }
                else if (parameter is ApplicationProgramStatic_tUnion)
                {
                    if ((parameter as ApplicationProgramStatic_tUnion).Item is MemoryUnion_t)
                    {
                        parCodeSegment = ((parameter as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t).CodeSegment;
                        parOffset = ((parameter as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t).Offset;
                        parBitOffset = ((parameter as ApplicationProgramStatic_tUnion).Item as MemoryUnion_t).BitOffset;
                        foreach (var unionParameter in (parameter as ApplicationProgramStatic_tUnion).Parameter)
                        {
                            uint unionParSizeInBit = ParameterHelper.GetParameterTypeSizeInBit(unionParameter.ParameterType);
                            if (unionParSizeInBit > parSizeInBit) // den größten ParameterType heraussuchen
                            {
                                parSizeInBit = unionParSizeInBit;
                            }
                        }
                    }
                }
                if (newCodeSegment == parCodeSegment) // wenn der gesuchte und der zu überprüfende Parameter im gleichen CodeSegemnt liegen
                {
                    // Alle Werte werden in Bit gerechnet
                    uint newStartaddress = (newOffset * 8) + newBitOffset;
                    uint newEndAddress = newStartaddress + newSizeInBit - 1;
                    uint parStartAddress = (parOffset * 8) + parBitOffset;
                    uint parEndAddress = parStartAddress + parSizeInBit - 1;
                    if (((newStartaddress >= parStartAddress) && (newStartaddress <= parEndAddress)) || ((newEndAddress >= parStartAddress) && (newEndAddress <= parEndAddress)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// löscht den Parameter Tab, als Vorbereitung für einen neuen Parameter
        /// </summary>
        private void ClearParParameterPage()
        {
            textParParId.Text = "";
            textParParName.Text = "";
            HandleKnxDataTypes.ReadKNXType(listBoxParParParameterTypeParams, new string[0]);
            textParParText.Text = "";
            textParParSuffixText.Text = "";
            comboBoxParParAccess.SelectedIndex = 0;
            textParParValue.Text = "";
            textParParInitialValue.Text = "";
            checkBoxParParCustomerAdjustable.Checked = false;
            textParParInternalDescription.Text = "";
            checkBoxParParLegacyPatchAlways.Checked = false;
            LanguageProcessing.PrepareLanguageGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvParParTranslations);

            checkBoxParParWithMemOrProp.Checked = false;
            //löschen der Tabs im Memory oder Propertry tabcontrol
            while (tabControlParParameterItem.TabCount > 0)
            {
                tabControlParParameterItem.TabPages.Clear();
            }

            FillCodeSegementComboBox(comboBoxParParMemCodeSegment, null);
            numericParParMemOffset.Value = 0;
            numericParParMemBitOffset.Value = 0;
            textParParMemBaseAddress.Text = "";
            textParParMemParameterAddress.Text = "";
            numericParParPropObjectIndex.Value = 0;
            numericParParPropObjectType.Value = 0;
            numericParParPropOccurence.Value = 0;
            numericParParPropPropertyId.Value = 0;
            numericParParPropOffset.Value = 0;
            numericParParPropBitOffset.Value = 0;
            ParameterHelper.FillParameterTypeSektion(comboBoxParParParametertypeName, null, listBoxParParSelectableParameters, numericParParParametertypeSizeInBit);
        }

        /// <summary>
        /// löscht den Union Tab, als Vorbereitung für eine neue Union
        /// </summary>
        private void ClearParUnionPage()
        {
            numericParUnionSizeInBit.Value = 0;
            textParUnionInternalDescription.Text = "";

            checkBoxParUnionWithMemOrProp.Checked = false;
            //löschen der Tabs im Memory oder Propertry tabcontrol
            while (tabControlParUnionParameterItem.TabCount > 0)
            {
                tabControlParUnionParameterItem.TabPages.Clear();
            }

            FillCodeSegementComboBox(comboBoxParUnionParUnionMemCodeSegment, null);
            numericParUnionParUnionMemOffset.Value = 0;
            numericParUnionParUnionMemBitOffset.Value = 0;
            textParUnionParUnionMemBaseAddress.Text = "";
            textParUnionParUnionMemParAddress.Text = "";
            numericParUnionParUnionPropObjectIndex.Value = 0;
            numericParUnionParUnionPropObjectType.Value = 0;
            numericParUnionParUnionPropOccurence.Value = 0;
            numericParUnionParUnionPropPropertyId.Value = 0;
            numericParUnionParUnionPropOffset.Value = 0;
            numericParUnionParUnionPropBitOffset.Value = 0;
            panelParUnionParameter.Visible = false;
            TreeViewParUnionParameters.Nodes.Clear();
        }

        /// <summary>
        /// löscht alle Angaben im UnionParatemer Tab als Vorbereitung für einen neuen UnionParameter
        /// </summary>
        private void ClearParUnionParameterPage()
        {
            panelParUnionParameter.Visible = true;
            textParUnionParId.Text = "";
            textParUnionParName.Text = "";
            HandleKnxDataTypes.ReadKNXType(listBoxParUnionParParameterTypeParams, new string[0]);
            numericParUnionParOffset.Value = 0;
            numericParUnionParBitOffset.Value = 0;
            textParUnionParText.Text = "";
            textParUnionParSuffixText.Text = "";
            comboBoxParUnionParAccess.SelectedIndex = 0;
            textParUnionParValue.Text = "";
            textParUnionParInitialValue.Text = "";
            checkBoxParUnionParCustomerAdjustable.Checked = false;
            checkBoxParUnionParDefaultUnionParameter.Checked = false;
            textParUnionParInternalDescription.Text = "";
            LanguageProcessing.PrepareLanguageGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvParUnionParTranslations);
            ParameterHelper.FillParameterTypeSektion(comboBoxParUnionParParametertypeName, null, listBoxParUnionParSelectableParameters, numericParUnionParParametertypeSizeInBit);
        }


        /***********************************************************************************************************************************************************************/
        // MemoryTable in der Parameter GUI

        /// <summary>
        /// Initialisiert die EEPROM Tabelle im Parameter Tab (muss separat gemacht werden, da die DataGridView einen Fehler hat, wenn es nicht angezeigt wird)
        /// </summary>
        private void InitParameterEepromView()
        {
            comboBoxParaCodeSegment.Items.Clear();
            if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment != null)
            {
                foreach (ApplicationProgramStatic_tCodeAbsoluteSegment codeSegement in ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment)
                {
                    uint endAddress = codeSegement.Address + codeSegement.Size;
                    comboBoxParaCodeSegment.Items.Add(new ComboBoxItem("Abs. Adressbereich: 0x" + codeSegement.Address.ToString("X4") + " - 0x" + endAddress.ToString("X4"), codeSegement));
                }
            }
            if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment != null)
            {
                foreach (ApplicationProgramStatic_tCodeRelativeSegment codeSegement in ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment)
                {
                    uint endAddress = codeSegement.Offset + codeSegement.Size;
                    comboBoxParaCodeSegment.Items.Add(new ComboBoxItem("Abs. Adressbereich: 0x" + codeSegement.Offset.ToString("X4") + " - 0x" + endAddress.ToString("X4"), codeSegement));
                }
            }
            if (comboBoxParaCodeSegment.Items.Count > 0)
            {
                comboBoxParaCodeSegment.SelectedIndex = 0;
                MemoryTable.CreateEEPROMViewContent(comboBoxParaCodeSegment, dataGridViewParaUserEeprom);
            }
        }



        // die ComboBox im Parameter Tab
        private void comboBoxParaCodeSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            MemoryTable.CreateEEPROMViewContent(comboBoxParaCodeSegment, dataGridViewParaUserEeprom);
        }

        /*
* die Farbe der DataGridView Zelle anpassen 
*/
        private void dataGridViewParaUserEeprom_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            MemoryTable.DatagridViewUserEepromCellFormatting(e, dataGridViewParaUserEeprom);
        }

        /*
 *  die Formatierung der DataGridView Zelle anpassen
 */
        private void dataGridViewParaUserEeprom_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            MemoryTable.DataGridViewUserEepromCellPainting(e, dataGridViewParaUserEeprom);
        }
    }
}
