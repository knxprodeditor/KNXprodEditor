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
    public partial class ChannelChooseWhenGui : UserControl
    {
        private static ChannelChooseWhenGui mChannelChooseWhenGui;

        public ChannelChooseWhenGui()
        {
            InitializeComponent();
            mChannelChooseWhenGui = this;
        }

        public static void FillChannelChooseWhenPage(ChannelChoose_tWhen channelChooseWhen)
        {
            DynamicTabControl.AddChannelChooseWhenTab();

            WhenTestHelper.FillWhenTestComboBox(DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag, mChannelChooseWhenGui.comboBoxCcwTest, channelChooseWhen.test, channelChooseWhen.@default);

            mChannelChooseWhenGui.checkBoxCcwDefault.Checked = channelChooseWhen.@default;
            mChannelChooseWhenGui.textCcwInternalDescription.Text = channelChooseWhen.InternalDescription;
        }

        public static void ClearChannelChooseWhenPage()
        {
            WhenTestHelper.FillWhenTestComboBox(DynamicTreeViewGui.GetSelectedTreeNode().Tag, mChannelChooseWhenGui.comboBoxCcwTest, null, false);
            mChannelChooseWhenGui.checkBoxCcwDefault.Checked = false;
            mChannelChooseWhenGui.textCcwInternalDescription.Text = "";
        }

        // Save button im ChannelChooseWhen Tab
        private void buttonChannelChooseWhenSave_Click(object sender, EventArgs e)
        {
            ChannelChoose_tWhen newChannelChooseWhen = new ChannelChoose_tWhen();
            if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ChannelChoose_tWhen) // bestehendes When Object verändert
            {
                newChannelChooseWhen = DynamicTreeViewGui.GetSelectedTreeNode().Tag as ChannelChoose_tWhen;
            }
            else
            {
                (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ChannelChoose_t).when =
                    HandleArrayFunctions.Add((DynamicTreeViewGui.GetSelectedTreeNode().Tag as ChannelChoose_t).when, newChannelChooseWhen);
                newChannelChooseWhen.Items = new object[0];
            }

            // Verarbeitung des Test Feldes
            if (comboBoxCcwTest.SelectedItem != null)
            {
                // der Test Wert ist in der ComboBox im Tag enthalten
                if ((comboBoxCcwTest.SelectedItem as ComboBoxItem).Tag is ParameterType_tTypeRestrictionEnumeration)
                {
                    newChannelChooseWhen.test = ((comboBoxCcwTest.SelectedItem as ComboBoxItem).Tag as ParameterType_tTypeRestrictionEnumeration).Value.ToString();
                }
            }
            else // ein individueller Test Wert wurde eingegeben
            {
                newChannelChooseWhen.test = comboBoxCcwTest.Text;
            }

            newChannelChooseWhen.@default = checkBoxCcwDefault.Checked;
            newChannelChooseWhen.InternalDescription = Extensions.NullIfEmpty(textCcwInternalDescription.Text);

            // TreeView anpassen
            if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ChannelChoose_tWhen) // bestehendes When Object verändert
            {
                string whenText = DynamicTreeViewGenerator.ResolveWhenText((DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ChannelChoose_t).ParamRefId, newChannelChooseWhen.test, newChannelChooseWhen.@default);
                DynamicTreeViewGui.SetSelectedNodeText(whenText);
            }
            else
            {
                TreeNode newTreeNode = DynamicTreeViewGenerator.TreeViewChannelChooseWhen(DynamicTreeViewGui.GetSelectedTreeNode().Tag as ChannelChoose_t, newChannelChooseWhen, DynamicTreeViewGui.GetSelectedTreeNode());
                DynamicTreeViewGui.SetSelectedTreeNode(newTreeNode);
            }
        }
    }
}
