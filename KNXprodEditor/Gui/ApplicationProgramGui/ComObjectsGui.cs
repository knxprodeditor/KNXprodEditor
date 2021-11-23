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
    public partial class ComObjectsGui : UserControl
    {
        private static ComObjectsGui mComObjectsGui;

        public ComObjectsGui()
        {
            InitializeComponent();
            mComObjectsGui = this;
        }

        /**********************************************************************************************************************/
        // ComObjects View

        public static void InitializeCommObjectsTreeView()
        {
            mComObjectsGui.CalculateComObjectsMemory();

            while (mComObjectsGui.treeViewCommObjects.Nodes.Count > 0)
            {
                mComObjectsGui.treeViewCommObjects.Nodes.RemoveAt(0);
            }

            if (ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject != null)
            {
                foreach (ComObject_t comObj in ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject)
                {
                    TreeNode node = mComObjectsGui.treeViewCommObjects.Nodes.Add("ComObject " + comObj.Number + " : " + comObj.Name + " (" + comObj.FunctionText + ")");
                    node.Tag = comObj;

                    /*
                     * Hier werden alle ComObjectRefs an das entsprechende ComObject angehängt (hat aber zur Zeit keinen Nutzen)
                     * 
                    List <ComObjectRef_t> comObjRefs = selectedApplicationProgram.Static.ComObjectRefs.ToList().FindAll(x => x.RefId == comObj.Id);
                    foreach(var comObjRef in comObjRefs)
                    {
                        TreeNode childNode = node.Nodes.Add(comObjRef.Id);
                        childNode.Tag = comObjRef;
                    }
                    */
                }
            }
            //treeViewCommObjects.TreeViewNodeSorter = new TreeNodeSorter();
            //treeViewCommObjects.Sort();
        }

        private TreeNode lastSelectedTreeViewCommObjectsNode;

        private void TreeViewCommObjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Highlight new node
            e.Node.BackColor = SystemColors.Highlight;
            e.Node.ForeColor = SystemColors.HighlightText;
            if (lastSelectedTreeViewCommObjectsNode != null)
            {
                // Set colors to normal for old node
                lastSelectedTreeViewCommObjectsNode.BackColor = SystemColors.Window;
                lastSelectedTreeViewCommObjectsNode.ForeColor = SystemColors.WindowText;
            }
            lastSelectedTreeViewCommObjectsNode = e.Node;

            ComObject_t comObj = e.Node.Tag as ComObject_t;

            textCoId.Text = comObj.Id;
            textCoName.Text = comObj.Name;
            textCoText.Text = comObj.Text;
            numericCoNumber.Value = comObj.Number;
            textCoFunctionText.Text = comObj.FunctionText;
            comboBoxCoPriority.SelectedItem = HandleKnxDataTypes.ReadKNXType(comObj.Priority);
            comboBoxCoObjectSize.SelectedItem = HandleKnxDataTypes.GetXmlEnumAttributeValueFromEnum(comObj.ObjectSize);
            checkBoxCoReadFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObj.ReadFlag);
            checkBoxCoWriteFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObj.WriteFlag);
            checkBoxCoCommunicationFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObj.CommunicationFlag);
            checkBoxCoTransmitFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObj.TransmitFlag);
            checkBoxCoUpdateFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObj.UpdateFlag);
            checkBoxCoReadOnInitFlag.Checked = HandleKnxDataTypes.ReadKNXType(comObj.ReadOnInitFlag);
            HandleDatapointTypes.FillDptInListbox(listCoDatapointType, comObj.DatapointType);
            textCoInternalDescription.Text = comObj.InternalDescription;
            comboBoxCoSecurityRequired.SelectedItem = HandleKnxDataTypes.ReadKNXType(comObj.SecurityRequired);
            checkBoxCoMayRead.Checked = comObj.MayRead;
            checkBoxCoReadFlagLocked.Checked = comObj.ReadFlagLocked;
            checkBoxCoWriteFlagLocked.Checked = comObj.WriteFlagLocked;
            checkBoxCoTransmitFlagLocked.Checked = comObj.TransmitFlagLocked;
            checkBoxCoUpdateFlagLocked.Checked = comObj.UpdateFlagLocked;
            checkBoxCoReadOnInitFlagLocked.Checked = comObj.ReadOnInitFlagLocked;

            dgvComObjTextTranslations.Rows.Clear();
            dgvComObjFunctionTextTranslations.Rows.Clear();
            LanguageProcessing.FillLanguageDataGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvComObjTextTranslations, comObj.Id, "Text");
            LanguageProcessing.FillLanguageDataGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvComObjFunctionTextTranslations, comObj.Id, "FunctionText");

        }

        private void CalculateComObjectsMemory()
        {
            if (ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.CodeSegment != null)
            {
                ApplicationProgramStatic_tCodeAbsoluteSegment codeSegment =
                    ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment.ToList().Find(x => x.Id == ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.CodeSegment);

                if (ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.OffsetSpecified)
                {
                    numericComObjMemStart.Value = codeSegment.Address + ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.Offset;
                    numericComObjMemEnd.Value = codeSegment.Address + codeSegment.Size + ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.Offset;
                }
                else
                {
                    numericComObjMemStart.Value = codeSegment.Address;
                    numericComObjMemEnd.Value = codeSegment.Address + codeSegment.Size;
                }
            }
        }


        public static uint GetHighestComObjectNumber()
        {
            uint highestCONumber = 0;
            if (ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject != null)
            {
                foreach (ComObject_t comObject in ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject)
                {
                    if (comObject.Number > highestCONumber)
                    {
                        highestCONumber = comObject.Number;
                    }
                }
            }
            return highestCONumber;
        }

        public static ref TreeView GetCommObjectsTreeView()
        {
            return ref mComObjectsGui.treeViewCommObjects;
        }

        /**********************************************************************************************************************/
        // ComObjects Edit

        private void treeViewCommObjects_MouseDown(object sender, MouseEventArgs e)
        {
            ContextMenuHandler.contextMenuLastClickedTreeView = treeViewCommObjects;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuHandler.ShowContextMenuAtMousePosition();

                treeViewCommObjects.SelectedNode = treeViewCommObjects.GetNodeAt(e.X, e.Y);

                ContextMenuHandler.SetToolStripMenuAddVisible(true);
                ContextMenuHandler.SettoolStripMenuPasteVisible(false);
                if (treeViewCommObjects.SelectedNode == null)
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
        /// Der gewählte Eintrag des DatapointTypes wurde geändert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listCoDatapointType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listCoDatapointType.SelectedItem != null)
            {
                uint dptSizeInBit = (listCoDatapointType.SelectedItem as HandleDatapointTypes.DatapointListBoxData).SizeInBit;
                ComObjectSize_t comObjectSize = 0;
                if (dptSizeInBit > 0 && dptSizeInBit < 8)
                {
                    comObjectSize = (ComObjectSize_t)dptSizeInBit - 1;
                }
                else if (dptSizeInBit > 7)
                {
                    uint dptSizeInByte = dptSizeInBit / 8;
                    comObjectSize = (ComObjectSize_t)dptSizeInByte + 6;
                }
                comboBoxCoObjectSize.SelectedItem = HandleKnxDataTypes.GetXmlEnumAttributeValueFromEnum(comObjectSize);
            }
        }

        /// <summary>
        /// Vorbereiten der ComObject Maske zur EIngabe eines neuen Com Objects
        /// </summary>
        public static void AddComObject()
        {
            mComObjectsGui.ClearComObjectsPage();

            if (mComObjectsGui.lastSelectedTreeViewCommObjectsNode != null)
            {
                // Set colors to normal for old node
                mComObjectsGui.lastSelectedTreeViewCommObjectsNode.BackColor = SystemColors.Window;
                mComObjectsGui.lastSelectedTreeViewCommObjectsNode.ForeColor = SystemColors.WindowText;
                mComObjectsGui.lastSelectedTreeViewCommObjectsNode = null;
            }

            //deselect existing ComObject Item
            mComObjectsGui.treeViewCommObjects.SelectedNode = null;
        }

        /// <summary>
        /// Zum Kopieren eines ComObjects wird das kopierte Element im TreeNode deselektiert
        /// Dadurch kann das ComObject als neues Object mit andere Nummer gespeichert werden
        /// </summary>
        public static void CopyComObject()
        {
            if (mComObjectsGui.lastSelectedTreeViewCommObjectsNode != null)
            {
                // Set colors to normal for old node
                mComObjectsGui.lastSelectedTreeViewCommObjectsNode.BackColor = SystemColors.Window;
                mComObjectsGui.lastSelectedTreeViewCommObjectsNode.ForeColor = SystemColors.WindowText;
                mComObjectsGui.lastSelectedTreeViewCommObjectsNode = null;
            }

            // deselect existing ComObject Item
            mComObjectsGui.treeViewCommObjects.SelectedNode = null;

            // delete old Id
            mComObjectsGui.textCoId.Text = "";
        }

        /// <summary>
        /// Das ausgewählte Com Object löschen
        /// </summary>
        public static void DeleteComObject()
        {
            DialogResult result = MessageBox.Show("Soll das ComObject wirklich gelöscht werden? Die Löschung wirkt sich auf alle Einträge im \"Parameter und KO\" Bereich aus!", "ComObject Löschung Sicherheitsabfrage",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            // Zwischenspeichern der ComObject Referenzen zum löschen (TreeViews werden verändert und zeigen dann nicht mehr auf das richtige Objekt)
            string comObjId = (mComObjectsGui.treeViewCommObjects.SelectedNode.Tag as ComObject_t).Id;
            int comObjIndex = mComObjectsGui.treeViewCommObjects.SelectedNode.Index;
            // Löschen des Com Objectes
            ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject =
                TreeViewArrayFunctions.DeleteArrayTreeView(ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject, comObjIndex, mComObjectsGui.treeViewCommObjects);

            //Löschen aller Translation Daten
            LanguageProcessing.DeleteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, comObjId);

            // Es sollen auch alle Verweise auf dieses ComObject gelöscht werden
            foreach (ComObjectRef_t comObjRef in ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs)
            {
                if (comObjRef.RefId == comObjId)
                {
                    DynamicSectionEdit.DeleteIdOfDynamic(comObjRef.Id);
                }
            }
        }

        /// <summary>
        /// Das eingegebene Com Object speichern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSaveComObject_Click(object sender, EventArgs e)
        {
            // prüfen, ob die Com Object Nummer bereits verwendet wurde: ja = abbruch
            // ComObject Nummer = Eindeutigkeitsmerkmal für ein Com Object
            foreach (TreeNode comObjNode in treeViewCommObjects.Nodes)
            {
                if (comObjNode != treeViewCommObjects.SelectedNode) // nicht das aktuell gewählte Com Object vergleichen
                {
                    if ((comObjNode.Tag as ComObject_t).Number == numericCoNumber.Value)
                    {
                        MessageBox.Show("Es muss für jedes Com Object eine unterschiedliche Nummer verwendet werden. Fehler für Nummer " + numericCoNumber.Value, "Com Object Nummer Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            ComObject_t newComObject = new ComObject_t();

            if (treeViewCommObjects.SelectedNode != null) // ein bestehender ParameterType Eintrag wurde verändert
            {
                newComObject = treeViewCommObjects.SelectedNode.Tag as ComObject_t;  // Tag ist Typ ComObject_t
            }
            else // ein neuer Eintrag wurde erzeugt
            {
                if (ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject == null)
                {
                    ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject = new ComObject_t[0];
                }
                ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject = HandleArrayFunctions.Add(ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject, newComObject);
            }

            // Die ID eine ParameterTypes setzt sich aus ApplicationProgram Id, _O- und Nummer des Com Objects zusammen
            newComObject.Id = ApplicationProgramGui.selectedApplicationProgram.Id + "_O-" + numericCoNumber.Value;
            newComObject.Name = Extensions.NullIfEmpty(textCoName.Text);
            newComObject.Text = Extensions.NullIfEmpty(textCoText.Text);
            newComObject.Number = (uint)numericCoNumber.Value;
            newComObject.FunctionText = Extensions.NullIfEmpty(textCoFunctionText.Text);
            newComObject.Priority = HandleGuiDataTypes.ReadComObjectPriority(comboBoxCoPriority.SelectedItem.ToString());
            newComObject.ObjectSize = HandleGuiDataTypes.ReadComObjectSize(comboBoxCoObjectSize.SelectedItem.ToString());
            newComObject.ReadFlag = HandleGuiDataTypes.ReadEnable(checkBoxCoReadFlag.Checked);
            newComObject.WriteFlag = HandleGuiDataTypes.ReadEnable(checkBoxCoWriteFlag.Checked);
            newComObject.CommunicationFlag = HandleGuiDataTypes.ReadEnable(checkBoxCoCommunicationFlag.Checked);
            newComObject.TransmitFlag = HandleGuiDataTypes.ReadEnable(checkBoxCoTransmitFlag.Checked);
            newComObject.UpdateFlag = HandleGuiDataTypes.ReadEnable(checkBoxCoUpdateFlag.Checked);
            newComObject.ReadOnInitFlag = HandleGuiDataTypes.ReadEnable(checkBoxCoReadOnInitFlag.Checked);
            newComObject.DatapointType = HandleGuiDataTypes.ReadDatapointTypes(listCoDatapointType);
            newComObject.InternalDescription = Extensions.NullIfEmpty(textCoInternalDescription.Text);
            newComObject.SecurityRequired = HandleGuiDataTypes.ReadComObjectSecurityRequirements(comboBoxCoSecurityRequired.SelectedItem.ToString());
            newComObject.MayRead = checkBoxCoMayRead.Checked;
            newComObject.ReadFlagLocked = checkBoxCoReadFlagLocked.Checked;
            newComObject.WriteFlagLocked = checkBoxCoWriteFlagLocked.Checked;
            newComObject.TransmitFlagLocked = checkBoxCoTransmitFlagLocked.Checked;
            newComObject.UpdateFlagLocked = checkBoxCoUpdateFlagLocked.Checked;
            newComObject.ReadOnInitFlagLocked = checkBoxCoReadOnInitFlagLocked.Checked;

            if (newComObject.Id != textCoId.Text && textCoId.Text != "")
            {
                //bevor die ID geändert wird muss erst auch die Translation ID angepasst werden!
                LanguageProcessing.ChangeTansElementRefId(ApplicationProgramGui.selectedApplicationManufacturer.Languages, textCoId.Text, newComObject.Id);
                // und auch die ID muss in den restlichen Tabellen angepasst werden
                ExchangeComObjectRefId(textCoId.Text, newComObject.Id);
            }

            // die Übersetzungen in die Translations schreiben
            LanguageProcessing.WriteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvComObjTextTranslations, newComObject.Id, "Text", ApplicationProgramGui.selectedApplicationProgram.Id);
            LanguageProcessing.WriteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvComObjFunctionTextTranslations, newComObject.Id, "FunctionText", ApplicationProgramGui.selectedApplicationProgram.Id);

            UserRamEdit.RearrangeComObjectsInUserRam();

            SortComObjectsTable();

            RefillTreeViewCommObjects();
        }

        /// <summary>
        /// Die Auflistung der ComObjekte wir neu aus den Daten gefüllt und der bisher markierte Eintrag wird wieder gewählt
        /// </summary>
        private void RefillTreeViewCommObjects()
        {
            int index = 0;
            ComObject_t comObjMem = null;
            if (treeViewCommObjects.SelectedNode != null) // alter Eintrag wurde bearbeitet
            {
                //das bisher gewählte ComObject ablegen, um es nach der neuen Befüllung des Treeviews wieder zu aktivieren
                comObjMem = treeViewCommObjects.SelectedNode.Tag as ComObject_t;


                //index = treeViewCommObjects.SelectedNode.Index; //den alten Index ablegen, um nach der neuen Befüllung des Treeviews ihn wieder zu aktivieren
            }
            else // neuer Eintrag wurde hinzugefügt
            {
                index = treeViewCommObjects.Nodes.Count; // neuer Index ist die alte Anzahl von TreeView Elementen
            }

            InitializeCommObjectsTreeView();

            if (comObjMem != null)
            {
                foreach (TreeNode comObjTreeNode in treeViewCommObjects.Nodes)
                {
                    if (comObjTreeNode.Tag == comObjMem)
                    {
                        treeViewCommObjects.SelectedNode = comObjTreeNode;
                        break;
                    }
                }
            }
            else
            {
                if (index >= treeViewCommObjects.Nodes.Count)
                {
                    index = treeViewCommObjects.Nodes.Count - 1;
                }
                treeViewCommObjects.SelectedNode = treeViewCommObjects.Nodes[index];
            }
        }

        private void ClearComObjectsPage()
        {
            textCoId.Text = "";
            textCoName.Text = "";
            textCoText.Text = "";
            numericCoNumber.Value = 0;
            textCoFunctionText.Text = "";
            comboBoxCoPriority.SelectedIndex = 0;
            comboBoxCoObjectSize.SelectedIndex = 0;
            checkBoxCoReadFlag.Checked = false;
            checkBoxCoWriteFlag.Checked = false;
            checkBoxCoCommunicationFlag.Checked = false;
            checkBoxCoTransmitFlag.Checked = false;
            checkBoxCoUpdateFlag.Checked = false;
            checkBoxCoReadOnInitFlag.Checked = false;
            HandleDatapointTypes.FillDptInListbox(listCoDatapointType, null);
            textCoInternalDescription.Text = "";
            comboBoxCoSecurityRequired.SelectedIndex = 0;
            checkBoxCoMayRead.Checked = false;
            checkBoxCoReadFlagLocked.Checked = false;
            checkBoxCoWriteFlagLocked.Checked = false;
            checkBoxCoTransmitFlagLocked.Checked = false;
            checkBoxCoUpdateFlagLocked.Checked = false;
            checkBoxCoReadOnInitFlagLocked.Checked = false;

            LanguageProcessing.PrepareLanguageGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvComObjTextTranslations);
            LanguageProcessing.PrepareLanguageGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvComObjFunctionTextTranslations);
        }

        /// <summary>
        /// Sortieren der ComObject Table anhand der ComObject Nummer
        /// </summary>
        private void SortComObjectsTable()
        {
            ComObject_t[] comObjectTable = ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject;
            Array.Sort(comObjectTable, delegate (ComObject_t comObj1, ComObject_t comObj2) {
                return comObj1.Number.CompareTo(comObj2.Number);
            });
        }


        /***************************************************************************************************************************/
        // Die ComObjectRef Id in den ComObjectRefs ändern

        private void ExchangeComObjectRefId(string oldId, string newId)
        {
            // durchsuchte die ComObjectRefs Liste nach der zu ändernden ComObject Id (als ComObjectRef RefId)
            List<ComObjectRef_t> comObjRefList = ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs.ToList().FindAll(x => x.RefId == oldId);
            foreach (ComObjectRef_t comObjRef in comObjRefList)
            {
                comObjRef.RefId = newId;

                // auch in der ComObjectRef Id steckt die Nummer des Kommunikationsobjektes
                // daher auch die Id ändern und in der Dynamic Sektion anpassen

                int indexOfRefNumber = comObjRef.Id.IndexOf("_R-");
                string newComObjRefId = newId + comObjRef.Id.Substring(indexOfRefNumber);
                ExchangeComObjRefRefId(comObjRef.Id, newComObjRefId);
                LanguageProcessing.ChangeTansElementRefId(ApplicationProgramGui.selectedApplicationManufacturer.Languages, comObjRef.Id, newComObjRefId);
                comObjRef.Id = newComObjRefId;
            }
        }



        /***************************************************************************************************************************/
        // Funktionen zum Austausch einer ComObjectRefRefId im Dynamic Bereich

        private void ExchangeComObjRefRefId(string oldId, string newId)
        {
            foreach (var dynChannel in ApplicationProgramGui.selectedApplicationProgram.Dynamic) //channels
            {
                if (dynChannel is ApplicationProgramChannel_t)
                {
                    foreach (var dynChannelItem in (dynChannel as ApplicationProgramChannel_t).Items) //ParameterBlock oder choose
                    {
                        ExchangeComObjRefRefIdInChannelOrChannelIndependent(dynChannelItem, oldId, newId);
                    }
                }
                if (dynChannel is ChannelIndependentBlock_t)
                {
                    foreach (var dynChannelItem in (dynChannel as ChannelIndependentBlock_t).Items) //ParameterBlock oder choose
                    {
                        ExchangeComObjRefRefIdInChannelOrChannelIndependent(dynChannelItem, oldId, newId);
                    }
                }
            }
        }


        void ExchangeComObjRefRefIdInChannelOrChannelIndependent(object dynChannelItem, string oldId, string newId)
        {
            if (dynChannelItem is ComObjectParameterBlock_t) //ParameterBlock
            {
                ExchangeComObjRefRefIdInParameterBlock((dynChannelItem as ComObjectParameterBlock_t), oldId, newId);
            }
            else if (dynChannelItem is ChannelChoose_t) //choose direkt unter channel
            {
                ExchangeComObjRefRefIdInChannelChoose((dynChannelItem as ChannelChoose_t), oldId, newId);
            }
            else if (dynChannelItem is ComObjectRefRef_t)
            {
                ExchangeComObjRefRefIdInComObjectRefRef((dynChannelItem as ComObjectRefRef_t), oldId, newId);
            }
        }

        /*
        /// <summary>
        /// Die Beschreibung eines ParameterRefRef auflösen und einen TreeNode anlegen
        /// </summary>
        /// <param name="paraRefRef">der zu beschriftende ParameterRefRef</param>
        /// <param name="parentNode">der TreeNode, an dem das neue Element angehängt werden soll</param>
        /// <returns></returns>
        private TreeNode ResolveParameterRefRef(object paraRefRef, TreeNode parentNode)
        {
            TreeNode childNode = null;
            string paraRefString = null;
            if (paraRefRef is ParameterRefRef_t)
            {
                paraRefString = (paraRefRef as ParameterRefRef_t).RefId;
            }
            else if (paraRefRef is ChannelChoose_t) //choose unter channel
            {
                paraRefString = (paraRefRef as ChannelChoose_t).ParamRefId;
            }
            else if (paraRefRef is ComObjectParameterBlock_t) //ParameterBlock unter when
            {
                paraRefString = (paraRefRef as ComObjectParameterBlock_t).ParamRefId;
            }
            else if (paraRefRef is ComObjectParameterChoose_t) //choose unter when
            {
                paraRefString = (paraRefRef as ComObjectParameterChoose_t).ParamRefId;
            }
            else if (paraRefRef is Assign_t) //assign unter when
            {
                paraRefString = (paraRefRef as Assign_t).TargetParamRefRef;
            }
            else
            {
                MessageBox.Show("Es konnte ein ParameterRefRef Typ nicht aufgelöst werden. Das Elemtent hat den Typ " + paraRefRef.GetType().ToString(), "ParameterRefFef Auflösung Fehler",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            if (paraRefString != null)
            {
                var ParametersRef_RefId = selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == paraRefString).RefId; //in ParameterRefs Sektion die RefID suchen
                LanguageData_tTranslationUnitTranslationElement ParaRefRef_RefIdTrans = null;
                if (selectedTranslationUnitApplication.TranslationElement != null)
                {
                    //RefId kann in translations oder Parameters zu finden sein
                    ParaRefRef_RefIdTrans = selectedTranslationUnitApplication.TranslationElement.ToList().Find(x => x.RefId == ParametersRef_RefId);

                }
                if (ParaRefRef_RefIdTrans != null) // in Translations gefunden
                {
                    foreach (var translationItem in ParaRefRef_RefIdTrans.Translation)
                    {
                        if (translationItem.AttributeName == "Text")
                        {
                            childNode = parentNode.Nodes.Add(translationItem.Text);
                            childNode.Tag = paraRefRef;

                        }
                    }
                }
                else
                {
                    var ParaRefRef_RefIdParas = selectedApplicationProgram.Static.Parameters.ToList().FindAll(x => x is ApplicationProgramStatic_tParameter); //in Parameter suchen
                    var ParaRefRef_RefIdPara = ParaRefRef_RefIdParas.Find(x => (x as ApplicationProgramStatic_tParameter).Id == ParametersRef_RefId);
                    if (ParaRefRef_RefIdPara != null)
                    {
                        childNode = parentNode.Nodes.Add((ParaRefRef_RefIdPara as ApplicationProgramStatic_tParameter).Name);
                        childNode.Tag = paraRefRef;
                    }
                    else
                    {
                        var ParaRefRef_RefIdUnions = selectedApplicationProgram.Static.Parameters.ToList().FindAll(x => x is ApplicationProgramStatic_tUnion); //in Parameter als Union suchen
                        if (ParaRefRef_RefIdUnions != null)
                        {
                            foreach (var UnionItem in ParaRefRef_RefIdUnions) //jede einzelne Union durchgehen
                            {
                                var ParaRefRef_RefIdUnion = (UnionItem as ApplicationProgramStatic_tUnion).Parameter.ToList().Find(x => (x as UnionParameter_t).Id == ParametersRef_RefId);
                                if (ParaRefRef_RefIdUnion != null)
                                {
                                    childNode = parentNode.Nodes.Add((ParaRefRef_RefIdUnion as UnionParameter_t).Name);
                                    childNode.Tag = paraRefRef;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Es konnte ein ParameterRefRef weder in Translation, Parameters noch Unions gefunden werden. Das Element hat die ID " + ParametersRef_RefId, "ParameterRefRef Auflösung Fehler",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            childNode.ImageIndex = (int)Images.parameter;
            childNode.SelectedImageIndex = (int)Images.parameter;
            return childNode;
        }
        */

        void ExchangeComObjRefRefIdInChannelChoose(ChannelChoose_t channelChooseItem, string oldId, string newId)
        {
            //var childNode = parentNode.Nodes.Add("ComObject Choose ParamRedId: " + COParBlockItem.ParamRefId);
            //var childNode = ResolveParameterRefRef(channelChooseItem, parentNode); //ParamRefId

            foreach (ChannelChoose_tWhen ChooseWhenItem in channelChooseItem.when)    //choose durchsuchen
            {
                ExchangeComObjRefRefIdInChannelChooseWhen(channelChooseItem, ChooseWhenItem, oldId, newId);
            }

        }

        void ExchangeComObjRefRefIdInChannelChooseWhen(ChannelChoose_t channelChooseItem, ChannelChoose_tWhen ChooseWhenItem, string oldId, string newId)
        {
            if (ChooseWhenItem.Items != null)
            {
                foreach (var whenItem in ChooseWhenItem.Items)    //when durchsuchen
                {
                    if (whenItem is ParameterRefRef_t)
                    {
                        //ResolveParameterRefRef((whenItem as ParameterRefRef_t), grandChildNode);
                    }
                    else if (whenItem is ChannelChoose_t)
                    {
                        ExchangeComObjRefRefIdInChannelChoose(whenItem as ChannelChoose_t, oldId, newId); //Rekursive Auflösung der choose-when Bäume
                    }
                    else if (whenItem is ComObjectParameterBlock_t) //ParameterBlock unter when
                    {
                        ExchangeComObjRefRefIdInParameterBlock((whenItem as ComObjectParameterBlock_t), oldId, newId);
                    }
                    else if (whenItem is ComObjectRefRef_t)
                    {
                        ExchangeComObjRefRefIdInComObjectRefRef((whenItem as ComObjectRefRef_t), oldId, newId);
                    }
                    else
                    {
                        MessageBox.Show("Unter when konnte in einem Channel konnte ein Item nicht zugeordnet werden. Das Item hat den Typ " + whenItem.GetType().ToString(), "Channel choose-when Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        void ExchangeComObjRefRefIdInParameterBlock(ComObjectParameterBlock_t comObjectParBlock, string oldId, string newId)
        {
            if (comObjectParBlock.Items != null)
            {
                foreach (var ParBlockItem in comObjectParBlock.Items) //ComObjectRefRef oder ParameterRefRef
                {
                    if (ParBlockItem is ComObjectRefRef_t)
                    {
                        ExchangeComObjRefRefIdInComObjectRefRef((ParBlockItem as ComObjectRefRef_t), oldId, newId);
                    }
                    else if (ParBlockItem is ParameterRefRef_t)
                    {
                        //ResolveParameterRefRef((ParBlockItem as ParameterRefRef_t), parBlockChild);
                    }
                    else if (ParBlockItem is ParameterSeparator_t)
                    {

                    }
                    else if (ParBlockItem is ComObjectParameterChoose_t)
                    {
                        ExchangeComObjRefRefIdInComObjectChoose((ParBlockItem as ComObjectParameterChoose_t), oldId, newId);
                    }
                    else if (ParBlockItem is Assign_t)
                    {
                        //ResolveParameterRefRef((ParBlockItem as Assign_t), parBlockChild); //in ResolveParameterRefRef wird nur die TargetParamRefRef ID aufgelöst, die SourceParamRefRef ID scheint der Absprungpunkt zu sein
                    }
                    else
                    {
                        MessageBox.Show("Unter when konnte in einem ParameterBlock ein Item nicht zugeordnet werden. Das Item hat den Typ " + ParBlockItem.GetType().ToString(), "ParameterBlock Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /*
        /// <summary>
        /// Die Beschreibung eines ComObjectParameterBlocks für den Eintrag im TreeView herausfinden
        /// </summary>
        /// <param name="comObjectParBlock">der zu beschreibende ComObjectParameterBlock</param>
        /// <param name="parentNode">der Node, an dem dér ComObjectParameterBlock angehängt werden soll</param>
        /// <returns></returns>
        TreeNode ResolveParameterBlockDescription(ComObjectParameterBlock_t comObjectParBlock, TreeNode parentNode)
        {
            TreeNode parBlockChild = null;

            if (comObjectParBlock.ParamRefId != null)
            {
                parBlockChild = ResolveParameterRefRef(comObjectParBlock, parentNode); //ParamRefId
            }
            else if (comObjectParBlock.Id != null) //Sonderfall: keine RefId, nur Id vorhanden -> nicht über ParamaterRefRef auflösen
            {
                var ParaRefRef_RefIdTrans = selectedTranslationUnitApplication.TranslationElement.ToList().Find(x => x.RefId == comObjectParBlock.Id);
                if (ParaRefRef_RefIdTrans != null) // in Translations gefunden
                {
                    foreach (var translationItem in ParaRefRef_RefIdTrans.Translation)
                    {
                        if (translationItem.AttributeName == "Text")
                        {
                            parBlockChild = parentNode.Nodes.Add(translationItem.Text);
                        }
                    }
                }
            }
            if ((parBlockChild != null) && (parBlockChild.Text == "")) // falls eine Übersetzung gefunden wurde, die aber leer ist
            {
                parBlockChild.Text = comObjectParBlock.Name;
            }
            if (parBlockChild == null)
            {
                parBlockChild = parentNode.Nodes.Add("ParameterBlock unter when ohne ID");
            }

            return parBlockChild;
        }
        */
        void ExchangeComObjRefRefIdInComObjectRefRef(ComObjectRefRef_t comObjectRefRefItem, string oldId, string newId)
        {
            //ComObjectRef_t comObjectsRef = selectedApplicationProgram.Static.ComObjectRefs.ToList().Find(x => x.Id == comObjectRefRefItem.RefId);

            if (comObjectRefRefItem.RefId == oldId)
            {
                comObjectRefRefItem.RefId = newId;
            }
        }


        void ExchangeComObjRefRefIdInComObjectChoose(ComObjectParameterChoose_t COParChooseItem, string oldId, string newId)
        {
            //var childNode = parentNode.Nodes.Add("ComObject Choose ParamRedId: " + COParChooseItem.ParamRefId);
            //var childNode = ResolveParameterRefRef(COParChooseItem, parentNode); //ParamRefId
            foreach (ComObjectParameterChoose_tWhen ChooseWhenItem in COParChooseItem.when)    //choose durchsuchen
            {
                ExchangeComObjRefRefIdInComObjectChooseWhen(COParChooseItem, ChooseWhenItem, oldId, newId);
            }
        }

        void ExchangeComObjRefRefIdInComObjectChooseWhen(ComObjectParameterChoose_t COParChooseItem, ComObjectParameterChoose_tWhen ChooseWhenItem, string oldId, string newId)
        {
            //string whenText = ResolveWhenText(COParChooseItem.ParamRefId, ChooseWhenItem.test, ChooseWhenItem.@default);

            if (ChooseWhenItem.Items != null)
            {
                foreach (var whenItem in ChooseWhenItem.Items)    //when durchsuchen
                {
                    if (whenItem is ParameterRefRef_t)
                    {
                        //ResolveParameterRefRef((whenItem as ParameterRefRef_t), grandChildNode); //RefId
                    }
                    else if (whenItem is ComObjectParameterChoose_t)
                    {
                        ExchangeComObjRefRefIdInComObjectChoose(whenItem as ComObjectParameterChoose_t, oldId, newId); //Rekursive Auflösung der choose-when Bäume
                    }
                    else if (whenItem is ComObjectParameterBlock_t)
                    {
                        ExchangeComObjRefRefIdInParameterBlock(whenItem as ComObjectParameterBlock_t, oldId, newId);
                        //var childChildNode = grandChildNode.Nodes.Add("ComObjectParameterBlock Id: " + (whenItem as ComObjectParameterBlock_t).Id);
                        //childChildNode.Tag = whenItem;
                    }
                    else if (whenItem is ParameterSeparator_t)
                    {

                    }
                    else if (whenItem is Assign_t)
                    {
                        //ResolveParameterRefRef((whenItem as Assign_t), grandChildNode);
                    }
                    else if (whenItem is ComObjectRefRef_t)
                    {
                        ExchangeComObjRefRefIdInComObjectRefRef((whenItem as ComObjectRefRef_t), oldId, newId);
                    }
                    else
                    {
                        MessageBox.Show("Unter when konnte in einem ComObject ein Item nicht zugeordnet werden. Das Item hat den Typ " + whenItem.GetType().ToString(), "ComObject when Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
