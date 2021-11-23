
namespace knxprod_ns
{
    partial class AssociationTableGui
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
            this.label737 = new System.Windows.Forms.Label();
            this.label736 = new System.Windows.Forms.Label();
            this.label735 = new System.Windows.Forms.Label();
            this.label734 = new System.Windows.Forms.Label();
            this.label733 = new System.Windows.Forms.Label();
            this.labelAssoTableNoAssociationTable = new System.Windows.Forms.Label();
            this.numericAssoTableMaxEntries = new System.Windows.Forms.NumericUpDown();
            this.numericAssoTableOffset = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAssoTableOffsetSpecified = new System.Windows.Forms.CheckBox();
            this.label672 = new System.Windows.Forms.Label();
            this.label671 = new System.Windows.Forms.Label();
            this.label670 = new System.Windows.Forms.Label();
            this.textAssoTableCodeSegment = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericAssoTableMaxEntries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAssoTableOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // label737
            // 
            this.label737.AutoSize = true;
            this.label737.Location = new System.Drawing.Point(3, 157);
            this.label737.Name = "label737";
            this.label737.Size = new System.Drawing.Size(218, 13);
            this.label737.TabIndex = 25;
            this.label737.Text = "Referenz: KNX Spec. 3/5/1 Resources 4.11";
            // 
            // label736
            // 
            this.label736.AutoSize = true;
            this.label736.Location = new System.Drawing.Point(3, 144);
            this.label736.Name = "label736";
            this.label736.Size = new System.Drawing.Size(670, 13);
            this.label736.TabIndex = 24;
            this.label736.Text = "Die andere Spalte beinhaltet die Nummern der Kommunikationsobjekte. Diese werden " +
    "als ASAP (Application Layer Access Point) bezeichnet.";
            // 
            // label735
            // 
            this.label735.AutoSize = true;
            this.label735.Location = new System.Drawing.Point(3, 131);
            this.label735.Name = "label735";
            this.label735.Size = new System.Drawing.Size(650, 13);
            this.label735.TabIndex = 23;
            this.label735.Text = "Eine Spalte beinhatet die Refereznz auf die Address Table und die Einträge werden" +
    " als TSAP (Tansport Layer Access Point) bezeichnet.";
            // 
            // label734
            // 
            this.label734.AutoSize = true;
            this.label734.Location = new System.Drawing.Point(3, 118);
            this.label734.Name = "label734";
            this.label734.Size = new System.Drawing.Size(162, 13);
            this.label734.TabIndex = 22;
            this.label734.Text = "Dazu werden 2 Spalten benötigt:";
            // 
            // label733
            // 
            this.label733.AutoSize = true;
            this.label733.Location = new System.Drawing.Point(3, 105);
            this.label733.Name = "label733";
            this.label733.Size = new System.Drawing.Size(417, 13);
            this.label733.TabIndex = 21;
            this.label733.Text = "Die Association Table verknüft die Gruppenadressen mit den Kommunikationsobjekten" +
    ".";
            // 
            // labelAssoTableNoAssociationTable
            // 
            this.labelAssoTableNoAssociationTable.AutoSize = true;
            this.labelAssoTableNoAssociationTable.ForeColor = System.Drawing.Color.Red;
            this.labelAssoTableNoAssociationTable.Location = new System.Drawing.Point(3, 0);
            this.labelAssoTableNoAssociationTable.Name = "labelAssoTableNoAssociationTable";
            this.labelAssoTableNoAssociationTable.Size = new System.Drawing.Size(232, 13);
            this.labelAssoTableNoAssociationTable.TabIndex = 20;
            this.labelAssoTableNoAssociationTable.Text = "ApplicationProgram hat keine AssociationTable!";
            // 
            // numericAssoTableMaxEntries
            // 
            this.numericAssoTableMaxEntries.Location = new System.Drawing.Point(84, 73);
            this.numericAssoTableMaxEntries.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numericAssoTableMaxEntries.Name = "numericAssoTableMaxEntries";
            this.numericAssoTableMaxEntries.Size = new System.Drawing.Size(301, 20);
            this.numericAssoTableMaxEntries.TabIndex = 19;
            // 
            // numericAssoTableOffset
            // 
            this.numericAssoTableOffset.Location = new System.Drawing.Point(105, 47);
            this.numericAssoTableOffset.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numericAssoTableOffset.Name = "numericAssoTableOffset";
            this.numericAssoTableOffset.Size = new System.Drawing.Size(280, 20);
            this.numericAssoTableOffset.TabIndex = 18;
            // 
            // checkBoxAssoTableOffsetSpecified
            // 
            this.checkBoxAssoTableOffsetSpecified.AutoSize = true;
            this.checkBoxAssoTableOffsetSpecified.Location = new System.Drawing.Point(84, 49);
            this.checkBoxAssoTableOffsetSpecified.Name = "checkBoxAssoTableOffsetSpecified";
            this.checkBoxAssoTableOffsetSpecified.Size = new System.Drawing.Size(15, 14);
            this.checkBoxAssoTableOffsetSpecified.TabIndex = 17;
            this.checkBoxAssoTableOffsetSpecified.UseVisualStyleBackColor = true;
            // 
            // label672
            // 
            this.label672.AutoSize = true;
            this.label672.Location = new System.Drawing.Point(3, 75);
            this.label672.Name = "label672";
            this.label672.Size = new System.Drawing.Size(59, 13);
            this.label672.TabIndex = 16;
            this.label672.Text = "MaxEntries";
            // 
            // label671
            // 
            this.label671.AutoSize = true;
            this.label671.Location = new System.Drawing.Point(3, 49);
            this.label671.Name = "label671";
            this.label671.Size = new System.Drawing.Size(35, 13);
            this.label671.TabIndex = 15;
            this.label671.Text = "Offset";
            // 
            // label670
            // 
            this.label670.AutoSize = true;
            this.label670.Location = new System.Drawing.Point(3, 23);
            this.label670.Name = "label670";
            this.label670.Size = new System.Drawing.Size(74, 13);
            this.label670.TabIndex = 14;
            this.label670.Text = "CodeSegment";
            // 
            // textAssoTableCodeSegment
            // 
            this.textAssoTableCodeSegment.Location = new System.Drawing.Point(83, 20);
            this.textAssoTableCodeSegment.Name = "textAssoTableCodeSegment";
            this.textAssoTableCodeSegment.Size = new System.Drawing.Size(302, 20);
            this.textAssoTableCodeSegment.TabIndex = 13;
            // 
            // AssociationTableGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label737);
            this.Controls.Add(this.label736);
            this.Controls.Add(this.label735);
            this.Controls.Add(this.label734);
            this.Controls.Add(this.label733);
            this.Controls.Add(this.labelAssoTableNoAssociationTable);
            this.Controls.Add(this.numericAssoTableMaxEntries);
            this.Controls.Add(this.numericAssoTableOffset);
            this.Controls.Add(this.checkBoxAssoTableOffsetSpecified);
            this.Controls.Add(this.label672);
            this.Controls.Add(this.label671);
            this.Controls.Add(this.label670);
            this.Controls.Add(this.textAssoTableCodeSegment);
            this.Name = "AssociationTableGui";
            this.Size = new System.Drawing.Size(1655, 856);
            ((System.ComponentModel.ISupportInitialize)(this.numericAssoTableMaxEntries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAssoTableOffset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label737;
        private System.Windows.Forms.Label label736;
        private System.Windows.Forms.Label label735;
        private System.Windows.Forms.Label label734;
        private System.Windows.Forms.Label label733;
        private System.Windows.Forms.Label labelAssoTableNoAssociationTable;
        private System.Windows.Forms.NumericUpDown numericAssoTableMaxEntries;
        private System.Windows.Forms.NumericUpDown numericAssoTableOffset;
        private System.Windows.Forms.CheckBox checkBoxAssoTableOffsetSpecified;
        private System.Windows.Forms.Label label672;
        private System.Windows.Forms.Label label671;
        private System.Windows.Forms.Label label670;
        private System.Windows.Forms.TextBox textAssoTableCodeSegment;
    }
}
