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
    public partial class AssociationTableGui : UserControl
    {
        private static AssociationTableGui mAssociationTableGui;

        public AssociationTableGui()
        {
            InitializeComponent();
            mAssociationTableGui = this;
        }

        public static void InitializeAssociationTable()
        {
            ApplicationProgramStatic_tAssociationTable associationTable = ApplicationProgramGui.selectedApplicationProgram.Static.AssociationTable;
            if (associationTable != null)
            {
                mAssociationTableGui.labelAssoTableNoAssociationTable.Visible = false;
                mAssociationTableGui.textAssoTableCodeSegment.Text = associationTable.CodeSegment;
                mAssociationTableGui.checkBoxAssoTableOffsetSpecified.Checked = associationTable.OffsetSpecified;
                mAssociationTableGui.numericAssoTableOffset.Value = associationTable.Offset;
                mAssociationTableGui.numericAssoTableMaxEntries.Value = associationTable.MaxEntries;
            }
            else
            {
                mAssociationTableGui.labelAssoTableNoAssociationTable.Visible = true;
            }
        }
    }
}
