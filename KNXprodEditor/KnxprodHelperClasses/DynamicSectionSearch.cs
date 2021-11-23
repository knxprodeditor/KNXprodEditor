using KnxProd.Model;
using System.Windows.Forms;

namespace knxprod_ns
{
    static class DynamicSectionSearch
    {
        /// <summary>
        /// Es werden passende RefIDs gesucht und die Anzahl der Verweise zurückgegeben
        /// </summary>
        /// <param name="id">ParameterRefId oder ComObjectRefId</param>
        /// <returns>Anzahl der gefundenen Verweise</returns>
        public static int SearchIdInDynamic(string id)
        {
            int refCounter = 0;
            foreach (var dynChannel in ApplicationProgramGui.selectedApplicationProgram.Dynamic) //channels
            {
                if (dynChannel is ApplicationProgramChannel_t)
                {
                    if((dynChannel as ApplicationProgramChannel_t).Id == id)
                    {
                        refCounter++;
                    }
                    if ((dynChannel as ApplicationProgramChannel_t).Items != null)
                    {
                        foreach (var dynChannelItem in (dynChannel as ApplicationProgramChannel_t).Items) //ParameterBlock oder choose
                        {
                            refCounter += SearchChannelOrChannelIndependent((dynChannel as ApplicationProgramChannel_t).Items, dynChannelItem, id);
                        }
                    }
                }
                if (dynChannel is ChannelIndependentBlock_t)
                {
                    if ((dynChannel as ChannelIndependentBlock_t).Items != null)
                    {
                        foreach (var dynChannelItem in (dynChannel as ChannelIndependentBlock_t).Items) //ParameterBlock oder choose
                        {
                            refCounter += SearchChannelOrChannelIndependent((dynChannel as ChannelIndependentBlock_t).Items, dynChannelItem, id);
                        }
                    }
                }
            }
            return refCounter;
        }


        static int SearchChannelOrChannelIndependent(object[] dynChannelItems, object dynChannelItem, string id)
        {
            int refCounter = 0;
            if (dynChannelItem is ComObjectParameterBlock_t) //ParameterBlock
            {
                if ((dynChannelItem as ComObjectParameterBlock_t).ParamRefId == id)
                {
                    refCounter++;
                }

                refCounter += SearchParameterBlock((dynChannelItem as ComObjectParameterBlock_t), id);
            }
            else if (dynChannelItem is ChannelChoose_t) //choose direkt unter channel
            {
                if((dynChannelItem as ChannelChoose_t).ParamRefId == id)
                {
                    refCounter++;
                }
                refCounter += SearchChannelChooseWhen((dynChannelItem as ChannelChoose_t), id);             
            }
            else if(dynChannelItem is ComObjectRefRef_t)
            {
                if ((dynChannelItem as ComObjectRefRef_t).RefId == id)
                {
                    refCounter++;
                }
            }
            return refCounter;
        }


        static int SearchChannelChooseWhen(ChannelChoose_t channelChooseItem, string id)
        {
            int refCounter = 0;
            foreach (var ChooseWhenItem in channelChooseItem.when)    //choose durchsuchen
            {
                foreach (var whenItem in ChooseWhenItem.Items)    //when durchsuchen
                {
                    if (whenItem is ParameterRefRef_t)
                    {
                        if((whenItem as ParameterRefRef_t).RefId == id)
                        {
                            refCounter++;
                        }
                    }
                    else if (whenItem is ChannelChoose_t)
                    {
                        if ((whenItem as ChannelChoose_t).ParamRefId == id)
                        {
                            refCounter++;
                        }
                        refCounter += SearchChannelChooseWhen(whenItem as ChannelChoose_t, id); //Rekursive Auflösung der choose-when Bäume  
                    }
                    else if (whenItem is ComObjectParameterBlock_t) //ParameterBlock unter when
                    {
                        if ((whenItem as ComObjectParameterBlock_t).ParamRefId == id)
                        {
                            refCounter++;
                        }
                        refCounter += SearchParameterBlock((whenItem as ComObjectParameterBlock_t), id);
                    }
                    else if (whenItem is ComObjectRefRef_t)
                    {
                        if ((whenItem as ComObjectRefRef_t).RefId == id)
                        {
                            refCounter++;
                        }
                    }        
                }
            }
            return refCounter;
        }

        static int SearchParameterBlock(ComObjectParameterBlock_t comObjectParBlock, string id)
        {
            int refCounter = 0;
            if (comObjectParBlock.Items != null)
            {
                foreach (var ParBlockItem in comObjectParBlock.Items) //ComObjectRefRef oder ParameterRefRef
                {
                    if (ParBlockItem is ComObjectRefRef_t)
                    {
                        if((ParBlockItem as ComObjectRefRef_t).RefId == id)
                        {
                            refCounter++;
                        }
                    }
                    else if (ParBlockItem is ParameterRefRef_t)
                    {
                        if ((ParBlockItem as ParameterRefRef_t).RefId == id)
                        {
                            refCounter++;
                        }
                    }
                    else if (ParBlockItem is ComObjectParameterChoose_t)
                    {
                        if((ParBlockItem as ComObjectParameterChoose_t).ParamRefId == id)
                        {
                            refCounter++;
                        }
                        refCounter += SearchComObjectChooseWhen((ParBlockItem as ComObjectParameterChoose_t), id);
                    }
                }
            }
            return refCounter;
        }

        static int SearchComObjectChooseWhen(ComObjectParameterChoose_t COParChooseItem, string id)
        {
            int refCounter = 0;
            foreach (var ChooseWhenItem in COParChooseItem.when)    //choose durchsuchen
            {
                if (ChooseWhenItem.Items != null)
                {
                    foreach (var whenItem in ChooseWhenItem.Items)    //when durchsuchen
                    {
                        if (whenItem is ParameterRefRef_t)
                        {
                            if((whenItem as ParameterRefRef_t).RefId == id)
                            {
                                refCounter++;
                            }
                        }
                        else if (whenItem is ComObjectParameterChoose_t)
                        {
                            if ((whenItem as ComObjectParameterChoose_t).ParamRefId == id)
                            {
                                refCounter++;
                            }
                            refCounter += SearchComObjectChooseWhen(whenItem as ComObjectParameterChoose_t, id); //Rekursive Auflösung der choose-when Bäume
                        }
                        else if (whenItem is ComObjectParameterBlock_t)
                        {
                            if ((whenItem as ComObjectParameterBlock_t).ParamRefId == id)
                            {
                                refCounter++;
                            }
                        }
                        else if (whenItem is ComObjectRefRef_t)
                        {
                            if ((whenItem as ComObjectRefRef_t).RefId == id)
                            {
                                refCounter++;
                            }
                        }
                    }
                }
            }
            return refCounter;
        }
    }
}