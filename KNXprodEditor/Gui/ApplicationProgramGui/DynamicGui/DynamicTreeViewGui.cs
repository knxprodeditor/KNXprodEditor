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
    public partial class DynamicTreeViewGui : UserControl
    {
        public static DynamicTreeViewGui mDynamicTreeViewGui;

        public DynamicTreeViewGui()
        {
            InitializeComponent();
            mDynamicTreeViewGui = this;
        }

        public static void InitializeParaKoTreeView()
        {
            DynamicTreeViewGenerator.GenerateParaKoTreeView(mDynamicTreeViewGui.ParaKoTreeView);

            // Das erste Element selektieren (Dadurch erfolgt auch Löschung aller Dynamic Tabs)
            if (mDynamicTreeViewGui.ParaKoTreeView.Nodes.Count > 0)
            {
                SetSelectedTreeNode(mDynamicTreeViewGui.ParaKoTreeView.Nodes[0]);
            }
        }

        /*
         * Funktion um alle Elemente des TreeView ein oder aus zu klappen
         */
        private void expandParaKoTreeView_CheckedChanged(object sender, EventArgs e)
        {
            if (expandParaKoTreeView.Checked == true)
            {
                ParaKoTreeView.ExpandAll();
            }
            else
            {
                ParaKoTreeView.CollapseAll();
                ParaKoTreeView.Nodes[0].Expand();
            }

        }

        public void ExpandFirstTwoLayer()
        {
            if (ParaKoTreeView.Nodes.Count > 0)
            {
                ParaKoTreeView.Nodes[0].Expand(); //erste Ebene ausklappen
                if (ParaKoTreeView.Nodes[0].Nodes.Count > 0)
                {
                    ParaKoTreeView.Nodes[0].Nodes[0].Expand(); //zweite Ebene ausklappen
                }
            }
        }

        public void DeleteAllTreeNodes()
        {
            while (ParaKoTreeView.Nodes.Count > 0)
            {
                ParaKoTreeView.Nodes.RemoveAt(0);
            }
        }

        private TreeNode lastSelectedParaKoTreeViewNode;
        private void ParaKoTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Highlight new node
            e.Node.BackColor = SystemColors.Highlight;
            e.Node.ForeColor = SystemColors.HighlightText;
            if (lastSelectedParaKoTreeViewNode != null)
            {
                // Set colors to normal for old node
                lastSelectedParaKoTreeViewNode.BackColor = SystemColors.Window;
                lastSelectedParaKoTreeViewNode.ForeColor = SystemColors.WindowText;
            }
            lastSelectedParaKoTreeViewNode = e.Node;


            DynamicTabControl.RemoveAllParamsCoTabs();
            if (e.Node.Tag is ApplicationProgramChannel_t)
            {
                DynamicTabControl.AddApplicationChannelTab();
                ApplicationChannelGui.FillApplicationProgramChannelPage(e.Node.Tag as ApplicationProgramChannel_t);
            }
            else if (e.Node.Tag is ChannelIndependentBlock_t)
            {
                DynamicTabControl.AddChannelIndependentTab();
            }
            else if (e.Node.Tag is ComObjectParameterBlock_t)
            {
                FillParameterOrUnionPage(ParameterRefGui.FillParameterRefPage(ComObjectParameterBlockGui.FillComObjectParameterBlockPage(e.Node.Tag as ComObjectParameterBlock_t)));
                newOrEditedParameterItem = newOrEditedParameterItems.ParaBlockParameter;
            }
            else if (e.Node.Tag is ParameterRefRef_t)
            {
                FillParameterOrUnionPage(ParameterRefGui.FillParameterRefPage(ParameterRefRefGui.FillParameterRefRefPage(e.Node.Tag as ParameterRefRef_t)));
                newOrEditedParameterItem = newOrEditedParameterItems.ParaOrUnionPara;
            }
            else if (e.Node.Tag is ChannelChoose_t)
            {
                FillParameterOrUnionPage(ParameterRefGui.FillParameterRefPage(ChannelChooseGui.FillChannelChoosePage(e.Node.Tag as ChannelChoose_t)));
                newOrEditedParameterItem = newOrEditedParameterItems.ChannelChooseParameter;
            }
            else if (e.Node.Tag is ComObjectParameterChoose_t)
            {
                FillParameterOrUnionPage(ParameterRefGui.FillParameterRefPage(ComObjectParameterChooseGui.FillComObjectParameterChoose(e.Node.Tag as ComObjectParameterChoose_t)));
                newOrEditedParameterItem = newOrEditedParameterItems.ComObjectParaChooseParameter;
            }
            else if (e.Node.Tag is Assign_t)
            {
                AssignGui.FillAssignPage(e.Node.Tag as Assign_t);
            }
            else if (e.Node.Tag is ChannelChoose_tWhen)
            {
                ChannelChooseWhenGui.FillChannelChooseWhenPage(e.Node.Tag as ChannelChoose_tWhen);
            }
            else if (e.Node.Tag is ComObjectParameterChoose_tWhen)
            {
                ComObjectParameterChooseWhenGui.FillComObjectParameterChooseWhenPage(e.Node.Tag as ComObjectParameterChoose_tWhen);
            }
            else if (e.Node.Tag is ComObjectRefRef_t)
            {
                ComObjectGui.FillComObjectPage(ComObjectRefGui.FillComObjectRefPage(ComObjectRefRefGui.FillComObjectRefRef(e.Node.Tag as ComObjectRefRef_t)));
                DynamicTabControl.SelectComObjectTab();
            }
            else if (e.Node.Tag is ApplicationProgram_t)
            {
                ApplicationGui.FillApplicationPage(e.Node.Tag as ApplicationProgram_t);
            }
            else if (e.Node.Tag is ParameterSeparator_t)
            {
                ParameterSeparatorGui.FillParameterSeparatorPage(e.Node.Tag as ParameterSeparator_t);
            }
            else
            {
                if (e.Node.Tag != null)
                {
                    MessageBox.Show("Datentyp nicht für Detailanzeige vorbereitet. Das Element hat den Typ " + e.Node.Tag.GetType().ToString(), "ComObject when Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Hilfsfunktion zur Unterscheidung von Parameter und Union Page
        /// </summary>
        /// <param name="ParametersItem"></param>
        public static void FillParameterOrUnionPage(object ParametersItem)
        {
            if (ParametersItem is ApplicationProgramStatic_tParameter)
            {
                AppParameterGui.FillParameterPage(ParametersItem as ApplicationProgramStatic_tParameter);
                DynamicTabControl.SelectParameterTab();
            }
            if (ParametersItem is UnionParameter_t)
            {
                UnionParameterGui.FillUnionParameterPage(ParametersItem as UnionParameter_t);
                DynamicTabControl.SelectUnionParameterTab();
            }
        }

        /// <summary>
        /// Funktion um einen beliebigen Mausklick im TreeView zu behandeln
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParaKoTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ParaKoTreeView.SelectedNode = ParaKoTreeView.GetNodeAt(e.X, e.Y);
                if (ParaKoTreeView.SelectedNode == null)
                {
                    return;
                }

                contextMenuAppParCo.Show(Cursor.Position.X, Cursor.Position.Y);

                // Schaltfläche löschen zeigen, wenn auf Objekt geklickt wurde 
                if (ParaKoTreeView.SelectedNode == null)
                {
                    this.toolStripMenuAppParCoDelete.Visible = false;
                    this.toolStripMenuCopyDynamicObject.Visible = false;
                    this.toolStripMenuPasteDynamicObject.Visible = false;
                }
                else
                {
                    this.toolStripMenuAppParCoDelete.Visible = true;
                    this.toolStripMenuCopyDynamicObject.Visible = true;
                    if (dynamicCopyObject != null) // nur einfügen anzeigen, wenn vorher etwas kopiert wurde
                    {
                        this.toolStripMenuPasteDynamicObject.Visible = true;
                    }
                    else
                    {
                        this.toolStripMenuPasteDynamicObject.Visible = false;
                    }
                }

                // zuerst alle toolStrip Items aus invisible setzen
                this.toolStripMenuAddApplicationChannel.Visible = false;
                this.toolStripMenuAddChannnelIndependentBlock.Visible = false;
                this.toolStripMenuAddParameterBlock.Visible = false;
                this.toolStripMenuAddComObject.Visible = false;
                this.toolStripMenuAddParameter.Visible = false;
                this.toolStripMenuAddChoose.Visible = false;
                this.toolStripMenuAddWhen.Visible = false;

                // dann je nach angeklicktem Element, die auswählbaren toolStrip Items visible setzen
                if (ParaKoTreeView.SelectedNode.Tag is ApplicationProgram_t)
                {
                    this.toolStripMenuAppParCoDelete.Visible = false; //ApplicationProgram soll nicht gelöscht werden können
                    this.toolStripMenuAddApplicationChannel.Visible = true;
                    this.toolStripMenuAddChannnelIndependentBlock.Visible = true;
                }
                else if (ParaKoTreeView.SelectedNode.Tag is ApplicationProgramChannel_t)
                {
                    this.toolStripMenuAddParameterBlock.Visible = true;
                    this.toolStripMenuAddComObject.Visible = true;
                    this.toolStripMenuAddChoose.Visible = true;
                }
                else if (ParaKoTreeView.SelectedNode.Tag is ChannelIndependentBlock_t)
                {
                    this.toolStripMenuAddParameterBlock.Visible = true;
                    this.toolStripMenuAddComObject.Visible = true;
                    this.toolStripMenuAddChoose.Visible = true;
                }
                else if (ParaKoTreeView.SelectedNode.Tag is ChannelChoose_t)
                {
                    this.toolStripMenuAddWhen.Visible = true;
                }
                else if (ParaKoTreeView.SelectedNode.Tag is ChannelChoose_tWhen)
                {
                    this.toolStripMenuAddParameterBlock.Visible = true;
                    this.toolStripMenuAddComObject.Visible = true;
                    this.toolStripMenuAddChoose.Visible = true;
                }
                else if (ParaKoTreeView.SelectedNode.Tag is ComObjectParameterBlock_t)
                {
                    this.toolStripMenuAddParameterBlock.Visible = true;
                    this.toolStripMenuAddComObject.Visible = true;
                    this.toolStripMenuAddParameter.Visible = true;
                    this.toolStripMenuAddChoose.Visible = true;
                }
                else if (ParaKoTreeView.SelectedNode.Tag is ComObjectParameterChoose_t)
                {
                    this.toolStripMenuAddWhen.Visible = true;
                }
                else if (ParaKoTreeView.SelectedNode.Tag is ComObjectParameterChoose_tWhen)
                {
                    this.toolStripMenuAddComObject.Visible = true;
                    this.toolStripMenuAddChoose.Visible = true;
                    this.toolStripMenuAddParameterBlock.Visible = true;
                    this.toolStripMenuAddParameter.Visible = true;
                }
            }
        }



        #region ContextMenu
        public enum newOrEditedParameterItems
        {
            ParaOrUnionPara,
            ComObjectParaChooseParameter,
            ChannelChooseParameter,
            ParaBlockParameter
        }

        public static newOrEditedParameterItems newOrEditedParameterItem;

        // einen Application Channel hinzufügen
        private void toolStripMenuAddApplicationChannel_Click(object sender, EventArgs e)
        {
            DynamicTabControl.RemoveAllParamsCoTabs();
            DynamicTabControl.AddApplicationChannelTab();
            ApplicationChannelGui.ClearAplicationChannelTab();
        }

        // einen ChannelIndependentBlock hinzufügen
        private void toolStripMenuAddChannnelIndependentBlock_Click(object sender, EventArgs e)
        {
            DynamicTabControl.RemoveAllParamsCoTabs();
            DynamicTabControl.AddIndependentChannelTab();

            ChannelIndependentBlock_t newChannelIndependentBlock = new ChannelIndependentBlock_t();
            DynamicSectionEdit.AddToDynamicObject(DynamicTreeViewGui.GetSelectedTreeNode().Tag, newChannelIndependentBlock);
            newChannelIndependentBlock.Items = new object[0];

            var channelNode = DynamicTreeViewGui.GetSelectedTreeNode().Nodes.Add("Channel Indepentent Block");//keine ID vorhanden!
            channelNode.Tag = newChannelIndependentBlock;
            channelNode.ImageIndex = (int)Images.channel;
            channelNode.SelectedImageIndex = (int)Images.channel;
        }


        private void toolStripMenuAddParameterBlock_Click(object sender, EventArgs e)
        {
            DynamicTabControl.RemoveAllParamsCoTabs();
            DynamicTabControl.AddParameterTab();
            AppParameterGui.ClearParameterPage();
            ParameterRefGui.ClearParameterRefPage();
            ComObjectParameterBlockGui.ClearComObjectParameterBlockPage();
            DynamicTreeViewGui.newOrEditedParameterItem = DynamicTreeViewGui.newOrEditedParameterItems.ParaBlockParameter;
        }

        // Parameter Behandlung
        private void toolStripMenuAddParameter_Click(object sender, EventArgs e)
        {
            DynamicTabControl.RemoveAllParamsCoTabs();
            DynamicTabControl.AddParameterTab();
            AppParameterGui.ClearParameterPage();
            ParameterRefGui.ClearParameterRefPage();
            newOrEditedParameterItem = newOrEditedParameterItems.ParaOrUnionPara;
        }

        private void toolStripMenuAddComObject_Click(object sender, EventArgs e)
        {
            ComObjectGui.ClearComObjectPage();
            ComObjectRefGui.ClearComObjectRefPage();
            ComObjectRefRefGui.ClearComObjectRefRefPage();
            DynamicTabControl.RemoveAllParamsCoTabs();
            DynamicTabControl.AddComObjectTab();
        }

        private void toolStripMenuAddChoose_Click(object sender, EventArgs e)
        {
            DynamicTabControl.RemoveAllParamsCoTabs();
            DynamicTabControl.AddParameterTab();
            AppParameterGui.ClearParameterPage();
            ParameterRefGui.ClearParameterRefPage();
            if (ParaKoTreeView.SelectedNode.Tag is ComObjectParameterChoose_t ||
                ParaKoTreeView.SelectedNode.Tag is ComObjectParameterBlock_t ||
                ParaKoTreeView.SelectedNode.Tag is ComObjectParameterChoose_tWhen)
            {
                newOrEditedParameterItem = newOrEditedParameterItems.ComObjectParaChooseParameter;
            }
            else if (ParaKoTreeView.SelectedNode.Tag is ChannelChoose_t ||
               ParaKoTreeView.SelectedNode.Tag is ApplicationProgramChannel_t ||
               ParaKoTreeView.SelectedNode.Tag is ChannelIndependentBlock_t)
            {
                newOrEditedParameterItem = newOrEditedParameterItems.ChannelChooseParameter;
            }
        }

        private void toolStripMenuAddWhen_Click(object sender, EventArgs e)
        {
            DynamicTabControl.RemoveAllParamsCoTabs();

            if (ParaKoTreeView.SelectedNode.Tag is ComObjectParameterChoose_t)
            {
                DynamicTabControl.AddComObjectParameterChooseWhenTab();
                ComObjectParameterChooseWhenGui.ClearComObjectParameterChooseWhenPage();
            }
            else if (ParaKoTreeView.SelectedNode.Tag is ChannelChoose_t)
            {
                DynamicTabControl.AddChannelChooseWhenTab();
                ChannelChooseWhenGui.ClearChannelChooseWhenPage();
            }
        }

        /// <summary>
        /// die verschiedenen Elemente löschen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuAppParCoDelete_Click(object sender, EventArgs e)
        {
            if (ParaKoTreeView.SelectedNode.Tag is ApplicationProgramChannel_t)
            {
                DynamicSectionEdit.DeleteApplicationProgramChannelAndReferences();
            }
            else if (ParaKoTreeView.SelectedNode.Tag is ChannelIndependentBlock_t)
            {
                DynamicSectionEdit.DeleteChannelIndependentBlockAndReferences();
            }
            else if (ParaKoTreeView.SelectedNode.Tag is ChannelChoose_t)
            {
                DynamicSectionEdit.DeleteChannelChooseAndReferences();
            }
            else if (ParaKoTreeView.SelectedNode.Tag is ChannelChoose_tWhen)
            {
                DynamicSectionEdit.DeleteChannelChooseWhen();
            }
            else if (ParaKoTreeView.SelectedNode.Tag is ComObjectParameterBlock_t)
            {
                DynamicSectionEdit.DeleteComObjParameterBlock();
            }
            else if (ParaKoTreeView.SelectedNode.Tag is ComObjectParameterChoose_t)
            {
                DynamicSectionEdit.DeleteComObjParaChooseAndReferences();
            }
            else if (ParaKoTreeView.SelectedNode.Tag is ComObjectParameterChoose_tWhen)
            {
                DynamicSectionEdit.DeleteComObjParaChooseWhen();
            }
            else if (ParaKoTreeView.SelectedNode.Tag is ComObjectRefRef_t)
            {
                ComObjectGui.DeleteComObjectAndReferences();
            }
            else if (ParaKoTreeView.SelectedNode.Tag is ParameterRefRef_t)
            {
                AppParameterGui.DeleteParameterAndReferences();
            }
        }

        private static object dynamicCopyObject = null; // das zu kopierende object mit all seinen Kindern wird hier zwischengespeichert
        private static ParameterRefRef_t dynamicCopyChooseParameterRefRef = null; // zum kopieren eines ComObjectParameterChoose gehört oft auch ein ParameterRefRef; die Id wird hier abgelegt
        /// <summary>
        /// kopieren beliebiger Knoten der Dynamic Sektion
        /// </summary>
        private void toolStripMenuCopyDynamicObject_Click(object sender, EventArgs e)
        {
            dynamicCopyObject = ObjectFunctions.DeepClone(ParaKoTreeView.SelectedNode.Tag);
            if (dynamicCopyObject is ComObjectParameterChoose_t)
            {
                dynamicCopyChooseParameterRefRef = null;
                if (ParaKoTreeView.SelectedNode.Parent.Tag is ComObjectParameterChoose_tWhen)
                {
                    foreach (object item in (ParaKoTreeView.SelectedNode.Parent.Tag as ComObjectParameterChoose_tWhen).Items)
                    {
                        if (item is ParameterRefRef_t)
                        {
                            if ((item as ParameterRefRef_t).RefId == (dynamicCopyObject as ComObjectParameterChoose_t).ParamRefId)
                            {
                                dynamicCopyChooseParameterRefRef = (item as ParameterRefRef_t);
                                break;
                            }
                        }
                    }
                }
                else if (ParaKoTreeView.SelectedNode.Parent.Tag is ComObjectParameterBlock_t)
                {
                    foreach (object item in (ParaKoTreeView.SelectedNode.Parent.Tag as ComObjectParameterBlock_t).Items)
                    {
                        if (item is ParameterRefRef_t)
                        {
                            if ((item as ParameterRefRef_t).RefId == (dynamicCopyObject as ComObjectParameterChoose_t).ParamRefId)
                            {
                                dynamicCopyChooseParameterRefRef = (item as ParameterRefRef_t);
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// einfügen beliebiger Knoten der Dynamic Sektion
        /// </summary>
        private void toolStripMenuPasteDynamicObject_Click(object sender, EventArgs e)
        {
            object pasteObject = ParaKoTreeView.SelectedNode.Tag;

            DynamicSectionEdit.CheckIfNewIdNecessary(dynamicCopyObject);

            // je nachdem was für ein Typ der kopierte Knoten ist und wo er eingefügt werden soll muss kontrolliert werden ob es passt
            if ((pasteObject is ApplicationProgram_t)
                &&
                ((dynamicCopyObject is ApplicationProgramChannel_t) || (dynamicCopyObject is ChannelIndependentBlock_t)))
            {
                DynamicSectionEdit.AddToDynamicObject(pasteObject, dynamicCopyObject);
            }
            else if (((pasteObject is ApplicationProgramChannel_t) || (pasteObject is ChannelIndependentBlock_t) ||
                (pasteObject is ChannelChoose_tWhen))
                &&
                ((dynamicCopyObject is ComObjectRefRef_t) || (dynamicCopyObject is ComObjectParameterBlock_t) ||
                (dynamicCopyObject is ChannelChoose_t)))
            {
                DynamicSectionEdit.AddToItemsObject(pasteObject, dynamicCopyObject);
            }
            else if ((pasteObject is ComObjectParameterBlock_t) && ((dynamicCopyObject is ApplicationProgramChannel_t) ||
                (dynamicCopyObject is ComObjectRefRef_t) || (dynamicCopyObject is ComObjectParameterBlock_t) ||
                (dynamicCopyObject is ParameterRefRef_t) || (dynamicCopyObject is ComObjectParameterChoose_t)))
            {
                // wenn das zu kopierende Objekt ein ComObjectParameterChoose ist, muss evtl auch der dazugehörende ParameterRefRef mit kopiert werden
                if (dynamicCopyObject is ComObjectParameterChoose_t)
                {
                    if (dynamicCopyChooseParameterRefRef != null)
                    {
                        DynamicSectionEdit.AddToItemsObject(pasteObject, dynamicCopyChooseParameterRefRef);
                    }
                }
                DynamicSectionEdit.AddToItemsObject(pasteObject, dynamicCopyObject);
            }
            else if ((pasteObject is ComObjectParameterChoose_tWhen) && ((dynamicCopyObject is ComObjectRefRef_t) ||
                (dynamicCopyObject is ComObjectParameterBlock_t) || (dynamicCopyObject is ParameterRefRef_t) ||
                (dynamicCopyObject is ComObjectParameterChoose_t)))
            {
                // wenn das zu kopierende Objekt ein ComObjectParameterChoose ist, muss evtl auch der dazugehörende ParameterRefRef mit kopiert werden
                if (dynamicCopyObject is ComObjectParameterChoose_t)
                {
                    if (dynamicCopyChooseParameterRefRef != null)
                    {
                        DynamicSectionEdit.AddToItemsObject(pasteObject, dynamicCopyChooseParameterRefRef);
                    }
                }
                DynamicSectionEdit.AddToItemsObject(pasteObject, dynamicCopyObject);
            }
            else if ((pasteObject is ChannelChoose_t) && (dynamicCopyObject is ChannelChoose_tWhen))
            {
                (pasteObject as ChannelChoose_t).when =
                    HandleArrayFunctions.Add((pasteObject as ChannelChoose_t).when, dynamicCopyObject as ChannelChoose_tWhen);
            }
            else if ((pasteObject is ComObjectParameterChoose_t) && (dynamicCopyObject is ComObjectParameterChoose_tWhen))
            {
                (pasteObject as ComObjectParameterChoose_t).when =
                    HandleArrayFunctions.Add((pasteObject as ComObjectParameterChoose_t).when, dynamicCopyObject as ComObjectParameterChoose_tWhen);
            }
            else
            {
                DialogResult result = MessageBox.Show("Das Element kann hier leider nicht eingefügt werden.",
                    "Copy Dynamic Object Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            while (ParaKoTreeView.Nodes.Count > 0)
            {
                ParaKoTreeView.Nodes.Clear();
            }
            InitializeParaKoTreeView();
            ParaKoTreeView.Nodes[0].Expand(); //erste Ebene ausklappen
            ParaKoTreeView.Nodes[0].Nodes[0].Expand(); //zweite Ebene ausklappen

            TreeNode nodeToSelect = TreeViewArrayFunctions.SearchTreeNodeByTag(dynamicCopyObject, ParaKoTreeView);
            ParaKoTreeView.SelectedNode = nodeToSelect;
        }
        #endregion

        public static TreeNode GetSelectedTreeNode()
        {
            return mDynamicTreeViewGui.ParaKoTreeView.SelectedNode;
        }

        public static void SetSelectedTreeNode(TreeNode node)
        {
            mDynamicTreeViewGui.ParaKoTreeView.SelectedNode = node;
        }

        public static void SetSelectedNodeName(string nodeName)
        {
            mDynamicTreeViewGui.ParaKoTreeView.SelectedNode.Name = nodeName;
        }

        public static void SetSelectedNodeText(string nodeText)
        {
            mDynamicTreeViewGui.ParaKoTreeView.SelectedNode.Name = nodeText;
        }

        public static ref TreeView GetTreeView()
        {
            return ref mDynamicTreeViewGui.ParaKoTreeView;
        }

        /***************************************************************************************************************************************************************/
        #region Drag&Drop Functions
        // diese Funktionen wurdn grundlegend aus dem Beispiel von https://www.codeproject.com/Articles/6184/TreeView-Rearrange übernommen

        private string NodeMap;
        private const int MAPSIZE = 128;
        private StringBuilder NewNodeMap = new StringBuilder(MAPSIZE);

        private void ParaKoTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void ParaKoTreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void ParaKoTreeView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false) && this.NodeMap != "")
            {
                TreeNode MovingNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                string[] NodeIndexes = this.NodeMap.Split('|');
                TreeNodeCollection InsertCollection = this.ParaKoTreeView.Nodes;
                for (int i = 0; i < NodeIndexes.Length - 1; i++)
                {
                    InsertCollection = InsertCollection[Int32.Parse(NodeIndexes[i])].Nodes;
                }

                if (InsertCollection != null)
                {
                    InsertCollection.Insert(Int32.Parse(NodeIndexes[NodeIndexes.Length - 1]), (TreeNode)MovingNode.Clone());
                    this.ParaKoTreeView.SelectedNode = InsertCollection[Int32.Parse(NodeIndexes[NodeIndexes.Length - 1])];
                    MovingNode.Remove();
                }


                // die Änderungen aus dem TreeView auch in die knxprod Daten übernehmen
                if (ParaKoTreeView.SelectedNode.Parent.Tag is ChannelChoose_t)
                {
                    ChannelChoose_tWhen[] newChannelChooseWhenSort = new ChannelChoose_tWhen[0];
                    foreach (TreeNode treeNode in ParaKoTreeView.SelectedNode.Parent.Nodes)
                    {
                        newChannelChooseWhenSort = HandleArrayFunctions.Add(newChannelChooseWhenSort, treeNode.Tag as ChannelChoose_tWhen);
                    }
                    (ParaKoTreeView.SelectedNode.Parent.Tag as ChannelChoose_t).when = newChannelChooseWhenSort;
                }
                else if (ParaKoTreeView.SelectedNode.Parent.Tag is ComObjectParameterChoose_t)
                {
                    ComObjectParameterChoose_tWhen[] newComObjParaChooseWhenSort = new ComObjectParameterChoose_tWhen[0];
                    foreach (TreeNode treeNode in ParaKoTreeView.SelectedNode.Parent.Nodes)
                    {
                        newComObjParaChooseWhenSort = HandleArrayFunctions.Add(newComObjParaChooseWhenSort, treeNode.Tag as ComObjectParameterChoose_tWhen);
                    }
                    (ParaKoTreeView.SelectedNode.Parent.Tag as ComObjectParameterChoose_t).when = newComObjParaChooseWhenSort;
                }
                else
                {
                    object[] newObjectSort = new object[0];
                    foreach (TreeNode treeNode in ParaKoTreeView.SelectedNode.Parent.Nodes)
                    {
                        // wenn der aktuelle Knoten ein Choose Knoten ist, muss geprüft werden, ob ein dazugehöriger ParameterRefRef existiert.
                        // Dieser muss dann auch umgesetzt werden, wird aber nicht im TreeView angezeigt.
                        if (treeNode.Tag is ChannelChoose_t || treeNode.Tag is ComObjectParameterChoose_t)
                        {
                            List<object> paraRefRefs = null;
                            if (treeNode.Parent.Tag is ComObjectParameterBlock_t)
                            {
                                paraRefRefs = (treeNode.Parent.Tag as ComObjectParameterBlock_t).Items.ToList().FindAll(x => x is ParameterRefRef_t);
                            }
                            else if (ParaKoTreeView.SelectedNode.Parent.Tag is ApplicationProgramChannel_t)
                            {
                                paraRefRefs = (treeNode.Parent.Tag as ApplicationProgramChannel_t).Items.ToList().FindAll(x => x is ParameterRefRef_t);
                            }
                            else if (ParaKoTreeView.SelectedNode.Parent.Tag is ChannelIndependentBlock_t)
                            {
                                paraRefRefs = (treeNode.Parent.Tag as ChannelIndependentBlock_t).Items.ToList().FindAll(x => x is ParameterRefRef_t);
                            }
                            else if (ParaKoTreeView.SelectedNode.Parent.Tag is ChannelChoose_tWhen)
                            {
                                paraRefRefs = (treeNode.Parent.Tag as ChannelChoose_tWhen).Items.ToList().FindAll(x => x is ParameterRefRef_t);
                            }
                            else if (ParaKoTreeView.SelectedNode.Parent.Tag is ComObjectParameterChoose_tWhen)
                            {
                                paraRefRefs = (treeNode.Parent.Tag as ComObjectParameterChoose_tWhen).Items.ToList().FindAll(x => x is ParameterRefRef_t);
                            }

                            if (paraRefRefs != null)
                            {
                                ParameterRefRef_t chooseParaRefRef = null;
                                if (treeNode.Tag is ChannelChoose_t)
                                {
                                    chooseParaRefRef = paraRefRefs.Find(x => (x as ParameterRefRef_t).RefId == (treeNode.Tag as ChannelChoose_t).ParamRefId) as ParameterRefRef_t;
                                }
                                else if (treeNode.Tag is ComObjectParameterChoose_t)
                                {
                                    chooseParaRefRef = paraRefRefs.Find(x => (x as ParameterRefRef_t).RefId == (treeNode.Tag as ComObjectParameterChoose_t).ParamRefId) as ParameterRefRef_t;
                                }

                                if (chooseParaRefRef != null)
                                {
                                    // wenn das zum choose passende ParaemetrRefRef Objekt gefunden wurde, wird es VOR dem choose Objekt in die Struktur eingefügt
                                    newObjectSort = HandleArrayFunctions.Add(newObjectSort, chooseParaRefRef);
                                }
                            }
                        }

                        // hier wird dann das eigentliche Element aus dem neu geordneten TreeView eingefügt
                        newObjectSort = HandleArrayFunctions.Add(newObjectSort, treeNode.Tag);
                    }

                    if (ParaKoTreeView.SelectedNode.Parent.Tag is ComObjectParameterBlock_t)
                    {
                        (ParaKoTreeView.SelectedNode.Parent.Tag as ComObjectParameterBlock_t).Items = newObjectSort;
                    }
                    else if (ParaKoTreeView.SelectedNode.Parent.Tag is ApplicationProgramChannel_t)
                    {
                        (ParaKoTreeView.SelectedNode.Parent.Tag as ApplicationProgramChannel_t).Items = newObjectSort;
                    }
                    else if (ParaKoTreeView.SelectedNode.Parent.Tag is ChannelIndependentBlock_t)
                    {
                        (ParaKoTreeView.SelectedNode.Parent.Tag as ChannelIndependentBlock_t).Items = newObjectSort;
                    }
                    else if (ParaKoTreeView.SelectedNode.Parent.Tag is ChannelChoose_tWhen)
                    {
                        (ParaKoTreeView.SelectedNode.Parent.Tag as ChannelChoose_tWhen).Items = newObjectSort;
                    }
                    else if (ParaKoTreeView.SelectedNode.Parent.Tag is ComObjectParameterChoose_tWhen)
                    {
                        (ParaKoTreeView.SelectedNode.Parent.Tag as ComObjectParameterChoose_tWhen).Items = newObjectSort;
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Ein Element konnte nicht sortiert werden.",
                            "Element Sort Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void ParaKoTreeView_DragOver(object sender, DragEventArgs e)
        {
            TreeNode NodeOver = this.ParaKoTreeView.GetNodeAt(this.ParaKoTreeView.PointToClient(Cursor.Position));
            TreeNode NodeMoving = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");


            // A bit long, but to summarize, process the following code only if the nodeover is null
            // and either the nodeover is not the same thing as nodemoving UNLESSS nodeover happens
            // to be the last node in the branch (so we can allow drag & drop below a parent branch)
            // und die Eltern der Nodes müssen die gleichen sein (Verschiebung nur unter einem Knoten möglich)
            if ((NodeOver != null && (NodeOver != NodeMoving || (NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1)))) && NodeMoving.Parent == NodeOver.Parent)
            {
                int OffsetY = this.ParaKoTreeView.PointToClient(Cursor.Position).Y - NodeOver.Bounds.Top;
                int NodeOverImageWidth = this.ParaKoTreeView.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
                Graphics g = this.ParaKoTreeView.CreateGraphics();

                // Image index of 1 is the non-folder icon
                //if (NodeOver.ImageIndex == 1)
                //{
                #region Standard Node
                if (OffsetY < (NodeOver.Bounds.Height / 2))
                {
                    //this.lblDebug.Text = "top";

                    #region If NodeOver is a child then cancel
                    TreeNode tnParadox = NodeOver;
                    while (tnParadox.Parent != null)
                    {
                        if (tnParadox.Parent == NodeMoving)
                        {
                            this.NodeMap = "";
                            return;
                        }

                        tnParadox = tnParadox.Parent;
                    }
                    #endregion
                    #region Store the placeholder info into a pipe delimited string
                    SetNewNodeMap(NodeOver, false);
                    if (SetMapsEqual() == true)
                        return;
                    #endregion
                    #region Clear placeholders above and below
                    this.Refresh();
                    #endregion
                    #region Draw the placeholders
                    this.DrawLeafTopPlaceholders(NodeOver);
                    #endregion
                }
                else
                {
                    //this.lblDebug.Text = "bottom";

                    #region If NodeOver is a child then cancel
                    TreeNode tnParadox = NodeOver;
                    while (tnParadox.Parent != null)
                    {
                        if (tnParadox.Parent == NodeMoving)
                        {
                            this.NodeMap = "";
                            return;
                        }

                        tnParadox = tnParadox.Parent;
                    }
                    #endregion
                    #region Allow drag drop to parent branches
                    TreeNode ParentDragDrop = null;
                    // If the node the mouse is over is the last node of the branch we should allow
                    // the ability to drop the "nodemoving" node BELOW the parent node
                    if (NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1))
                    {
                        int XPos = this.ParaKoTreeView.PointToClient(Cursor.Position).X;
                        if (XPos < NodeOver.Bounds.Left)
                        {
                            ParentDragDrop = NodeOver.Parent;

                            if (XPos < (ParentDragDrop.Bounds.Left - this.ParaKoTreeView.ImageList.Images[ParentDragDrop.ImageIndex].Size.Width))
                            {
                                if (ParentDragDrop.Parent != null)
                                    ParentDragDrop = ParentDragDrop.Parent;
                            }
                        }
                    }
                    #endregion
                    #region Store the placeholder info into a pipe delimited string
                    // Since we are in a special case here, use the ParentDragDrop node as the current "nodeover"
                    SetNewNodeMap(ParentDragDrop != null ? ParentDragDrop : NodeOver, true);
                    if (SetMapsEqual() == true)
                        return;
                    #endregion
                    #region Clear placeholders above and below
                    this.Refresh();
                    #endregion
                    #region Draw the placeholders
                    DrawLeafBottomPlaceholders(NodeOver, ParentDragDrop);
                    #endregion
                }
                #endregion
                //}
                /*
				else
				{
					#region Folder Node
					if (OffsetY < (NodeOver.Bounds.Height / 3))
					{
						//this.lblDebug.Text = "folder top";

						#region If NodeOver is a child then cancel
						TreeNode tnParadox = NodeOver;
						while (tnParadox.Parent != null)
						{
							if (tnParadox.Parent == NodeMoving)
							{
								this.NodeMap = "";
								return;
							}

							tnParadox = tnParadox.Parent;
						}
						#endregion
						#region Store the placeholder info into a pipe delimited string
						SetNewNodeMap(NodeOver, false);
						if (SetMapsEqual() == true)
							return;
						#endregion
						#region Clear placeholders above and below
						this.Refresh();
						#endregion
						#region Draw the placeholders
						this.DrawFolderTopPlaceholders(NodeOver);
						#endregion
					}
					else if ((NodeOver.Parent != null && NodeOver.Index == 0) && (OffsetY > (NodeOver.Bounds.Height - (NodeOver.Bounds.Height / 3))))
					{
						//this.lblDebug.Text = "folder bottom";

						#region If NodeOver is a child then cancel
						TreeNode tnParadox = NodeOver;
						while (tnParadox.Parent != null)
						{
							if (tnParadox.Parent == NodeMoving)
							{
								this.NodeMap = "";
								return;
							}

							tnParadox = tnParadox.Parent;
						}
						#endregion
						#region Store the placeholder info into a pipe delimited string
						SetNewNodeMap(NodeOver, true);
						if (SetMapsEqual() == true)
							return;
						#endregion
						#region Clear placeholders above and below
						this.Refresh();
						#endregion
						#region Draw the placeholders
						DrawFolderTopPlaceholders(NodeOver);
						#endregion
					}
					else
					{
						//this.lblDebug.Text = "folder over";

						if (NodeOver.Nodes.Count > 0)
						{
							NodeOver.Expand();
							//this.Refresh();
						}
						else
						{
							#region Prevent the node from being dragged onto itself
							if (NodeMoving == NodeOver)
								return;
							#endregion
							#region If NodeOver is a child then cancel
							TreeNode tnParadox = NodeOver;
							while (tnParadox.Parent != null)
							{
								if (tnParadox.Parent == NodeMoving)
								{
									this.NodeMap = "";
									return;
								}

								tnParadox = tnParadox.Parent;
							}
							#endregion
							#region Store the placeholder info into a pipe delimited string
							SetNewNodeMap(NodeOver, false);
							NewNodeMap = NewNodeMap.Insert(NewNodeMap.Length, "|0");

							if (SetMapsEqual() == true)
								return;
							#endregion
							#region Clear placeholders above and below
							this.Refresh();
							#endregion
							#region Draw the "add to folder" placeholder
							DrawAddToFolderPlaceholder(NodeOver);
							#endregion
						}
					}
					#endregion
				}*/
            }
        }



        #region Helper Methods
        private void DrawLeafTopPlaceholders(TreeNode NodeOver)
        {
            Graphics g = this.ParaKoTreeView.CreateGraphics();

            int NodeOverImageWidth = this.ParaKoTreeView.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
            int LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            int RightPos = this.ParaKoTreeView.Width - 4;

            Point[] LeftTriangle = new Point[5]{
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 4),
                                                   new Point(LeftPos, NodeOver.Bounds.Top + 4),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Y),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 5)};

            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Top - 4),
                                                    new Point(RightPos, NodeOver.Bounds.Top + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Top - 5)};


            g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
            g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Top), new Point(RightPos, NodeOver.Bounds.Top));

        }//eom

        private void DrawLeafBottomPlaceholders(TreeNode NodeOver, TreeNode ParentDragDrop)
        {
            Graphics g = this.ParaKoTreeView.CreateGraphics();

            int NodeOverImageWidth = this.ParaKoTreeView.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
            // Once again, we are not dragging to node over, draw the placeholder using the ParentDragDrop bounds
            int LeftPos, RightPos;
            if (ParentDragDrop != null)
                LeftPos = ParentDragDrop.Bounds.Left - (this.ParaKoTreeView.ImageList.Images[ParentDragDrop.ImageIndex].Size.Width + 8);
            else
                LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            RightPos = this.ParaKoTreeView.Width - 4;

            Point[] LeftTriangle = new Point[5]{
                                                   new Point(LeftPos, NodeOver.Bounds.Bottom - 4),
                                                   new Point(LeftPos, NodeOver.Bounds.Bottom + 4),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Bottom),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Bottom - 1),
                                                   new Point(LeftPos, NodeOver.Bounds.Bottom - 5)};

            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Bottom - 4),
                                                    new Point(RightPos, NodeOver.Bounds.Bottom + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Bottom),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Bottom - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Bottom - 5)};


            g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
            g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Bottom), new Point(RightPos, NodeOver.Bounds.Bottom));
        }//eom

        private void DrawFolderTopPlaceholders(TreeNode NodeOver)
        {
            Graphics g = this.ParaKoTreeView.CreateGraphics();
            int NodeOverImageWidth = this.ParaKoTreeView.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;

            int LeftPos, RightPos;
            LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            RightPos = this.ParaKoTreeView.Width - 4;

            Point[] LeftTriangle = new Point[5]{
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 4),
                                                   new Point(LeftPos, NodeOver.Bounds.Top + 4),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Y),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 5)};

            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Top - 4),
                                                    new Point(RightPos, NodeOver.Bounds.Top + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Top - 5)};


            g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
            g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Top), new Point(RightPos, NodeOver.Bounds.Top));

        }//eom
        private void DrawAddToFolderPlaceholder(TreeNode NodeOver)
        {
            Graphics g = this.ParaKoTreeView.CreateGraphics();
            int RightPos = NodeOver.Bounds.Right + 6;
            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
                                                    new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2)),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 5)};

            this.Refresh();
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
        }//eom

        private void SetNewNodeMap(TreeNode tnNode, bool boolBelowNode)
        {
            NewNodeMap.Length = 0;

            if (boolBelowNode)
                NewNodeMap.Insert(0, (int)tnNode.Index + 1);
            else
                NewNodeMap.Insert(0, (int)tnNode.Index);
            TreeNode tnCurNode = tnNode;

            while (tnCurNode.Parent != null)
            {
                tnCurNode = tnCurNode.Parent;

                if (NewNodeMap.Length == 0 && boolBelowNode == true)
                {
                    NewNodeMap.Insert(0, (tnCurNode.Index + 1) + "|");
                }
                else
                {
                    NewNodeMap.Insert(0, tnCurNode.Index + "|");
                }
            }
        }//oem

        private bool SetMapsEqual()
        {
            if (this.NewNodeMap.ToString() == this.NodeMap)
                return true;
            else
            {
                this.NodeMap = this.NewNodeMap.ToString();
                return false;
            }
        }//oem
        #endregion
        #endregion
    }
}
