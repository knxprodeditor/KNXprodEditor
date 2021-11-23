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
    public partial class AddressTableGui : UserControl
    {
        private static AddressTableGui mAddressTableGui;

        public AddressTableGui()
        {
            InitializeComponent();
            mAddressTableGui = this;
        }

        public static void InitializeAddressTable()
        {
            ApplicationProgramStatic_tAddressTable addressTable = ApplicationProgramGui.selectedApplicationProgram.Static.AddressTable;
            if (addressTable != null)
            {
                mAddressTableGui.labelAddressTableNoAddressTable.Visible = false;
                mAddressTableGui.textAddressTableCodeSegment.Text = addressTable.CodeSegment;
                mAddressTableGui.checkBoxAddressTableOffsetSpecified.Checked = addressTable.OffsetSpecified;
                mAddressTableGui.numericAddressTableOffset.Value = addressTable.Offset;
                mAddressTableGui.numericAddressTableMaxEntries.Value = addressTable.MaxEntries;
            }
            else
            {
                mAddressTableGui.labelAddressTableNoAddressTable.Visible = true;
            }
        }
    }
}
