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
    public partial class DynamicMemoryTable : UserControl
    {
        public static DynamicMemoryTable mDynamicMemoryTable;

        public DynamicMemoryTable()
        {
            InitializeComponent();
            mDynamicMemoryTable = this;
        }

        /// <summary>
        /// Initialisiert die EEPROM Tabelle im Parameter und KO Tab
        /// </summary>
        public static void InitEEPROMView()
        {
            mDynamicMemoryTable.comboBoxParCoCodeSegment.Items.Clear();

            if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment != null)
            {
                foreach (ApplicationProgramStatic_tCodeAbsoluteSegment codeSegement in ApplicationProgramGui.selectedApplicationProgram.Static.Code.AbsoluteSegment)
                {
                    uint endAddress = codeSegement.Address + codeSegement.Size;
                    mDynamicMemoryTable.comboBoxParCoCodeSegment.Items.Add(new ComboBoxItem("Abs. Adressbereich: 0x" + codeSegement.Address.ToString("X4") + " - 0x" + endAddress.ToString("X4"), codeSegement));
                }
            }
            if (ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment != null)
            {
                foreach (ApplicationProgramStatic_tCodeRelativeSegment codeSegement in ApplicationProgramGui.selectedApplicationProgram.Static.Code.RelativeSegment)
                {
                    uint endAddress = codeSegement.Offset + codeSegement.Size;
                    mDynamicMemoryTable.comboBoxParCoCodeSegment.Items.Add(new ComboBoxItem("Rel. Adressbereich: 0x" + codeSegement.Offset.ToString("X4") + " - 0x" + endAddress.ToString("X4"), codeSegement));
                }
            }

            if (mDynamicMemoryTable.comboBoxParCoCodeSegment.Items.Count > 0)
            {
                mDynamicMemoryTable.comboBoxParCoCodeSegment.SelectedIndex = 0; // den ersten Speicherbereich aktivieren
                MemoryTable.CreateEEPROMViewContent(mDynamicMemoryTable.comboBoxParCoCodeSegment, mDynamicMemoryTable.dataGridViewParCoUserEeprom);
            }
        }

        // die ComboBox im Parameter und KO Tab
        private void ComboBoxParCoCodeSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            MemoryTable.CreateEEPROMViewContent(comboBoxParCoCodeSegment, dataGridViewParCoUserEeprom);
        }

        /*
         * die Formatierung der DataGridView Zelle anpassen
         */
        private void dataGridViewParCoUserEeprom_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            MemoryTable.DataGridViewUserEepromCellPainting(e, dataGridViewParCoUserEeprom);
        }

        /*
         * die Farbe der DataGridView Zelle anpassen 
         */
        private void dataGridViewParCoUserEeprom_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            MemoryTable.DatagridViewUserEepromCellFormatting(e, dataGridViewParCoUserEeprom);
        }

    }
}
