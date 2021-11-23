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
    public partial class ComObjectRefGui : UserControl
    {
        private static ComObjectRefGui mComObjectRefGui;

        public ComObjectRefGui()
        {
            InitializeComponent();
            mComObjectRefGui = this;
        }

        /***********************************************************************************************************************/
        //ComObjectRef View

        public static ComObject_t FillComObjectRefPage(ComObjectRef_t comObjRef)
        {
            DynamicTabControl.AddComObjectRefTab();
            ComObject_t returnComObject = ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject.ToList().Find(x => x.Id == comObjRef.RefId);

            mComObjectRefGui.comboBoxCorId.Items.Clear();
            foreach (ComObjectRef_t comObjRefItem in ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs)
            {
                if (comObjRefItem.RefId == comObjRef.RefId) // wenn der ComObjectRef auf das gleiche ComObject zeigt
                {
                    ComboBoxItem newComboBoxItem = new ComboBoxItem(comObjRefItem.Id, comObjRefItem);
                    mComObjectRefGui.comboBoxCorId.Items.Add(newComboBoxItem);
                    if (comObjRefItem.Id == comObjRef.Id)
                    {
                        mComObjectRefGui.comboBoxCorId.SelectedItem = newComboBoxItem;
                    }
                }
            }

            mComObjectRefGui.textCorRefId.Text = comObjRef.RefId;
            mComObjectRefGui.textCorName.Text = comObjRef.Name;
            mComObjectRefGui.textCorText.Text = comObjRef.Text;
            mComObjectRefGui.textCorTag.Text = comObjRef.Text;
            mComObjectRefGui.textCorFunctionText.Text = comObjRef.FunctionText;
            mComObjectRefGui.comboBoxCorPriority.SelectedItem = HandleKnxDataTypes.ReadKNXType(comObjRef.Priority);
            mComObjectRefGui.comboBoxCorObjectSize.SelectedItem = HandleKnxDataTypes.GetXmlEnumAttributeValueFromEnum(comObjRef.ObjectSize);
            mComObjectRefGui.checkBoxCorReadFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObjRef.ReadFlag);
            mComObjectRefGui.checkBoxCorWriteFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObjRef.WriteFlag);
            mComObjectRefGui.checkBoxCorCommunicationFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObjRef.CommunicationFlag);
            mComObjectRefGui.checkBoxCorTransmitFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObjRef.TransmitFlag);
            mComObjectRefGui.checkBoxCorUpdateFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObjRef.UpdateFlag);
            mComObjectRefGui.checkBoxCorReadOnInitFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObjRef.ReadOnInitFlag);
            HandleDatapointTypes.FillDptInListbox(mComObjectRefGui.listCorDatapointType, comObjRef.DatapointType);
            mComObjectRefGui.textCorTextParameterRefId.Text = comObjRef.TextParameterRefId;
            mComObjectRefGui.textCorInternalDescription.Text = comObjRef.InternalDescription;
            HandleKnxDataTypes.ReadKNXType(mComObjectRefGui.listCorRoles, comObjRef.Roles);
            mComObjectRefGui.comboBoxCorSecurityRequired.SelectedItem = HandleKnxDataTypes.ReadKNXType(comObjRef.SecurityRequired);
            mComObjectRefGui.checkBoxCorMayRead.Checked = comObjRef.MayRead;
            mComObjectRefGui.checkBoxCorReadFlagLocked.Checked = comObjRef.ReadFlagLocked;
            mComObjectRefGui.checkBoxCorWriteFlagLocked.Checked = comObjRef.WriteFlagLocked;
            mComObjectRefGui.checkBoxCorTransmitFlagLocked.Checked = comObjRef.TransmitFlagLocked;
            mComObjectRefGui.checkBoxCorUpdateFlagLocked.Checked = comObjRef.UpdateFlagLocked;
            mComObjectRefGui.checkBoxCorReadOnInitFlagLocked.Checked = comObjRef.ReadOnInitFlagLocked;
            mComObjectRefGui.textCorSemantics.Text = comObjRef.Semantics;
            LanguageProcessing.FillLanguageDataGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, mComObjectRefGui.dgvCorTranslationsText, comObjRef.Id, "Text");
            LanguageProcessing.FillLanguageDataGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, mComObjectRefGui.dgvCorTranlastionsVisibleDescription, comObjRef.Id, "VisibleDescription");
            LanguageProcessing.FillLanguageDataGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, mComObjectRefGui.dgvCorTranslationsFunctionText, comObjRef.Id, "FunctionText");
            return returnComObject;
        }

        public static string GetComObjectRefRefId()
        {
            return mComObjectRefGui.textCorRefId.Text;
        }

        public static ComboBoxItem GetComObjectRefItem()
        {
            return mComObjectRefGui.comboBoxCorId.SelectedItem as ComboBoxItem;
        }

        public static void ClearComObjectRefPage()
        {
            mComObjectRefGui.comboBoxCorId.SelectedIndex = -1;
            mComObjectRefGui.textCorRefId.Text = "";
            mComObjectRefGui.textCorName.Text = "";
            mComObjectRefGui.textCorText.Text = "";
            mComObjectRefGui.textCorTag.Text = "";
            mComObjectRefGui.textCorFunctionText.Text = "";
            mComObjectRefGui.comboBoxCorPriority.SelectedIndex = -1;
            mComObjectRefGui.comboBoxCorObjectSize.SelectedIndex = -1;
            mComObjectRefGui.checkBoxCorReadFlag.Checked = false;
            mComObjectRefGui.checkBoxCorWriteFlag.Checked = false;
            mComObjectRefGui.checkBoxCorCommunicationFlag.Checked = false;
            mComObjectRefGui.checkBoxCorTransmitFlag.Checked = false;
            mComObjectRefGui.checkBoxCorUpdateFlag.Checked = false;
            mComObjectRefGui.checkBoxCorReadOnInitFlag.Checked = false;
            HandleDatapointTypes.FillDptInListbox(mComObjectRefGui.listCorDatapointType, null);
            mComObjectRefGui.textCorTextParameterRefId.Text = "";
            mComObjectRefGui.textCorInternalDescription.Text = "";
            mComObjectRefGui.listCorRoles.Items.Clear();
            mComObjectRefGui.comboBoxCorSecurityRequired.SelectedIndex = -1;
            mComObjectRefGui.checkBoxCorMayRead.Checked = false;
            mComObjectRefGui.checkBoxCorReadFlagLocked.Checked = false;
            mComObjectRefGui.checkBoxCorWriteFlagLocked.Checked = false;
            mComObjectRefGui.checkBoxCorTransmitFlagLocked.Checked = false;
            mComObjectRefGui.checkBoxCorUpdateFlagLocked.Checked = false;
            mComObjectRefGui.checkBoxCorReadOnInitFlagLocked.Checked = false;
            mComObjectRefGui.textCorSemantics.Text = "";
        }

        // der gewählte Eintrag in der ComObjectRef Id ComboBox wurde geändert
        private void comboBoxCorId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshComObjectRefPage();
        }

        private void RefreshComObjectRefPage()
        {
            DynamicTabControl.RemoveTabAtIndex(2); // ComObjectPage löschen
            DynamicTabControl.RemoveTabAtIndex(1); // ComObjectRefPage löschen
            ComObjectGui.FillComObjectPage(FillComObjectRefPage((comboBoxCorId.SelectedItem as ComboBoxItem).Tag as ComObjectRef_t));
            DynamicTabControl.SelectComObjectRefTab();
        }

        /***********************************************************************************************************************/
        //ComObjectRef Edit

        // der speichern Button im ComObjectRef Tab
        private void ButtonComObjRefSave_Click(object sender, EventArgs e)
        {
            ComObjectRef_t comObjRef = ComObjectRefGui.GetComObjectRefItem().Tag as ComObjectRef_t;
            // = selectedApplicationProgram.Static.ComObjectRefs.ToList().Find(x => x.Id == (ParaKoTreeView.SelectedNode.Tag as ComObjectRefRef_t).RefId);

            (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectRefRef_t).RefId = comObjRef.Id;
            ComObjectRefRefGui.SetComObjectRefRefRefId(comObjRef.Id);
            DynamicSectionEdit.CleanAllDyamicReferences();

            comObjRef.Name = Extensions.NullIfEmpty(textCorName.Text);
            comObjRef.Text = Extensions.NullIfEmpty(textCorText.Text);
            comObjRef.Tag = Extensions.NullIfEmpty(textCorTag.Text);
            comObjRef.FunctionText = Extensions.NullIfEmpty(textCorFunctionText.Text);
            comObjRef.Priority = HandleGuiDataTypes.ReadComObjectPriority(comboBoxCorPriority.SelectedItem.ToString());
            comObjRef.ObjectSize = HandleGuiDataTypes.ReadComObjectSize(comboBoxCorObjectSize.SelectedItem.ToString());
            comObjRef.ReadFlag = HandleGuiDataTypes.ReadEnable(checkBoxCorReadFlag.Checked);
            comObjRef.WriteFlag = HandleGuiDataTypes.ReadEnable(checkBoxCorWriteFlag.Checked);
            comObjRef.CommunicationFlag = HandleGuiDataTypes.ReadEnable(checkBoxCorCommunicationFlag.Checked);
            comObjRef.TransmitFlag = HandleGuiDataTypes.ReadEnable(checkBoxCorTransmitFlag.Checked);
            comObjRef.UpdateFlag = HandleGuiDataTypes.ReadEnable(checkBoxCorUpdateFlag.Checked);
            comObjRef.ReadOnInitFlag = HandleGuiDataTypes.ReadEnable(checkBoxCorReadOnInitFlag.Checked);
            comObjRef.DatapointType = HandleGuiDataTypes.ReadDatapointTypes(listCorDatapointType);
            comObjRef.TextParameterRefId = Extensions.NullIfEmpty(textCorTextParameterRefId.Text);
            comObjRef.InternalDescription = Extensions.NullIfEmpty(textCorInternalDescription.Text);
            comObjRef.Roles = HandleGuiDataTypes.ReadListBox(listCorRoles);
            comObjRef.SecurityRequired = HandleGuiDataTypes.ReadComObjectSecurityRequirements(comboBoxCorSecurityRequired.SelectedItem.ToString());
            comObjRef.MayRead = checkBoxCorMayRead.Checked;
            comObjRef.ReadFlagLocked = checkBoxCorReadFlagLocked.Checked;
            comObjRef.WriteFlagLocked = checkBoxCorWriteFlagLocked.Checked;
            comObjRef.TransmitFlagLocked = checkBoxCorTransmitFlagLocked.Checked;
            comObjRef.UpdateFlagLocked = checkBoxCorUpdateFlagLocked.Checked;
            comObjRef.ReadOnInitFlagLocked = checkBoxCorReadOnInitFlagLocked.Checked;
            comObjRef.Semantics = Extensions.NullIfEmpty(textCorSemantics.Text);
            LanguageProcessing.WriteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvCorTranslationsText, comObjRef.Id, "Text", ApplicationProgramGui.selectedApplicationProgram.Id);
            LanguageProcessing.WriteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvCorTranlastionsVisibleDescription, comObjRef.Id, "VisibleDescription", ApplicationProgramGui.selectedApplicationProgram.Id);
            LanguageProcessing.WriteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvCorTranslationsFunctionText, comObjRef.Id, "FunctionText", ApplicationProgramGui.selectedApplicationProgram.Id);

            RefreshComObjectRefPage();
        }
    }
}
