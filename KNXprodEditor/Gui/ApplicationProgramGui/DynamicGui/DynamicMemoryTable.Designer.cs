
namespace knxprod_ns
{
    partial class DynamicMemoryTable
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label331 = new System.Windows.Forms.Label();
            this.comboBoxParCoCodeSegment = new System.Windows.Forms.ComboBox();
            this.dataGridViewParCoUserEeprom = new System.Windows.Forms.DataGridView();
            this.label314 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParCoUserEeprom)).BeginInit();
            this.SuspendLayout();
            // 
            // label331
            // 
            this.label331.AutoSize = true;
            this.label331.Location = new System.Drawing.Point(3, 6);
            this.label331.Name = "label331";
            this.label331.Size = new System.Drawing.Size(74, 13);
            this.label331.TabIndex = 42;
            this.label331.Text = "CodeSegment";
            // 
            // comboBoxParCoCodeSegment
            // 
            this.comboBoxParCoCodeSegment.FormattingEnabled = true;
            this.comboBoxParCoCodeSegment.Location = new System.Drawing.Point(83, 3);
            this.comboBoxParCoCodeSegment.Name = "comboBoxParCoCodeSegment";
            this.comboBoxParCoCodeSegment.Size = new System.Drawing.Size(332, 21);
            this.comboBoxParCoCodeSegment.TabIndex = 41;
            this.comboBoxParCoCodeSegment.SelectedIndexChanged += new System.EventHandler(this.ComboBoxParCoCodeSegment_SelectedIndexChanged);
            // 
            // dataGridViewParCoUserEeprom
            // 
            this.dataGridViewParCoUserEeprom.AllowUserToAddRows = false;
            this.dataGridViewParCoUserEeprom.AllowUserToDeleteRows = false;
            this.dataGridViewParCoUserEeprom.AllowUserToResizeColumns = false;
            this.dataGridViewParCoUserEeprom.AllowUserToResizeRows = false;
            this.dataGridViewParCoUserEeprom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewParCoUserEeprom.Location = new System.Drawing.Point(6, 51);
            this.dataGridViewParCoUserEeprom.Name = "dataGridViewParCoUserEeprom";
            this.dataGridViewParCoUserEeprom.RowHeadersWidth = 80;
            this.dataGridViewParCoUserEeprom.Size = new System.Drawing.Size(409, 628);
            this.dataGridViewParCoUserEeprom.TabIndex = 40;
            this.dataGridViewParCoUserEeprom.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewParCoUserEeprom_CellFormatting);
            this.dataGridViewParCoUserEeprom.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewParCoUserEeprom_CellPainting);
            // 
            // label314
            // 
            this.label314.AutoSize = true;
            this.label314.Location = new System.Drawing.Point(3, 34);
            this.label314.Name = "label314";
            this.label314.Size = new System.Drawing.Size(140, 13);
            this.label314.TabIndex = 39;
            this.label314.Text = "UserEEPROM Adress Table";
            // 
            // DynamicMemoryTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label331);
            this.Controls.Add(this.comboBoxParCoCodeSegment);
            this.Controls.Add(this.dataGridViewParCoUserEeprom);
            this.Controls.Add(this.label314);
            this.Name = "DynamicMemoryTable";
            this.Size = new System.Drawing.Size(420, 684);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParCoUserEeprom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label331;
        private System.Windows.Forms.Label label314;
        public System.Windows.Forms.ComboBox comboBoxParCoCodeSegment;
        public System.Windows.Forms.DataGridView dataGridViewParCoUserEeprom;
    }
}
