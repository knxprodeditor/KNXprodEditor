using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnxProd.Model;

namespace knxprod_ns
{
    class ParameterHelper
    {
        /// <summary>
        /// Füllt eine ComboBox mit allen verfügabren Parametern und wählt einen Parameter aus
        /// </summary>
        /// <param name="comboBox">die zu füllende ComboBox</param>
        /// <param name="parameterOrUnionParameter">der zu selektierende Paremeter</param>
        public static void FillParameterCollectionComboBox(ComboBox comboBox, object parameterOrUnionParameter)
        {
            comboBox.Items.Clear();
            foreach (var parameter in ApplicationProgramGui.selectedApplicationProgram.Static.Parameters)
            {
                if (parameter is ApplicationProgramStatic_tParameter)
                {
                    ComboBoxItem newComboBoxItem = new ComboBoxItem((parameter as ApplicationProgramStatic_tParameter).Name, parameter);
                    comboBox.Items.Add(newComboBoxItem);
                    if (parameter == parameterOrUnionParameter)
                    {
                        comboBox.SelectedItem = newComboBoxItem;
                    }
                }
                else if (parameter is ApplicationProgramStatic_tUnion)
                {
                    foreach (UnionParameter_t unionPara in (parameter as ApplicationProgramStatic_tUnion).Parameter)
                    {
                        ComboBoxItem newComboBoxItem = new ComboBoxItem(unionPara.Name, unionPara);
                        comboBox.Items.Add(newComboBoxItem);
                        if (unionPara == parameterOrUnionParameter)
                        {
                            comboBox.SelectedItem = newComboBoxItem;
                        }
                    }
                }
            }
            if (parameterOrUnionParameter == null)
            {
                comboBox.SelectedIndex = -1;
            }
        }


        /// <summary>
        /// Füllt eine ComboBox mit allen verfügbaren Parametertypen
        /// </summary>
        /// <param name="parTypesComboBox">die zu füllende ComboBox</param>
        /// <param name="paraTypeId">die ID des Parametertyps, der ausgewählt werden soll</param>
        /// <param name="listSelectableParameters">die Liste der auszuwählenden Paramter</param>
        /// <param name="paraTypeSizeInBit">die numeric Box für die Größe des Parametertyps</param>
        public static void FillParameterTypeSektion(ComboBox parTypesComboBox, string paraTypeId, ListBox listSelectableParameters, NumericUpDown paraTypeSizeInBit)
        {
            ParameterType_t selectedParameterType = new ParameterType_t();

            parTypesComboBox.SelectedIndex = -1; // immer zuerst deselektieren, falls ParameterType ID nicht gefunden wird
            parTypesComboBox.Items.Clear(); // alle alten Elemente aus der ComboBox löschen

            foreach (ParameterType_t paraType in ApplicationProgramGui.selectedApplicationProgram.Static.ParameterTypes)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(paraType.Name, paraType);
                parTypesComboBox.Items.Add(comboBoxItem);

                if (paraType.Id == paraTypeId)
                {
                    parTypesComboBox.SelectedItem = comboBoxItem;
                    selectedParameterType = paraType;
                }
                else if (paraTypeId == null)
                {
                    parTypesComboBox.SelectedIndex = -1;
                }
            }

            listSelectableParameters.Items.Clear();
            if (selectedParameterType.Item is ParameterType_tTypeRestriction)
            {
                foreach (var enumeration in (selectedParameterType.Item as ParameterType_tTypeRestriction).Enumeration)
                {
                    listSelectableParameters.Items.Add(ApplicationProgramGui.languageProcessingApplication.ReadTranslation(enumeration.Id, "Text"));
                }
            }
            if (selectedParameterType.Id != null)
            {
                paraTypeSizeInBit.Value = GetParameterTypeSizeInBit(selectedParameterType.Id);
            }
        }


        /// <summary>
        /// findet die Größe eines Parametertyps
        /// </summary>
        /// <param name="parameterType">die ID des Parametertyps, zu dem die Größe ermittelt werden soll</param>
        /// <returns></returns>
        public static uint GetParameterTypeSizeInBit(string parameterType)
        {
            uint parSizeInBit = 0;
            ParameterType_t paraType = ApplicationProgramGui.selectedApplicationProgram.Static.ParameterTypes.ToList().Find(x => x.Id == parameterType);
            if (paraType != null)
            {
                if (paraType.Item is ParameterType_tTypeRestriction)
                {
                    parSizeInBit = (paraType.Item as ParameterType_tTypeRestriction).SizeInBit;
                }
                else if (paraType.Item is ParameterType_tTypeNumber)
                {
                    parSizeInBit = (paraType.Item as ParameterType_tTypeNumber).SizeInBit;
                }
                else if (paraType.Item is ParameterType_tTypeText)
                {
                    parSizeInBit = (paraType.Item as ParameterType_tTypeText).SizeInBit;
                }
                else if (paraType.Item is ParameterType_tTypeTime)
                {
                    parSizeInBit = (paraType.Item as ParameterType_tTypeTime).SizeInBit;
                }
                else if (paraType.Item is ParameterType_tTypeFloat)
                {
                    if ((paraType.Item as ParameterType_tTypeFloat).Encoding == ParameterType_tTypeFloatEncoding.DPT9)
                    {
                        parSizeInBit = 16;
                    }
                    else if ((paraType.Item as ParameterType_tTypeFloat).Encoding == ParameterType_tTypeFloatEncoding.IEEE754Single)
                    {
                        parSizeInBit = 32;
                    }
                    else if ((paraType.Item as ParameterType_tTypeFloat).Encoding == ParameterType_tTypeFloatEncoding.IEEE754Double)
                    {
                        parSizeInBit = 64;
                    }
                }
            }
            return parSizeInBit;
        }
    }
}
