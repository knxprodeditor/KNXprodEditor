using KnxProd.Model;
using System.Linq;
using System.Windows.Forms;

namespace knxprod_ns
{
    class DynamicSectionEdit
    {
        /// <summary>
        /// Alle Referenzen kontrollieren und falls nicht mehr benötigt nicht löschen
        /// </summary>
        public static void CleanAllDyamicReferences()
        {
            int index = 0;
            foreach (ParameterRef_t paraRef in ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs)
            {
                // löschen des ParameterRef aus ParameterRefs Tabelle, wenn der ParameterRef nicht mehr benötigt wird
                if (DynamicSectionSearch.SearchIdInDynamic(paraRef.Id) == 0)
                {
                    ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs = HandleArrayFunctions.Delete(ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs, index);
                    index--;
                }
                LanguageProcessing.DeleteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, paraRef.Id);
                index++;
            }
            index = 0;
            foreach (ComObjectRef_t comObjRef in ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs)
            {
                if (DynamicSectionSearch.SearchIdInDynamic(comObjRef.Id) == 0)
                {
                    ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs = HandleArrayFunctions.Delete(ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs, index);
                    index--;
                }
                LanguageProcessing.DeleteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, comObjRef.Id);
                index++;
            }
        }

        /// <summary>
        /// löscht ein Object aus dem ParaKoTreeView und dem entsprechendem Array
        /// </summary>
        /// <param name="objectWithItems">das Object, an dem das zu löschende Objekt in dem Items Feld zu finden ist</param>
        /// <param name="deleteObject">das zu löschende Objekt</param>
        /// <param name="treeView">der TreeView, in dem das zu löschende Objekt aufgeführt ist</param>
        public static void DeleteItemsFromDynamicAndTreeView(object dynamicWithItems, object deleteObject, TreeView treeView)
        {
            int index = 0;
            if (dynamicWithItems is ApplicationProgram_t)
            {
                foreach (object item in (dynamicWithItems as ApplicationProgram_t).Dynamic)
                {
                    if (item == deleteObject)
                    {
                        (dynamicWithItems as ApplicationProgram_t).Dynamic =
                            TreeViewArrayFunctions.DeleteArrayTreeView((dynamicWithItems as ApplicationProgram_t).Dynamic, index, treeView);
                        break;
                    }
                    index++;
                }
            }
        }

        /// <summary>
        /// Es werden sämtliche passende IDs gesucht und das Element gelöscht (mit allen eventuell vorhandenen angehängten Knoten)
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteIdOfDynamic(string id)
        {
            int index = 0;
            foreach (var dynChannel in ApplicationProgramGui.selectedApplicationProgram.Dynamic) //channels
            {
                if (dynChannel is ApplicationProgramChannel_t)
                {
                    if((dynChannel as ApplicationProgramChannel_t).Id == id){
                        ApplicationProgramGui.selectedApplicationProgram.Dynamic = TreeViewArrayFunctions.DeleteArrayTreeView(ApplicationProgramGui.selectedApplicationProgram.Dynamic, index, DynamicTreeViewGui.GetTreeView());
                    }
                    int appProgChannelIndex = 0;
                    foreach (var dynChannelItem in (dynChannel as ApplicationProgramChannel_t).Items) //ParameterBlock oder choose
                    {
                        (dynChannel as ApplicationProgramChannel_t).Items = DeleteChannelOrChannelIndependent((dynChannel as ApplicationProgramChannel_t).Items, dynChannelItem, id, appProgChannelIndex);
                        appProgChannelIndex++;
                    }
                }
                if (dynChannel is ChannelIndependentBlock_t)
                {
                    int chaIndBlockIndex = 0;
                    foreach (var dynChannelItem in (dynChannel as ChannelIndependentBlock_t).Items) //ParameterBlock oder choose
                    {
                        (dynChannel as ChannelIndependentBlock_t).Items = DeleteChannelOrChannelIndependent((dynChannel as ChannelIndependentBlock_t).Items, dynChannelItem, id, chaIndBlockIndex);
                        chaIndBlockIndex++;
                    }
                }
                index++;
            }
            CleanAllDyamicReferences();
        }


        static object[] DeleteChannelOrChannelIndependent(object[] dynChannelItems, object dynChannelItem, string id, int index)
        {
            object[] returnDynChannelItems = new object[0];
            if (dynChannelItem is ComObjectParameterBlock_t) //ParameterBlock
            {
                if ((dynChannelItem as ComObjectParameterBlock_t).ParamRefId == id)
                {
                    returnDynChannelItems = TreeViewArrayFunctions.DeleteArrayTreeView(dynChannelItems, index, DynamicTreeViewGui.GetTreeView());
                }
                else
                {
                    returnDynChannelItems = dynChannelItems;
                    int comObjParBlockIndex = 0;
                    if((dynChannelItem as ComObjectParameterBlock_t).Items != null){
                        foreach (var parameterBlockItem in (dynChannelItem as ComObjectParameterBlock_t).Items) //ParameterRefRef oder choose
                        {
                            if (parameterBlockItem is ParameterRefRef_t) //ParameterRefRef im ParameterBlock
                            {
                                if ((parameterBlockItem as ParameterRefRef_t).RefId == id)
                                {
                                    (dynChannelItem as ComObjectParameterBlock_t).Items = TreeViewArrayFunctions.DeleteArrayTreeView((dynChannelItem as ComObjectParameterBlock_t).Items, comObjParBlockIndex, DynamicTreeViewGui.GetTreeView());
                                    comObjParBlockIndex--;
                                }
                            }
                            if (parameterBlockItem is ComObjectParameterChoose_t) //choose im ParameterBlock
                            {
                                if ((parameterBlockItem as ComObjectParameterChoose_t).ParamRefId == id)
                                {
                                    (dynChannelItem as ComObjectParameterBlock_t).Items = TreeViewArrayFunctions.DeleteArrayTreeView((dynChannelItem as ComObjectParameterBlock_t).Items, comObjParBlockIndex, DynamicTreeViewGui.GetTreeView());
                                    comObjParBlockIndex--;
                                }
                                else
                                {
                                    DeleteComObjectChooseWhen((parameterBlockItem as ComObjectParameterChoose_t), id);
                                }
                            }
                            if(parameterBlockItem is ComObjectRefRef_t)
                            {
                                if((parameterBlockItem as ComObjectRefRef_t).RefId == id)
                                {
                                    (dynChannelItem as ComObjectParameterBlock_t).Items = TreeViewArrayFunctions.DeleteArrayTreeView((dynChannelItem as ComObjectParameterBlock_t).Items, comObjParBlockIndex, DynamicTreeViewGui.GetTreeView());
                                    comObjParBlockIndex--;
                                }
                            }
                            comObjParBlockIndex++;
                        }
                    }
                }
            }
            else if (dynChannelItem is ChannelChoose_t) //choose direkt unter channel
            {
                if((dynChannelItem as ChannelChoose_t).ParamRefId == id)
                {
                    returnDynChannelItems = TreeViewArrayFunctions.DeleteArrayTreeView(dynChannelItems, index, DynamicTreeViewGui.GetTreeView());
                }
                else
                {
                    returnDynChannelItems = dynChannelItems;
                    DeleteChannelChooseWhen((dynChannelItem as ChannelChoose_t), id);
                }               
            }
            else if(dynChannelItem is ComObjectRefRef_t)
            {
                if((dynChannelItem as ComObjectRefRef_t).RefId == id)
                {
                    returnDynChannelItems = TreeViewArrayFunctions.DeleteArrayTreeView(dynChannelItems, index, DynamicTreeViewGui.GetTreeView());
                }
            }
            else
            {
                returnDynChannelItems = dynChannelItems;
            }
            return returnDynChannelItems;
        }


        private static void DeleteChannelChooseWhen(ChannelChoose_t channelChooseItem, string id)
        {
            foreach (var ChooseWhenItem in channelChooseItem.when)    //choose durchsuchen
            {
                int index = 0;
                foreach (var whenItem in ChooseWhenItem.Items)    //when durchsuchen
                {
                    if (whenItem is ParameterRefRef_t)
                    {
                        if((whenItem as ParameterRefRef_t).RefId == id)
                        {
                            ChooseWhenItem.Items = TreeViewArrayFunctions.DeleteArrayTreeView(ChooseWhenItem.Items, index, DynamicTreeViewGui.GetTreeView());
                            index--;
                        }
                    }
                    else if (whenItem is ChannelChoose_t)
                    {
                        if ((whenItem as ChannelChoose_t).ParamRefId == id)
                        {
                            ChooseWhenItem.Items = TreeViewArrayFunctions.DeleteArrayTreeView(ChooseWhenItem.Items, index, DynamicTreeViewGui.GetTreeView());
                        }
                        else
                        {
                            DeleteChannelChooseWhen(whenItem as ChannelChoose_t, id); //Rekursive Auflösung der choose-when Bäume
                        }
                    }
                    else if (whenItem is ComObjectParameterBlock_t) //ParameterBlock unter when
                    {
                        if ((whenItem as ComObjectParameterBlock_t).ParamRefId == id || (whenItem as ComObjectParameterBlock_t).Id == id)
                        {
                            ChooseWhenItem.Items = TreeViewArrayFunctions.DeleteArrayTreeView(ChooseWhenItem.Items, index, DynamicTreeViewGui.GetTreeView());
                            index--;
                        }
                        else
                        {
                            DeleteWhenParameterBlock((whenItem as ComObjectParameterBlock_t), id);
                        }
                    }
                    else if (whenItem is ComObjectRefRef_t)
                    {
                        if ((whenItem as ComObjectRefRef_t).RefId == id)
                        {
                            ChooseWhenItem.Items = TreeViewArrayFunctions.DeleteArrayTreeView(ChooseWhenItem.Items, index, DynamicTreeViewGui.GetTreeView());
                            index--;
                        }
                    }        
                    index++;
                }
            }
        }

       

        private static void DeleteWhenParameterBlock(ComObjectParameterBlock_t comObjectParBlock, string id)
        {
            if (comObjectParBlock.Items != null)
            {
                int index = 0;
                foreach (var ParBlockItem in comObjectParBlock.Items) //ComObjectRefRef oder ParameterRefRef
                {
                    if (ParBlockItem is ComObjectRefRef_t)
                    {
                        if((ParBlockItem as ComObjectRefRef_t).RefId == id)
                        {
                            comObjectParBlock.Items = TreeViewArrayFunctions.DeleteArrayTreeView(comObjectParBlock.Items, index, DynamicTreeViewGui.GetTreeView());
                            index--;
                        }
                    }
                    else if (ParBlockItem is ParameterRefRef_t)
                    {
                        if ((ParBlockItem as ParameterRefRef_t).RefId == id)
                        {
                            comObjectParBlock.Items = TreeViewArrayFunctions.DeleteArrayTreeView(comObjectParBlock.Items, index, DynamicTreeViewGui.GetTreeView());
                            index--;
                        }
                    }
                    else if (ParBlockItem is ParameterSeparator_t)
                    {
                        if ((ParBlockItem as ParameterSeparator_t).Id == id)
                        {
                            comObjectParBlock.Items = TreeViewArrayFunctions.DeleteArrayTreeView(comObjectParBlock.Items, index, DynamicTreeViewGui.GetTreeView());
                            index--;
                        }
                    }
                    else if (ParBlockItem is ComObjectParameterChoose_t)
                    {
                        if((ParBlockItem as ComObjectParameterChoose_t).ParamRefId == id)
                        {
                            comObjectParBlock.Items = TreeViewArrayFunctions.DeleteArrayTreeView(comObjectParBlock.Items, index, DynamicTreeViewGui.GetTreeView());
                            index--;
                        }
                        else
                        {
                            DeleteComObjectChooseWhen((ParBlockItem as ComObjectParameterChoose_t), id);
                        }
                        
                    }
                    else if (ParBlockItem is Assign_t)
                    {
                        if ((ParBlockItem as Assign_t).SourceParamRefRef == id || (ParBlockItem as Assign_t).TargetParamRefRef == id)
                        {
                            comObjectParBlock.Items = TreeViewArrayFunctions.DeleteArrayTreeView(comObjectParBlock.Items, index, DynamicTreeViewGui.GetTreeView());
                            index--;
                        }
                    }
                    index++;
                }
            }
        }

        private static void DeleteComObjectChooseWhen(ComObjectParameterChoose_t COParChooseItem, string id)
        {
            foreach (var ChooseWhenItem in COParChooseItem.when)    //choose durchsuchen
            {
                if (ChooseWhenItem.Items != null)
                {
                    int index = 0;
                    foreach (var whenItem in ChooseWhenItem.Items)    //when durchsuchen
                    {
                        if (whenItem is ParameterRefRef_t)
                        {
                            if((whenItem as ParameterRefRef_t).RefId == id)
                            {
                                ChooseWhenItem.Items = TreeViewArrayFunctions.DeleteArrayTreeView(ChooseWhenItem.Items, index, DynamicTreeViewGui.GetTreeView());
                                index--;
                            }
                        }
                        else if (whenItem is ComObjectParameterChoose_t)
                        {
                            if ((whenItem as ComObjectParameterChoose_t).ParamRefId == id)
                            {
                                ChooseWhenItem.Items = TreeViewArrayFunctions.DeleteArrayTreeView(ChooseWhenItem.Items, index, DynamicTreeViewGui.GetTreeView());
                            }
                            else
                            {
                                DeleteComObjectChooseWhen(whenItem as ComObjectParameterChoose_t, id); //Rekursive Auflösung der choose-when Bäume
                            }
                        }
                        else if (whenItem is ComObjectParameterBlock_t)
                        {
                            if ((whenItem as ComObjectParameterBlock_t).Id == id || (whenItem as ComObjectParameterBlock_t).ParamRefId == id)
                            {
                                ChooseWhenItem.Items = TreeViewArrayFunctions.DeleteArrayTreeView(ChooseWhenItem.Items, index, DynamicTreeViewGui.GetTreeView());
                                index--;
                            }
                        }
                        else if (whenItem is ParameterSeparator_t)
                        {
                            if ((whenItem as ParameterSeparator_t).Id == id)
                            {
                                ChooseWhenItem.Items = TreeViewArrayFunctions.DeleteArrayTreeView(ChooseWhenItem.Items, index, DynamicTreeViewGui.GetTreeView());
                                index--;
                            }
                        }
                        else if (whenItem is Assign_t)
                        {
                            if ((whenItem as Assign_t).TargetParamRefRef == id || (whenItem as Assign_t).SourceParamRefRef == id)
                            {
                                ChooseWhenItem.Items = TreeViewArrayFunctions.DeleteArrayTreeView(ChooseWhenItem.Items, index, DynamicTreeViewGui.GetTreeView());
                                index--;
                            }
                        }
                        else if (whenItem is ComObjectRefRef_t)
                        {
                            if ((whenItem as ComObjectRefRef_t).RefId == id)
                            {
                                ChooseWhenItem.Items = TreeViewArrayFunctions.DeleteArrayTreeView(ChooseWhenItem.Items, index, DynamicTreeViewGui.GetTreeView());
                                index--;
                            }
                        }
                        index++;
                    }
                }
            }
        }

        // Löschen eines ComObjectParameterChoose
        public static void DeleteComObjParaChooseAndReferences()
        {
            // löschen des ParameterRefRef vor dem ComObjectParameterChoose mit der gleichen ParameterRef Id
            int index = 0;
            if (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag is ApplicationProgramChannel_t)
            {
                foreach (object item in (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ApplicationProgramChannel_t).Items)
                {
                    if (item is ParameterRefRef_t)
                    {
                        if ((item as ParameterRefRef_t).RefId == (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_t).ParamRefId)
                        {
                            (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ApplicationProgramChannel_t).Items =
                                HandleArrayFunctions.Delete((DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ApplicationProgramChannel_t).Items, index);
                            index--;
                        }
                    }
                    index++;
                }
            }
            else if (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag is ChannelIndependentBlock_t)
            {
                foreach (object item in (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ChannelIndependentBlock_t).Items)
                {
                    if (item is ParameterRefRef_t)
                    {
                        if ((item as ParameterRefRef_t).RefId == (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_t).ParamRefId)
                        {
                            (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ChannelIndependentBlock_t).Items =
                                HandleArrayFunctions.Delete((DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ChannelIndependentBlock_t).Items, index);
                            index--;
                        }
                    }
                    index++;
                }
            }
            else if (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag is ChannelChoose_tWhen)
            {
                foreach (object item in (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ChannelChoose_tWhen).Items)
                {
                    if (item is ParameterRefRef_t)
                    {
                        if ((item as ParameterRefRef_t).RefId == (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_t).ParamRefId)
                        {
                            (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ChannelChoose_tWhen).Items =
                                HandleArrayFunctions.Delete((DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ChannelChoose_tWhen).Items, index);
                            index--;
                        }
                    }
                    index++;
                }
            }
            else if (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag is ComObjectParameterBlock_t)
            {
                foreach (object item in (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterBlock_t).Items)
                {
                    if (item is ParameterRefRef_t)
                    {
                        if ((item as ParameterRefRef_t).RefId == (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_t).ParamRefId)
                        {
                            (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterBlock_t).Items =
                                HandleArrayFunctions.Delete((DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterBlock_t).Items, index);
                            index--;
                        }
                    }
                    index++;
                }
            }
            else if (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag is ComObjectParameterChoose_tWhen)
            {
                foreach (object item in (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterChoose_tWhen).Items)
                {
                    if (item is ParameterRefRef_t)
                    {
                        if ((item as ParameterRefRef_t).RefId == (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectParameterChoose_t).ParamRefId)
                        {
                            (DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterChoose_tWhen).Items =
                                HandleArrayFunctions.Delete((DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag as ComObjectParameterChoose_tWhen).Items, index);
                            index--;
                        }
                    }
                    index++;
                }
            }


            // löschen des ComObjectParameterChoose aus TreeView und Dynamic Section
            DeleteItemsFromObjectAndTreeView(DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag, DynamicTreeViewGui.GetSelectedTreeNode().Tag, DynamicTreeViewGui.GetTreeView());
        }

        // einen ApplicationChannel löschen
        public static void DeleteApplicationProgramChannelAndReferences()
        {
            // löschen des ApplicationProgramChannel aus TreeView und Dynamic Section
            DeleteItemsFromDynamicAndTreeView(DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag, DynamicTreeViewGui.GetSelectedTreeNode().Tag, DynamicTreeViewGui.GetTreeView());
        }

        // einen ChannelIndependentBlock löschen
        public static void DeleteChannelIndependentBlockAndReferences()
        {
            // löschen des ChannelIndependentBlock aus TreeView und Dynamic Section
            DeleteItemsFromDynamicAndTreeView(DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag, DynamicTreeViewGui.GetSelectedTreeNode().Tag, DynamicTreeViewGui.GetTreeView());
        }

        // Löschen eines ComObjectParameterChooseWhen
        public static void DeleteComObjParaChooseWhen()
        {
            // löschen des ComObjectParameterChooseWhen aus TreeView und Dynamic Section
            DeleteWhensFromChooseAndTreeView(DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag, DynamicTreeViewGui.GetSelectedTreeNode().Tag, DynamicTreeViewGui.GetTreeView());
        }


        // Löschen eines ChannelChoose
        public static void DeleteChannelChooseAndReferences()
        {
            // löschen des ChannelChoose aus TreeView und Dynamic Section
            DeleteItemsFromObjectAndTreeView(DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag, DynamicTreeViewGui.GetSelectedTreeNode().Tag, DynamicTreeViewGui.GetTreeView());
        }

        // Löschen eines ChannelChooseWhen
        public static void DeleteChannelChooseWhen()
        {
            // löschen des ChannelChooseWhen aus TreeView und Dynamic Section
            DeleteWhensFromChooseAndTreeView(DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag, DynamicTreeViewGui.GetSelectedTreeNode().Tag, DynamicTreeViewGui.GetTreeView());
        }

        public static void DeleteComObjParameterBlock()
        {
            // löschen des ComObjectParameterBlock aus TreeView und Dynamic Section
            DeleteItemsFromObjectAndTreeView(DynamicTreeViewGui.GetSelectedTreeNode().Parent.Tag, DynamicTreeViewGui.GetSelectedTreeNode().Tag, DynamicTreeViewGui.GetTreeView());
        }

        /// <summary>
        /// löscht ein Object aus dem ParaKoTreeView und dem entsprechendem Array
        /// </summary>
        /// <param name="objectWithItems">das Object, an dem das zu löschende Objekt in dem Items Feld zu finden ist</param>
        /// <param name="deleteObject">das zu löschende Objekt</param>
        /// <param name="treeView">der TreeView, in dem das zu löschende Objekt aufgeführt ist</param>
        public static void DeleteItemsFromObjectAndTreeView(object objectWithItems, object deleteObject, TreeView treeView)
        {
            int index = 0;
            if (objectWithItems is ApplicationProgramChannel_t)
            {
                foreach (object item in (objectWithItems as ApplicationProgramChannel_t).Items)
                {
                    if (item == deleteObject)
                    {
                        (objectWithItems as ApplicationProgramChannel_t).Items =
                            TreeViewArrayFunctions.DeleteArrayTreeView((objectWithItems as ApplicationProgramChannel_t).Items, index, treeView);
                        break;
                    }
                    index++;
                }
            }
            else if (objectWithItems is ChannelIndependentBlock_t)
            {
                foreach (object item in (objectWithItems as ChannelIndependentBlock_t).Items)
                {
                    if (item == deleteObject)
                    {
                        (objectWithItems as ChannelIndependentBlock_t).Items =
                            TreeViewArrayFunctions.DeleteArrayTreeView((objectWithItems as ChannelIndependentBlock_t).Items, index, treeView);
                        break;
                    }
                    index++;
                }
            }
            else if (objectWithItems is ChannelChoose_tWhen)
            {
                foreach (object item in (objectWithItems as ChannelChoose_tWhen).Items)
                {
                    if (item == deleteObject)
                    {
                        (objectWithItems as ChannelChoose_tWhen).Items =
                            TreeViewArrayFunctions.DeleteArrayTreeView((objectWithItems as ChannelChoose_tWhen).Items, index, treeView);
                        break;
                    }
                    index++;
                }
            }
            else if (objectWithItems is ComObjectParameterBlock_t)
            {
                foreach (object item in (objectWithItems as ComObjectParameterBlock_t).Items)
                {
                    if (item == deleteObject)
                    {
                        (objectWithItems as ComObjectParameterBlock_t).Items =
                            TreeViewArrayFunctions.DeleteArrayTreeView((objectWithItems as ComObjectParameterBlock_t).Items, index, treeView);
                        break;
                    }
                    index++;
                }
            }
            else if (objectWithItems is ComObjectParameterChoose_tWhen)
            {
                foreach (object item in (objectWithItems as ComObjectParameterChoose_tWhen).Items)
                {
                    if (item == deleteObject)
                    {
                        (objectWithItems as ComObjectParameterChoose_tWhen).Items =
                            TreeViewArrayFunctions.DeleteArrayTreeView((objectWithItems as ComObjectParameterChoose_tWhen).Items, index, treeView);
                        break;
                    }
                    index++;
                }
            }
            // alle nach dem Löschen der Elternknoten obsolet gewordenen Referenzen löschen
            CleanAllDyamicReferences();
        }

        /*****************************************************************************************************************************/
        // Allgemeine Funktionen für ChannelChooseWhen und ComObjectParameterChooseWhen

        /// <summary>
        /// löscht ein When aus dem ParaKoTreeView und dem entsprechendem choose Array
        /// </summary>
        /// <param name="chooseWithWhens">das übergeordnete choose mit when array</param>
        /// <param name="deleteWhen">das zu löschende When Objekt</param>
        /// <param name="treeView">der TreeView, aus dem das when zu entfernen ist</param>
        private static void DeleteWhensFromChooseAndTreeView(object chooseWithWhens, object deleteWhen, TreeView treeView)
        {
            int index = 0;
            if (chooseWithWhens is ComObjectParameterChoose_t)
            {
                foreach (object item in (chooseWithWhens as ComObjectParameterChoose_t).when)
                {
                    if (item == deleteWhen)
                    {
                        (chooseWithWhens as ComObjectParameterChoose_t).when =
                            TreeViewArrayFunctions.DeleteArrayTreeView((chooseWithWhens as ComObjectParameterChoose_t).when, index, treeView);
                        break;
                    }
                    index++;
                }
            }
            else if (chooseWithWhens is ChannelChoose_t)
            {
                foreach (object item in (chooseWithWhens as ChannelChoose_t).when)
                {
                    if (item == deleteWhen)
                    {
                        (chooseWithWhens as ChannelChoose_t).when =
                            TreeViewArrayFunctions.DeleteArrayTreeView((chooseWithWhens as ChannelChoose_t).when, index, treeView);
                        break;
                    }
                    index++;
                }
            }
            // alle nach dem Löschen der Elternknoten obsolet gewordenen Referenzen löschen
            CleanAllDyamicReferences();
        }


        /// <summary>
        /// ein Object an ein Items Feld anhängen
        /// </summary>
        /// <param name="objectWithItems">ein Objekt, welches ein Items Feld enthält</param>
        /// <param name="newObject">das neue Objekt, welches angehangen werden soll</param>
        public static void AddToItemsObject(object objectWithItems, object newObject)
        {
            if (objectWithItems is ApplicationProgramChannel_t)
            {
                if ((objectWithItems as ApplicationProgramChannel_t).Items == null)
                {
                    (objectWithItems as ApplicationProgramChannel_t).Items = new object[0];
                }
                (objectWithItems as ApplicationProgramChannel_t).Items =
                    HandleArrayFunctions.Add((objectWithItems as ApplicationProgramChannel_t).Items, newObject);
            }
            else if (objectWithItems is ChannelIndependentBlock_t)
            {
                if ((objectWithItems as ChannelIndependentBlock_t).Items == null)
                {
                    (objectWithItems as ChannelIndependentBlock_t).Items = new object[0];
                }
                (objectWithItems as ChannelIndependentBlock_t).Items =
                    HandleArrayFunctions.Add((objectWithItems as ChannelIndependentBlock_t).Items, newObject);
            }
            else if (objectWithItems is ChannelChoose_tWhen)
            {
                if ((objectWithItems as ChannelChoose_tWhen).Items == null)
                {
                    (objectWithItems as ChannelChoose_tWhen).Items = new object[0];
                }
                (objectWithItems as ChannelChoose_tWhen).Items =
                    HandleArrayFunctions.Add((objectWithItems as ChannelChoose_tWhen).Items, newObject);
            }
            else if (objectWithItems is ComObjectParameterBlock_t)
            {
                if ((objectWithItems as ComObjectParameterBlock_t).Items == null)
                {
                    (objectWithItems as ComObjectParameterBlock_t).Items = new object[0];
                }
                (objectWithItems as ComObjectParameterBlock_t).Items =
                    HandleArrayFunctions.Add((objectWithItems as ComObjectParameterBlock_t).Items, newObject);
            }
            else if (objectWithItems is ComObjectParameterChoose_tWhen)
            {
                if ((objectWithItems as ComObjectParameterChoose_tWhen).Items == null)
                {
                    (objectWithItems as ComObjectParameterChoose_tWhen).Items = new object[0];
                }
                (objectWithItems as ComObjectParameterChoose_tWhen).Items =
                    HandleArrayFunctions.Add((objectWithItems as ComObjectParameterChoose_tWhen).Items, newObject);
            }
        }


        /// <summary>
        /// ein Object an ein Dynamic Feld anhängen
        /// </summary>
        /// <param name="objectWithItems">ein Objekt, welches ein Dynamic Feld enthält</param>
        /// <param name="newObject">das neue Objekt, welches angehangen werden soll</param>
        public static void AddToDynamicObject(object objectWithDynamic, object newObject)
        {
            if (objectWithDynamic is ApplicationProgram_t)
            {
                if ((objectWithDynamic as ApplicationProgram_t).Dynamic == null)
                {
                    (objectWithDynamic as ApplicationProgram_t).Dynamic = new object[0];
                }
                (objectWithDynamic as ApplicationProgram_t).Dynamic =
                    HandleArrayFunctions.Add((objectWithDynamic as ApplicationProgram_t).Dynamic, newObject);
            }
        }


        /// <summary>
        /// Einige Elemente erhalten bei Erstellung eine automatisch berechnete Id.
        /// Diese Elemente bekommen bei einer Kopie eine neue Id.
        /// </summary>
        /// <param name="copyObject">das zu kopierende Objekt mit all seinen Anhängen</param>
        public static void CheckIfNewIdNecessary(object copyObject)
        {
            if (copyObject is ComObjectParameterBlock_t)
            {
                int newComObjParBlockId = ComObjectParameterBlockGui.FindComObjectParameterBlockIds() + 1;
                (copyObject as ComObjectParameterBlock_t).Id = ApplicationProgramGui.selectedApplicationProgram.Id + "_PB-" + newComObjParBlockId.ToString();
                if ((copyObject as ComObjectParameterBlock_t).Items != null)
                {
                    foreach (object item in (copyObject as ComObjectParameterBlock_t).Items)
                    {
                        CheckIfNewIdNecessary(item);
                    }
                }
            }
            else if (copyObject is ComObjectParameterChoose_t)
            {
                if ((copyObject as ComObjectParameterChoose_t).when != null)
                {
                    foreach (ComObjectParameterChoose_tWhen when in (copyObject as ComObjectParameterChoose_t).when)
                    {
                        CheckIfNewIdNecessary(when);
                    }
                }
            }
            else if (copyObject is ChannelChoose_t)
            {
                if ((copyObject as ChannelChoose_t).when != null)
                {
                    foreach (ChannelChoose_tWhen when in (copyObject as ChannelChoose_t).when)
                    {
                        CheckIfNewIdNecessary(when);
                    }
                }
            }
            else if (copyObject is ChannelChoose_tWhen)
            {
                if ((copyObject as ChannelChoose_tWhen).Items != null)
                {
                    foreach (object item in (copyObject as ChannelChoose_tWhen).Items)
                    {
                        CheckIfNewIdNecessary(item);
                    }
                }
            }
            else if (copyObject is ComObjectParameterChoose_tWhen)
            {
                if ((copyObject as ComObjectParameterChoose_tWhen).Items != null)
                {
                    foreach (object item in (copyObject as ComObjectParameterChoose_tWhen).Items)
                    {
                        CheckIfNewIdNecessary(item);
                    }
                }
            }
            else if (copyObject is ApplicationProgramChannel_t)
            {
                int newAppChannelId = ApplicationChannelGui.FindApplicationProgramChannelIds() + 1;
                (copyObject as ApplicationProgramChannel_t).Id = ApplicationProgramGui.selectedApplicationProgram.Id + "_CH-" + newAppChannelId.ToString();
                if ((copyObject as ApplicationProgramChannel_t).Items != null)
                {
                    foreach (object item in (copyObject as ApplicationProgramChannel_t).Items)
                    {
                        CheckIfNewIdNecessary(item);
                    }
                }
            }
            else if (copyObject is ChannelIndependentBlock_t)
            {
                if ((copyObject as ChannelIndependentBlock_t).Items != null)
                {
                    foreach (object item in (copyObject as ChannelIndependentBlock_t).Items)
                    {
                        CheckIfNewIdNecessary(item);
                    }
                }
            }
        }



        /// <summary>
        /// eine Kopie eines beliebigen Baumknontens erzeugen mit neuen Ref IDs (Parameter und ComObjects)
        /// wurde entwickelt, aber doch nicht benötigt....
        /// </summary>
        /// <param name="copyObject"></param>
        private void CreateIndependentCopy(object copyObject)
        {
            if (copyObject is ComObjectParameterBlock_t)
            {
                (copyObject as ComObjectParameterBlock_t).ParamRefId = CreateNewParameterRef((copyObject as ComObjectParameterBlock_t).ParamRefId);
                if ((copyObject as ComObjectParameterBlock_t).Items != null)
                {
                    foreach (object item in (copyObject as ComObjectParameterBlock_t).Items)
                    {
                        CreateIndependentCopy(item);
                    }
                }
            }
            else if (copyObject is ParameterRefRef_t)
            {
                (copyObject as ParameterRefRef_t).RefId = CreateNewParameterRef((copyObject as ParameterRefRef_t).RefId);
            }
            else if (copyObject is ComObjectParameterChoose_t)
            {
                (copyObject as ComObjectParameterChoose_t).ParamRefId = CreateNewParameterRef((copyObject as ComObjectParameterChoose_t).ParamRefId);
                foreach (ComObjectParameterChoose_tWhen when in (copyObject as ComObjectParameterChoose_t).when)
                {
                    CreateIndependentCopy(when);
                }
            }
            else if (copyObject is ChannelChoose_t)
            {
                (copyObject as ChannelChoose_t).ParamRefId = CreateNewParameterRef((copyObject as ChannelChoose_t).ParamRefId);
                foreach (ChannelChoose_tWhen when in (copyObject as ChannelChoose_t).when)
                {
                    CreateIndependentCopy(when);
                }
            }
            else if (copyObject is ComObjectRefRef_t)
            {
                (copyObject as ComObjectRefRef_t).RefId = CreateNewComObjectRef((copyObject as ComObjectRefRef_t).RefId);
            }
            else if (copyObject is ChannelChoose_tWhen)
            {
                if ((copyObject as ChannelChoose_tWhen).Items != null)
                {
                    foreach (object item in (copyObject as ChannelChoose_tWhen).Items)
                    {
                        CreateIndependentCopy(item);
                    }
                }
            }
            else if (copyObject is ComObjectParameterChoose_tWhen)
            {
                if ((copyObject as ComObjectParameterChoose_tWhen).Items != null)
                {
                    foreach (object item in (copyObject as ComObjectParameterChoose_tWhen).Items)
                    {
                        CreateIndependentCopy(item);
                    }
                }
            }
            else if (copyObject is ApplicationProgramChannel_t)
            {
                if ((copyObject as ApplicationProgramChannel_t).Items != null)
                {
                    foreach (object item in (copyObject as ApplicationProgramChannel_t).Items)
                    {
                        CreateIndependentCopy(item);
                    }
                }
            }
            else if (copyObject is ChannelIndependentBlock_t)
            {
                if ((copyObject as ChannelIndependentBlock_t).Items != null)
                {
                    foreach (object item in (copyObject as ChannelIndependentBlock_t).Items)
                    {
                        CreateIndependentCopy(item);
                    }
                }
            }
        }

        /// <summary>
        /// erzeugt einen neuen ParameterRef Eintrag mit neuer Id
        /// </summary>
        /// <param name="oldParaRefId">alte ParameterRef Id</param>
        /// <returns>neue ParameterRef Id</returns>
        private string CreateNewParameterRef(string oldParaRefId)
        {
            ParameterRef_t paraRef = ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == oldParaRefId);
            ParameterRef_t newParaRef = new ParameterRef_t();
            int nextParameterRefNumber = AppParameterGui.HighestUsedParaRefNumber() + 1;

            int indexOfRefNumber = paraRef.Id.IndexOf("_R-");

            // das ParameterRef Object anhängen
            ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs = HandleArrayFunctions.Add(ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs, newParaRef);

            newParaRef.Id = paraRef.Id.Substring(0, indexOfRefNumber + 3) + nextParameterRefNumber.ToString();
            newParaRef.RefId = paraRef.RefId;
            newParaRef.Name = paraRef.Name;
            newParaRef.Text = paraRef.Text;
            newParaRef.SuffixText = paraRef.SuffixText;
            newParaRef.Tag = paraRef.Tag;
            newParaRef.DisplayOrderSpecified = paraRef.DisplayOrderSpecified;
            newParaRef.DisplayOrder = paraRef.DisplayOrder;
            newParaRef.AccessSpecified = paraRef.AccessSpecified;
            newParaRef.Access = paraRef.Access;
            newParaRef.Value = paraRef.Value;
            newParaRef.InitialValue = paraRef.InitialValue;
            newParaRef.CustomerAdjustableSpecified = paraRef.CustomerAdjustableSpecified;
            newParaRef.CustomerAdjustable = paraRef.CustomerAdjustable;
            newParaRef.TextParameterRefId = paraRef.TextParameterRefId;
            newParaRef.InternalDescription = paraRef.InternalDescription;
            newParaRef.ForbidGrantingUseByCustomer = paraRef.ForbidGrantingUseByCustomer;
            newParaRef.Semantics = paraRef.Semantics;

            /*
             * die Language Daten werden erstmal nicht mit kopiert... 
             */

            return newParaRef.Id;
        }

        /// <summary>
        /// erzeugt einen neuen ParameterRef Eintrag mit neuer Id
        /// </summary>
        /// <param name="oldComObjRefId">alte ComObjectRef Id</param>
        /// <returns>neue ComObjectRef Id</returns>
        private string CreateNewComObjectRef(string oldComObjRefId)
        {

            ComObjectRef_t comObjRef = ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs.ToList().Find(x => x.Id == oldComObjRefId);
            ComObjectRef_t newComObjRef = new ComObjectRef_t();
            int nextComObjectRefNumber = ComObjectGui.HighestUsedComObjRefNumber() + 1;

            int indexOfRefNumber = comObjRef.Id.IndexOf("_R-");

            // das ConbObjectRef Object einhängen
            ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs = HandleArrayFunctions.Add(ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs, newComObjRef);

            newComObjRef.Id = comObjRef.Id.Substring(0, indexOfRefNumber + 3) + nextComObjectRefNumber.ToString();
            newComObjRef.RefId = comObjRef.RefId;
            newComObjRef.Name = comObjRef.Name;
            newComObjRef.Text = comObjRef.Text;
            newComObjRef.Tag = comObjRef.Tag;
            newComObjRef.FunctionText = comObjRef.FunctionText;
            newComObjRef.Priority = comObjRef.Priority;
            newComObjRef.ObjectSize = comObjRef.ObjectSize;
            newComObjRef.ReadFlag = comObjRef.ReadFlag;
            newComObjRef.WriteFlag = comObjRef.WriteFlag;
            newComObjRef.CommunicationFlag = comObjRef.CommunicationFlag;
            newComObjRef.TransmitFlag = comObjRef.TransmitFlag;
            newComObjRef.UpdateFlag = comObjRef.UpdateFlag;
            newComObjRef.ReadOnInitFlag = comObjRef.ReadOnInitFlag;
            newComObjRef.DatapointType = comObjRef.DatapointType;
            newComObjRef.TextParameterRefId = comObjRef.TextParameterRefId;
            newComObjRef.InternalDescription = comObjRef.InternalDescription;
            newComObjRef.Roles = comObjRef.Roles;
            newComObjRef.SecurityRequiredSpecified = comObjRef.SecurityRequiredSpecified;
            newComObjRef.SecurityRequired = comObjRef.SecurityRequired;
            newComObjRef.MayReadSpecified = comObjRef.MayReadSpecified;
            newComObjRef.MayRead = comObjRef.MayRead;
            newComObjRef.ReadFlagLockedSpecified = comObjRef.ReadFlagLockedSpecified;
            newComObjRef.ReadFlagLocked = comObjRef.ReadFlagLocked;
            newComObjRef.WriteFlagLockedSpecified = comObjRef.WriteFlagLockedSpecified;
            newComObjRef.WriteFlagLocked = comObjRef.WriteFlagLocked;
            newComObjRef.TransmitFlagLockedSpecified = comObjRef.TransmitFlagLockedSpecified;
            newComObjRef.TransmitFlagLocked = comObjRef.TransmitFlagLocked;
            newComObjRef.UpdateFlagLockedSpecified = comObjRef.UpdateFlagLockedSpecified;
            newComObjRef.UpdateFlagLocked = comObjRef.UpdateFlagLocked;
            newComObjRef.ReadOnInitFlagLockedSpecified = comObjRef.ReadOnInitFlagLockedSpecified;
            newComObjRef.ReadOnInitFlagLocked = comObjRef.ReadOnInitFlagLocked;
            newComObjRef.Semantics = comObjRef.Semantics;

            return newComObjRef.Id;
        }
    }
}