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
    public partial class ComObjectRefRefGui : UserControl
    {
        private static ComObjectRefRefGui mComObjectRefRefGui;

        public ComObjectRefRefGui()
        {
            InitializeComponent();
            mComObjectRefRefGui = this;
        }

        /****************************************************************************************************************************/
        // ComObjectRefRef View

        public static ComObjectRef_t FillComObjectRefRef(ComObjectRefRef_t comObjRefRef)
        {
            DynamicTabControl.AddComObjectRefRefTab();
            ComObjectRef_t returnComObjectRef = ApplicationProgramGui.selectedApplicationProgram.Static.ComObjectRefs.ToList().Find(x => x.Id == comObjRefRef.RefId);

            mComObjectRefRefGui.textCorrRefId.Text = comObjRefRef.RefId;
            mComObjectRefRefGui.textCorrInternalDescription.Text = comObjRefRef.InternalDescription;

            return returnComObjectRef;
        }

        public static void ClearComObjectRefRefPage()
        {
            mComObjectRefRefGui.textCorrRefId.Text = "";
            mComObjectRefRefGui.textCorrInternalDescription.Text = "";
        }

        /****************************************************************************************************************************/
        // ComObjectRefRef Edit



        // der speichern Button im ComObjectRefRef Tab
        private void buttonComObjRefRefSave_Click(object sender, EventArgs e)
        {
            ComObjectRefRef_t comObjRefRef = DynamicTreeViewGui.GetSelectedTreeNode().Tag as ComObjectRefRef_t;

            comObjRefRef.InternalDescription = Extensions.NullIfEmpty(textCorrInternalDescription.Text);
        }

        public static void SetComObjectRefRefRefId(string comObjectRefRefRefId)
        {
            mComObjectRefRefGui.textCorrRefId.Text = comObjectRefRefRefId;
        }
    }
}
