using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnxProd.Model;

namespace knxprod_ns
{
    class WhenTestHelper
    {
        /// <summary>
        /// Füllt eine ComboBox mit den Informationen zu den auswählbaren Test Werten für ein When Element
        /// </summary>
        /// <param name="whenTestComboBox">die zu befüllende ComboBox</param>
        /// <param name="testValue"></param>
        /// <param name="defaultValue"></param>
        public static void FillWhenTestComboBox(object chooseObject, ComboBox whenTestComboBox, string testValue, bool defaultValue)
        {
            whenTestComboBox.Items.Clear();

            ParameterRef_t chooseParaRef = null;
            // alle Möglichkeiten für When auflisten
            if (chooseObject is ChannelChoose_t)
            {
                if ((chooseObject as ChannelChoose_t).when != null)
                {
                    chooseParaRef = ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == (chooseObject as ChannelChoose_t).ParamRefId);
                }
            }
            else if (chooseObject is ComObjectParameterChoose_t)
            {
                if ((chooseObject as ComObjectParameterChoose_t).when != null)
                {
                    chooseParaRef = ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == (chooseObject as ComObjectParameterChoose_t).ParamRefId);
                }
            }

            if (chooseParaRef != null)
            {
                List<object> parameters = ApplicationProgramGui.selectedApplicationProgram.Static.Parameters.ToList().FindAll(x => x is ApplicationProgramStatic_tParameter);
                ParameterType_t chooseParameterType = null;

                ApplicationProgramStatic_tParameter chooseParameter = parameters.Find(x => (x as ApplicationProgramStatic_tParameter).Id == chooseParaRef.RefId) as ApplicationProgramStatic_tParameter;
                if (chooseParameter != null)
                {
                    chooseParameterType = ApplicationProgramGui.selectedApplicationProgram.Static.ParameterTypes.ToList().Find(x => x.Id == chooseParameter.ParameterType);
                }
                else // nicht als Parameter gefunden, daher nun in Unsions suchen
                {
                    List<object> unions = ApplicationProgramGui.selectedApplicationProgram.Static.Parameters.ToList().FindAll(x => x is ApplicationProgramStatic_tUnion);
                    foreach (ApplicationProgramStatic_tUnion union in unions)
                    {
                        if (union.Parameter != null)
                        {
                            UnionParameter_t unionParameter = union.Parameter.ToList().Find(x => x.Id == chooseParaRef.RefId);
                            if (unionParameter != null)
                            {
                                chooseParameterType = ApplicationProgramGui.selectedApplicationProgram.Static.ParameterTypes.ToList().Find(x => x.Id == unionParameter.ParameterType);
                                break;
                            }
                        }
                    }
                }

                // Die Auswahlmöglichkeiten des gefundenen ParameterType in die ComboBox füllen
                if (chooseParameterType.Item is ParameterType_tTypeRestriction)
                {
                    foreach (ParameterType_tTypeRestrictionEnumeration enumItem in (chooseParameterType.Item as ParameterType_tTypeRestriction).Enumeration)
                    {
                        ComboBoxItem newComboBoxItem = new ComboBoxItem(DynamicTreeViewGenerator.resolveParamTypeForWhenText(chooseParameterType, enumItem.Value.ToString(), defaultValue), enumItem);
                        whenTestComboBox.Items.Add(newComboBoxItem);
                        if (enumItem.Value.ToString() == testValue)
                        {
                            whenTestComboBox.SelectedItem = newComboBoxItem;
                        }
                    }
                }
                else
                {
                    if (testValue != null)
                    {
                        ComboBoxItem newComboBoxItem = new ComboBoxItem(testValue, chooseParameterType.Item);
                        whenTestComboBox.Items.Add(newComboBoxItem);
                        whenTestComboBox.SelectedItem = newComboBoxItem;
                    }
                }
                if (testValue == null && whenTestComboBox.Items.Count > 0)
                {
                    whenTestComboBox.SelectedIndex = 0;
                }
            }
        }
    }
}
