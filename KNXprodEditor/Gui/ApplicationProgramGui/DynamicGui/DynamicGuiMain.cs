using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace knxprod_ns
{
    public partial class DynamicGuiMain : UserControl
    {
        public DynamicGuiMain()
        {
            InitializeComponent();
            panelDynamicTreeView.Controls.Add(new DynamicTreeViewGui());
            panelDynamicTabControl.Controls.Add(new DynamicTabControl());
            panelDynamicMemoryTable.Controls.Add(new DynamicMemoryTable());
        }

        private void sblibHeader_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialogSblibHeader = new SaveFileDialog();

            saveFileDialogSblibHeader.Filter = "Header file (*.h)|*.h|All files (*.*)|*.*";
            saveFileDialogSblibHeader.FilterIndex = 1;
            saveFileDialogSblibHeader.RestoreDirectory = true;

            if (saveFileDialogSblibHeader.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter headerFile =
                    new System.IO.StreamWriter(saveFileDialogSblibHeader.FileName))
                {
                    KNXprodToCHeader.CreateSblibHeader(headerFile, saveFileDialogSblibHeader.FileName);
                }
            }
        }
    }
}
