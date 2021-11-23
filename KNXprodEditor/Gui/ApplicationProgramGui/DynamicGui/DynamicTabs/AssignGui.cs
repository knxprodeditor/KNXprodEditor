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
    public partial class AssignGui : UserControl
    {
        private static AssignGui mAssignGui;

        public AssignGui()
        {
            InitializeComponent();
            mAssignGui = this;
        }

        public static void FillAssignPage(Assign_t assign)
        {
            DynamicTabControl.AddAssignTab();
            mAssignGui.textAssTargetParamRefRef.Text = assign.TargetParamRefRef;
            mAssignGui.textAssSourceParamRefRef.Text = assign.SourceParamRefRef;
            mAssignGui.textAssValue.Text = assign.Value;
            mAssignGui.textAssInternalDescription.Text = assign.InternalDescription;
            mAssignGui.checkBoxAssignTargetParamRefRef.Checked = true;
        }


        private void CheckBoxAssignSourceParamRefRef_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAssignSourceParamRefRef.Checked)
            {
                checkBoxAssignTargetParamRefRef.Checked = false;
                DynamicTabControl.RemoveParameterRefTab();
                DynamicTabControl.RemoveParameterTab();
                DynamicTabControl.RemoveUnionParameterTab();
                ParameterRef_t resolveParaRefRef = ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == textAssSourceParamRefRef.Text);
                DynamicTreeViewGui.FillParameterOrUnionPage(ParameterRefGui.FillParameterRefPage(resolveParaRefRef));
            }
        }

        private void CheckBoxAssignTargetParamRefRef_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAssignTargetParamRefRef.Checked)
            {
                checkBoxAssignSourceParamRefRef.Checked = false;
                DynamicTabControl.RemoveParameterRefTab();
                DynamicTabControl.RemoveParameterTab();
                DynamicTabControl.RemoveUnionParameterTab();
                ParameterRef_t resolveParaRefRef = ApplicationProgramGui.selectedApplicationProgram.Static.ParameterRefs.ToList().Find(x => x.Id == textAssTargetParamRefRef.Text);
                DynamicTreeViewGui.FillParameterOrUnionPage(ParameterRefGui.FillParameterRefPage(resolveParaRefRef));
            }
        }
    }
}
