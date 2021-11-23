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
    public partial class ParameterSeparatorGui : UserControl
    {
        private static ParameterSeparatorGui mParameterSeparatorGui;

        public ParameterSeparatorGui()
        {
            InitializeComponent();
            mParameterSeparatorGui = this;
        }

        public static void FillParameterSeparatorPage(ParameterSeparator_t paramSeparator)
        {
            mParameterSeparatorGui.textPsId.Text = paramSeparator.Id;
            mParameterSeparatorGui.textPsName.Text = paramSeparator.Name;
            mParameterSeparatorGui.textPsText.Text = paramSeparator.Text;
            mParameterSeparatorGui.textPsAccess.Text = HandleKnxDataTypes.ReadKNXType(paramSeparator.Access);
            mParameterSeparatorGui.textPsUIHint.Text = HandleKnxDataTypes.GetXmlEnumAttributeValueFromEnum(paramSeparator.UIHint);
            mParameterSeparatorGui.textPsUIHintFieldSpecified.Text = HandleKnxDataTypes.ReadKNXType(paramSeparator.UIHintSpecified);
            mParameterSeparatorGui.textPsTextParameterRefId.Text = paramSeparator.TextParameterRefId;
            mParameterSeparatorGui.textPsInternalDescription.Text = paramSeparator.InternalDescription;
            mParameterSeparatorGui.textPsCell.Text = paramSeparator.Cell;
            mParameterSeparatorGui.textPsIcon.Text = paramSeparator.Icon;
            DynamicTabControl.AddParameterSeparatorTab();
        }
    }
}
