using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using KnxProd.Model;

namespace knxprod_ns
{
    public static class HandleKnxDataTypes
    {
        public static string ReadKNXType (Access_t access)
        {
            switch (access)
            {
                case Access_t.None:
                    return "None";
                case Access_t.Read:
                    return "Read";
                case Access_t.ReadWrite:
                    return "ReadWrite";
                default:
                    return null;
            }
        }


        public static string ReadKNXType (bool boolValue)
        {
            switch (boolValue)
            {
                case true:
                    return "true";
                case false:
                    return "false";
                default:
                    return "";
            }
        }

        public static string ReadKNXType (ParameterBlockLayout_t parameterBlockLayout)
        {
            switch (parameterBlockLayout)
            {
                case ParameterBlockLayout_t.Grid:
                    return "Grid";
                case ParameterBlockLayout_t.List:
                    return "List";
                case ParameterBlockLayout_t.Table:
                    return "Table";
                default:
                    return "";
            }
        }

        public static string ReadKNXType (ComObjectPriority_t comObjectPriority)
        {
            switch (comObjectPriority)
            {
                case ComObjectPriority_t.Alert:
                    return"Alert";
                case ComObjectPriority_t.High:
                    return "High";
                case ComObjectPriority_t.Low:
                    return "Low";
                default:
                    return "";
            }
        }



        public static string GetXmlEnumAttributeValueFromEnum<TEnum>(this TEnum value) where TEnum : struct, IConvertible
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum) return null;//or string.Empty, or throw exception

            var member = enumType.GetMember(value.ToString()).FirstOrDefault();
            if (member == null) return null;//or string.Empty, or throw exception

            var attribute = member.GetCustomAttributes(false).OfType<XmlEnumAttribute>().FirstOrDefault();
            if (attribute == null) return null;//or string.Empty, or throw exception
            return attribute.Name;
        }

        public static bool ReadKNXType (Enable_t enable)
        {
            switch (enable)
            {
                case Enable_t.Disabled:
                    return false;
                case Enable_t.Enabled:
                    return true;
                default:
                    return false;
            }
        }


        public static void ReadKNXType (ListBox listbox, string[] data)
        {
            listbox.Items.Clear();

            if (data != null)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    listbox.Items.Add(data[i]);
                }
            }
        }


        public static void ReadKNXType (ListBox listbox, byte[] data)
        {
            listbox.Items.Clear();
            if (data != null)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    listbox.Items.Add(data[i]);
                }
            }
        }

        public static string ReadKNXType (ComObjectSecurityRequirements_t comObjectSercurityRequirements)
        {
            switch (comObjectSercurityRequirements)
            {
                case ComObjectSecurityRequirements_t.Auth:
                    return "Auth";
                case ComObjectSecurityRequirements_t.AuthAndConf:
                    return "AuthAndConf";
                case ComObjectSecurityRequirements_t.None:
                    return "None";
                default:
                    return "";
            }
        }


        public static string ReadKNXType (RFRxCapabilities_t rfrxCapabilities)
        {
            switch (rfrxCapabilities)
            {
                case RFRxCapabilities_t.Ready:
                    return "Ready";
                case RFRxCapabilities_t.ReadyFast:
                    return "ReadyFast";
                case RFRxCapabilities_t.Slow:
                    return "Slow";
                default:
                    return "";
            }
        }

        public static string ReadKNXType (RFTxCapabilities_t rftxCapabilities)
        {
            switch (rftxCapabilities)
            {
                case RFTxCapabilities_t.Ready:
                    return "Ready";
                case RFTxCapabilities_t.ReadyFast:
                    return "ReadyFast";
                case RFTxCapabilities_t.ReadyFastSlow:
                    return "ReadyFastSlow";
                default:
                    return "";
            }
        }


        public static void ReadCouplerCapability(ListBox listbox, CouplerCapability_t[] couplerCapability)
        {
            if (couplerCapability != null)
            {
                for (int i = 0; i < couplerCapability.Length; i++)
                {
                    switch (couplerCapability[i])
                    {
                        case CouplerCapability_t.RfMultiFast:
                            {
                                listbox.Items.Add("RfMultiFast");
                                break;
                            }
                        case CouplerCapability_t.RfMultiSlow:
                            {
                                listbox.Items.Add("RfMultiSlow");
                                break;
                            }
                        case CouplerCapability_t.RfReady:
                            {
                                listbox.Items.Add("RfReady");
                                break;
                            }
                        case CouplerCapability_t.SecurityProxy:
                            {
                                listbox.Items.Add("SecurityProxy");
                                break;
                            }
                    }
                }
            }
        }

        public static string ReadKNXType (Hardware_tProductAttributeName productAttributeName)
        {
            switch (productAttributeName)
            {
                case Hardware_tProductAttributeName.CatalogName:
                    return "CatalogName";
                case Hardware_tProductAttributeName.Colour:
                    return "Colour";
                case Hardware_tProductAttributeName.Series:
                    return "Series";
                default:
                    return "";
            }
        }

        public static string ReadKNXType (ApplicationProgramType_t applicationProgramType)
        {
            switch (applicationProgramType)
            {
                case ApplicationProgramType_t.ApplicationProgram:
                    return "ApplicationProgram";
                case ApplicationProgramType_t.PeiProgram:
                    return "PeiProgram";
                default:
                    return "";
            }
        }

        public static string ReadKNXType (LoadProcedureStyle_t loadProcedureStyle)
        {
            switch (loadProcedureStyle)
            {
                case LoadProcedureStyle_t.DefaultProcedure:
                    return "DefaultProcedure";
                case LoadProcedureStyle_t.MergedProcedure:
                    return "MergedProcedure";
                case LoadProcedureStyle_t.ProductProcedure:
                    return "ProductProcedure";
                default:
                    return "";
            }
        }

        public static string ReadKNXType (ApplicationProgramIPConfig_t applicationProgramIPConfig)
        {
            switch (applicationProgramIPConfig)
            {
                case ApplicationProgramIPConfig_t.Custom:
                    return "Custom";
                case ApplicationProgramIPConfig_t.Tool:
                    return "Tool";
                default:
                    return "";
            }
        }

        public static string ReadKNXType (MemoryType_t memoryType)
        {
            switch (memoryType)
            {
                case MemoryType_t.EEPROM:
                    return "EEPROM";
                case MemoryType_t.FLASH:
                    return "FLASH";
                case MemoryType_t.RAM:
                    return "RAM";
                default:
                    return "";
            }
        }

        public static string ReadKNXType (ParameterType_tTypeRestrictionBase typeResBase)
        {
            switch (typeResBase)
            {
                case ParameterType_tTypeRestrictionBase.BinaryValue:
                    return "BinaryValue";
                case ParameterType_tTypeRestrictionBase.Value:
                    return "Value";
                default:
                    return "";
            }
        }

        public static string ReadKNXType (ParameterType_tTypeRestrictionUIHint typeRestrictionUIHint)
        {
            switch (typeRestrictionUIHint)
            {
                case ParameterType_tTypeRestrictionUIHint.Buttons:
                    {
                        return "Buttons";
                    }
                case ParameterType_tTypeRestrictionUIHint.DropDown:
                    {
                        return "DropDown";
                    }
                case ParameterType_tTypeRestrictionUIHint.Segmented:
                    {
                        return "Segmented";
                    }
                case ParameterType_tTypeRestrictionUIHint.Text:
                    {
                        return "Text";
                    }
                default:
                    return "";
            }
        }

        public static string ReadKNXType (HorizontalAlignment_t horizontalAlignment)
        {
            switch (horizontalAlignment)
            {
                case HorizontalAlignment_t.Left:
                    {
                        return "Left";
                    }
                case HorizontalAlignment_t.Middle:
                    {
                        return "Middle";
                    }
                case HorizontalAlignment_t.Repeat:
                    {
                        return "Repeat";
                    }
                case HorizontalAlignment_t.Right:
                    {
                        return "Right";
                    }
                case HorizontalAlignment_t.Stretch:
                    {
                        return "Stretch";
                    }
                default:
                    {
                        return "";
                    }
            }
        }

        public static string ReadKNXType (ParameterType_tTypeNumberType numberType)
        {
            switch (numberType)
            {
                case ParameterType_tTypeNumberType.signedInt:
                    {
                        return "signedInt";
                    }
                case ParameterType_tTypeNumberType.unsignedInt:
                    {
                        return "unsignedInt";
                    }
                default:
                    {
                        return "";
                    }
            }
        }

        public static string ReadKNXType (ParameterType_tTypeNumberUIHint uIHint)
        {
            switch (uIHint)
            {
                case ParameterType_tTypeNumberUIHint.CheckBox:
                    {
                        return "CheckBox";
                    }
                case ParameterType_tTypeNumberUIHint.ProgressBar:
                    {
                        return "ProgressBar";
                    }
                case ParameterType_tTypeNumberUIHint.Slider:
                    {
                        return "Slider";
                    }
                default:
                    {
                        return "";
                    }
            }
        }

        public static string ReadKNXType (ParameterType_tTypeFloatEncoding floatEncoding)
        {
            switch (floatEncoding)
            {
                case ParameterType_tTypeFloatEncoding.DPT9:
                    {
                        return "DPT9";
                    }
                case ParameterType_tTypeFloatEncoding.IEEE754Double:
                    {
                        return "IEEE754Double";
                    }
                case ParameterType_tTypeFloatEncoding.IEEE754Single:
                    {
                        return "IEEE754Single";
                    }
                default:
                    return "";
            }
        }

        public static string ReadKNXType (ParameterType_tTypeFloatUIHint floatUIHint)
        {
            switch (floatUIHint)
            {
                case ParameterType_tTypeFloatUIHint.Slider:
                    {
                        return "Slider";
                    }
                default:
                    {
                        return "";
                    }
            }
        }

        public static uint GetComObjectByteSize(ComObjectSize_t comObjSizeItem)
        {
            uint comObjSize = 0;
            switch (comObjSizeItem)
            {
                case ComObjectSize_t.Item1Bit:
                    comObjSize = 1;
                    break;
                case ComObjectSize_t.Item2Bit:
                    comObjSize = 1;
                    break;
                case ComObjectSize_t.Item3Bit:
                    comObjSize = 1;
                    break;
                case ComObjectSize_t.Item4Bit:
                    comObjSize = 1;
                    break;
                case ComObjectSize_t.Item5Bit:
                    comObjSize = 1;
                    break;
                case ComObjectSize_t.Item6Bit:
                    comObjSize = 1;
                    break;
                case ComObjectSize_t.Item7Bit:
                    comObjSize = 1;
                    break;
                case ComObjectSize_t.Item1Byte:
                    comObjSize = 1;
                    break;
                case ComObjectSize_t.Item2Bytes:
                    comObjSize = 2;
                    break;
                case ComObjectSize_t.Item3Bytes:
                    comObjSize = 3;
                    break;
                case ComObjectSize_t.Item4Bytes:
                    comObjSize = 4;
                    break;
                case ComObjectSize_t.Item5Bytes:
                    comObjSize = 5;
                    break;
                case ComObjectSize_t.Item6Bytes:
                    comObjSize = 6;
                    break;
                case ComObjectSize_t.Item7Bytes:
                    comObjSize = 7;
                    break;
                case ComObjectSize_t.Item8Bytes:
                    comObjSize = 8;
                    break;
                case ComObjectSize_t.Item9Bytes:
                    comObjSize = 9;
                    break;
                case ComObjectSize_t.Item10Bytes:
                    comObjSize = 10;
                    break;
                case ComObjectSize_t.Item11Bytes:
                    comObjSize = 11;
                    break;
                case ComObjectSize_t.Item12Bytes:
                    comObjSize = 12;
                    break;
                case ComObjectSize_t.Item13Bytes:
                    comObjSize = 13;
                    break;
                case ComObjectSize_t.Item14Bytes:
                    comObjSize = 14;
                    break;
                case ComObjectSize_t.Item15Bytes:
                    comObjSize = 15;
                    break;
                case ComObjectSize_t.Item16Bytes:
                    comObjSize = 16;
                    break;
                case ComObjectSize_t.Item17Bytes:
                    comObjSize = 17;
                    break;
                case ComObjectSize_t.Item18Bytes:
                    comObjSize = 18;
                    break;
                case ComObjectSize_t.Item19Bytes:
                    comObjSize = 19;
                    break;
                case ComObjectSize_t.Item20Bytes:
                    comObjSize = 20;
                    break;
                case ComObjectSize_t.Item21Bytes:
                    comObjSize = 21;
                    break;
                case ComObjectSize_t.Item22Bytes:
                    comObjSize = 22;
                    break;
                case ComObjectSize_t.Item23Bytes:
                    comObjSize = 23;
                    break;
                case ComObjectSize_t.Item24Bytes:
                    comObjSize = 24;
                    break;
                case ComObjectSize_t.Item25Bytes:
                    comObjSize = 25;
                    break;
                case ComObjectSize_t.Item26Bytes:
                    comObjSize = 26;
                    break;
                case ComObjectSize_t.Item27Bytes:
                    comObjSize = 27;
                    break;
                case ComObjectSize_t.Item28Bytes:
                    comObjSize = 28;
                    break;
                case ComObjectSize_t.Item29Bytes:
                    comObjSize = 29;
                    break;
                case ComObjectSize_t.Item30Bytes:
                    comObjSize = 30;
                    break;
                case ComObjectSize_t.Item31Bytes:
                    comObjSize = 31;
                    break;
                case ComObjectSize_t.Item32Bytes:
                    comObjSize = 32;
                    break;
                case ComObjectSize_t.Item33Bytes:
                    comObjSize = 33;
                    break;
                case ComObjectSize_t.Item34Bytes:
                    comObjSize = 34;
                    break;
                case ComObjectSize_t.Item35Bytes:
                    comObjSize = 35;
                    break;
                case ComObjectSize_t.Item36Bytes:
                    comObjSize = 36;
                    break;
                case ComObjectSize_t.Item37Bytes:
                    comObjSize = 37;
                    break;
                case ComObjectSize_t.Item38Bytes:
                    comObjSize = 38;
                    break;
                case ComObjectSize_t.Item39Bytes:
                    comObjSize = 39;
                    break;
                case ComObjectSize_t.Item40Bytes:
                    comObjSize = 40;
                    break;
                case ComObjectSize_t.Item41Bytes:
                    comObjSize = 41;
                    break;
                case ComObjectSize_t.Item42Bytes:
                    comObjSize = 42;
                    break;
                case ComObjectSize_t.Item43Bytes:
                    comObjSize = 43;
                    break;
                case ComObjectSize_t.Item44Bytes:
                    comObjSize = 44;
                    break;
                case ComObjectSize_t.Item45Bytes:
                    comObjSize = 45;
                    break;
                case ComObjectSize_t.Item46Bytes:
                    comObjSize = 46;
                    break;
                case ComObjectSize_t.Item47Bytes:
                    comObjSize = 47;
                    break;
                case ComObjectSize_t.Item48Bytes:
                    comObjSize = 48;
                    break;
                case ComObjectSize_t.Item49Bytes:
                    comObjSize = 49;
                    break;
                case ComObjectSize_t.Item50Bytes:
                    comObjSize = 50;
                    break;
            }
            return comObjSize;
        }

    }
}