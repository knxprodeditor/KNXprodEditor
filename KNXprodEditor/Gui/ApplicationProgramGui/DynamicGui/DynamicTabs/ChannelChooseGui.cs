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
    public partial class ChannelChooseGui : UserControl
    {
        private static ChannelChooseGui mChannelChooseGui;

        public ChannelChooseGui()
        {
            InitializeComponent();
            mChannelChooseGui = this;
        }

        public static ParameterRef_t FillChannelChoosePage(ChannelChoose_t chChoose)
        {
            DynamicTabControl.AddChannelChooseTab();
            ParameterRef_t returnParameterRef = ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == chChoose.ParamRefId);

            mChannelChooseGui.textCcParamRefId.Text = chChoose.ParamRefId;
            mChannelChooseGui.textCcInternalDescription.Text = chChoose.InternalDescription;
            return returnParameterRef;
        }

        public static void ChannelChooseParameterSave(ComboBox comboBoxParaCollection)
        {
            ChannelChoose_t newChannelChoose = new ChannelChoose_t();
            int nextParameterRefNumber = AppParameterGui.HighestUsedParaRefNumber() + 1;
            ParameterRef_t newParaRef = new ParameterRef_t();

            if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ChannelChoose_t) // bestehendes Choose Object verändert
            {
                newChannelChoose = DynamicTreeViewGui.GetSelectedTreeNode().Tag as ChannelChoose_t;
                newParaRef = ObjectFunctions.DeepClone(ParameterRefGui.GetParameterRefIdItem().Tag as ParameterRef_t);

                // Überprüfen, ob alter ParameterRef noch verwendet wird, sonst löschen (neuer ParameterRef wurde erzeugt)
                if (DynamicSectionSearch.SearchIdInDynamic((DynamicTreeViewGui.GetSelectedTreeNode().Tag as ChannelChoose_t).ParamRefId) <= 1)
                {
                    int index = 0;
                    foreach (ParameterRef_t paraRef in ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs)
                    {
                        if (paraRef.Id == (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ChannelChoose_t).ParamRefId)
                        {
                            ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs = HandleArrayFunctions.Delete(ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs, index);
                            index--;
                        }
                        index++;
                    }
                }
            }
            else // neues Choose Object erzeugt
            {
                DynamicSectionEdit.AddToItemsObject(DynamicTreeViewGui.GetSelectedTreeNode().Tag, newChannelChoose);
                newChannelChoose.when = new ChannelChoose_tWhen[0];
            }


            // das ParameterRef Object anhängen
            ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs = HandleArrayFunctions.Add(ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs, newParaRef);

            if ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag is ApplicationProgramStatic_tParameter)
            {
                newParaRef.Id = ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag as ApplicationProgramStatic_tParameter).Id + "_R-" + nextParameterRefNumber.ToString();
                newParaRef.RefId = ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag as ApplicationProgramStatic_tParameter).Id;
            }
            else if ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag is UnionParameter_t)
            {
                newParaRef.Id = ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag as UnionParameter_t).Id + "_R-" + nextParameterRefNumber.ToString();
                newParaRef.RefId = ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag as UnionParameter_t).Id;
            }
            newChannelChoose.ParamRefId = newParaRef.Id;
            newParaRef.Tag = nextParameterRefNumber.ToString();

            if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ChannelChoose_t) // bestehender Parameter verändert
            {
                DynamicTreeViewGui.SetSelectedNodeText(DynamicTreeViewGenerator.ResolveParameterRefRef(newChannelChoose, new TreeNode()).Text);
                DynamicTabControl.RemoveAllParamsCoTabs();
                DynamicTreeViewGui.FillParameterOrUnionPage(ParameterRefGui.FillParameterRefPage(FillChannelChoosePage(newChannelChoose)));
            }
            else // neuer Parameter angelegt
            {
                TreeNode childNode = DynamicTreeViewGenerator.TreeViewChannelChoose(newChannelChoose, DynamicTreeViewGui.GetSelectedTreeNode());
                DynamicTreeViewGui.SetSelectedTreeNode(childNode);
            }
        }

        public static void SetChannelChooseParameterRefId(string channelChooseParameterRefId)
        {
            mChannelChooseGui.textCcParamRefId.Text = channelChooseParameterRefId;
        }
    }
}
