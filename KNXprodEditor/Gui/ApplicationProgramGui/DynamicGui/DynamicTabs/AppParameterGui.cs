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
    public partial class AppParameterGui : UserControl
    {
        public static AppParameterGui mAppParameterGui;

        public AppParameterGui()
        {
            InitializeComponent();
            mAppParameterGui = this;
        }

        /*************************************************************************************************************/
        // App Parameter View



        public static void FillParameterPage(ApplicationProgramStatic_tParameter par)
        {
            DynamicTabControl.AddParameterTab();

            // die Auswahl aller Parameter und Union Parameter in die ComboBox laden
            ParameterHelper.FillParameterCollectionComboBox(mAppParameterGui.comboBoxPParameterCollection, par);

            mAppParameterGui.textPId.Text = par.Id;
            mAppParameterGui.textPName.Text = par.Name;
            mAppParameterGui.textPParameterType.Text = par.ParameterType;
            HandleKnxDataTypes.ReadKNXType(mAppParameterGui.listPParameterTypeParams, par.ParameterTypeParams);
            mAppParameterGui.textPText.Text = par.Text;
            mAppParameterGui.textPSuffixText.Text = par.SuffixText;
            mAppParameterGui.textPAccess.Text = HandleKnxDataTypes.ReadKNXType(par.Access);
            mAppParameterGui.textPValue.Text = par.Value;
            mAppParameterGui.textPInitialValue.Text = par.InitialValue;
            mAppParameterGui.textPCustomerAdjustable.Text = HandleKnxDataTypes.ReadKNXType(par.CustomerAdjustable);
            mAppParameterGui.textPInternalDescription.Text = par.InternalDescription;
            mAppParameterGui.textPLegacyPatchAlways.Text = HandleKnxDataTypes.ReadKNXType(par.LegacyPatchAlways);

            //Füllen der ParameterType und SelectableParameters Felder
            ParameterHelper.FillParameterTypeSektion(mAppParameterGui.comboBoxPParameterTypeName, par.ParameterType, mAppParameterGui.listPSelectableParameters, mAppParameterGui.numericPParameterTypeSizeInBit);

            // Übersetzungen aus DataGridView löschen und neue Daten einfügen
            mAppParameterGui.dgvAppProgParTranslations.Rows.Clear();
            LanguageProcessing.FillLanguageDataGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, mAppParameterGui.dgvAppProgParTranslations, par.Id, "Text");

            //löschen der Tabs im Memory oder Propertry tabcontrol
            while (mAppParameterGui.tabControlParItem.TabCount > 0)
            {
                mAppParameterGui.tabControlParItem.TabPages.Clear();
                mAppParameterGui.textParBaseAddress.Text = "";
            }

            if (par.Item is MemoryParameter_t)
            {
                mAppParameterGui.tabControlParItem.TabPages.Add(mAppParameterGui.tabParMemory);
                mAppParameterGui.textPMCodeSegment.Text = (par.Item as MemoryParameter_t).CodeSegment;
                mAppParameterGui.textPMOffset.Text = (par.Item as MemoryParameter_t).Offset.ToString() + " (dez) = 0x" + (par.Item as MemoryParameter_t).Offset.ToString("X4");
                mAppParameterGui.textPMBitOffset.Text = (par.Item as MemoryParameter_t).BitOffset.ToString();
                uint memAddr = 0;
                if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment != null)
                {
                    ApplicationProgramStatic_tCodeAbsoluteSegment absoluteSegment = ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment.ToList().Find(x => x.Id == (par.Item as MemoryParameter_t).CodeSegment);
                    if (absoluteSegment != null)
                    {
                        mAppParameterGui.textParBaseAddress.Text = absoluteSegment.Address.ToString() + " (dez) = 0x" + absoluteSegment.Address.ToString("X4");
                        memAddr = absoluteSegment.Address + (par.Item as MemoryParameter_t).Offset + (par.Item as MemoryParameter_t).BitOffset;
                    }
                }
                if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment != null)
                {
                    ApplicationProgramStatic_tCodeRelativeSegment relativeSegment = ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment.ToList().Find(x => x.Id == (par.Item as MemoryParameter_t).CodeSegment);
                    if (relativeSegment != null)
                    {
                        mAppParameterGui.textParBaseAddress.Text = relativeSegment.Offset.ToString() + " (dez) = 0x" + relativeSegment.Offset.ToString("X4");
                        memAddr = relativeSegment.Offset + (par.Item as MemoryParameter_t).Offset + (par.Item as MemoryParameter_t).BitOffset;
                    }
                }
                mAppParameterGui.textParMemAddress.Text = memAddr.ToString() + " (dez) = 0x" + (memAddr).ToString("X4");
                MemoryTable.selectEEPROMTableCell((par.Item as MemoryParameter_t).CodeSegment, (par.Item as MemoryParameter_t).Offset, (par.Item as MemoryParameter_t).BitOffset, DynamicMemoryTable.mDynamicMemoryTable.comboBoxParCoCodeSegment, DynamicMemoryTable.mDynamicMemoryTable.dataGridViewParCoUserEeprom); // die Adresse des Parameters in der EEPROM Tabelle auswählen
            }
            else if (par.Item is PropertyParameter_t)
            {
                mAppParameterGui.tabControlParItem.TabPages.Add(mAppParameterGui.tabParProperty);
                mAppParameterGui.textPPObjectIndex.Text = (par.Item as PropertyParameter_t).ObjectIndex.ToString();
                mAppParameterGui.textPPObjectType.Text = (par.Item as PropertyParameter_t).ObjectType.ToString();
                mAppParameterGui.textPPOccurence.Text = (par.Item as PropertyParameter_t).Occurrence.ToString();
                mAppParameterGui.textPPPropertyId.Text = (par.Item as PropertyParameter_t).PropertyId.ToString();
                mAppParameterGui.textPPOffset.Text = (par.Item as PropertyParameter_t).Offset.ToString() + " (dez) = 0x" + (par.Item as PropertyParameter_t).Offset.ToString("X4");
                mAppParameterGui.textPPBitOffset.Text = (par.Item as PropertyParameter_t).BitOffset.ToString();
                //selectEEPROMTableCell((par.Item as PropertyParameter_t).Offset, (par.Item as PropertyParameter_t).BitOffset); // Property hat kein CodeSegemnt, somit auch keine Speicherstelle
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der ParameterType für einen Parameter verändert wurde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxPParameterTypeName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectedParaTypeId = (((sender as ComboBox).SelectedItem as ComboBoxItem).Tag as ParameterType_t).Id;

            ParameterHelper.FillParameterTypeSektion(comboBoxPParameterTypeName, selectedParaTypeId, listPSelectableParameters, numericPParameterTypeSizeInBit);
        }

        /*************************************************************************************************************/
        // App Parameter Edit

        // im Parameter Tab wurde der Eintrag in der ComboBox verändert
        private void comboBoxPParameterCollection_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DynamicTabControl.RemoveAllParamsCoTabs();
            DynamicTreeViewGui.FillParameterOrUnionPage((comboBoxPParameterCollection.SelectedItem as ComboBoxItem).Tag);
        }

        // speichern Button im Parameter Tab in der Parameter und KO Übersicht
        private void buttonParCoParameterSave_Click(object sender, EventArgs e)
        {
            HandleParSaveButton(comboBoxPParameterCollection);
        }



        public static void HandleParSaveButton(ComboBox comboBoxParaCollection)
        {
            if (comboBoxParaCollection.SelectedIndex == -1)
            {
                MessageBox.Show("Bitte einen Parameter auswählen", "Parameter Auswahl Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // prüfen, ob Parameter verändert wurde, falls nicht -> Ende
            if ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag is ApplicationProgramStatic_tParameter)
            {
                if (ParameterRefGui.GetParameterRefRefId() == ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag as ApplicationProgramStatic_tParameter).Id)
                {
                    return;
                }
            }
            else if ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag is UnionParameter_t)
            {
                if (ParameterRefGui.GetParameterRefRefId() == ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag as UnionParameter_t).Id)
                {
                    return;
                }
            }

            if (DynamicTreeViewGui.newOrEditedParameterItem == DynamicTreeViewGui.newOrEditedParameterItems.ParaOrUnionPara)
            {
                mAppParameterGui.ParaOrUnionParaSave(comboBoxParaCollection);
            }
            else if (DynamicTreeViewGui.newOrEditedParameterItem == DynamicTreeViewGui.newOrEditedParameterItems.ComObjectParaChooseParameter)
            {
                ComObjectParameterChooseGui.ComObjectParaChooseParameterSave(comboBoxParaCollection);
            }
            else if (DynamicTreeViewGui.newOrEditedParameterItem == DynamicTreeViewGui.newOrEditedParameterItems.ChannelChooseParameter)
            {
                ChannelChooseGui.ChannelChooseParameterSave(comboBoxParaCollection);
            }
            else if (DynamicTreeViewGui.newOrEditedParameterItem == DynamicTreeViewGui.newOrEditedParameterItems.ParaBlockParameter)
            {
                ComObjectParameterBlockGui.ComObjectParameterBlockSave(comboBoxParaCollection);
            }
        }

        private void ParaOrUnionParaSave(ComboBox comboBoxParaCollection)
        {
            int nextParameterRefNumber = HighestUsedParaRefNumber() + 1;

            ParameterRef_t newParaRef = new ParameterRef_t();
            ParameterRefRef_t newParaRefRef = new ParameterRefRef_t();

            if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ParameterRefRef_t) // bestehender Parameter verändert
            {
                newParaRefRef = (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ParameterRefRef_t);
                newParaRef = ObjectFunctions.DeepClone(ParameterRefGui.GetParameterRefIdItem().Tag as ParameterRef_t);

                // Überprüfen, ob alter ParameterRef noch verwendet wird, sonst löschen (neuer ParameterRef wird gleich erzeugt)
                if (DynamicSectionSearch.SearchIdInDynamic((DynamicTreeViewGui.GetSelectedTreeNode().Tag as ParameterRefRef_t).RefId) <= 1)
                {
                    int index = 0;
                    foreach (ParameterRef_t paraRef in ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs)
                    {
                        if (paraRef.Id == (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ParameterRefRef_t).RefId)
                        {
                            ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs = HandleArrayFunctions.Delete(ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs, index);
                            index--;
                        }
                        index++;
                    }
                }
            }

            else //neuen Parameter erzeugt
            {
                // je nachdem an welchem Element der neue ParameterRefRef angehängt werden soll muss korrekt gehandelt werden
                DynamicSectionEdit.AddToItemsObject(DynamicTreeViewGui.GetSelectedTreeNode().Tag, newParaRefRef);
            }

            // das ParameterRef Object anhängen
            ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs = HandleArrayFunctions.Add(ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs, newParaRef);

            if ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag is ApplicationProgramStatic_tParameter)
            {
                newParaRef.Id = ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag as ApplicationProgramStatic_tParameter).Id + "_R-" + nextParameterRefNumber.ToString();
                newParaRef.RefId = ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag as ApplicationProgramStatic_tParameter).Id;
            }
            else if ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag is UnionParameter_t)
            {
                newParaRef.Id = ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag as UnionParameter_t).Id + "_R-" + nextParameterRefNumber.ToString();
                newParaRef.RefId = ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag as UnionParameter_t).Id;
            }
            newParaRefRef.RefId = newParaRef.Id;
            newParaRef.Tag = nextParameterRefNumber.ToString();



            if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ParameterRefRef_t) // bestehender Parameter verändert
            {
                DynamicTreeViewGui.SetSelectedNodeText(DynamicTreeViewGenerator.ResolveParameterRefRef(newParaRefRef, new TreeNode()).Text);
                DynamicTabControl.RemoveAllParamsCoTabs();
                DynamicTreeViewGui.FillParameterOrUnionPage(ParameterRefGui.FillParameterRefPage(ParameterRefRefGui.FillParameterRefRefPage(newParaRefRef)));
            }
            else
            {
                TreeNode childNode = DynamicTreeViewGenerator.ResolveParameterRefRef(newParaRefRef, DynamicTreeViewGui.GetSelectedTreeNode());
                DynamicTreeViewGui.SetSelectedTreeNode(childNode);
            }
        }

        public static void DeleteParameterAndReferences()
        {
            int index = 0;
            // löschen des ParameterRef aus ParameterRefs Tabelle, wenn der ParameterRef nicht mehr benötigt wird
            if (DynamicSectionSearch.SearchIdInDynamic((DynamicTreeViewGui.GetSelectedTreeNode().Tag as ParameterRefRef_t).RefId) <= 1)
            {
                foreach (ParameterRef_t paraRef in ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs)
                {
                    if (paraRef.Id == (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ParameterRefRef_t).RefId)
                    {
                        ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs = HandleArrayFunctions.Delete(ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs, index);
                        index--;
                    }
                    index++;
                }
            }

            // löschen der ParameterRefRef aus TreeView und Dynamic Section
            DynamicSectionEdit.DeleteItemsFromObjectAndTreeView(DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag, DynamicTreeViewGui.GetSelectedTreeNode().Tag, DynamicTreeViewGui.GetTreeView());
        }


        /// <summary>
        /// Findet die höchste bisher verwendete Nummer für ParameterRef
        /// </summary>
        /// <returns>höchste bisher verwendete Nummer</returns>
        public static int HighestUsedParaRefNumber()
        {
            int highestId = 0;
            foreach (ParameterRef_t parameter in ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs)
            {
                if (parameter.Id != null)
                {
                    int indexOfNumber = parameter.Id.IndexOf("_R-");
                    int parId = int.Parse(parameter.Id.Remove(0, indexOfNumber + 3)); // App Id + _P-
                    if (parId > highestId)
                    {
                        highestId = parId;
                    }
                }
            }
            return highestId;
        }

        /// <summary>
        /// bereitet den Parameter Tab für die Auswahl eines neuen Parameters vor
        /// </summary>
        public static void ClearParameterPage()
        {

            // die Auswahl aller Parameter und Union Parameter in die ComboBox laden
            ParameterHelper.FillParameterCollectionComboBox(mAppParameterGui.comboBoxPParameterCollection, null);

            mAppParameterGui.textPId.Text = "";
            mAppParameterGui.textPName.Text = "";
            mAppParameterGui.textPParameterType.Text = "";
            HandleKnxDataTypes.ReadKNXType(mAppParameterGui.listPParameterTypeParams, new string[0]);
            mAppParameterGui.textPText.Text = "";
            mAppParameterGui.textPSuffixText.Text = "";
            mAppParameterGui.textPAccess.Text = "";
            mAppParameterGui.textPValue.Text = "";
            mAppParameterGui.textPInitialValue.Text = "";
            mAppParameterGui.textPCustomerAdjustable.Text = "";
            mAppParameterGui.textPInternalDescription.Text = "";
            mAppParameterGui.textPLegacyPatchAlways.Text = "";
            LanguageProcessing.PrepareLanguageGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, mAppParameterGui.dgvAppProgParTranslations);
            while (mAppParameterGui.tabControlParItem.TabCount > 0)
            {
                mAppParameterGui.tabControlParItem.TabPages.Clear();
            }
            mAppParameterGui.comboBoxPParameterTypeName.SelectedIndex = -1;
            HandleKnxDataTypes.ReadKNXType(mAppParameterGui.listPSelectableParameters, new string[0]);
            mAppParameterGui.numericPParameterTypeSizeInBit.Value = 0;
        }       
    }
}
