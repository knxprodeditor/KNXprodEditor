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
    public static class HandleGuiDataTypes
    {
        public static Access_t ReadAccess (string access)
        {
            switch (access)
            {
                case "None":
                    return Access_t.None;
                case "Read":
                    return Access_t.Read;
                case "ReadWrite":
                    return Access_t.ReadWrite;
                default:
                    return Access_t.None;
            }
        }


        public static bool ReadBool (string boolValue)
        {
            switch (boolValue)
            {
                case "true":
                    return true;
                case "false":
                    return false;
                default:
                    return false;
            }
        }

        public static ParameterBlockLayout_t ReadParameterBlockLayout (string parameterBlockLayout)
        {
            switch (parameterBlockLayout)
            {
                case "Grid":
                    return ParameterBlockLayout_t.Grid;
                case "List":
                    return ParameterBlockLayout_t.List;
                case "Table":
                    return ParameterBlockLayout_t.Table;
                default:
                    return ParameterBlockLayout_t.Grid;
            }
        }

        public static ComObjectPriority_t ReadComObjectPriority(string comObjectPriority)
        {
            switch (comObjectPriority)
            {
                case "Alert":
                    return ComObjectPriority_t.Alert;
                case "High":
                    return ComObjectPriority_t.High;
                case "Low":
                    return ComObjectPriority_t.Low;
                default:
                    return ComObjectPriority_t.Low;
            }
        }

    
        public static ComObjectSize_t ReadComObjectSize(string xmlAtribute)
        {
            var enumType = typeof(ComObjectSize_t);
            string enumMemberName = null; ;
            foreach(var enumMember in enumType.GetMembers())
            {
                var attribute = enumMember.GetCustomAttributes(false).OfType<XmlEnumAttribute>().FirstOrDefault();
                if (attribute != null)
                {
                    if (attribute.Name == xmlAtribute)
                    {
                        enumMemberName = enumMember.Name;
                        break;
                    }
                }
            }
            return (ComObjectSize_t)Enum.Parse(typeof(ComObjectSize_t), enumMemberName);
        }
        
        public static Enable_t ReadEnable (bool enable)
        {
            switch (enable)
            {
                case false:
                    return Enable_t.Disabled;
                case true:
                    return Enable_t.Enabled;
                default:
                    return Enable_t.Disabled;
            }
        }

        
        public static string[] ReadListBox(ListBox listbox)
        {
            string[] newStringField = new string[0];

            foreach(var listBoxItem in listbox.Items)
            {
                newStringField = HandleArrayFunctions.Add(newStringField, listBoxItem.ToString());
            }

            if(newStringField.Length == 0)
            {
                newStringField = null;
            }
            return newStringField;
        }
        


        public static string[] ReadDatapointTypes(ListBox listbox)
        {
            string[] DatapointTypesArray = new string[1];
            if (listbox.SelectedItem != null)
            {
                DatapointTypesArray[0] = (listbox.SelectedItem as HandleDatapointTypes.DatapointListBoxData).DPST;
                return DatapointTypesArray;
            }
            return null;
        }

        public static byte[] ReadGuiType(ListBox listbox)
        {
            byte[] data = new byte[0];
            for (int i = 0; i < listbox.Items.Count; i++)
            {
                Array.Resize(ref data, i + 1);
                data[i] = (byte)listbox.Items[i];
            }
            if(data.Length == 0)
            {
                data = null;
            }
            return data;
        }
        
        public static ComObjectSecurityRequirements_t ReadComObjectSecurityRequirements(string comObjectSercurityRequirements)
        {
            switch (comObjectSercurityRequirements)
            {
                case "Auth":
                    return ComObjectSecurityRequirements_t.Auth;
                case "AuthAndConf":
                    return ComObjectSecurityRequirements_t.AuthAndConf;
                case "None":
                    return ComObjectSecurityRequirements_t.None;
                default:
                    return ComObjectSecurityRequirements_t.None;
            }
        }


        public static RFRxCapabilities_t ReadRFRxCapabilities(string rfrxCapabilities)
        {
            switch (rfrxCapabilities)
            {
                case "Ready":
                    return RFRxCapabilities_t.Ready;
                case "ReadyFast":
                    return RFRxCapabilities_t.ReadyFast;
                case "Slow":
                    return RFRxCapabilities_t.Slow;
                default:
                    return RFRxCapabilities_t.Ready;
            }
        }

        public static RFTxCapabilities_t ReadRFTxCapabilities(string rftxCapabilities)
        {
            switch (rftxCapabilities)
            {
                case "Ready":
                    return RFTxCapabilities_t.Ready;
                case "ReadyFast":
                    return RFTxCapabilities_t.ReadyFast;
                case "ReadyFastSlow":
                    return RFTxCapabilities_t.ReadyFastSlow;
                default:
                    return RFTxCapabilities_t.Ready;
            }
        }

        
        public static CouplerCapability_t[] ReadCouplerCapability(ListBox listbox)
        {
            CouplerCapability_t[] newCouplerCapability = new CouplerCapability_t[0];

            foreach (object couplerCapabilityitem in listbox.Items)
            {
                switch (couplerCapabilityitem.ToString())
                {
                    case "RfReady":
                        newCouplerCapability = HandleArrayFunctions.Add(newCouplerCapability, CouplerCapability_t.RfReady);
                        break;
                    case "RfMultiFast":
                        newCouplerCapability = HandleArrayFunctions.Add(newCouplerCapability, CouplerCapability_t.RfMultiFast);
                        break;
                    case "RfMultiSlow":
                        newCouplerCapability = HandleArrayFunctions.Add(newCouplerCapability, CouplerCapability_t.RfMultiSlow);
                        break;
                    case "SecurityProxy":
                        newCouplerCapability = HandleArrayFunctions.Add(newCouplerCapability, CouplerCapability_t.SecurityProxy);
                        break;
                }
            }
            if(newCouplerCapability.Length == 0)
            {
                newCouplerCapability = null;
            }
            return newCouplerCapability;
        }
        

        public static Hardware_tProductAttributeName ReadProductAttributeName(string productAttributeName)
        {
            switch (productAttributeName)
            {
                case "CatalogName":
                    return Hardware_tProductAttributeName.CatalogName;
                case "Colour":
                    return Hardware_tProductAttributeName.Colour;
                case "Series":
                    return Hardware_tProductAttributeName.Series;
                default:
                    return Hardware_tProductAttributeName.CatalogName;
            }
        }

        public static ApplicationProgramType_t ReadApplicationProgramType(string applicationProgramType)
        {
            switch (applicationProgramType)
            {
                case "ApplicationProgram":
                    return ApplicationProgramType_t.ApplicationProgram;
                case "PeiProgram":
                    return ApplicationProgramType_t.PeiProgram;
                default:
                    return ApplicationProgramType_t.ApplicationProgram;
            }
        }

        public static LoadProcedureStyle_t ReadLoadProcedureStyle(string loadProcedureStyle)
        {
            switch (loadProcedureStyle)
            {
                case "DefaultProcedure":
                    return LoadProcedureStyle_t.DefaultProcedure;
                case "MergedProcedure":
                    return LoadProcedureStyle_t.MergedProcedure;
                case "ProductProcedure":
                    return LoadProcedureStyle_t.ProductProcedure;
                default:
                    return LoadProcedureStyle_t.DefaultProcedure;
            }
        }

        public static ApplicationProgramIPConfig_t ReadApplicationProgramIPConfig(string applicationProgramIPConfig)
        {
            switch (applicationProgramIPConfig)
            {
                case "Custom":
                    return ApplicationProgramIPConfig_t.Custom;
                case "Tool":
                    return ApplicationProgramIPConfig_t.Tool;
                default:
                    return ApplicationProgramIPConfig_t.Custom;
            }
        }
        
        public static MemoryType_t ReadMemType (string memoryType)
        {
            switch (memoryType)
            {
                case "EEPROM":
                    return MemoryType_t.EEPROM;
                case "FLASH":
                    return MemoryType_t.FLASH;
                case "RAM":
                    return MemoryType_t.RAM;
                default:
                    return MemoryType_t.EEPROM;
            }
        }
        
        public static ParameterType_tTypeRestrictionBase ReadTypeResBase (string typeResBase)
        {
            switch (typeResBase)
            {
                case "BinaryValue":
                    return ParameterType_tTypeRestrictionBase.BinaryValue;
                case "Value":
                    return ParameterType_tTypeRestrictionBase.Value;
                default:
                    return ParameterType_tTypeRestrictionBase.Value;
            }
        }
        
        public static ParameterType_tTypeRestrictionUIHint ReadTypeResUIHint (string typeRestrictionUIHint)
        {
            switch (typeRestrictionUIHint)
            {
                case "Buttons":
                    {
                        return ParameterType_tTypeRestrictionUIHint.Buttons;
                    }
                case "DropDown":
                    {
                        return ParameterType_tTypeRestrictionUIHint.DropDown;
                    }
                case "Segmented":
                    {
                        return ParameterType_tTypeRestrictionUIHint.Segmented;
                    }
                case "Text":
                    {
                        return ParameterType_tTypeRestrictionUIHint.Text;
                    }
                default:
                    return ParameterType_tTypeRestrictionUIHint.Text;
            }
        }
        
        public static HorizontalAlignment_t ReadPictureAlignment(string horizontalAlignment)
        {
            switch (horizontalAlignment)
            {
                case "Left":
                    {
                        return HorizontalAlignment_t.Left; 
                    }
                case "Middle":
                    {
                        return HorizontalAlignment_t.Middle;
                    }
                case "Repeat":
                    {
                        return HorizontalAlignment_t.Repeat;
                    }
                case "Right":
                    {
                        return HorizontalAlignment_t.Right;
                    }
                case "Stretch":
                    {
                        return HorizontalAlignment_t.Stretch;
                    }
                default:
                    {
                        return HorizontalAlignment_t.Left;
                    }
            }
        }

        
        public static ParameterType_tTypeNumberType ReadTypeNumberType(string numberType)
        {
            switch (numberType)
            {
                case "signedInt":
                    {
                        return ParameterType_tTypeNumberType.signedInt;
                    }
                case "unsignedInt":
                    {
                        return ParameterType_tTypeNumberType.unsignedInt;
                    }
                default:
                    {
                        return ParameterType_tTypeNumberType.signedInt;
                    }
            }
        }
        
        public static ParameterType_tTypeNumberUIHint ReadTypeNumberUIHint (string uIHint)
        {
            switch (uIHint)
            {
                case "CheckBox":
                    {
                        return ParameterType_tTypeNumberUIHint.CheckBox;
                    }
                case "ProgressBar":
                    {
                        return ParameterType_tTypeNumberUIHint.ProgressBar;
                    }
                case "Slider":
                    {
                        return ParameterType_tTypeNumberUIHint.Slider;
                    }
                default:
                    {
                        return ParameterType_tTypeNumberUIHint.Slider;
                    }
            }
        }
        
        public static ParameterType_tTypeFloatEncoding ReadTypeFloatEncoding (string floatEncoding)
        {
            switch (floatEncoding)
            {
                case "DPT 9":
                    {
                        return ParameterType_tTypeFloatEncoding.DPT9;
                    }
                case "IEEE-754 Double":
                    {
                        return ParameterType_tTypeFloatEncoding.IEEE754Double;
                    }
                case "IEEE-754 Single":
                    {
                        return ParameterType_tTypeFloatEncoding.IEEE754Single;
                    }
                default:
                    return ParameterType_tTypeFloatEncoding.DPT9;
            }
        }

        public static ParameterType_tTypeFloatUIHint ReadTypeFloatUIHint (string floatUIHint)
        {
            switch (floatUIHint)
            {
                case "Slider":
                    {
                        return ParameterType_tTypeFloatUIHint.Slider;
                    }
                default:
                    {
                        return ParameterType_tTypeFloatUIHint.Slider;
                    }
            }
        }
    }
}