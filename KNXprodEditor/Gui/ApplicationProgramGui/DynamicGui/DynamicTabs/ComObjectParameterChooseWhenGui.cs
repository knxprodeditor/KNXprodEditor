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
    public partial class ComObjectParameterChooseWhenGui : UserControl
    {
        private static ComObjectParameterChooseWhenGui mComObjectParameterChooseWhenGui;

        public ComObjectParameterChooseWhenGui()
        {
            InitializeComponent();
            mComObjectParameterChooseWhenGui = this;
        }

        public static void FillComObjectParameterChooseWhenPage(ComObjectParameterChoose_tWhen comObjectParameterChoose)
        {
            DynamicTabControl.AddComObjectParameterChooseWhenTab();

            WhenTestHelper.FillWhenTestComboBox(DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag, mComObjectParameterChooseWhenGui.comboBoxCopcwTest, comObjectParameterChoose.test, comObjectParameterChoose.@default);

            mComObjectParameterChooseWhenGui.checkBoxCopcwDefault.Checked = comObjectParameterChoose.@default;
            mComObjectParameterChooseWhenGui.textCopcwInternalDescription.Text = comObjectParameterChoose.InternalDescription;
        }

        public static void ClearComObjectParameterChooseWhenPage()
        {
            WhenTestHelper.FillWhenTestComboBox(DynamicTreeViewGui.GetSelectedTreeNode().Tag, mComObjectParameterChooseWhenGui.comboBoxCopcwTest, null, false);
            mComObjectParameterChooseWhenGui.checkBoxCopcwDefault.Checked = false;
            mComObjectParameterChooseWhenGui.textCopcwInternalDescription.Text = "";
        }

        // Save button im ComObjectParameterChooseWhen Tab
        private void buttonComObjParChooseWhenSave_Click(object sender, EventArgs e)
        {
            ComObjectParameterChoose_tWhen newComObjParChooseWhen = new ComObjectParameterChoose_tWhen();
            if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ComObjectParameterChoose_tWhen) // bestehendes When Object verändert
            {
                newComObjParChooseWhen = DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_tWhen;
            }
            else
            {
                (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_t).when =
                    HandleArrayFunctions.Add((DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_t).when, newComObjParChooseWhen);
                newComObjParChooseWhen.Items = new object[0];
            }

            // Verarbeitung des Test Feldes
            if (comboBoxCopcwTest.SelectedItem != null)
            {
                // der Test Wert ist in der ComboBox im Tag enthalten
                if ((comboBoxCopcwTest.SelectedItem as ComboBoxItem).Tag is ParameterType_tTypeRestrictionEnumeration)
                {
                    newComObjParChooseWhen.test = ((comboBoxCopcwTest.SelectedItem as ComboBoxItem).Tag as ParameterType_tTypeRestrictionEnumeration).Value.ToString();
                }
            }
            else // ein individueller Test Wert wurde eingegeben
            {
                newComObjParChooseWhen.test = comboBoxCopcwTest.Text;
            }

            newComObjParChooseWhen.@default = checkBoxCopcwDefault.Checked;
            newComObjParChooseWhen.InternalDescription = Extensions.NullIfEmpty(textCopcwInternalDescription.Text);

            // TreeView anpassen
            if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ComObjectParameterChoose_tWhen) // bestehendes When Object verändert
            {
                string whenText = DynamicTreeViewGenerator.ResolveWhenText((DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterChoose_t).ParamRefId, newComObjParChooseWhen.test, newComObjParChooseWhen.@default);
                DynamicTreeViewGui.GetSelectedTreeNode().Text = whenText;
            }
            else
            {
                TreeNode newTreeNode = DynamicTreeViewGenerator.TreeViewComObjectChooseWhen(DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_t, newComObjParChooseWhen, DynamicTreeViewGui.GetSelectedTreeNode());
                DynamicTreeViewGui.SetSelectedTreeNode(newTreeNode);
            }
        }
    }
}
