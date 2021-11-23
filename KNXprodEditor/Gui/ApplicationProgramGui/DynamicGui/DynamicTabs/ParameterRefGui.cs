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
    public partial class ParameterRefGui : UserControl
    {
        public static ParameterRefGui mParameterRefGui;

        public ParameterRefGui()
        {
            InitializeComponent();
            mParameterRefGui = this;
        }

        /***********************************************************************************************************************************/
        // ParameterRef View

        public static object FillParameterRefPage(ParameterRef_t parRef)
        {
            object returnParameter = null;
            if (parRef != null)
            {
                DynamicTabControl.AddParameterRefTab();
                var ParaRefRef_RefIdParas = ApplicationProgramGui.selectedApplicationProgram.Static.Parameters.ToList().FindAll(x => x is ApplicationProgramStatic_tParameter); //in Parameter suchen
                returnParameter = ParaRefRef_RefIdParas.Find(x => (x as ApplicationProgramStatic_tParameter).Id == parRef.RefId);
                if (returnParameter == null) //ist nicht in Parameters zu finden
                {

                    var ParaRefRef_RefIdUnions = ApplicationProgramGui.selectedApplicationProgram.Static.Parameters.ToList().FindAll(x => x is ApplicationProgramStatic_tUnion); //in Parameter als Union suchen
                    if (ParaRefRef_RefIdUnions != null)
                    {
                        foreach (var UnionItem in ParaRefRef_RefIdUnions) //jede einzelne Union durchgehen
                        {
                            var ParaRefRef_RefIdUnion = (UnionItem as ApplicationProgramStatic_tUnion).Parameter.ToList().Find(x => (x as UnionParameter_t).Id == parRef.RefId);
                            if (ParaRefRef_RefIdUnion != null)
                            {
                                returnParameter = ParaRefRef_RefIdUnion;
                                break;
                            }
                        }
                    }
                }

                // die ComboBox für die Auswahl für die passenden ParameterRef füllen
                mParameterRefGui.comboBoxPrId.Items.Clear();
                foreach (ParameterRef_t paraRef in ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs)
                {
                    if (paraRef.RefId == parRef.RefId) // wenn der ParameterRef auf den gleichen Parameter zeigt
                    {
                        ComboBoxItem newComboBoxItem = new ComboBoxItem(paraRef.Id, paraRef);
                        mParameterRefGui.comboBoxPrId.Items.Add(newComboBoxItem);
                        if (paraRef.Id == parRef.Id)
                        {
                            mParameterRefGui.comboBoxPrId.SelectedItem = newComboBoxItem;
                        }
                    }
                }
                mParameterRefGui.comboBoxPrId.Items.Add("neue ID erzeugen");

                mParameterRefGui.textPrRefId.Text = parRef.RefId;
                mParameterRefGui.textPrName.Text = parRef.Name;
                mParameterRefGui.textPrText.Text = parRef.Text;
                mParameterRefGui.textPrSuffixText.Text = parRef.SuffixText;
                mParameterRefGui.textPrTag.Text = parRef.Tag;
                mParameterRefGui.checkBoxPrDisplayOrderSpecified.Checked = parRef.DisplayOrderSpecified;
                mParameterRefGui.numericPrDisplayOrder.Value = parRef.DisplayOrder;
                mParameterRefGui.checkBoxPrAccessSpecified.Checked = parRef.AccessSpecified;
                mParameterRefGui.comboBoxPrAccess.Text = HandleKnxDataTypes.ReadKNXType(parRef.Access);
                mParameterRefGui.textPrValue.Text = parRef.Value;
                mParameterRefGui.textPrInitialValue.Text = parRef.InitialValue;
                mParameterRefGui.checkBoxPrCustomerAdjustable.Checked = parRef.CustomerAdjustable;
                mParameterRefGui.textPrTextParameterRefId.Text = parRef.TextParameterRefId;
                mParameterRefGui.textPrInternalDescription.Text = parRef.InternalDescription;
                mParameterRefGui.checkBoxPrForbidGrantingUseByCustomer.Checked = parRef.ForbidGrantingUseByCustomer;
                mParameterRefGui.textPrSemantics.Text = parRef.Semantics;
                LanguageProcessing.FillLanguageDataGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, mParameterRefGui.dgvPrTranlsationsText, parRef.Id, "Text");
            }
            return returnParameter;
        }

        // löscht die Id Felder des ParameterRef
        public static void ClearParameterRefPage()
        {
            mParameterRefGui.comboBoxPrId.SelectedIndex = -1;
            mParameterRefGui.textPrRefId.Text = "";
            mParameterRefGui.textPrName.Text = "";
            mParameterRefGui.textPrText.Text = "";
            mParameterRefGui.textPrSuffixText.Text = "";
            mParameterRefGui.textPrTag.Text = "";
            mParameterRefGui.checkBoxPrDisplayOrderSpecified.Checked = false;
            mParameterRefGui.numericPrDisplayOrder.Value = 0;
            mParameterRefGui.checkBoxPrAccessSpecified.Checked = false;
            mParameterRefGui.comboBoxPrAccess.Text = HandleKnxDataTypes.ReadKNXType(Access_t.None);
            mParameterRefGui.textPrValue.Text = "";
            mParameterRefGui.textPrInitialValue.Text = "";
            mParameterRefGui.checkBoxPrCustomerAdjustable.Checked = false;
            mParameterRefGui.textPrTextParameterRefId.Text = "";
            mParameterRefGui.textPrInternalDescription.Text = "";
            mParameterRefGui.checkBoxPrForbidGrantingUseByCustomer.Checked = false;
            mParameterRefGui.textPrSemantics.Text = "";
            LanguageProcessing.PrepareLanguageGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, mParameterRefGui.dgvPrTranlsationsText);
        }

        // der gewählte Eintrag in der ParameterRef Id ComboBox wurde veränder
        private void comboBoxPrId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBoxPrId.SelectedItem.ToString() == "neue ID erzeugen")
            {
                ClearParameterRefPage();
                int nextParameterRefNumber = AppParameterGui.HighestUsedParaRefNumber() + 1;

                ParameterRef_t oldParaRef = ((comboBoxPrId.Items[0] as ComboBoxItem).Tag as ParameterRef_t);
                ParameterRef_t newParaRef = new ParameterRef_t();

                int ParaRefNumberPos = oldParaRef.Id.IndexOf("_R-") + 3;
                int CountToRemove = oldParaRef.Id.Count() - ParaRefNumberPos;
                string ParaRefName = oldParaRef.Id.Remove(ParaRefNumberPos, CountToRemove);
                ParaRefName = ParaRefName + nextParameterRefNumber.ToString();

                newParaRef.Id = ParaRefName;
                newParaRef.RefId = oldParaRef.RefId;
                textPrRefId.Text = newParaRef.RefId;

                // das ParameterRef Object anhängen
                ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs = HandleArrayFunctions.Add(ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs, newParaRef);

                ComboBoxItem newComboBoxItem = new ComboBoxItem(newParaRef.Id, newParaRef);
                comboBoxPrId.Items.Add(newComboBoxItem);
                comboBoxPrId.SelectedItem = newComboBoxItem;
            }
            else
            {
                RefreshParameterRefPage();
            }
        }

        /***********************************************************************************************************************************/
        // ParameterRef Edit
        // der speichern Button im ParameterRef Tab
        private void buttonParameterRefSave_Click(object sender, EventArgs e)
        {
            ParameterRef_t paraRef = new ParameterRef_t();
            paraRef = (comboBoxPrId.SelectedItem as ComboBoxItem).Tag as ParameterRef_t;

            if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ParameterRefRef_t)
            {
                (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ParameterRefRef_t).RefId = paraRef.Id;
                ParameterRefRefGui.SetParameterRefRefRefId(paraRef.Id);
                //paraRef = selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == (ParaKoTreeView.SelectedNode.Tag as ParameterRefRef_t).RefId);
            }
            else if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ComObjectParameterChoose_t)
            {
                // Im Falle eines ComObjectParameterChoose ist eventuell ein ParameterRefRef mit gleicher ParamRefId vorhanden, wird aber im TreeView nicht angezeigt
                // Trotzdem muss die ParamRedId dieses ParameterRefRef angepasst werden
                ParameterRefRef_t paraRefRef = ComObjectParameterChooseGui.FindParaRefRefToComObjParaChoose();
                if (paraRefRef != null)
                {
                    paraRefRef.RefId = paraRef.Id;
                }

                (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_t).ParamRefId = paraRef.Id;
                ComObjectParameterChooseGui.SetComObjectParameterChooseParameterRefId(paraRef.Id);



                //paraRef = selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == (ParaKoTreeView.SelectedNode.Tag as ComObjectParameterChoose_t).ParamRefId);
            }
            else if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ChannelChoose_t)
            {
                (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ChannelChoose_t).ParamRefId = paraRef.Id;
                ChannelChooseGui.SetChannelChooseParameterRefId(paraRef.Id);
                //paraRef = selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == (ParaKoTreeView.SelectedNode.Tag as ChannelChoose_t).ParamRefId);
            }
            else if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ComObjectParameterBlock_t)
            {
                (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterBlock_t).ParamRefId = paraRef.Id;
                ComObjectParameterBlockGui.SetComObjectParameterBlockParameterRefId(paraRef.Id);
                //paraRef = selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == (ParaKoTreeView.SelectedNode.Tag as ComObjectParameterBlock_t).ParamRefId);
            }
            DynamicSectionEdit.CleanAllDyamicReferences();

            paraRef.Name = Extensions.NullIfEmpty(textPrName.Text);
            paraRef.Text = Extensions.NullIfEmpty(textPrText.Text);
            paraRef.SuffixText = Extensions.NullIfEmpty(textPrSuffixText.Text);
            paraRef.Tag = Extensions.NullIfEmpty(textPrTag.Text);
            paraRef.DisplayOrderSpecified = checkBoxPrDisplayOrderSpecified.Checked;
            paraRef.DisplayOrder = (int)numericPrDisplayOrder.Value;
            paraRef.AccessSpecified = checkBoxPrAccessSpecified.Checked;
            paraRef.Access = HandleGuiDataTypes.ReadAccess(comboBoxPrAccess.SelectedItem.ToString());
            paraRef.Value = Extensions.NullIfEmpty(textPrValue.Text);
            paraRef.InitialValue = Extensions.NullIfEmpty(textPrInitialValue.Text);
            paraRef.CustomerAdjustable = checkBoxPrCustomerAdjustable.Checked;
            paraRef.TextParameterRefId = Extensions.NullIfEmpty(textPrTextParameterRefId.Text);
            paraRef.InternalDescription = Extensions.NullIfEmpty(textPrInternalDescription.Text);
            paraRef.ForbidGrantingUseByCustomer = checkBoxPrForbidGrantingUseByCustomer.Checked;
            paraRef.Semantics = Extensions.NullIfEmpty(textPrSemantics.Text);
            LanguageProcessing.WriteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvPrTranlsationsText, paraRef.Id, "Text", ApplicationProgramGui.selectedApplicationProgram.Id);

            RefreshParameterRefPage();
        }

        private void RefreshParameterRefPage()
        {
            DynamicTabControl.RemoveTabAtIndex(2);
            DynamicTabControl.RemoveTabAtIndex(1);
            DynamicTreeViewGui.FillParameterOrUnionPage(FillParameterRefPage((comboBoxPrId.SelectedItem as ComboBoxItem).Tag as ParameterRef_t));
            DynamicTabControl.SelectParameterRefTab();
        }

        public static string GetParameterRefRefId()
        {
            return mParameterRefGui.textPrRefId.Text;
        }

        public static ComboBoxItem GetParameterRefIdItem()
        {
            return mParameterRefGui.comboBoxPrId.SelectedItem as ComboBoxItem;
        }            
    }
}
