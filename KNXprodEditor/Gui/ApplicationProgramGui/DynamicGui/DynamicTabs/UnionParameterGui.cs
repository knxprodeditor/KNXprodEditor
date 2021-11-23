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
    public partial class UnionParameterGui : UserControl
    {
        public static UnionParameterGui mUnionParameterGui;

        public UnionParameterGui()
        {
            InitializeComponent();
            mUnionParameterGui = this;
        }

        /**************************************************************************************************************************/
        // UnionParameter View

        public static void FillUnionParameterPage(UnionParameter_t union) //Union und UnionParameter werden hier verarbeitet
        {
            DynamicTabControl.AddUnionParameterTab();

            // suchen der Union, in der der UnionParameter angehängt ist
            ApplicationProgramStatic_tUnion aktUnion = new ApplicationProgramStatic_tUnion();
            bool unionFound = false;
            foreach (var parameter in ApplicationProgramGui.selectedApplicationProgram.Static.Parameters)
            {
                if (parameter is ApplicationProgramStatic_tUnion)
                {
                    aktUnion = parameter as ApplicationProgramStatic_tUnion;
                    foreach (UnionParameter_t unionPara in (parameter as ApplicationProgramStatic_tUnion).Parameter)
                    {
                        if (unionPara == union)
                        {
                            unionFound = true;
                            break;
                        }
                    }
                }
                if (unionFound)
                    break;
            }

            // die Daten der Union mit in die UnionParameterPage einfügen
            mUnionParameterGui.FillUnionPage(aktUnion);

            // die Auswahl aller Parameter und Union Parameter in die ComboBox laden
            ParameterHelper.FillParameterCollectionComboBox(mUnionParameterGui.comboBoxUUnionParameterCollection, union);

            mUnionParameterGui.textUParId.Text = union.Id;
            mUnionParameterGui.textUParName.Text = union.Name;
            mUnionParameterGui.textUParParameterType.Text = union.ParameterType;
            HandleKnxDataTypes.ReadKNXType(mUnionParameterGui.listUParParameterTypeParams, union.ParameterTypeParams);
            mUnionParameterGui.textUParText.Text = union.Text;
            mUnionParameterGui.textUParSuffixText.Text = union.SuffixText;
            mUnionParameterGui.textUParAccess.Text = HandleKnxDataTypes.ReadKNXType(union.Access);
            mUnionParameterGui.textUParValue.Text = union.Value;
            mUnionParameterGui.textUParInitialValue.Text = union.InitialValue;
            mUnionParameterGui.textUParCustomerAdjustable.Text = HandleKnxDataTypes.ReadKNXType(union.CustomerAdjustable);
            mUnionParameterGui.textUParInternalDescription.Text = union.InternalDescription;
            mUnionParameterGui.textUParDefaultUnionParameter.Text = HandleKnxDataTypes.ReadKNXType(union.DefaultUnionParameter);
            mUnionParameterGui.textUParOffset.Text = union.Offset.ToString();
            mUnionParameterGui.textUParBitOffset.Text = union.BitOffset.ToString();

            //Füllen der ParameterType und SelectableParameters Felder
            ParameterHelper.FillParameterTypeSektion(mUnionParameterGui.comboBoxUnionParameterTypeName, union.ParameterType, mUnionParameterGui.listUnionParSelectableParameters, mUnionParameterGui.numericUnionParameterTypeSizeInBit);

            mUnionParameterGui.dgvAppProgUParTranslations.Rows.Clear();
            LanguageProcessing.FillLanguageDataGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, mUnionParameterGui.dgvAppProgUParTranslations, union.Id, "Text");
        }



        /// <summary>
        /// Wird aufgerufen, wenn der ParameterType für einen UnionParameter verändert wurde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxUnionParameterTypeName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectedParaTypeId = (((sender as ComboBox).SelectedItem as ComboBoxItem).Tag as ParameterType_t).Id;

            ParameterHelper.FillParameterTypeSektion(comboBoxUnionParameterTypeName, selectedParaTypeId, listUnionParSelectableParameters, numericUnionParameterTypeSizeInBit);
        }

        // im UnionParameter Tab wurde der Eintrag in der ComboBox verändert
        private void comboBoxUUnionParameterCollection_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DynamicTabControl.RemoveAllParamsCoTabs();
            DynamicTreeViewGui.FillParameterOrUnionPage((comboBoxUUnionParameterCollection.SelectedItem as ComboBoxItem).Tag);
        }


        private void FillUnionPage(ApplicationProgramStatic_tUnion unionItem) //füllt die Parameter der Union in die UnionParameter Seite
        {
            textUSizeInBit.Text = unionItem.SizeInBit.ToString();
            textUInternalDescription.Text = unionItem.InternalDescription;

            //löschen der Tabs im Memory oder Propertry tabcontrol
            while (tabControlUnionItem.TabCount > 0)
            {
                tabControlUnionItem.TabPages.Clear();
                textUnionParBaseAddress.Text = "";
            }

            if (unionItem.Item is MemoryUnion_t)
            {
                tabControlUnionItem.TabPages.Add(this.tabUnionMemory);
                textUMCodeSegment.Text = (unionItem.Item as MemoryUnion_t).CodeSegment;
                textUMOffset.Text = (unionItem.Item as MemoryUnion_t).Offset.ToString() + " (dez) = 0x" + (unionItem.Item as MemoryUnion_t).Offset.ToString("X4");
                textUMBitOffset.Text = (unionItem.Item as MemoryUnion_t).BitOffset.ToString();
                uint memAddr = 0;
                if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment != null)
                {
                    ApplicationProgramStatic_tCodeAbsoluteSegment absoluteSegment = ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment.ToList().Find(x => x.Id == (unionItem.Item as MemoryUnion_t).CodeSegment);
                    if (absoluteSegment != null)
                    {
                        textUnionParBaseAddress.Text = absoluteSegment.Address.ToString() + " (dez) = 0x" + absoluteSegment.Address.ToString("X4");
                        memAddr = absoluteSegment.Address + (unionItem.Item as MemoryUnion_t).Offset + (unionItem.Item as MemoryUnion_t).BitOffset;
                    }
                }
                if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment != null)
                {
                    ApplicationProgramStatic_tCodeRelativeSegment relativeSegment = ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment.ToList().Find(x => x.Id == (unionItem.Item as MemoryUnion_t).CodeSegment);
                    if (relativeSegment != null)
                    {
                        textUnionParBaseAddress.Text = relativeSegment.Offset.ToString() + " (dez) = 0x" + relativeSegment.Offset.ToString("X4");
                        memAddr = relativeSegment.Offset + (unionItem.Item as MemoryUnion_t).Offset + (unionItem.Item as MemoryUnion_t).BitOffset;
                    }
                }
                textUnionParMemAddress.Text = memAddr.ToString() + " (dez) = 0x" + memAddr.ToString("X4");
                MemoryTable.selectEEPROMTableCell((unionItem.Item as MemoryUnion_t).CodeSegment, (unionItem.Item as MemoryUnion_t).Offset, (unionItem.Item as MemoryUnion_t).BitOffset, DynamicMemoryTable.mDynamicMemoryTable.comboBoxParCoCodeSegment, DynamicMemoryTable.mDynamicMemoryTable.dataGridViewParCoUserEeprom);
            }
            else if (unionItem.Item is PropertyUnion_t)
            {
                tabControlUnionItem.TabPages.Add(this.tabUnionProperty);
                textUPropObjectIndex.Text = (unionItem.Item as PropertyUnion_t).ObjectIndex.ToString();
                textUPropObjectType.Text = (unionItem.Item as PropertyUnion_t).ObjectType.ToString();
                textUPropOccurence.Text = (unionItem.Item as PropertyUnion_t).Occurrence.ToString();
                textUPropPropertyId.Text = (unionItem.Item as PropertyUnion_t).PropertyId.ToString();
                textUPropOffset.Text = (unionItem.Item as PropertyUnion_t).Offset.ToString() + " (dez) = 0x" + (unionItem.Item as PropertyUnion_t).Offset.ToString("X4");
                textUPropBitOffset.Text = (unionItem.Item as PropertyUnion_t).BitOffset.ToString();
                //selectEEPROMTableCell((unionItem.Item as PropertyUnion_t).Offset, (unionItem.Item as PropertyUnion_t).BitOffset); // Property hat kein CodeSegemnt, somit auch keine Speicherstelle
            }
        }

        /**************************************************************************************************************************/
        // UnionParameter Edit
        private void buttonUnionParSave_Click(object sender, EventArgs e)
        {
            AppParameterGui.HandleParSaveButton(comboBoxUUnionParameterCollection);
        }
    }
}
