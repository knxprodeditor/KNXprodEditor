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
    public partial class ParameterRefRefGui : UserControl
    {
        private static ParameterRefRefGui mParameterRefRefGui;

        public ParameterRefRefGui()
        {
            InitializeComponent();
            mParameterRefRefGui = this;
        }

        /****************************************************************************************************************************/
        // ParameterRefRef View

        public static ParameterRef_t FillParameterRefRefPage(ParameterRefRef_t parRefRef)
        {
            DynamicTabControl.AddParameterRefRefTab();
            ParameterRef_t returnParameterRef = ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == parRefRef.RefId);

            mParameterRefRefGui.textPrrRefId.Text = parRefRef.RefId;
            mParameterRefRefGui.textPrrHelpContext.Text = parRefRef.HelpContext;
            mParameterRefRefGui.numericPrrIdentLevel.Value = parRefRef.IndentLevel;
            mParameterRefRefGui.textPrrInternalDescription.Text = parRefRef.InternalDescription;
            mParameterRefRefGui.textPrrCell.Text = parRefRef.Cell;
            mParameterRefRefGui.textPrrIcon.Text = parRefRef.Icon;
            return returnParameterRef;
        }

        /****************************************************************************************************************************/
        // ParameterRefRef Edit


        // der speichern Button im ParameterRefRef Tab
        private void buttonParaRefRefSave_Click(object sender, EventArgs e)
        {
            ParameterRefRef_t paraRefRef = DynamicTreeViewGui.GetSelectedTreeNode().Tag as ParameterRefRef_t;

            paraRefRef.HelpContext = Extensions.NullIfEmpty(textPrrHelpContext.Text);
            paraRefRef.IndentLevel = (sbyte)numericPrrIdentLevel.Value;
            paraRefRef.InternalDescription = Extensions.NullIfEmpty(textPrrInternalDescription.Text);
            paraRefRef.Cell = Extensions.NullIfEmpty(textPrrCell.Text);
            paraRefRef.Icon = Extensions.NullIfEmpty(textPrrIcon.Text);
        }

        public static void SetParameterRefRefRefId(string parameterRefRefRefId)
        {
            mParameterRefRefGui.textPrrRefId.Text = parameterRefRefRefId;
        }
    }
}
