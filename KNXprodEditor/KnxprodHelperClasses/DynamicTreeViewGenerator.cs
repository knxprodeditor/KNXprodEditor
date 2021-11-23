using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnxProd.Model;

namespace knxprod_ns
{
    static class DynamicTreeViewGenerator
    {
        public static void GenerateParaKoTreeView(TreeView ParaKoTreeView)
        {
            var appNode = ParaKoTreeView.Nodes.Add("Application: " + ApplicationProgramGui.selectedApplicationProgram.Id);
            appNode.Tag = ApplicationProgramGui.selectedApplicationProgram;
            appNode.ImageIndex = (int)Images.application;
            appNode.SelectedImageIndex = (int)Images.application;
            foreach (var dynChannel in ApplicationProgramGui.selectedApplicationProgram.Dynamic) //channels
            {
                if (dynChannel is ApplicationProgramChannel_t)
                {
                    var channelNode = appNode.Nodes.Add("Channel ID: " + (dynChannel as ApplicationProgramChannel_t).Id);
                    channelNode.Tag = dynChannel;
                    channelNode.ImageIndex = (int)Images.channel;
                    channelNode.SelectedImageIndex = (int)Images.channel;
                    if((dynChannel as ApplicationProgramChannel_t).Items != null)
                    {
                        foreach (var dynChannelItem in (dynChannel as ApplicationProgramChannel_t).Items) //ParameterBlock oder choose
                        {
                            ReadChannelOrChannelIndependent(dynChannelItem, channelNode);
                        }
                    }
                }
                if (dynChannel is ChannelIndependentBlock_t)
                {
                    var channelNode = appNode.Nodes.Add("Channel Indepentent Block");//keine ID vorhanden!
                    channelNode.Tag = dynChannel;
                    channelNode.ImageIndex = (int)Images.channel;
                    channelNode.SelectedImageIndex = (int)Images.channel;
                    foreach (var dynChannelItem in (dynChannel as ChannelIndependentBlock_t).Items) //ParameterBlock oder choose
                    {
                        ReadChannelOrChannelIndependent(dynChannelItem, channelNode);
                    }
                }
            }
            // die ParameterRefRef vor oder nach ComObjectParameterChoose mit gleicher RefId 
            RemoveChooseParameterRefRef(ParaKoTreeView.TopNode);
        }


        static void ReadChannelOrChannelIndependent(object dynChannelItem, TreeNode channelNode)
        {
            if (dynChannelItem is ComObjectParameterBlock_t) //ParameterBlock
            {
                TreeViewParameterBlock((dynChannelItem as ComObjectParameterBlock_t), channelNode);
            }
            else if (dynChannelItem is ChannelChoose_t) //choose direkt unter channel
            {
                TreeViewChannelChoose((dynChannelItem as ChannelChoose_t), channelNode);
            }
            else if (dynChannelItem is ComObjectRefRef_t)
            {
                TreeViewComObjectRefRef((dynChannelItem as ComObjectRefRef_t), channelNode);
            }
        }



        /// <summary>
        /// Die Beschreibung eines ParameterRefRef auflösen und einen TreeNode anlegen
        /// </summary>
        /// <param name="paraRefRef">der zu beschriftende ParameterRefRef</param>
        /// <param name="parentNode">der TreeNode, an dem das neue Element angehängt werden soll</param>
        /// <returns></returns>
        public static TreeNode ResolveParameterRefRef(object paraRefRef, TreeNode parentNode)
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
                var ParametersRef_RefId = ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == paraRefString).RefId; //in ParameterRefs Sektion die RefID suchen
                LanguageData_tTranslationUnitTranslationElement ParaRefRef_RefIdTrans = null;
                if (ApplicationProgramGui.selectedTranslationUnitApplication.TranslationElement != null)
                {
                    //RefId kann in translations oder Parameters zu finden sein
                    ParaRefRef_RefIdTrans = ApplicationProgramGui.selectedTranslationUnitApplication.TranslationElement.ToList().Find(x => x.RefId == ParametersRef_RefId);

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
                    var ParaRefRef_RefIdParas = ApplicationProgramGui.selectedApplicationProgram.Static.Parameters.ToList().FindAll(x => x is ApplicationProgramStatic_tParameter); //in Parameter suchen
                    var ParaRefRef_RefIdPara = ParaRefRef_RefIdParas.Find(x => (x as ApplicationProgramStatic_tParameter).Id == ParametersRef_RefId);
                    if (ParaRefRef_RefIdPara != null)
                    {
                        childNode = parentNode.Nodes.Add((ParaRefRef_RefIdPara as ApplicationProgramStatic_tParameter).Name);
                        childNode.Tag = paraRefRef;
                    }
                    else
                    {
                        var ParaRefRef_RefIdUnions = ApplicationProgramGui.selectedApplicationProgram.Static.Parameters.ToList().FindAll(x => x is ApplicationProgramStatic_tUnion); //in Parameter als Union suchen
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
            if (paraRefRef is ChannelChoose_t || paraRefRef is ComObjectParameterChoose_t)
            {
                childNode.ImageIndex = (int)Images.choose;
                childNode.SelectedImageIndex = (int)Images.choose;
            }
            else
            {
                childNode.ImageIndex = (int)Images.parameter;
                childNode.SelectedImageIndex = (int)Images.parameter;
            }
            return childNode;
        }


        public static TreeNode TreeViewChannelChoose(ChannelChoose_t channelChooseItem, TreeNode parentNode)
        {
            //var childNode = parentNode.Nodes.Add("ComObject Choose ParamRedId: " + COParBlockItem.ParamRefId);
            var childNode = ResolveParameterRefRef(channelChooseItem, parentNode); //ParamRefId

            foreach (ChannelChoose_tWhen ChooseWhenItem in channelChooseItem.when)    //choose durchsuchen
            {
                TreeViewChannelChooseWhen(channelChooseItem, ChooseWhenItem, childNode);
            }
            return childNode;
        }

        public static TreeNode TreeViewChannelChooseWhen(ChannelChoose_t channelChooseItem, ChannelChoose_tWhen ChooseWhenItem, TreeNode treeNode)
        {
            string whenText = ResolveWhenText(channelChooseItem.ParamRefId, ChooseWhenItem.test, ChooseWhenItem.@default);

            var grandChildNode = treeNode.Nodes.Add(whenText);
            grandChildNode.Tag = ChooseWhenItem;
            grandChildNode.ImageIndex = (int)Images.when;
            grandChildNode.SelectedImageIndex = (int)Images.when;
            if (ChooseWhenItem.Items != null)
            {
                foreach (var whenItem in ChooseWhenItem.Items)    //when durchsuchen
                {
                    if (whenItem is ParameterRefRef_t)
                    {
                        ResolveParameterRefRef((whenItem as ParameterRefRef_t), grandChildNode);
                    }
                    else if (whenItem is ChannelChoose_t)
                    {
                        TreeViewChannelChoose(whenItem as ChannelChoose_t, grandChildNode); //Rekursive Auflösung der choose-when Bäume
                    }
                    else if (whenItem is ComObjectParameterBlock_t) //ParameterBlock unter when
                    {
                        TreeViewParameterBlock((whenItem as ComObjectParameterBlock_t), grandChildNode);
                    }
                    else if (whenItem is ComObjectRefRef_t)
                    {
                        TreeViewComObjectRefRef((whenItem as ComObjectRefRef_t), grandChildNode);
                    }
                    else
                    {
                        MessageBox.Show("Unter when konnte in einem Channel konnte ein Item nicht zugeordnet werden. Das Item hat den Typ " + whenItem.GetType().ToString(), "Channel choose-when Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return grandChildNode;
        }

        static void TreeViewParameterBlock(ComObjectParameterBlock_t comObjectParBlock, TreeNode parentNode)
        {
            TreeNode parBlockChild = ResolveParameterBlockDescription(comObjectParBlock, parentNode);

            parBlockChild.Tag = comObjectParBlock;
            parBlockChild.ImageIndex = (int)Images.comObjectParameterBlock;
            parBlockChild.SelectedImageIndex = (int)Images.comObjectParameterBlock;

            if (comObjectParBlock.Items != null)
            {
                foreach (var ParBlockItem in comObjectParBlock.Items) //ComObjectRefRef oder ParameterRefRef
                {
                    if (ParBlockItem is ComObjectRefRef_t)
                    {
                        TreeViewComObjectRefRef((ParBlockItem as ComObjectRefRef_t), parBlockChild);
                    }
                    else if (ParBlockItem is ParameterRefRef_t)
                    {
                        ResolveParameterRefRef((ParBlockItem as ParameterRefRef_t), parBlockChild);
                    }
                    else if (ParBlockItem is ParameterSeparator_t)
                    {
                        var grandChildNode = parBlockChild.Nodes.Add("Parameter Separator");
                        grandChildNode.Tag = ParBlockItem;
                        grandChildNode.ImageIndex = (int)Images.separator;
                        grandChildNode.SelectedImageIndex = (int)Images.separator;
                    }
                    else if (ParBlockItem is ComObjectParameterChoose_t)
                    {
                        TreeViewComObjectChoose((ParBlockItem as ComObjectParameterChoose_t), parBlockChild);
                    }
                    else if (ParBlockItem is Assign_t)
                    {
                        ResolveParameterRefRef((ParBlockItem as Assign_t), parBlockChild); //in ResolveParameterRefRef wird nur die TargetParamRefRef ID aufgelöst, die SourceParamRefRef ID scheint der Absprungpunkt zu sein
                    }
                    else
                    {
                        MessageBox.Show("Unter when konnte in einem ParameterBlock ein Item nicht zugeordnet werden. Das Item hat den Typ " + ParBlockItem.GetType().ToString(), "ParameterBlock Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Die Beschreibung eines ComObjectParameterBlocks für den Eintrag im TreeView herausfinden
        /// </summary>
        /// <param name="comObjectParBlock">der zu beschreibende ComObjectParameterBlock</param>
        /// <param name="parentNode">der Node, an dem dér ComObjectParameterBlock angehängt werden soll</param>
        /// <returns></returns>
        public static TreeNode ResolveParameterBlockDescription(ComObjectParameterBlock_t comObjectParBlock, TreeNode parentNode)
        {
            TreeNode parBlockChild = null;

            if (comObjectParBlock.ParamRefId != null)
            {
                parBlockChild = ResolveParameterRefRef(comObjectParBlock, parentNode); //ParamRefId
            }
            else if (comObjectParBlock.Id != null) //Sonderfall: keine RefId, nur Id vorhanden -> nicht über ParamaterRefRef auflösen
            {
                var ParaRefRef_RefIdTrans = ApplicationProgramGui.selectedTranslationUnitApplication.TranslationElement.ToList().Find(x => x.RefId == comObjectParBlock.Id);
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

        public static TreeNode TreeViewComObjectRefRef(ComObjectRefRef_t comObjectRefRefItem, TreeNode parentNode)
        {
            ComObjectRef_t comObjectsRef = ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs.ToList().Find(x => x.Id == comObjectRefRefItem.RefId);
            ComObject_t comObject = ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectTable.ComObject.ToList().Find(x => x.Id == comObjectsRef.RefId);
            TreeNode childNode = parentNode.Nodes.Add("ComObject " + comObject.Number.ToString() + " : " + comObject.Name + " (" + comObject.FunctionText + ")");
            childNode.Tag = comObjectRefRefItem;
            childNode.ImageIndex = (int)Images.comObject;
            childNode.SelectedImageIndex = (int)Images.comObject;
            return childNode;
        }


        public static TreeNode TreeViewComObjectChoose(ComObjectParameterChoose_t COParChooseItem, TreeNode parentNode)
        {
            //var childNode = parentNode.Nodes.Add("ComObject Choose ParamRedId: " + COParChooseItem.ParamRefId);
            var childNode = ResolveParameterRefRef(COParChooseItem, parentNode); //ParamRefId
            if (COParChooseItem.when != null)
            {
                foreach (ComObjectParameterChoose_tWhen ChooseWhenItem in COParChooseItem.when)    //choose durchsuchen
                {
                    TreeViewComObjectChooseWhen(COParChooseItem, ChooseWhenItem, childNode);
                }
            }
            return childNode;
        }

        public static TreeNode TreeViewComObjectChooseWhen(ComObjectParameterChoose_t COParChooseItem, ComObjectParameterChoose_tWhen ChooseWhenItem, TreeNode treeNode)
        {
            string whenText = ResolveWhenText(COParChooseItem.ParamRefId, ChooseWhenItem.test, ChooseWhenItem.@default);

            var grandChildNode = treeNode.Nodes.Add(whenText);
            grandChildNode.Tag = ChooseWhenItem;
            grandChildNode.ImageIndex = (int)Images.when;
            grandChildNode.SelectedImageIndex = (int)Images.when;
            if (ChooseWhenItem.Items != null)
            {
                foreach (var whenItem in ChooseWhenItem.Items)    //when durchsuchen
                {
                    if (whenItem is ParameterRefRef_t)
                    {
                        ResolveParameterRefRef((whenItem as ParameterRefRef_t), grandChildNode); //RefId
                    }
                    else if (whenItem is ComObjectParameterChoose_t)
                    {
                        TreeViewComObjectChoose(whenItem as ComObjectParameterChoose_t, grandChildNode); //Rekursive Auflösung der choose-when Bäume
                    }
                    else if (whenItem is ComObjectParameterBlock_t)
                    {
                        TreeViewParameterBlock(whenItem as ComObjectParameterBlock_t, grandChildNode);
                        //var childChildNode = grandChildNode.Nodes.Add("ComObjectParameterBlock Id: " + (whenItem as ComObjectParameterBlock_t).Id);
                        //childChildNode.Tag = whenItem;
                    }
                    else if (whenItem is ParameterSeparator_t)
                    {
                        var grandGrandChildNode = grandChildNode.Nodes.Add("Parameter Separator");
                        grandGrandChildNode.Tag = whenItem;
                    }
                    else if (whenItem is Assign_t)
                    {
                        ResolveParameterRefRef((whenItem as Assign_t), grandChildNode);
                    }
                    else if (whenItem is ComObjectRefRef_t)
                    {
                        TreeViewComObjectRefRef((whenItem as ComObjectRefRef_t), grandChildNode);
                    }
                    else
                    {
                        MessageBox.Show("Unter when konnte in einem ComObject ein Item nicht zugeordnet werden. Das Item hat den Typ " + whenItem.GetType().ToString(), "ComObject when Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return grandChildNode;
        }


        public static string ResolveWhenText(string ParamRefId, string testValue, bool defaultValue)
        {
            string whenText = "";
            if (testValue == null || testValue == "")
            {
                if (defaultValue == true)
                {
                    whenText = "default = true";
                }
                else if (defaultValue == false)
                {
                    whenText = "default = false";
                }
            }
            else
            {
                string paraType = "";
                ParameterRef_t parRef = ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == ParamRefId);
                foreach (var param in ApplicationProgramGui.selectedApplicationProgram.Static.Parameters)
                {
                    if (param is ApplicationProgramStatic_tParameter)
                    {
                        if ((param as ApplicationProgramStatic_tParameter).Id == parRef.RefId)
                        {
                            paraType = (param as ApplicationProgramStatic_tParameter).ParameterType;
                            break;
                        }
                    }
                    else if (param is ApplicationProgramStatic_tUnion)
                    {
                        UnionParameter_t unionParam = (param as ApplicationProgramStatic_tUnion).Parameter.ToList().Find(x => x.Id == parRef.RefId);
                        if (unionParam != null)
                        {
                            paraType = unionParam.ParameterType;
                            break;
                        }
                    }
                }
                ParameterType_t paramType = ApplicationProgramGui.selectedApplicationProgram.Static.ParameterTypes.ToList().Find(x => x.Id == paraType);

                whenText = resolveParamTypeForWhenText(paramType, testValue, defaultValue);
            }
            return whenText;
        }

        public static string resolveParamTypeForWhenText(ParameterType_t paramType, string testValue, bool defaultValue)
        {
            string whenText = "When: ";

            if (paramType.Item is ParameterType_tTypeRestriction)
            {
                ParameterType_tTypeRestrictionEnumeration enumeration = null;
                enumeration = (paramType.Item as ParameterType_tTypeRestriction).Enumeration.ToList().Find(x => x.Value.ToString() == testValue);
                if (enumeration != null)
                {
                    LanguageData_tTranslationUnitTranslationElement paramText = ApplicationProgramGui.selectedTranslationUnitApplication.TranslationElement.ToList().Find(x => x.RefId == enumeration.Id);
                    foreach (LanguageData_tTranslationUnitTranslationElementTranslation translation in paramText.Translation)
                    {
                        whenText += testValue + ": " + translation.Text;
                    }
                }
            }
            else if (paramType.Item is ParameterType_tTypeNumber)
            {
                whenText = " TypeNumber: " + (paramType.Item as ParameterType_tTypeNumber).Type;
            }
            else
            {
                MessageBox.Show("Es konnte die Parameter-Auflösung für einen When-Fall nicht ermittelt werden. Der Parameter hat den Typ " + paramType.Item.GetType().ToString(), "When Parameter-Auflösung Fehler",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return whenText;
        }


        

        /// <summary>
        /// Die ParameterRefRef, die mit gleicher RarameterRef Id vor oder nach einem ComObjectParameterChoose in der Dynamic Section stehen, sollen nicht im TreeView angezeigt werden
        /// Alle TreeView Elemente werden rekursiv durchlaufen und entsprechende ParameterRefRef gelöscht
        /// </summary>
        /// <param name="parentNode">Den ersten TreeNode des TreeView</param>
        private static void RemoveChooseParameterRefRef(TreeNode parentNode)
        {
            if (parentNode != null)
            {
                foreach (TreeNode childNode in parentNode.Nodes)
                {
                    if (childNode != null)
                    {
                        if (childNode.Tag is ComObjectParameterChoose_t)
                        {
                            foreach (TreeNode node in parentNode.Nodes)
                            {
                                if (node != null)
                                {
                                    if (node.Tag is ParameterRefRef_t)
                                    {
                                        if ((node.Tag as ParameterRefRef_t).RefId == (childNode.Tag as ComObjectParameterChoose_t).ParamRefId)
                                        {
                                            parentNode.Nodes.Remove(node);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    RemoveChooseParameterRefRef(childNode); // rekursiv alle Elemente durchlaufen
                }
            }
        }

    }
}
