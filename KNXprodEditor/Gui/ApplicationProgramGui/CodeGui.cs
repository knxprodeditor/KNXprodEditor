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
    public partial class CodeGui : UserControl
    {
        private static CodeGui mCodeGui;

        public CodeGui()
        {
            InitializeComponent();
            mCodeGui = this;
        }

        public static void InitializeCodeTreeView()
        {
            while (mCodeGui.TreeViewCode.Nodes.Count > 0)
            {
                mCodeGui.TreeViewCode.Nodes.RemoveAt(0);
            }
            if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment != null)
            {
                foreach (var absoluteSegment in ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment)
                {
                    var node = mCodeGui.TreeViewCode.Nodes.Add(absoluteSegment.Id);
                    node.Tag = absoluteSegment;
                }
            }
            if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment != null)
            {
                foreach (var relativeSegment in ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment)
                {
                    var node = mCodeGui.TreeViewCode.Nodes.Add(relativeSegment.Id);
                    node.Tag = relativeSegment;
                }
            }
        }

        private TreeNode lastSelectedTreeViewCodeNode;

        private void TreeViewCode_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Highlight new node
            e.Node.BackColor = SystemColors.Highlight;
            e.Node.ForeColor = SystemColors.HighlightText;
            if (lastSelectedTreeViewCodeNode != null)
            {
                // Set colors to normal for old node
                lastSelectedTreeViewCodeNode.BackColor = SystemColors.Window;
                lastSelectedTreeViewCodeNode.ForeColor = SystemColors.WindowText;
            }
            lastSelectedTreeViewCodeNode = e.Node;


            RemoveAllCodeTabs();
            if (e.Node.Tag is ApplicationProgramStatic_tCodeAbsoluteSegment)
            {
                FillCodeAbsoluteSegment(e.Node.Tag as ApplicationProgramStatic_tCodeAbsoluteSegment);
            }
            else if (e.Node.Tag is ApplicationProgramStatic_tCodeRelativeSegment)
            {
                FillCodeRelativeSegment(e.Node.Tag as ApplicationProgramStatic_tCodeRelativeSegment);
            }
        }

        private void RemoveAllCodeTabs()
        {
            while (tabControlCode.TabPages.Count > 0)
            {
                tabControlCode.TabPages.Clear();
            }
        }

        private void FillCodeAbsoluteSegment(ApplicationProgramStatic_tCodeAbsoluteSegment absoluteSegment)
        {
            tabControlCode.TabPages.Add(this.tabCodeAbsoluteSegment);

            HandleKnxDataTypes.ReadKNXType(listCodeAbsData, absoluteSegment.Data);
            HandleKnxDataTypes.ReadKNXType(listCodeAbsMask, absoluteSegment.Mask);
            textCodeAbsId.Text = absoluteSegment.Id;
            textCodeAbsName.Text = absoluteSegment.Name;
            numericCodeAbsSize.Value = absoluteSegment.Size;
            textCodeAbsInternalDescription.Text = absoluteSegment.InternalDescription;
            comboBoxCodeAbsMemoryType.SelectedItem = HandleKnxDataTypes.ReadKNXType(absoluteSegment.MemoryType);
            numericCodeAbsAddress.Value = absoluteSegment.Address;
            checkBoxCodeAbsUserMemory.Checked = absoluteSegment.UserMemory;
        }

        private void FillCodeRelativeSegment(ApplicationProgramStatic_tCodeRelativeSegment relativeSegment)
        {
            tabControlCode.TabPages.Add(this.tabCodeRelativeSegment);

            HandleKnxDataTypes.ReadKNXType(listCodeRelData, relativeSegment.Data);
            HandleKnxDataTypes.ReadKNXType(listCodeRelMask, relativeSegment.Mask);
            textCodeRelId.Text = relativeSegment.Id;
            textCodeRelName.Text = relativeSegment.Name;
            numericCodeRelSize.Value = relativeSegment.Size;
            textCodeRelInternalDescription.Text = relativeSegment.InternalDescription;
            numericCodeRelLoadStateMachine.Value = relativeSegment.LoadStateMachine;
            numericCodeRelOffset.Value = relativeSegment.Offset;
        }

    }
}
