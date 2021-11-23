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
    public partial class ComObjectParameterBlockGui : UserControl
    {
        private static ComObjectParameterBlockGui mComObjectParameterBlockGui;

        public ComObjectParameterBlockGui()
        {
            InitializeComponent();
            mComObjectParameterBlockGui = this;
        }

        /*****************************************************************************************************************/
        // ComObjectParameterBlock View

        public static ParameterRef_t FillComObjectParameterBlockPage(ComObjectParameterBlock_t comObjParBlock)
        {
            // ToDo: rows und colums werden nicht beachtet!

            ParameterRef_t returnParameterRef = ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == comObjParBlock.ParamRefId);

            DynamicTabControl.AddComObjectParameterBlockTab();
            mComObjectParameterBlockGui.textCopbId.Text = comObjParBlock.Id;
            mComObjectParameterBlockGui.textCopbName.Text = comObjParBlock.Name;
            mComObjectParameterBlockGui.textCopbText.Text = comObjParBlock.Text;
            mComObjectParameterBlockGui.comboBoxCopbAccess.SelectedItem = HandleKnxDataTypes.ReadKNXType(comObjParBlock.Access);
            mComObjectParameterBlockGui.numericCopbHelpTopic.Value = comObjParBlock.HelpTopic;
            mComObjectParameterBlockGui.textCopbInternalDescription.Text = comObjParBlock.InternalDescription;
            mComObjectParameterBlockGui.textCopbParamRefId.Text = comObjParBlock.ParamRefId;
            mComObjectParameterBlockGui.textCopbTextParemeterRefId.Text = comObjParBlock.TextParameterRefId;
            mComObjectParameterBlockGui.checkBoxCopbInline.Checked = comObjParBlock.Inline;
            mComObjectParameterBlockGui.comboBoxCopbLayout.SelectedItem = HandleKnxDataTypes.ReadKNXType(comObjParBlock.Layout);
            mComObjectParameterBlockGui.textCopbCell.Text = comObjParBlock.Cell;
            mComObjectParameterBlockGui.textCopbIcon.Text = comObjParBlock.Icon;
            mComObjectParameterBlockGui.textCopbHelpContext.Text = comObjParBlock.HelpContext;
            mComObjectParameterBlockGui.checkBoxCopbShowInComObjectTree.Checked = comObjParBlock.ShowInComObjectTree;
            mComObjectParameterBlockGui.textCopbSemantics.Text = comObjParBlock.Semantics;
            LanguageProcessing.FillLanguageDataGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, mComObjectParameterBlockGui.dgvCopbTranslationsText, comObjParBlock.Id, "Text");
            return returnParameterRef;
        }

        /*****************************************************************************************************************/
        // ComObjectParameterBlock Edit
        // ComObjectParameterBlock Behandlung


        private void buttonSaveParCoComObjectParameterBlock_Click(object sender, EventArgs e)
        {
            ComObjectParameterBlock_t comObjectParBlock = DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterBlock_t;

            comObjectParBlock.Name = Extensions.NullIfEmpty(textCopbName.Text);
            comObjectParBlock.Text = Extensions.NullIfEmpty(textCopbText.Text);
            comObjectParBlock.Access = HandleGuiDataTypes.ReadAccess(comboBoxCopbAccess.SelectedItem?.ToString() ?? "");
            comObjectParBlock.HelpTopic = (uint)numericCopbHelpTopic.Value;
            comObjectParBlock.InternalDescription = Extensions.NullIfEmpty(textCopbInternalDescription.Text);
            comObjectParBlock.ParamRefId = Extensions.NullIfEmpty(textCopbParamRefId.Text);
            comObjectParBlock.TextParameterRefId = Extensions.NullIfEmpty(textCopbTextParemeterRefId.Text);
            comObjectParBlock.Inline = checkBoxCopbInline.Checked;
            comObjectParBlock.Layout = HandleGuiDataTypes.ReadParameterBlockLayout(comboBoxCopbLayout.SelectedItem?.ToString() ?? "");
            comObjectParBlock.Cell = Extensions.NullIfEmpty(textCopbCell.Text);
            comObjectParBlock.Icon = Extensions.NullIfEmpty(textCopbIcon.Text);
            comObjectParBlock.HelpContext = Extensions.NullIfEmpty(textCopbHelpContext.Text);
            comObjectParBlock.ShowInComObjectTree = checkBoxCopbShowInComObjectTree.Checked;
            comObjectParBlock.Semantics = Extensions.NullIfEmpty(textCopbSemantics.Text);

            if (comObjectParBlock.Id != textCopbId.Text)
            {
                //bevor die ID geändert wird muss erst auch die Translation ID angepasst werden!
                LanguageProcessing.ChangeTansElementRefId(ApplicationProgramGui.selectedApplicationManufacturer.Languages, textCopbId.Text, comObjectParBlock.Id);
            }

            // die Übersetzungen in die Translations schreiben
            LanguageProcessing.WriteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvCopbTranslationsText, comObjectParBlock.Id, "Text", ApplicationProgramGui.selectedApplicationProgram.Id);

            DynamicTreeViewGui.SetSelectedNodeName(comObjectParBlock.Name);
        }

        // speichern des Parameters für den ComObjectParameterBlock
        public static void ComObjectParameterBlockSave(ComboBox comboBoxParaCollection)
        {
            int highestId = FindComObjectParameterBlockIds();
            bool newItem = false;
            TreeNode channelChild = new TreeNode();

            ParameterRef_t newParaRef = new ParameterRef_t();
            ComObjectParameterBlock_t newComObjectParBlock = new ComObjectParameterBlock_t();
            int nextParameterRefNumber = AppParameterGui.HighestUsedParaRefNumber() + 1;

            if ((DynamicTreeViewGui.GetSelectedTreeNode().Tag is ComObjectParameterBlock_t) && (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterBlock_t).Id == mComObjectParameterBlockGui.textCopbId.Text) // ein bestehender ParameterType Eintrag wurde verändert
            {
                newComObjectParBlock = DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterBlock_t;
                newParaRef = ObjectFunctions.DeepClone(ParameterRefGui.GetParameterRefIdItem().Tag as ParameterRef_t);

                // Überprüfen, ob alter ParameterRef noch verwendet wird, sonst löschen (neuer ParameterRef wird erzeugt)
                if (DynamicSectionSearch.SearchIdInDynamic((DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterBlock_t).ParamRefId) <= 1)
                {
                    int index = 0;
                    foreach (ParameterRef_t paraRef in ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs)
                    {
                        if (paraRef.Id == (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterBlock_t).ParamRefId)
                        {
                            ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs = HandleArrayFunctions.Delete(ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs, index);
                            index--;
                        }
                        index++;
                    }
                }
            }
            else // ein neuer Eintrag wurde erzeugt und dieser wird als Kind vom angewählten Element angehängt
            {
                newItem = true;
                newComObjectParBlock.Id = ApplicationProgramGui.selectedApplicationProgram.Id + "_PB-" + (highestId + 1).ToString();

                // je nachdem an welchem Element das neue ComObjectRefRef angehängt werden soll muss korrekt gehandelt werden
                DynamicSectionEdit.AddToItemsObject(DynamicTreeViewGui.GetSelectedTreeNode().Tag, newComObjectParBlock);

                newComObjectParBlock.Items = new object[0];
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

            // der Tag ist immer die RarameterRef Nummer
            newParaRef.Tag = nextParameterRefNumber.ToString();

            // den Namen des Parameters bereitstellen
            string paraName = "";
            if ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag is ApplicationProgramStatic_tParameter)
            {
                paraName = ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag as ApplicationProgramStatic_tParameter).Name;
            }
            else if ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag is UnionParameter_t)
            {
                paraName = ((comboBoxParaCollection.SelectedItem as ComboBoxItem).Tag as UnionParameter_t).Name;
            }

            newComObjectParBlock.Name = paraName;
            newComObjectParBlock.ParamRefId = newParaRef.Id;

            if (newItem)
            {
                channelChild = DynamicTreeViewGenerator.ResolveParameterBlockDescription(newComObjectParBlock, DynamicTreeViewGui.GetSelectedTreeNode()); // neuen TreeNode anlegen
                channelChild.Tag = newComObjectParBlock;
                channelChild.ImageIndex = (int)Images.comObjectParameterBlock;
                channelChild.SelectedImageIndex = (int)Images.comObjectParameterBlock;

                DynamicTreeViewGui.SetSelectedTreeNode(channelChild);
            }
            else
            {
                // Missbrauchen der View Methode, es wird ein Dummy TreeNode erzeugt, um den Text zu entnehmen
                DynamicTreeViewGui.GetSelectedTreeNode().Text = DynamicTreeViewGenerator.ResolveParameterBlockDescription(newComObjectParBlock, new TreeNode()).Text;
                DynamicTabControl.RemoveAllParamsCoTabs();
                DynamicTreeViewGui.FillParameterOrUnionPage(ParameterRefGui.FillParameterRefPage(FillComObjectParameterBlockPage(newComObjectParBlock)));
                //ParaKoTreeView.SelectedNode.Text = paraName;
            }
        }

        public static void ClearComObjectParameterBlockPage()
        {
            mComObjectParameterBlockGui.textCopbId.Text = "";
            mComObjectParameterBlockGui.textCopbName.Text = "";
            mComObjectParameterBlockGui.textCopbText.Text = "";
            mComObjectParameterBlockGui.comboBoxCopbAccess.SelectedIndex = -1;
            mComObjectParameterBlockGui.numericCopbHelpTopic.Value = 0;
            mComObjectParameterBlockGui.textCopbInternalDescription.Text = "";
            mComObjectParameterBlockGui.textCopbParamRefId.Text = "";
            mComObjectParameterBlockGui.textCopbTextParemeterRefId.Text = "";
            mComObjectParameterBlockGui.checkBoxCopbInline.Checked = false;
            mComObjectParameterBlockGui.comboBoxCopbLayout.SelectedIndex = -1;
            mComObjectParameterBlockGui.textCopbCell.Text = "";
            mComObjectParameterBlockGui.textCopbIcon.Text = "";
            mComObjectParameterBlockGui.textCopbHelpContext.Text = "";
            mComObjectParameterBlockGui.checkBoxCopbShowInComObjectTree.Checked = false;
            mComObjectParameterBlockGui.textCopbSemantics.Text = "";
        }


        public static void SetComObjectParameterBlockParameterRefId(string comObjectParameterBlockParameterRefId)
        {
            mComObjectParameterBlockGui.textCopbParamRefId.Text = comObjectParameterBlockParameterRefId;
        }


        public static int FindComObjectParameterBlockIds()
        {
            List<string> usedComObjParBlockIds = new List<string>();
            List<int> usedComObjParBlockIdNumbers = new List<int>();
            foreach (var dynChannel in ApplicationProgramGui.selectedApplicationProgram.Dynamic) //channels
            {
                if (dynChannel is ApplicationProgramChannel_t)
                {
                    if ((dynChannel as ApplicationProgramChannel_t).Items != null)
                    {
                        foreach (var dynChannelItem in (dynChannel as ApplicationProgramChannel_t).Items) //ParameterBlock oder choose
                        {
                            ReadComObjectParameterBlockIdsFromChannelOrChannelIndependent(dynChannelItem, usedComObjParBlockIds);
                        }
                    }
                }
                if (dynChannel is ChannelIndependentBlock_t)
                {
                    if ((dynChannel as ChannelIndependentBlock_t).Items != null)
                    {
                        foreach (var dynChannelItem in (dynChannel as ChannelIndependentBlock_t).Items) //ParameterBlock oder choose
                        {
                            ReadComObjectParameterBlockIdsFromChannelOrChannelIndependent(dynChannelItem, usedComObjParBlockIds);
                        }
                    }
                }
            }
            foreach (string usedComObjParBlockId in usedComObjParBlockIds)
            {
                usedComObjParBlockIdNumbers.Add(int.Parse(usedComObjParBlockId.Remove(0, ApplicationProgramGui.selectedApplicationProgram.Id.Length + 4)));
            }
            usedComObjParBlockIdNumbers.Sort();
            int highestUsedComObjParBlockIdNumber = 0;
            if (usedComObjParBlockIdNumbers.Count > 0)
            {
                highestUsedComObjParBlockIdNumber = usedComObjParBlockIdNumbers[usedComObjParBlockIdNumbers.Count - 1];
            }
            return highestUsedComObjParBlockIdNumber;
        }


        static void ReadComObjectParameterBlockIdsFromChannelOrChannelIndependent(object dynChannelItem, List<string> usedComObjParBlockIds)
        {
            if (dynChannelItem is ComObjectParameterBlock_t) //ParameterBlock
            {
                if (!usedComObjParBlockIds.Contains((dynChannelItem as ComObjectParameterBlock_t).Id))
                {
                    usedComObjParBlockIds.Add((dynChannelItem as ComObjectParameterBlock_t).Id);
                }
                if ((dynChannelItem as ComObjectParameterBlock_t).Items != null)
                {
                    foreach (var parameterBlockItem in (dynChannelItem as ComObjectParameterBlock_t).Items) //ParameterRefRef oder choose
                    {
                        if (parameterBlockItem is ComObjectParameterChoose_t) //choose im ParameterBlock
                        {
                            ReadComObjectParameterBlockIdsFromComObjectChooseWhen((parameterBlockItem as ComObjectParameterChoose_t), usedComObjParBlockIds);
                        }
                    }
                }
            }
            if (dynChannelItem is ChannelChoose_t) //choose direkt unter channel
            {
                ReadComObjectParameterBlockIdsFromChannelChooseWhen(dynChannelItem as ChannelChoose_t, usedComObjParBlockIds);
            }
        }


        static void ReadComObjectParameterBlockIdsFromChannelChooseWhen(ChannelChoose_t channelChooseItem, List<string> usedComObjParBlockIds)
        {
            foreach (var ChooseWhenItem in channelChooseItem.when)    //choose durchsuchen
            {
                foreach (var whenItem in ChooseWhenItem.Items)    //when durchsuchen
                {
                    if (whenItem is ChannelChoose_t)
                    {
                        ReadComObjectParameterBlockIdsFromChannelChooseWhen(whenItem as ChannelChoose_t, usedComObjParBlockIds); //Rekursive Auflösung der choose-when Bäume
                    }
                    else if (whenItem is ComObjectParameterBlock_t) //ParameterBlock unter when
                    {
                        ReadComObjectParameterBlockIdsFromWhenParameterBlock((whenItem as ComObjectParameterBlock_t), usedComObjParBlockIds);
                    }
                }
            }
        }

        static void ReadComObjectParameterBlockIdsFromWhenParameterBlock(ComObjectParameterBlock_t comObjectParBlock, List<string> usedComObjParBlockIds)
        {
            if (!usedComObjParBlockIds.Contains(comObjectParBlock.Id))
            {
                usedComObjParBlockIds.Add(comObjectParBlock.Id);
            }
            if (comObjectParBlock.Items != null)
            {
                foreach (var ParBlockItem in comObjectParBlock.Items) //ComObjectRefRef oder ParameterRefRef
                {
                    if (ParBlockItem is ComObjectParameterChoose_t)
                    {
                        ReadComObjectParameterBlockIdsFromComObjectChooseWhen(ParBlockItem as ComObjectParameterChoose_t, usedComObjParBlockIds);
                    }
                }
            }
        }


        static void ReadComObjectParameterBlockIdsFromComObjectChooseWhen(ComObjectParameterChoose_t COParChooseItem, List<string> usedComObjParBlockIds)
        {
            foreach (var ChooseWhenItem in COParChooseItem.when)    //choose durchsuchen
            {
                if (ChooseWhenItem.Items != null)
                {
                    foreach (var whenItem in ChooseWhenItem.Items)    //when durchsuchen
                    {
                        if (whenItem is ComObjectParameterChoose_t)
                        {
                            ReadComObjectParameterBlockIdsFromComObjectChooseWhen(whenItem as ComObjectParameterChoose_t, usedComObjParBlockIds); //Rekursive Auflösung der choose-when Bäume
                        }
                        else if (whenItem is ComObjectParameterBlock_t)
                        {
                            ReadComObjectParameterBlockIdsFromWhenParameterBlock(whenItem as ComObjectParameterBlock_t, usedComObjParBlockIds);
                        }
                    }
                }
            }
        }
    }
}
