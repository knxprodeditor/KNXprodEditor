namespace knxprod_ns
{
    partial class FormKNXprodEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKNXprodEditor));
            this.textFileSelection = new System.Windows.Forms.TextBox();
            this.tabControlCatHardApp = new System.Windows.Forms.TabControl();
            this.tabCatalogFile = new System.Windows.Forms.TabPage();
            this.tabHardwareFile = new System.Windows.Forms.TabPage();
            this.tabApplicationFile = new System.Windows.Forms.TabPage();
            this.comboBoxSelectedLanguage = new System.Windows.Forms.ComboBox();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.knxprodFileOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.knxprodFileSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.knxprodFileSaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ConverterEngineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBoxCurrentConverterEnginePath = new System.Windows.Forms.ToolStripTextBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.label221 = new System.Windows.Forms.Label();
            this.label328 = new System.Windows.Forms.Label();
            this.labelKnxprodFileSaved = new System.Windows.Forms.Label();
            this.tabControlCatHardApp.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textFileSelection
            // 
            this.textFileSelection.Location = new System.Drawing.Point(602, 3);
            this.textFileSelection.Name = "textFileSelection";
            this.textFileSelection.Size = new System.Drawing.Size(525, 20);
            this.textFileSelection.TabIndex = 11;
            // 
            // tabControlCatHardApp
            // 
            this.tabControlCatHardApp.AccessibleName = "";
            this.tabControlCatHardApp.Controls.Add(this.tabCatalogFile);
            this.tabControlCatHardApp.Controls.Add(this.tabHardwareFile);
            this.tabControlCatHardApp.Controls.Add(this.tabApplicationFile);
            this.tabControlCatHardApp.Location = new System.Drawing.Point(0, 27);
            this.tabControlCatHardApp.Name = "tabControlCatHardApp";
            this.tabControlCatHardApp.SelectedIndex = 0;
            this.tabControlCatHardApp.Size = new System.Drawing.Size(1676, 911);
            this.tabControlCatHardApp.TabIndex = 12;
            this.tabControlCatHardApp.Tag = "";
            // 
            // tabCatalogFile
            // 
            this.tabCatalogFile.Location = new System.Drawing.Point(4, 22);
            this.tabCatalogFile.Name = "tabCatalogFile";
            this.tabCatalogFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabCatalogFile.Size = new System.Drawing.Size(1668, 885);
            this.tabCatalogFile.TabIndex = 0;
            this.tabCatalogFile.Text = "Catalog";
            this.tabCatalogFile.UseVisualStyleBackColor = true;
            // 
            // tabHardwareFile
            // 
            this.tabHardwareFile.Location = new System.Drawing.Point(4, 22);
            this.tabHardwareFile.Name = "tabHardwareFile";
            this.tabHardwareFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabHardwareFile.Size = new System.Drawing.Size(1668, 885);
            this.tabHardwareFile.TabIndex = 2;
            this.tabHardwareFile.Text = "Hardware";
            this.tabHardwareFile.UseVisualStyleBackColor = true;
            // 
            // tabApplicationFile
            // 
            this.tabApplicationFile.Location = new System.Drawing.Point(4, 22);
            this.tabApplicationFile.Name = "tabApplicationFile";
            this.tabApplicationFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabApplicationFile.Size = new System.Drawing.Size(1668, 885);
            this.tabApplicationFile.TabIndex = 1;
            this.tabApplicationFile.Text = "ApplicationProgram";
            this.tabApplicationFile.UseVisualStyleBackColor = true;
            // 
            // comboBoxSelectedLanguage
            // 
            this.comboBoxSelectedLanguage.FormattingEnabled = true;
            this.comboBoxSelectedLanguage.Location = new System.Drawing.Point(340, 3);
            this.comboBoxSelectedLanguage.Name = "comboBoxSelectedLanguage";
            this.comboBoxSelectedLanguage.Size = new System.Drawing.Size(103, 21);
            this.comboBoxSelectedLanguage.TabIndex = 2;
            this.comboBoxSelectedLanguage.SelectionChangeCommitted += new System.EventHandler(this.comboBoxSelectedLanguage_SelectionChangeCommitted);
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.knxprodFileOpenToolStripMenuItem,
            this.knxprodFileSaveToolStripMenuItem,
            this.knxprodFileSaveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.ConverterEngineToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // knxprodFileOpenToolStripMenuItem
            // 
            this.knxprodFileOpenToolStripMenuItem.Name = "knxprodFileOpenToolStripMenuItem";
            this.knxprodFileOpenToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.knxprodFileOpenToolStripMenuItem.Text = "Datei öffnen";
            this.knxprodFileOpenToolStripMenuItem.Click += new System.EventHandler(this.knxprodFileOpenToolStripMenuItem_Click);
            // 
            // knxprodFileSaveToolStripMenuItem
            // 
            this.knxprodFileSaveToolStripMenuItem.Name = "knxprodFileSaveToolStripMenuItem";
            this.knxprodFileSaveToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.knxprodFileSaveToolStripMenuItem.Text = "Datei speichern          STRG+S";
            this.knxprodFileSaveToolStripMenuItem.Click += new System.EventHandler(this.knxprodFileSaveToolStripMenuItem_Click);
            // 
            // knxprodFileSaveAsToolStripMenuItem
            // 
            this.knxprodFileSaveAsToolStripMenuItem.Name = "knxprodFileSaveAsToolStripMenuItem";
            this.knxprodFileSaveAsToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.knxprodFileSaveAsToolStripMenuItem.Text = "Datei speichern unter";
            this.knxprodFileSaveAsToolStripMenuItem.Click += new System.EventHandler(this.knxprodFileSaveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(223, 6);
            // 
            // ConverterEngineToolStripMenuItem
            // 
            this.ConverterEngineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxCurrentConverterEnginePath});
            this.ConverterEngineToolStripMenuItem.Name = "ConverterEngineToolStripMenuItem";
            this.ConverterEngineToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.ConverterEngineToolStripMenuItem.Text = "ConverterEngine Pfad";
            this.ConverterEngineToolStripMenuItem.Click += new System.EventHandler(this.ConverterEngineToolStripMenuItem_Click);
            this.ConverterEngineToolStripMenuItem.MouseHover += new System.EventHandler(this.ConverterEngineToolStripMenuItem_MouseHover);
            // 
            // toolStripTextBoxCurrentConverterEnginePath
            // 
            this.toolStripTextBoxCurrentConverterEnginePath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxCurrentConverterEnginePath.Name = "toolStripTextBoxCurrentConverterEnginePath";
            this.toolStripTextBoxCurrentConverterEnginePath.Size = new System.Drawing.Size(600, 23);
            this.toolStripTextBoxCurrentConverterEnginePath.Click += new System.EventHandler(this.toolStripTextBoxCurrentConverterEnginePath_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1679, 24);
            this.menuStrip2.TabIndex = 14;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // label221
            // 
            this.label221.AutoSize = true;
            this.label221.Location = new System.Drawing.Point(251, 6);
            this.label221.Name = "label221";
            this.label221.Size = new System.Drawing.Size(83, 13);
            this.label221.TabIndex = 16;
            this.label221.Text = "Anzeigesprache";
            // 
            // label328
            // 
            this.label328.AutoSize = true;
            this.label328.Location = new System.Drawing.Point(524, 6);
            this.label328.Name = "label328";
            this.label328.Size = new System.Drawing.Size(72, 13);
            this.label328.TabIndex = 17;
            this.label328.Text = "aktuelle Datei";
            // 
            // labelKnxprodFileSaved
            // 
            this.labelKnxprodFileSaved.AutoSize = true;
            this.labelKnxprodFileSaved.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelKnxprodFileSaved.Location = new System.Drawing.Point(1134, 8);
            this.labelKnxprodFileSaved.Name = "labelKnxprodFileSaved";
            this.labelKnxprodFileSaved.Size = new System.Drawing.Size(71, 13);
            this.labelKnxprodFileSaved.TabIndex = 18;
            this.labelKnxprodFileSaved.Text = "gespeichert...";
            this.labelKnxprodFileSaved.Visible = false;
            // 
            // FormKNXprodEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(1679, 941);
            this.Controls.Add(this.labelKnxprodFileSaved);
            this.Controls.Add(this.label328);
            this.Controls.Add(this.label221);
            this.Controls.Add(this.tabControlCatHardApp);
            this.Controls.Add(this.textFileSelection);
            this.Controls.Add(this.comboBoxSelectedLanguage);
            this.Controls.Add(this.menuStrip2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormKNXprodEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.tabControlCatHardApp.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textFileSelection;
        private System.Windows.Forms.TabControl tabControlCatHardApp;
        private System.Windows.Forms.TabPage tabApplicationFile;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem knxprodFileOpenToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.TabPage tabHardwareFile;
        private System.Windows.Forms.ComboBox comboBoxSelectedLanguage;
        private System.Windows.Forms.Label label221;
        private System.Windows.Forms.Label label328;
        private System.Windows.Forms.ToolStripMenuItem knxprodFileSaveAsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem ConverterEngineToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxCurrentConverterEnginePath;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem knxprodFileSaveToolStripMenuItem;
        private System.Windows.Forms.Label labelKnxprodFileSaved;
        private System.Windows.Forms.TabPage tabCatalogFile;
    }
}