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
    public partial class ComObjectParameterChooseGui : UserControl
    {
        public static ComObjectParameterChooseGui mComObjectParameterChooseGui;

        public ComObjectParameterChooseGui()
        {
            InitializeComponent();
            mComObjectParameterChooseGui = this;
        }

        public static ParameterRef_t FillComObjectParameterChoose(ComObjectParameterChoose_t comObjParChoose)
        {
            DynamicTabControl.AddComObjectParameterChooseTab();

            ParameterRef_t returnParameterRef = ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == comObjParChoose.ParamRefId);

            mComObjectParameterChooseGui.textCopcParamRefId.Text = comObjParChoose.ParamRefId;
            mComObjectParameterChooseGui.textCopcInternalDescription.Text = comObjParChoose.InternalDescription;

            //Herausfinden, ob zu dem ComObjectParameterChoose ein ParameterRefRef mit gleicher ParamRefId existiert
            if (FindParaRefRefToComObjParaChoose() != null)
            {
                mComObjectParameterChooseGui.checkBoxCopcWithParameterRefRef.Checked = true;
            }
            else
            {
                mComObjectParameterChooseGui.checkBoxCopcWithParameterRefRef.Checked = false;
            }

            return returnParameterRef;
        }


        // Save button im ComObjectParameterChoose Tab
        private void buttonComObjectParameterChooseSave_Click(object sender, EventArgs e)
        {
            ComObjectParameterChoose_t newComObjParaChoose = DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_t;
            newComObjParaChoose.InternalDescription = Extensions.NullIfEmpty(textCopcInternalDescription.Text);

            if (checkBoxCopcWithParameterRefRef.Checked) // Choose soll einen ParameterRefRef mit gleicher ParamRefId haben
            {
                if (FindParaRefRefToComObjParaChoose() == null) // hat aber zur Zeit keinen
                {
                    // daher einen anlegen
                    ParameterRefRef_t newParameterRefRef = new ParameterRefRef_t();
                    newParameterRefRef.RefId = newComObjParaChoose.ParamRefId;

                    if (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag is ComObjectParameterBlock_t)
                    {
                        (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterBlock_t).Items =
                            HandleArrayFunctions.Add((DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterBlock_t).Items, newParameterRefRef);
                    }
                    else if (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag is ComObjectParameterChoose_tWhen)
                    {
                        (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterChoose_tWhen).Items =
                           HandleArrayFunctions.Add((DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterChoose_tWhen).Items, newParameterRefRef);
                    }
                }
            }
            else // Choose soll keinen ParameterRefRef mit gleicher ParamRefId haben
            {
                ParameterRefRef_t paraRefRef = FindParaRefRefToComObjParaChoose();
                if (paraRefRef != null) // hat aber zur Zeit einen
                {
                    // dann muss er gelöscht werden
                    int index = 0;
                    if (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag is ComObjectParameterBlock_t)
                    {
                        foreach (object item in (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterBlock_t).Items)
                        {
                            if (item == paraRefRef)
                            {
                                (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterBlock_t).Items =
                                    HandleArrayFunctions.Delete((DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterBlock_t).Items, index);
                                break;
                            }
                            index++;
                        }
                    }
                    else if (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag is ComObjectParameterChoose_tWhen)
                    {
                        foreach (object item in (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterChoose_tWhen).Items)
                        {
                            if (item == paraRefRef)
                            {
                                (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterChoose_tWhen).Items =
                                    HandleArrayFunctions.Delete((DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterChoose_tWhen).Items, index);
                                break;
                            }
                            index++;
                        }
                    }
                }
            }
        }

        // Speichern eines Parameters für ein ComObjectParameterChoose
        public static void ComObjectParaChooseParameterSave(ComboBox comboBoxParaCollection)
        {
            ComObjectParameterChoose_t newComObjectParaChoose = new ComObjectParameterChoose_t();
            int nextParameterRefNumber = AppParameterGui.HighestUsedParaRefNumber() + 1;
            ParameterRef_t newParaRef = new ParameterRef_t();
            ParameterRefRef_t newParaRefRef = new ParameterRefRef_t();

            if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ComObjectParameterChoose_t) // bestehendes Choose Object verändert
            {
                newComObjectParaChoose = DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_t;

                newParaRefRef = FindParaRefRefToComObjParaChoose();
                newParaRef = ObjectFunctions.DeepClone(ParameterRefGui.GetParameterRefIdItem().Tag as ParameterRef_t);

                // Überprüfen, ob alter ParameterRef noch verwendet wird, sonst löschen (neuer ParameterRef wird erzeugt)
                // (hier 2 Objekte zulassen, da vor einem ComObjectParameterChoose immer noch ein ParameterRefRef ist mit der gleichen ParamerterRef Id)
                if (DynamicSectionSearch.SearchIdInDynamic((DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_t).ParamRefId) <= 2)
                {
                    int index = 0;
                    foreach (ParameterRef_t paraRef in ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs)
                    {
                        if (paraRef.Id == (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_t).ParamRefId)
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
                DynamicSectionEdit.AddToItemsObject(DynamicTreeViewGui.GetSelectedTreeNode().Tag, newParaRefRef);
                DynamicSectionEdit.AddToItemsObject(DynamicTreeViewGui.GetSelectedTreeNode().Tag, newComObjectParaChoose);
                newComObjectParaChoose.when = new ComObjectParameterChoose_tWhen[0];
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
            newComObjectParaChoose.ParamRefId = newParaRef.Id;
            newParaRef.Tag = nextParameterRefNumber.ToString();
            if (newParaRefRef != null)
            {
                newParaRefRef.RefId = newParaRef.Id;
            }

            if (DynamicTreeViewGui.GetSelectedTreeNode().Tag is ComObjectParameterChoose_t) // bestehender Parameter verändert
            {
                DynamicTreeViewGui.SetSelectedNodeText(DynamicTreeViewGenerator.ResolveParameterRefRef(newComObjectParaChoose, new TreeNode()).Text);
                DynamicTabControl.RemoveAllParamsCoTabs();
                DynamicTreeViewGui.FillParameterOrUnionPage(ParameterRefGui.FillParameterRefPage(FillComObjectParameterChoose(newComObjectParaChoose)));
            }
            else // neuer Parameter angelegt
            {
                TreeNode childNode = DynamicTreeViewGenerator.TreeViewComObjectChoose(newComObjectParaChoose, DynamicTreeViewGui.GetSelectedTreeNode());
                DynamicTreeViewGui.SetSelectedTreeNode(childNode);
            }
        }

        /// <summary>
        /// Das zu einem ComObjectParameterChoose zugehörige ParameterRefRef Element finden
        /// </summary>
        /// <returns>Das zu einem ComObjectParameterChoose zugehörige ParameterRefRef Element</returns>
        public static ParameterRefRef_t FindParaRefRefToComObjParaChoose()
        {
            ParameterRefRef_t newParaRefRef = null;

            if (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag is ComObjectParameterBlock_t)
            {
                var paraRefRefItems = (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterBlock_t).Items.ToList().FindAll(x => x is ParameterRefRef_t);
                newParaRefRef = (paraRefRefItems.ToList().Find(x => (x as ParameterRefRef_t).RefId == (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_t).ParamRefId)) as ParameterRefRef_t;
            }
            else if (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag is ComObjectParameterChoose_tWhen)
            {
                var paraRefRefItems = (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterChoose_tWhen).Items.ToList().FindAll(x => x is ParameterRefRef_t);
                newParaRefRef = (paraRefRefItems.ToList().Find(x => (x as ParameterRefRef_t).RefId == (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_t).ParamRefId)) as ParameterRefRef_t;
            }
            return newParaRefRef;
        }

        public static void SetComObjectParameterChooseParameterRefId(string comObjectParameterChooseParameterRefId)
        {
            mComObjectParameterChooseGui.textCopcParamRefId.Text = comObjectParameterChooseParameterRefId;
        }
            
    }
}
