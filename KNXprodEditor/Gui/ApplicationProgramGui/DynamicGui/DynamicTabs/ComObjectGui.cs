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
    public partial class ComObjectGui : UserControl
    {
        private static ComObjectGui mComObjectGui;

        public ComObjectGui()
        {
            InitializeComponent();
            mComObjectGui = this;
        }

        /****************************************************************************************************************/
        // ComObject View

        public static void FillComObjectPage(ComObject_t comObj)
        {
            DynamicTabControl.AddComObjectTab();
            mComObjectGui.comboBoxAppCoComObjects.Items.Clear();
            foreach (var comObjItem in ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(comObjItem.Number + " : " + comObjItem.Name + " (" + comObjItem.FunctionText + ")", comObjItem);
                mComObjectGui.comboBoxAppCoComObjects.Items.Add(comboBoxItem);
                if (comObjItem == comObj)
                {
                    mComObjectGui.comboBoxAppCoComObjects.SelectedItem = comboBoxItem;
                }
            }
            mComObjectGui.textAppCoId.Text = comObj.Id;
            mComObjectGui.textAppCoName.Text = comObj.Name;
            mComObjectGui.textAppCoText.Text = comObj.Text;
            mComObjectGui.numericAppCoNumber.Value = comObj.Number;
            mComObjectGui.textAppCoFunctionText.Text = comObj.FunctionText;
            mComObjectGui.comboBoxAppCoPriority.SelectedItem = HandleKnxDataTypes.ReadKNXType(comObj.Priority);
            mComObjectGui.comboBoxAppCoObjectSize.SelectedItem = HandleKnxDataTypes.GetXmlEnumAttributeValueFromEnum(comObj.ObjectSize);
            mComObjectGui.checkBoxAppCoReadFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObj.ReadFlag);
            mComObjectGui.checkBoxAppCoWriteFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObj.WriteFlag);
            mComObjectGui.checkBoxAppCoCommunicationFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObj.CommunicationFlag);
            mComObjectGui.checkBoxAppCoTransmitFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObj.TransmitFlag);
            mComObjectGui.checkBoxAppCoUpdateFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObj.UpdateFlag);
            mComObjectGui.checkBoxAppCoReadOnInitFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObj.ReadOnInitFlag);
            HandleDatapointTypes.FillDptInListbox(mComObjectGui.listBoxAppCoDatapoitType, comObj.DatapointType);
            mComObjectGui.textAppCoInternalDescription.Text = comObj.InternalDescription;
            mComObjectGui.comboBoxAppCoSecurityRequired.SelectedItem = HandleKnxDataTypes.ReadKNXType(comObj.SecurityRequired);
            mComObjectGui.checkBoxAppCoMayRead.Checked = comObj.MayRead;
            mComObjectGui.checkBoxAppCoReadFlagLocked.Checked = comObj.ReadFlagLocked;
            mComObjectGui.checkBoxAppCoWriteFlagLocked.Checked = comObj.WriteFlagLocked;
            mComObjectGui.checkBoxAppCoTransmitFlagLocked.Checked = comObj.TransmitFlagLocked;
            mComObjectGui.checkBoxAppCoUpdateFlagLocked.Checked = comObj.UpdateFlagLocked;
            mComObjectGui.checkBoxAppCoReadOnInitFlagLocked.Checked = comObj.ReadOnInitFlagLocked;

            mComObjectGui.dgvAppCoTextTranslations.Rows.Clear();
            mComObjectGui.dgvAppCoFunctionTextTranslations.Rows.Clear();
            LanguageProcessing.FillLanguageDataGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, mComObjectGui.dgvAppCoTextTranslations, comObj.Id, "Text");
            LanguageProcessing.FillLanguageDataGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, mComObjectGui.dgvAppCoFunctionTextTranslations, comObj.Id, "FunctionText");
        }


        /****************************************************************************************************************/
        // ComObject Edit

        private void comboBoxAppCoComObjects_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DynamicTabControl.RemoveComObjectTab();
            FillComObjectPage((comboBoxAppCoComObjects.SelectedItem as ComboBoxItem).Tag as ComObject_t);
            DynamicTabControl.SelectComObjectTab();
        }



        //ComObjectRefRef, ComObjectRef müssen angepasst werden, das ComObject besteht bereits
        private void buttonSaveParCoComObject_Click(object sender, EventArgs e)
        {
            if (comboBoxAppCoComObjects.SelectedIndex == -1)
            {
                MessageBox.Show("Bitte ein ComObject auswählen", "ComObject Auswahl Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // prüfen, ob ComObject verändert wurde, falls nicht -> Ende
            if (ComObjectRefGui.GetComObjectRefRefId() == ((comboBoxAppCoComObjects.SelectedItem as ComboBoxItem).Tag as ComObject_t).Id)
            {
                return;
            }

            string choosenComObjId = ((comboBoxAppCoComObjects.SelectedItem as ComboBoxItem).Tag as ComObject_t).Id;
            /*
            //ComObjectRefs durchsuchen, höchsten Index für ComObjectRef Id suchen und um eins erhöhen = neue Id  
            List<ComObjectRef_t> ComObjectRefs = selectedApplicationProgram.Static.ComObjectRefs.ToList().FindAll(x => x.Id.Contains(choosenComObjId));
            List<int> ComObjectRefIDs = new List<int>();
            foreach (ComObjectRef_t comObjRef in ComObjectRefs)
            {
                int indexOfNumber = comObjRef.Id.IndexOf("_R-") + 3;
                ComObjectRefIDs.Add(int.Parse(comObjRef.Id.Remove(0, indexOfNumber)));
            }
            ComObjectRefIDs.Sort();
            int nextComObjRefId = 0;
            if (ComObjectRefIDs.Count > 0)
            {
                nextComObjRefId = ComObjectRefIDs[ComObjectRefIDs.Count - 1] + 1;
            }
            */

            int nextComObjRefId = HighestUsedComObjRefNumber() + 1;

            ComObjectRef_t newComObjRef = new ComObjectRef_t();
            ComObjectRefRef_t newComObjRefRef = new ComObjectRefRef_t();

            TreeNode childNode = new TreeNode();

            if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ComObjectRefRef_t) // bestehendes ComObject verändert
            {
                newComObjRefRef = (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectRefRef_t);
                newComObjRef = ObjectFunctions.DeepClone(ComObjectRefGui.GetComObjectRefItem().Tag as ComObjectRef_t);

                // Überprüfen, ob alter CobObjectRef noch verwendet wird, sonst löschen (neuer ComObjectRef wird gleich erzeugt)
                if (DynamicSectionSearch.SearchIdInDynamic((DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectRefRef_t).RefId) <= 1)
                {
                    int index = 0;
                    foreach (ComObjectRef_t comObjRef in ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs)
                    {
                        if (comObjRef.Id == (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectRefRef_t).RefId)
                        {
                            ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs = HandleArrayFunctions.Delete(ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs, index);
                            index--;
                        }
                        index++;
                    }
                }
            }
            else //neues ComObject erzeugt
            {
                // je nachdem an welchem Element das neue ComObjectRefRef angehängt werden soll muss korrekt gehandelt werden
                DynamicSectionEdit.AddToItemsObject(DynamicTreeViewGui.GetSelectedTreeNode().Tag, newComObjRefRef);
            }

            // das ComObjectRef Object anhängen
            ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs = HandleArrayFunctions.Add(ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs, newComObjRef);

            newComObjRef.Id = choosenComObjId + "_R-" + nextComObjRefId.ToString();
            newComObjRef.RefId = choosenComObjId;
            newComObjRef.Tag = nextComObjRefId.ToString();

            newComObjRefRef.RefId = newComObjRef.Id;

            if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ComObjectRefRef_t) // bestehendes ComObject verändert
            {
                DynamicTreeViewGui.SetSelectedNodeText(DynamicTreeViewGenerator.TreeViewComObjectRefRef(newComObjRefRef, new TreeNode()).Text);
                DynamicTabControl.RemoveAllParamsCoTabs();
                FillComObjectPage(ComObjectRefGui.FillComObjectRefPage(ComObjectRefRefGui.FillComObjectRefRef(newComObjRefRef)));
                DynamicTabControl.SelectComObjectTab();
            }
            else
            {
                // auch in den TreeView muss das neue ComObjectRefRef eingebunden werden
                childNode = DynamicTreeViewGenerator.TreeViewComObjectRefRef(newComObjRefRef, DynamicTreeViewGui.GetSelectedTreeNode());
                DynamicTreeViewGui.SetSelectedTreeNode(childNode);
            }
        }

        public static void DeleteComObjectAndReferences()
        {
            int index = 0;
            // löschen des ComObjectRef aus ComObjectRefs Tabelle, wenn das ComObjectRef nicht mehr benötigt wird
            if (DynamicSectionSearch.SearchIdInDynamic((DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectRefRef_t).RefId) <= 1)
            {
                foreach (ComObjectRef_t comObjRef in ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs)
                {
                    if (comObjRef.Id == (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectRefRef_t).RefId)
                    {
                        ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs = HandleArrayFunctions.Delete(ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs, index);
                        index--;
                    }
                    index++;
                }
            }

            // löschen der ComObjectRefRef aus TreeView und Dynamic Section
            DynamicSectionEdit.DeleteItemsFromObjectAndTreeView(DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag, DynamicTreeViewGui.GetSelectedTreeNode().Tag, DynamicTreeViewGui.GetTreeView());
        }

        /// <summary>
        /// Findet die höchste bisher verwendete Nummer für ComObjectRef
        /// </summary>
        /// <returns>höchste bisher verwendete Nummer</returns>
        public static int HighestUsedComObjRefNumber()
        {
            int highestId = 0;
            foreach (ComObjectRef_t comObjRef in ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs)
            {
                if (comObjRef.Id != null)
                {
                    int indexOfNumber = comObjRef.Id.IndexOf("_R-");
                    int parId = int.Parse(comObjRef.Id.Remove(0, indexOfNumber + 3)); // App Id + _P-
                    if (parId > highestId)
                    {
                        highestId = parId;
                    }
                }
            }
            return highestId;
        }

        public static void ClearComObjectPage()
        {
            DynamicTabControl.AddComObjectTab();
            mComObjectGui.comboBoxAppCoComObjects.Items.Clear();
            if (ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject != null)
            {
                foreach (var comObjItem in ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject)
                {
                    ComboBoxItem comboBoxItem = new ComboBoxItem(comObjItem.Number + " : " + comObjItem.Name, comObjItem);
                    mComObjectGui.comboBoxAppCoComObjects.Items.Add(comboBoxItem);
                    mComObjectGui.comboBoxAppCoComObjects.SelectedIndex = -1;
                }
            }
            mComObjectGui.textAppCoId.Text = "";
            mComObjectGui.textAppCoName.Text = "";
            mComObjectGui.textAppCoText.Text = "";
            mComObjectGui.numericAppCoNumber.Value = 0;
            mComObjectGui.textAppCoFunctionText.Text = "";
            mComObjectGui.comboBoxAppCoPriority.SelectedIndex = -1;
            mComObjectGui.comboBoxAppCoObjectSize.SelectedIndex = -1;
            mComObjectGui.checkBoxAppCoReadFlag.Checked = false;
            mComObjectGui.checkBoxAppCoWriteFlag.Checked = false;
            mComObjectGui.checkBoxAppCoCommunicationFlag.Checked = false;
            mComObjectGui.checkBoxAppCoTransmitFlag.Checked = false;
            mComObjectGui.checkBoxAppCoUpdateFlag.Checked = false;
            mComObjectGui.checkBoxAppCoReadOnInitFlag.Checked = false;
            HandleDatapointTypes.FillDptInListbox(mComObjectGui.listBoxAppCoDatapoitType, null);
            mComObjectGui.textAppCoInternalDescription.Text = "";
            mComObjectGui.comboBoxAppCoSecurityRequired.SelectedIndex = -1;
            mComObjectGui.checkBoxAppCoMayRead.Checked = false;
            mComObjectGui.checkBoxAppCoReadFlagLocked.Checked = false;
            mComObjectGui.checkBoxAppCoWriteFlagLocked.Checked = false;
            mComObjectGui.checkBoxAppCoTransmitFlagLocked.Checked = false;
            mComObjectGui.checkBoxAppCoUpdateFlagLocked.Checked = false;
            mComObjectGui.checkBoxAppCoReadOnInitFlagLocked.Checked = false;

            mComObjectGui.dgvAppCoTextTranslations.Rows.Clear();
            mComObjectGui.dgvAppCoFunctionTextTranslations.Rows.Clear();
            LanguageProcessing.PrepareLanguageGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, mComObjectGui.dgvAppCoTextTranslations);
            LanguageProcessing.PrepareLanguageGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, mComObjectGui.dgvAppCoFunctionTextTranslations);
        }
    }
}
