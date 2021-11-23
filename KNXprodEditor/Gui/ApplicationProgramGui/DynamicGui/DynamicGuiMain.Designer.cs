
namespace knxprod_ns
{
    partial class DynamicGuiMain
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
            this.panelDynamicTabControl = new System.Windows.Forms.Panel();
            this.panelDynamicTreeView = new System.Windows.Forms.Panel();
            this.label311 = new System.Windows.Forms.Label();
            this.sblibHeader = new System.Windows.Forms.Button();
            this.panelDynamicMemoryTable = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelDynamicTabControl
            // 
            this.panelDynamicTabControl.Location = new System.Drawing.Point(420, 3);
            this.panelDynamicTabControl.Name = "panelDynamicTabControl";
            this.panelDynamicTabControl.Size = new System.Drawing.Size(801, 844);
            this.panelDynamicTabControl.TabIndex = 40;
            // 
            // panelDynamicTreeView
            // 
            this.panelDynamicTreeView.Location = new System.Drawing.Point(3, 3);
            this.panelDynamicTreeView.Name = "panelDynamicTreeView";
            this.panelDynamicTreeView.Size = new System.Drawing.Size(411, 844);
            this.panelDynamicTreeView.TabIndex = 39;
            // 
            // label311
            // 
            this.label311.AutoSize = true;
            this.label311.Location = new System.Drawing.Point(1357, 8);
            this.label311.Name = "label311";
            this.label311.Size = new System.Drawing.Size(255, 13);
            this.label311.TabIndex = 36;
            this.label311.Text = "C Header Datei für die gewählte Applikation erstellen";
            // 
            // sblibHeader
            // 
            this.sblibHeader.Location = new System.Drawing.Point(1227, 3);
            this.sblibHeader.Name = "sblibHeader";
            this.sblibHeader.Size = new System.Drawing.Size(124, 23);
            this.sblibHeader.TabIndex = 35;
            this.sblibHeader.Text = "DIY C Header erstellen";
            this.sblibHeader.UseVisualStyleBackColor = true;
            this.sblibHeader.Click += new System.EventHandler(this.sblibHeader_Click);
            // 
            // panelDynamicMemoryTable
            // 
            this.panelDynamicMemoryTable.Location = new System.Drawing.Point(1227, 32);
            this.panelDynamicMemoryTable.Name = "panelDynamicMemoryTable";
            this.panelDynamicMemoryTable.Size = new System.Drawing.Size(419, 815);
            this.panelDynamicMemoryTable.TabIndex = 41;
            // 
            // DynamicGuiMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelDynamicMemoryTable);
            this.Controls.Add(this.panelDynamicTabControl);
            this.Controls.Add(this.panelDynamicTreeView);
            this.Controls.Add(this.label311);
            this.Controls.Add(this.sblibHeader);
            this.Name = "DynamicGuiMain";
            this.Size = new System.Drawing.Size(1679, 884);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelDynamicTabControl;
        private System.Windows.Forms.Panel panelDynamicTreeView;
        private System.Windows.Forms.Label label311;
        private System.Windows.Forms.Button sblibHeader;
        private System.Windows.Forms.Panel panelDynamicMemoryTable;
    }
}
