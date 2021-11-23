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
    public partial class ParameterTypesGui : UserControl
    {
        private static ParameterTypesGui mParameterTypesGui;

        public ParameterTypesGui()
        {
            InitializeComponent();
            mParameterTypesGui = this;
        }

        /*****************************************************************************************************************/
        // ParameterTypes View

        public static void InitializeParameterTypeTreeView()
        {
            mParameterTypesGui.ParaTypesTreeView.Nodes.Clear();

            foreach (ParameterType_t parameterType in ApplicationProgramGui.selectedApplicationProgram.Static.ParameterTypes)
            {
                TreeNode node = mParameterTypesGui.ParaTypesTreeView.Nodes.Add(parameterType.Name);
                node.Tag = parameterType;
            }
        }

        private TreeNode lastSelectedParaTypesTreeViewNode;
        private void ParaTypesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Highlight new node
            e.Node.BackColor = SystemColors.Highlight;
            e.Node.ForeColor = SystemColors.HighlightText;
            if (lastSelectedParaTypesTreeViewNode != null)
            {
                // Set colors to normal for old node
                lastSelectedParaTypesTreeViewNode.BackColor = SystemColors.Window;
                lastSelectedParaTypesTreeViewNode.ForeColor = SystemColors.WindowText;
            }
            lastSelectedParaTypesTreeViewNode = e.Node;


            //RemoveAllRarameterTypesTabs();
            if (e.Node.Tag is ParameterType_t)
            {
                FillParameterTypePage(e.Node.Tag as ParameterType_t);
            }
            else
            {
                ParameterTypesErrorMessage(e.Node.Tag);
            }
        }

        private void RemoveAllRarameterTypesTabs()
        {
            while (tabControlParameterTypes.TabPages.Count > 0)
            {
                tabControlParameterTypes.TabPages.Clear();
            }
        }

        private void ParameterTypesErrorMessage(object errorobject)
        {
            MessageBox.Show("Dieser ParameterType wird bisher nicht unterstützt. Das Object hat den Typ " + errorobject.GetType(), "ParameterType Fehler",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void FillParameterTypePage(ParameterType_t parameterType)
        {
            string typename = parameterType.Item.GetType().Name;
            if (typename == "Object") // kein Item vorhanden
            {
                comboBoxParTypeItem.SelectedItem = "kein Item";
            }
            else
            {
                comboBoxParTypeItem.SelectedItem = typename.Replace("ParameterType_tType", "");
            }


            textParTypeId.Text = parameterType.Id;
            textParTypeName.Text = parameterType.Name;
            textParTypeInternalDescription.Text = parameterType.InternalDescription;
            textParTypePlugin.Text = parameterType.Plugin;
            textParTypeValidationErrorRef.Text = parameterType.ValidationErrorRef;


            if (parameterType.Item is ParameterType_tTypeRestriction)
            {
                FillTypeRestrictionPage(parameterType.Item as ParameterType_tTypeRestriction);
            }
            else if (parameterType.Item is ParameterType_tTypeRestrictionEnumeration)
            {
                FillEnumerationPage(parameterType.Item as ParameterType_tTypeRestrictionEnumeration);
            }
            else if (parameterType.Item is ParameterType_tTypeNumber)
            {
                FillTypeNumberPage(parameterType.Item as ParameterType_tTypeNumber);
            }
            else if (parameterType.Item is ParameterType_tTypeFloat)
            {
                FillTypeFloatPage(parameterType.Item as ParameterType_tTypeFloat);
            }
            else if (parameterType.Item is ParameterType_tTypeText)
            {
                FillTypeTextPage(parameterType.Item as ParameterType_tTypeText);
            }
            else if (parameterType.Item is object)
            {
                RemoveAllRarameterTypesTabs();
            }
            else
            {
                ParameterTypesErrorMessage(parameterType.Item);
            }
        }

        private void FillTypeRestrictionPage(ParameterType_tTypeRestriction typeRestriction)
        {
            while (treeViewTypeResEnum.Nodes.Count > 0)
            {
                treeViewTypeResEnum.Nodes.RemoveAt(0);
            }

            groupBoxEnumItem.Visible = false;

            //tabControlParameterTypes.TabPages.Add(this.tabTypeRestriction);
            comboBoxTypeResBase.SelectedItem = HandleKnxDataTypes.ReadKNXType(typeRestriction.Base);
            numericTypeResSizeInBit.Value = typeRestriction.SizeInBit;
            checkBoxTypeResuIHintSpecified.Checked = typeRestriction.UIHintSpecified;
            comboBoxTypeResuIHint.SelectedItem = HandleKnxDataTypes.ReadKNXType(typeRestriction.UIHint);

            //Füllen der Liste der Enumeration Objekte
            if (typeRestriction.Enumeration != null)
            {
                foreach (ParameterType_tTypeRestrictionEnumeration typeRestrictionItem in typeRestriction.Enumeration)
                {
                    var transElement = ApplicationProgramGui.selectedTranslationUnitApplication.TranslationElement.ToList().Find(x => x.RefId == typeRestrictionItem.Id);
                    if (transElement != null) // in Translations gefunden
                    {
                        foreach (var translationItem in transElement.Translation)
                        {
                            if (translationItem.AttributeName == "Text")
                            {
                                if (translationItem.Text == "") // wenn kein Text in der Übersetung vorhanden, dann die ID für den TreeView verwenden
                                {
                                    var treeNode = treeViewTypeResEnum.Nodes.Add(typeRestrictionItem.Id);
                                    treeNode.Tag = typeRestrictionItem;
                                }
                                else
                                {
                                    var treeNode = treeViewTypeResEnum.Nodes.Add(translationItem.Text);
                                    treeNode.Tag = typeRestrictionItem;
                                }
                            }
                        }
                    }
                    else //falls keine Translation gefunden wurde
                    {
                        var treeNode = treeViewTypeResEnum.Nodes.Add(typeRestrictionItem.Id);
                        treeNode.Tag = typeRestrictionItem;
                    }
                }
            }
        }

        // Auswahl in Enumeration TreeView im Restriction Parametertype auswerten
        private TreeNode lastSelectedTreeViewTypeResEnumNode;
        private void treeViewTypeResEnum_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Highlight new node
            e.Node.BackColor = SystemColors.Highlight;
            e.Node.ForeColor = SystemColors.HighlightText;
            if (lastSelectedTreeViewTypeResEnumNode != null)
            {
                // Set colors to normal for old node
                lastSelectedTreeViewTypeResEnumNode.BackColor = SystemColors.Window;
                lastSelectedTreeViewTypeResEnumNode.ForeColor = SystemColors.WindowText;
            }
            lastSelectedTreeViewTypeResEnumNode = e.Node;

            FillEnumerationPage(e.Node.Tag as ParameterType_tTypeRestrictionEnumeration);
        }

        private void FillEnumerationPage(ParameterType_tTypeRestrictionEnumeration enumeration)
        {
            groupBoxEnumItem.Visible = true;
            textEnumText.Text = enumeration.Text;
            textEnumIcon.Text = enumeration.Icon;
            comboBoxEnumPictureAlignment.SelectedItem = HandleKnxDataTypes.ReadKNXType(enumeration.PictureAlignment);
            numericEnumValue.Value = enumeration.Value;
            textEnumId.Text = enumeration.Id;
            checkBoxEnumDisplayOrderSpecified.Checked = enumeration.DisplayOrderSpecified;
            numericEnumDisplayOrder.Value = enumeration.DisplayOrder;
            HandleKnxDataTypes.ReadKNXType(listEnumBinaryValue, enumeration.BinaryValue);
            dgvParaTypeTypeResEnumItemTranslations.Rows.Clear();
            LanguageProcessing.FillLanguageDataGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvParaTypeTypeResEnumItemTranslations, enumeration.Id, "Text");
        }

        private void FillTypeNumberPage(ParameterType_tTypeNumber typeNumber)
        {
            //tabControlParameterTypes.TabPages.Add(this.tabTypeNumber);
            numericTypeNumSizeInBit.Value = typeNumber.SizeInBit;
            comboBoxTypeNumType.SelectedItem = HandleKnxDataTypes.ReadKNXType(typeNumber.Type);
            numericTypeNumMinInclusive.Value = typeNumber.minInclusive;
            numericTypeNumMaxInclusive.Value = typeNumber.maxInclusive;
            numericTypeNumIncrement.Value = typeNumber.Increment;
            checkBoxTypeNumuIHintSpecified.Checked = typeNumber.UIHintSpecified;
            comboBoxTypeNumuIHint.SelectedItem = HandleKnxDataTypes.ReadKNXType(typeNumber.UIHint);
            checkBoxTypeNumDisplayOffsetSpecified.Checked = typeNumber.DisplayOffsetSpecified;
            numericTypeNumDisplayOffset.Value = (decimal)typeNumber.DisplayOffset;
            checkBoxTypeNumDisplayFactorSpecified.Checked = typeNumber.DisplayFactorSpecified;
            numericTypeNumDisplayFactor.Value = (decimal)typeNumber.DisplayFactor;
        }

        private void FillTypeFloatPage(ParameterType_tTypeFloat typeFloat)
        {
            //tabControlParameterTypes.TabPages.Add(this.tabTypeFloat);
            comboBoxTypeFloatEncoding.SelectedItem = HandleKnxDataTypes.ReadKNXType(typeFloat.Encoding);
            numericTypeFloatMinInclusive.Value = (decimal)typeFloat.minInclusive;
            numericTypeFloatMaxInclusive.Value = (decimal)typeFloat.maxInclusive;
            checkBoxTypeFloatIncrementSpecified.Checked = typeFloat.IncrementSpecified;
            numericTypeFloatIncrement.Value = (decimal)typeFloat.Increment;
            checkBoxTypeFloatuIHintSpecified.Checked = typeFloat.UIHintSpecified;
            comboBoxTypeFloatuIHint.SelectedItem = HandleKnxDataTypes.ReadKNXType(typeFloat.UIHint);
            checkBoxTypeFloatDisplayOffsetSpecified.Checked = typeFloat.DisplayOffsetSpecified;
            numericTypeFloatDisplayOffset.Value = (decimal)typeFloat.DisplayOffset;
            checkBoxTypeFloatDisplayFactorSpecified.Checked = typeFloat.DisplayFactorSpecified;
            numericTypeFloatDisplayFactor.Value = (decimal)typeFloat.DisplayFactor;
        }

        private void FillTypeTextPage(ParameterType_tTypeText typeText)
        {
            numericTypeTextSizeInBit.Value = typeText.SizeInBit;
            textTypeTextPattern.Text = typeText.Pattern;
        }


        /*****************************************************************************************************************/
        // ParameterTypes Edit

        /// <summary>
        /// Für die ParameterTypes Übersicht das contextMenu entsprechend der Position des Mauszeigers anpassen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParaTypesTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            ContextMenuHandler.contextMenuLastClickedTreeView = ParaTypesTreeView;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuHandler.ShowContextMenuAtMousePosition();
                //TreeNode clickedNode = ParaTypesTreeView.GetNodeAt(e.X, e.Y);
                ParaTypesTreeView.SelectedNode = ParaTypesTreeView.GetNodeAt(e.X, e.Y);

                ContextMenuHandler.SetToolStripMenuAddVisible(true);
                ContextMenuHandler.SettoolStripMenuPasteVisible(false);
                if (ParaTypesTreeView.SelectedNode == null)
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
        /// Vorbereiten für die Eingabe eines neuen Parametertyps
        /// </summary>
        public static void AddParameterType()
        {
            mParameterTypesGui.ClearParaTypeMainPage();
            mParameterTypesGui.ClearParaTypeTypeResPage();
            mParameterTypesGui.ClearParaTypeEnumPage();
            mParameterTypesGui.ClearParaTypeNumberPage();
            mParameterTypesGui.ClearParaTypeFloatPage();
            mParameterTypesGui.ClearParaTypeTextPage();

            // Set colors to normal for old node
            if (mParameterTypesGui.lastSelectedParaTypesTreeViewNode != null)
            {
                mParameterTypesGui.lastSelectedParaTypesTreeViewNode.BackColor = SystemColors.Window;
                mParameterTypesGui.lastSelectedParaTypesTreeViewNode.ForeColor = SystemColors.WindowText;
                mParameterTypesGui.lastSelectedTreeViewTypeResEnumNode = null;
            }

            //deselect existing Enum Item
            mParameterTypesGui.ParaTypesTreeView.SelectedNode = null;
        }

        /// <summary>
        /// Zum Kopieren eines ParameterTypes wird das kopierte Element im TreeNode deselektiert
        /// Dadurch kann der ParameterType als neues Objekt gespeichert werden
        /// </summary>
        public static void CopyParameterType()
        {
            // Set colors to normal for old node
            if (mParameterTypesGui.lastSelectedParaTypesTreeViewNode != null)
            {
                mParameterTypesGui.lastSelectedParaTypesTreeViewNode.BackColor = SystemColors.Window;
                mParameterTypesGui.lastSelectedParaTypesTreeViewNode.ForeColor = SystemColors.WindowText;
                mParameterTypesGui.lastSelectedTreeViewTypeResEnumNode = null;
            }

            //deselect existing Enum Item
            mParameterTypesGui.ParaTypesTreeView.SelectedNode = null;

            // delete old Id
            mParameterTypesGui.textParTypeId.Text = "";
        }

        public static void DeleteParameterType()
        {
            DialogResult result = MessageBox.Show("Soll wirklich der ParameterType gelöscht werden? Die Löschung wirkt sich auf alle Parameter aus, die diesen ParameterType verwenden!", "ParameterType Löschung Sicherheitsabfrage", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            string ParaTypeId = (mParameterTypesGui.ParaTypesTreeView.SelectedNode.Tag as ParameterType_t).Id; // zum Merken der ID

            // wenn der Parametertype eine Restriction ist, müssen die Translations Daten der Restriction Items vorher gelöscht werden
            if ((mParameterTypesGui.ParaTypesTreeView.SelectedNode.Tag as ParameterType_t).Item is ParameterType_tTypeRestriction)
            {
                if (((mParameterTypesGui.ParaTypesTreeView.SelectedNode.Tag as ParameterType_t).Item as ParameterType_tTypeRestriction).Enumeration != null)
                {
                    foreach (ParameterType_tTypeRestrictionEnumeration enumItem in ((mParameterTypesGui.ParaTypesTreeView.SelectedNode.Tag as ParameterType_t).Item as ParameterType_tTypeRestriction).Enumeration)
                    {
                        LanguageProcessing.DeleteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, enumItem.Id);
                    }
                }
            }

            ApplicationProgramGui.selectedApplicationProgram.Static.ParameterTypes = TreeViewArrayFunctions.DeleteArrayTreeView(ApplicationProgramGui.selectedApplicationProgram.Static.ParameterTypes, mParameterTypesGui.ParaTypesTreeView.SelectedNode.Index, mParameterTypesGui.ParaTypesTreeView);
            mParameterTypesGui.ChangeParameterTypeId(ParaTypeId, ""); // die ID des ParameterTypes aus den Parameters löschen
        }

        /// <summary>
        /// Der ParameterType Typ wurde verändert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxParTypeItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveAllRarameterTypesTabs();
            switch (comboBoxParTypeItem.SelectedItem)
            {
                case "Restriction":
                    tabControlParameterTypes.TabPages.Add(this.tabTypeRestriction);
                    ClearParaTypeTypeResPage();
                    ClearParaTypeEnumPage();
                    break;
                case "Number":
                    tabControlParameterTypes.TabPages.Add(this.tabTypeNumber);
                    ClearParaTypeNumberPage();
                    break;
                case "Float":
                    tabControlParameterTypes.TabPages.Add(this.tabTypeFloat);
                    ClearParaTypeFloatPage();
                    break;
                case "Text":
                    tabControlParameterTypes.TabPages.Add(this.tabTypeText);
                    ClearParaTypeTextPage();
                    break;
                case "kein Item":
                    break;
            }
        }

        /// <summary>
        /// Button "speichern" für einen neuen Parametertypen
        /// </summary>
        private void buttonSaveParaType_Click(object sender, EventArgs e)
        {
            // prüfen, ob der Name bereits verwendet wurde: ja = abbruch
            // Name = Eindeutigkeitsmerkmal für den Parameter
            foreach (TreeNode parNode in ParaTypesTreeView.Nodes)
            {
                if (parNode != ParaTypesTreeView.SelectedNode)
                {
                    if ((parNode.Tag as ParameterType_t).Name == textParTypeName.Text)
                    {
                        MessageBox.Show("Es muss für jedes Element ein unterschiedlicher Name verwendet werden. Fehler für Name " + textParTypeName.Text, "ParameterType Name Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            ParameterType_t newParType = new ParameterType_t();
            if (ParaTypesTreeView.SelectedNode != null) // ein bestehender ParameterType Eintrag wurde verändert
            {
                newParType = ParaTypesTreeView.SelectedNode.Tag as ParameterType_t;  // Tag ist Typ ParameterType_t
            }
            else // ein neuer Eintrag wurde erzeugt
            {
                ApplicationProgramGui.selectedApplicationProgram.Static.ParameterTypes = HandleArrayFunctions.Add(ApplicationProgramGui.selectedApplicationProgram.Static.ParameterTypes, newParType);
            }

            // Die ID eine ParameterTypes setzt sich aus ApplicationProgram Id, _PT und Name des Parametertyps zusammen
            newParType.Id = ApplicationProgramGui.selectedApplicationProgram.Id + "_PT-" + HandleParNames.ExchangeNameToId(textParTypeName.Text);
            newParType.Name = Extensions.NullIfEmpty(textParTypeName.Text);
            newParType.InternalDescription = Extensions.NullIfEmpty(textParTypeInternalDescription.Text);
            newParType.Plugin = Extensions.NullIfEmpty(textParTypePlugin.Text);
            newParType.ValidationErrorRef = Extensions.NullIfEmpty(textParTypeValidationErrorRef.Text);

            if (newParType.Id != textParTypeId.Text) // die ID wurde verändert
            {
                ChangeParameterTypeId(textParTypeId.Text, newParType.Id);
            }

            switch (comboBoxParTypeItem.SelectedItem.ToString())
            {
                case "Restriction":
                    ParameterType_tTypeRestriction newParTypeTypeRes = new ParameterType_tTypeRestriction();
                    if (newParType.Item == null)
                    {
                        newParTypeTypeRes.Enumeration = new ParameterType_tTypeRestrictionEnumeration[0];
                    }
                    else
                    {
                        newParTypeTypeRes.Enumeration = (newParType.Item as ParameterType_tTypeRestriction).Enumeration;
                    }
                    newParType.Item = newParTypeTypeRes;
                    newParTypeTypeRes.Base = HandleGuiDataTypes.ReadTypeResBase(comboBoxTypeResBase.SelectedItem.ToString());
                    newParTypeTypeRes.SizeInBit = (uint)numericTypeResSizeInBit.Value;
                    newParTypeTypeRes.UIHintSpecified = checkBoxTypeResuIHintSpecified.Checked;
                    newParTypeTypeRes.UIHint = HandleGuiDataTypes.ReadTypeResUIHint(comboBoxTypeResuIHint.SelectedItem.ToString());
                    break;
                case "Number":
                    ParameterType_tTypeNumber newParTypeTypeNum = new ParameterType_tTypeNumber();
                    newParType.Item = newParTypeTypeNum;
                    newParTypeTypeNum.SizeInBit = (uint)numericTypeNumSizeInBit.Value;
                    newParTypeTypeNum.Type = HandleGuiDataTypes.ReadTypeNumberType(comboBoxTypeNumType.SelectedItem.ToString());
                    newParTypeTypeNum.minInclusive = (long)numericTypeNumMinInclusive.Value;
                    newParTypeTypeNum.maxInclusive = (long)numericTypeNumMaxInclusive.Value;
                    newParTypeTypeNum.Increment = (long)numericTypeNumIncrement.Value;
                    newParTypeTypeNum.UIHintSpecified = checkBoxTypeNumuIHintSpecified.Checked;
                    newParTypeTypeNum.UIHint = HandleGuiDataTypes.ReadTypeNumberUIHint(comboBoxTypeNumuIHint.SelectedItem.ToString());
                    newParTypeTypeNum.DisplayOffsetSpecified = checkBoxTypeNumDisplayOffsetSpecified.Checked;
                    newParTypeTypeNum.DisplayOffset = (double)numericTypeNumDisplayOffset.Value;
                    newParTypeTypeNum.DisplayFactorSpecified = checkBoxTypeNumDisplayFactorSpecified.Checked;
                    newParTypeTypeNum.DisplayFactor = (double)numericTypeNumDisplayFactor.Value;
                    break;
                case "Float":
                    ParameterType_tTypeFloat newParTypeTypeFloat = new ParameterType_tTypeFloat();
                    newParType.Item = newParTypeTypeFloat;
                    newParTypeTypeFloat.Encoding = HandleGuiDataTypes.ReadTypeFloatEncoding(comboBoxTypeFloatEncoding.SelectedItem.ToString());
                    newParTypeTypeFloat.minInclusive = (double)numericTypeFloatMinInclusive.Value;
                    newParTypeTypeFloat.maxInclusive = (double)numericTypeFloatMaxInclusive.Value;
                    newParTypeTypeFloat.IncrementSpecified = checkBoxTypeFloatIncrementSpecified.Checked;
                    newParTypeTypeFloat.Increment = (double)numericTypeFloatIncrement.Value;
                    newParTypeTypeFloat.UIHintSpecified = checkBoxTypeFloatuIHintSpecified.Checked;
                    newParTypeTypeFloat.UIHint = HandleGuiDataTypes.ReadTypeFloatUIHint(comboBoxTypeFloatuIHint.SelectedItem.ToString());
                    newParTypeTypeFloat.DisplayFormat = textTypeFloatDisplayFormat.Text;
                    newParTypeTypeFloat.DisplayOffsetSpecified = checkBoxTypeFloatDisplayOffsetSpecified.Checked;
                    newParTypeTypeFloat.DisplayOffset = (double)numericTypeFloatDisplayOffset.Value;
                    newParTypeTypeFloat.DisplayFactorSpecified = checkBoxTypeFloatDisplayFactorSpecified.Checked;
                    newParTypeTypeFloat.DisplayFactor = (double)numericTypeFloatDisplayFactor.Value;
                    break;
                case "Text":
                    ParameterType_tTypeText newParTypeTypeText = new ParameterType_tTypeText();
                    newParType.Item = newParTypeTypeText;
                    newParTypeTypeText.SizeInBit = (uint)numericTypeTextSizeInBit.Value;
                    newParTypeTypeText.Pattern = Extensions.NullIfEmpty(textTypeTextPattern.Text);
                    break;
                case "kein Item":
                    object newObject = new object();
                    newParType.Item = newObject;
                    break;
            }
            RefillParaTypesTreeView();
        }

        /// <summary>
        /// den ParaTypesTreeView neu befüllen und das vorherige Element wieder selektieren
        /// </summary>
        private void RefillParaTypesTreeView()
        {
            int index;
            if (ParaTypesTreeView.SelectedNode != null) // alter Eintrag wurde bearbeitet
            {
                index = ParaTypesTreeView.SelectedNode.Index; //den alten Index ablegen, um nach der neuen Befüllung des Treeviews ihn wieder zu aktivieren
            }
            else // neuer Eintrag wurde hinzugefügt
            {
                index = ParaTypesTreeView.Nodes.Count; // neuer Index ist die alte Anzahl von TreeView Elementen
            }
            InitializeParameterTypeTreeView();
            if (index >= ParaTypesTreeView.Nodes.Count)
            {
                index = ParaTypesTreeView.Nodes.Count - 1;
            }
            ParaTypesTreeView.SelectedNode = ParaTypesTreeView.Nodes[index];
        }

        /// <summary>
        /// Duchsucht die Sektion Parameters und ersetzt die gegebene alte ID durch die neue ID
        /// </summary>
        /// <param name="oldId">zu ersetzende ParameterType ID</param>
        /// <param name="newId">neue ParameterType ID</param>
        private void ChangeParameterTypeId(string oldId, string newId)
        {
            foreach (object ParOrUinion in ApplicationProgramGui.selectedApplicationProgram.Static.Parameters)
            {
                if (ParOrUinion is ApplicationProgramStatic_tParameter)
                {
                    if ((ParOrUinion as ApplicationProgramStatic_tParameter).ParameterType == oldId)
                    {
                        (ParOrUinion as ApplicationProgramStatic_tParameter).ParameterType = newId;
                    }
                }
                else if (ParOrUinion is ApplicationProgramStatic_tUnion)
                {
                    foreach (UnionParameter_t unionPar in (ParOrUinion as ApplicationProgramStatic_tUnion).Parameter)
                    {
                        if (unionPar.ParameterType == oldId)
                        {
                            unionPar.ParameterType = newId;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// der Button "Parameter sortieren" im ParameterTypes Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSortParameterTypes_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Es wird die ParameterTypes Table nach Namen sortiert. Diese Reihenfolge wird auch in den knxprod Daten abgelegt.",
                "ParameterType Sortierung Hinweis", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            SortParameterTypesTable();
        }

        /// <summary>
        /// Sortieren der ParametersTypes Table anhand des Namens
        /// </summary>
        private void SortParameterTypesTable()
        {
            Array.Sort(ApplicationProgramGui.selectedApplicationProgram.Static.ParameterTypes, delegate (ParameterType_t paraType1, ParameterType_t paraType2)
            {
                return paraType1.Name.CompareTo(paraType2.Name);
            });

            RefillParameterTypeTreeView();
        }

        // <summary>
        /// Die Auflistung der ParameterTypes wird neu aus den Daten gefüllt und der bisher markierte Eintrag wird wieder gewählt
        /// </summary>
        private void RefillParameterTypeTreeView()
        {
            int index = 0;
            ParameterType_t parameterTypeMem = null;
            if (ParameterGui.GetSelectedParameterTreeViewNode() != null)
            {
                //den bisher gewählten Parameter ablegen, um ihn nach der neuen Befüllung des Treeviews wieder zu aktivieren
                parameterTypeMem = ParaTypesTreeView.SelectedNode.Tag as ParameterType_t;
            }
            else
            {
                index = ParaTypesTreeView.Nodes.Count; // neuer Index ist die alte Anzahl von TreeView Elementen
            }

            // neu befüllen des TreeView
            InitializeParameterTypeTreeView();

            if (parameterTypeMem != null)
            {
                foreach (TreeNode parameterTypeTreeNode in ParaTypesTreeView.Nodes)
                {
                    if (parameterTypeTreeNode.Tag == parameterTypeMem)
                    {
                        ParaTypesTreeView.SelectedNode = parameterTypeTreeNode;
                        break;
                    }
                }
            }
            else
            {
                if (index >= ParaTypesTreeView.Nodes.Count)
                {
                    index = ParaTypesTreeView.Nodes.Count - 1;
                }
                ParaTypesTreeView.SelectedNode = ParaTypesTreeView.Nodes[index];
            }
        }


        // Ab hier nur für Enumeration Items
        /*******************************************************************************************************************************/

        ///<summary>
        /// Für die Enumeration Übersicht das contextMenu entsprechend der Position des Mauszeigers anpassen
        /// </summary>
        private void treeViewTypeResEnum_MouseDown(object sender, MouseEventArgs e)
        {
            ContextMenuHandler.contextMenuLastClickedTreeView = treeViewTypeResEnum;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuHandler.ShowContextMenuAtMousePosition();
                //TreeNode clickedNode = ParaTypesTreeView.GetNodeAt(e.X, e.Y);
                treeViewTypeResEnum.SelectedNode = treeViewTypeResEnum.GetNodeAt(e.X, e.Y);

                ContextMenuHandler.SetToolStripMenuAddVisible(true);
                ContextMenuHandler.SettoolStripMenuPasteVisible(false);
                if (treeViewTypeResEnum.SelectedNode == null)
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
        /// Vorbereiten für die Eingabe eines neuen Enumeration Items
        /// </summary>
        public static void AddEnumItem()
        {
            if (mParameterTypesGui.ParaTypesTreeView.SelectedNode == null)
            {
                MessageBox.Show("Bitte zuerst den ParameterTypen speichern", "ParameterType Seicherfehler",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mParameterTypesGui.ClearParaTypeEnumPage();
            mParameterTypesGui.groupBoxEnumItem.Visible = true;
            // Set colors to normal for old node
            if (mParameterTypesGui.lastSelectedTreeViewTypeResEnumNode != null)
            {
                mParameterTypesGui.lastSelectedTreeViewTypeResEnumNode.BackColor = SystemColors.Window;
                mParameterTypesGui.lastSelectedTreeViewTypeResEnumNode.ForeColor = SystemColors.WindowText;
                mParameterTypesGui.lastSelectedTreeViewTypeResEnumNode = null;
            }

            //deselect existing Enum Item
            mParameterTypesGui.treeViewTypeResEnum.SelectedNode = null;
        }

        /// <summary>
        /// Zum Kopieren eines EnumItems wird das kopierte Element im TreeNode deselektiert
        /// Dadurch kann das Enum Item als neues Objekt gespeichert werden
        /// </summary>
        public static void CopyEnumItem()
        {
            // Set colors to normal for old node
            if (mParameterTypesGui.lastSelectedTreeViewTypeResEnumNode != null)
            {
                mParameterTypesGui.lastSelectedTreeViewTypeResEnumNode.BackColor = SystemColors.Window;
                mParameterTypesGui.lastSelectedTreeViewTypeResEnumNode.ForeColor = SystemColors.WindowText;
                mParameterTypesGui.lastSelectedTreeViewTypeResEnumNode = null;
            }

            //deselect existing Enum Item
            mParameterTypesGui.treeViewTypeResEnum.SelectedNode = null;

            // delete old Id
            mParameterTypesGui.textEnumId.Text = "";
        }

        public static void DeleteEnumItem()
        {
            ((mParameterTypesGui.ParaTypesTreeView.SelectedNode.Tag as ParameterType_t).Item as ParameterType_tTypeRestriction).Enumeration =
                HandleArrayFunctions.Delete(((mParameterTypesGui.ParaTypesTreeView.SelectedNode.Tag as ParameterType_t).Item as ParameterType_tTypeRestriction).Enumeration, mParameterTypesGui.treeViewTypeResEnum.SelectedNode.Index);
            LanguageProcessing.DeleteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, (mParameterTypesGui.treeViewTypeResEnum.SelectedNode.Tag as ParameterType_tTypeRestrictionEnumeration).Id);
            mParameterTypesGui.RefillTreeViewTypeResEnum();
        }


        /*
         * Button "speichern" im Enumeration Item Feld
         */
        private void buttonParTypeEnumSave_Click(object sender, EventArgs e)
        {
            // prüfen, ob Value bereits verwendet wurde: ja = abbruch
            // Value = Eindeutigkeitsmerkmal für ID
            foreach (TreeNode enumNode in treeViewTypeResEnum.Nodes)
            {
                if (enumNode != treeViewTypeResEnum.SelectedNode)
                {
                    if ((enumNode.Tag as ParameterType_tTypeRestrictionEnumeration).Value == (int)numericEnumValue.Value)
                    {
                        MessageBox.Show("Es muss für jedes Element eine unterschiedliche Value verwendet werden. Fehler für Value " + (int)numericEnumValue.Value, "Enum Value Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            ParameterType_tTypeRestrictionEnumeration newEnumItem = new ParameterType_tTypeRestrictionEnumeration();
            if (treeViewTypeResEnum.SelectedNode != null) // ein bestehender Enumeration Eintrag wurde verändert
            {
                newEnumItem = treeViewTypeResEnum.SelectedNode.Tag as ParameterType_tTypeRestrictionEnumeration;  // Tag ist Typ ParameterType_tTypeRestrictionEnumeration
            }
            else // ein neuer Eintrag wurde erzeugt
            {
                ((ParaTypesTreeView.SelectedNode.Tag as ParameterType_t).Item as ParameterType_tTypeRestriction).Enumeration = HandleArrayFunctions.Add(((ParaTypesTreeView.SelectedNode.Tag as ParameterType_t).Item as ParameterType_tTypeRestriction).Enumeration, newEnumItem);
            }

            newEnumItem.Text = Extensions.NullIfEmpty(textEnumText.Text);
            newEnumItem.Icon = Extensions.NullIfEmpty(textEnumIcon.Text);
            newEnumItem.PictureAlignment = HandleGuiDataTypes.ReadPictureAlignment(comboBoxEnumPictureAlignment.SelectedItem.ToString());
            newEnumItem.Value = (uint)numericEnumValue.Value;
            newEnumItem.DisplayOrderSpecified = checkBoxEnumDisplayOrderSpecified.Checked;
            newEnumItem.DisplayOrder = (int)numericEnumDisplayOrder.Value;
            newEnumItem.BinaryValue = HandleGuiDataTypes.ReadGuiType(listEnumBinaryValue);


            // Die ID der Enumeration setzt sich aus ApplicationProgram Id, _PT, ParameterType Name, _EN- und der Value des Enumeration Items zusammen            
            newEnumItem.Id = ApplicationProgramGui.selectedApplicationProgram.Id.ToString() + "_PT-" + HandleParNames.ExchangeNameToId((ParaTypesTreeView.SelectedNode.Tag as ParameterType_t).Name) + "_EN-" + newEnumItem.Value;

            // bevor die ID geändert wird muss erst auch die Translation ID angepasst werden!
            // wenn die ID verändert wurde, aber nur, wenn kein neues Element erstellt wurde
            if ((newEnumItem.Id != textEnumId.Text) && (treeViewTypeResEnum.SelectedNode != null))
            {
                LanguageProcessing.ChangeTansElementRefId(ApplicationProgramGui.selectedApplicationManufacturer.Languages, textEnumId.Text, newEnumItem.Id);
            }

            // die Übersetzungen in die Translations schreiben
            LanguageProcessing.WriteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvParaTypeTypeResEnumItemTranslations, newEnumItem.Id, "Text", ApplicationProgramGui.selectedApplicationProgram.Id);

            RefillTreeViewTypeResEnum();
            textEnumId.Text = newEnumItem.Id;
        }

        /// <summary>
        /// den RefillTreeViewTypeResEnum neu befüllen und das vorherige Element wieder selektieren
        /// </summary>
        private void RefillTreeViewTypeResEnum()
        {
            int index;
            if (treeViewTypeResEnum.SelectedNode != null) // alter Eintrag wurde bearbeitet
            {
                index = treeViewTypeResEnum.SelectedNode.Index; //den alten Index ablegen, um nach der neuen Befüllung des Treeviews ihn wieder zu aktivieren
            }
            else // neuer Eintrag wurde hinzugefügt
            {
                index = treeViewTypeResEnum.Nodes.Count; // neuer Index ist die alte Anzahl von TreeView Elementen
            }
            FillTypeRestrictionPage((ParaTypesTreeView.SelectedNode.Tag as ParameterType_t).Item as ParameterType_tTypeRestriction);
            if (index >= treeViewTypeResEnum.Nodes.Count && index != 0)
            {
                index = treeViewTypeResEnum.Nodes.Count - 1;
            }
            if (index >= 0 && treeViewTypeResEnum.Nodes.Count > 0)
            {
                treeViewTypeResEnum.SelectedNode = treeViewTypeResEnum.Nodes[index];
            }
            groupBoxEnumItem.Visible = true;
        }


        public static ref TreeView GetParaTypesTreeView()
        {
            return ref mParameterTypesGui.ParaTypesTreeView;
        }

        public static ref TreeView GetTypeResEnumTreeView()
        {
            return ref mParameterTypesGui.treeViewTypeResEnum;
        }


        /*
         * löschen der Inhalte der einzelnen Seiten
         */
        private void ClearParaTypeMainPage()
        {
            comboBoxParTypeItem.SelectedIndex = 0;
            textParTypeId.Text = "";
            textParTypeName.Text = "";
            textParTypeInternalDescription.Text = "";
            textParTypePlugin.Text = "";
            textParTypeValidationErrorRef.Text = "";
        }

        private void ClearParaTypeTypeResPage()
        {
            comboBoxTypeResBase.SelectedIndex = 0;
            numericTypeResSizeInBit.Value = 0;
            comboBoxTypeResuIHint.SelectedIndex = 0;
            while (treeViewTypeResEnum.Nodes.Count > 0)
            {
                treeViewTypeResEnum.Nodes.RemoveAt(0);
            }
        }

        private void ClearParaTypeEnumPage()
        {
            groupBoxEnumItem.Visible = false;
            textEnumText.Text = "";
            textEnumIcon.Text = "";
            comboBoxEnumPictureAlignment.SelectedIndex = 0;
            numericEnumValue.Value = 0;
            textEnumId.Text = "";
            numericEnumDisplayOrder.Value = 0;
            listEnumBinaryValue.Items.Clear();
            LanguageProcessing.PrepareLanguageGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvParaTypeTypeResEnumItemTranslations);
        }

        private void ClearParaTypeNumberPage()
        {
            numericTypeNumSizeInBit.Value = 0;
            comboBoxTypeNumType.SelectedIndex = 0;
            numericTypeNumMinInclusive.Value = 0;
            numericTypeNumMaxInclusive.Value = 0;
            numericTypeNumIncrement.Value = 0;
            comboBoxTypeNumuIHint.SelectedIndex = 0;
            numericTypeNumDisplayOffset.Value = 0;
            numericTypeNumDisplayFactor.Value = 0;
        }

        private void ClearParaTypeFloatPage()
        {
            comboBoxTypeFloatEncoding.SelectedIndex = 0;
            numericTypeFloatMinInclusive.Value = 0;
            numericTypeFloatMaxInclusive.Value = 0;
            numericTypeFloatIncrement.Value = 0;
            comboBoxTypeFloatuIHint.SelectedIndex = 0;
            numericTypeFloatDisplayOffset.Value = 0;
            numericTypeFloatDisplayFactor.Value = 0;
        }

        private void ClearParaTypeTextPage()
        {
            numericTypeTextSizeInBit.Value = 8;
            textTypeTextPattern.Text = "";
        }
    }
}
