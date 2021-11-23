
namespace knxprod_ns
{
    partial class AddressTableGui
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
            this.label738 = new System.Windows.Forms.Label();
            this.label732 = new System.Windows.Forms.Label();
            this.label731 = new System.Windows.Forms.Label();
            this.label730 = new System.Windows.Forms.Label();
            this.labelAddressTableNoAddressTable = new System.Windows.Forms.Label();
            this.numericAddressTableMaxEntries = new System.Windows.Forms.NumericUpDown();
            this.numericAddressTableOffset = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAddressTableOffsetSpecified = new System.Windows.Forms.CheckBox();
            this.label673 = new System.Windows.Forms.Label();
            this.label674 = new System.Windows.Forms.Label();
            this.label675 = new System.Windows.Forms.Label();
            this.textAddressTableCodeSegment = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericAddressTableMaxEntries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAddressTableOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // label738
            // 
            this.label738.AutoSize = true;
            this.label738.Location = new System.Drawing.Point(3, 155);
            this.label738.Name = "label738";
            this.label738.Size = new System.Drawing.Size(218, 13);
            this.label738.TabIndex = 30;
            this.label738.Text = "Referenz: KNX Spec. 3/5/1 Resources 4.10";
            // 
            // label732
            // 
            this.label732.AutoSize = true;
            this.label732.Location = new System.Drawing.Point(1, 142);
            this.label732.Name = "label732";
            this.label732.Size = new System.Drawing.Size(535, 13);
            this.label732.TabIndex = 29;
            this.label732.Text = "Durch die Association Table werden die Einträge der Address Table mit den Kommuni" +
    "kationsobjekten verknüpft.";
            // 
            // label731
            // 
            this.label731.AutoSize = true;
            this.label731.Location = new System.Drawing.Point(1, 129);
            this.label731.Name = "label731";
            this.label731.Size = new System.Drawing.Size(573, 13);
            this.label731.TabIndex = 28;
            this.label731.Text = "Die Address Table hat 2 Byte große Einträge, welche Die Gruppenadressen mit ihrer" +
    " 2- oder 3-stufigen Struktur abbilden.";
            // 
            // label730
            // 
            this.label730.AutoSize = true;
            this.label730.Location = new System.Drawing.Point(1, 116);
            this.label730.Name = "label730";
            this.label730.Size = new System.Drawing.Size(916, 13);
            this.label730.TabIndex = 27;
            this.label730.Text = "In der Address Table sind alle dem Gerät zugewiesenen Gruppenadressen hinterlegt." +
    " Die Reihenfolge wird durch die ETS bestimmt und in das Gerät als kompletter Dat" +
    "ensatz hinein geschrieben.";
            // 
            // labelAddressTableNoAddressTable
            // 
            this.labelAddressTableNoAddressTable.AutoSize = true;
            this.labelAddressTableNoAddressTable.ForeColor = System.Drawing.Color.Red;
            this.labelAddressTableNoAddressTable.Location = new System.Drawing.Point(3, 0);
            this.labelAddressTableNoAddressTable.Name = "labelAddressTableNoAddressTable";
            this.labelAddressTableNoAddressTable.Size = new System.Drawing.Size(216, 13);
            this.labelAddressTableNoAddressTable.TabIndex = 26;
            this.labelAddressTableNoAddressTable.Text = "ApplicationProgram hat keine AddressTable!";
            // 
            // numericAddressTableMaxEntries
            // 
            this.numericAddressTableMaxEntries.Location = new System.Drawing.Point(84, 73);
            this.numericAddressTableMaxEntries.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numericAddressTableMaxEntries.Name = "numericAddressTableMaxEntries";
            this.numericAddressTableMaxEntries.Size = new System.Drawing.Size(301, 20);
            this.numericAddressTableMaxEntries.TabIndex = 25;
            // 
            // numericAddressTableOffset
            // 
            this.numericAddressTableOffset.Location = new System.Drawing.Point(105, 47);
            this.numericAddressTableOffset.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numericAddressTableOffset.Name = "numericAddressTableOffset";
            this.numericAddressTableOffset.Size = new System.Drawing.Size(280, 20);
            this.numericAddressTableOffset.TabIndex = 24;
            // 
            // checkBoxAddressTableOffsetSpecified
            // 
            this.checkBoxAddressTableOffsetSpecified.AutoSize = true;
            this.checkBoxAddressTableOffsetSpecified.Location = new System.Drawing.Point(84, 49);
            this.checkBoxAddressTableOffsetSpecified.Name = "checkBoxAddressTableOffsetSpecified";
            this.checkBoxAddressTableOffsetSpecified.Size = new System.Drawing.Size(15, 14);
            this.checkBoxAddressTableOffsetSpecified.TabIndex = 23;
            this.checkBoxAddressTableOffsetSpecified.UseVisualStyleBackColor = true;
            // 
            // label673
            // 
            this.label673.AutoSize = true;
            this.label673.Location = new System.Drawing.Point(3, 75);
            this.label673.Name = "label673";
            this.label673.Size = new System.Drawing.Size(59, 13);
            this.label673.TabIndex = 22;
            this.label673.Text = "MaxEntries";
            // 
            // label674
            // 
            this.label674.AutoSize = true;
            this.label674.Location = new System.Drawing.Point(3, 49);
            this.label674.Name = "label674";
            this.label674.Size = new System.Drawing.Size(35, 13);
            this.label674.TabIndex = 21;
            this.label674.Text = "Offset";
            // 
            // label675
            // 
            this.label675.AutoSize = true;
            this.label675.Location = new System.Drawing.Point(3, 23);
            this.label675.Name = "label675";
            this.label675.Size = new System.Drawing.Size(74, 13);
            this.label675.TabIndex = 20;
            this.label675.Text = "CodeSegment";
            // 
            // textAddressTableCodeSegment
            // 
            this.textAddressTableCodeSegment.Location = new System.Drawing.Point(83, 20);
            this.textAddressTableCodeSegment.Name = "textAddressTableCodeSegment";
            this.textAddressTableCodeSegment.Size = new System.Drawing.Size(302, 20);
            this.textAddressTableCodeSegment.TabIndex = 19;
            // 
            // AddressTableGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label738);
            this.Controls.Add(this.label732);
            this.Controls.Add(this.label731);
            this.Controls.Add(this.label730);
            this.Controls.Add(this.labelAddressTableNoAddressTable);
            this.Controls.Add(this.numericAddressTableMaxEntries);
            this.Controls.Add(this.numericAddressTableOffset);
            this.Controls.Add(this.checkBoxAddressTableOffsetSpecified);
            this.Controls.Add(this.label673);
            this.Controls.Add(this.label674);
            this.Controls.Add(this.label675);
            this.Controls.Add(this.textAddressTableCodeSegment);
            this.Name = "AddressTableGui";
            this.Size = new System.Drawing.Size(922, 175);
            ((System.ComponentModel.ISupportInitialize)(this.numericAddressTableMaxEntries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAddressTableOffset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label738;
        private System.Windows.Forms.Label label732;
        private System.Windows.Forms.Label label731;
        private System.Windows.Forms.Label label730;
        private System.Windows.Forms.Label labelAddressTableNoAddressTable;
        private System.Windows.Forms.NumericUpDown numericAddressTableMaxEntries;
        private System.Windows.Forms.NumericUpDown numericAddressTableOffset;
        private System.Windows.Forms.CheckBox checkBoxAddressTableOffsetSpecified;
        private System.Windows.Forms.Label label673;
        private System.Windows.Forms.Label label674;
        private System.Windows.Forms.Label label675;
        private System.Windows.Forms.TextBox textAddressTableCodeSegment;
    }
}
