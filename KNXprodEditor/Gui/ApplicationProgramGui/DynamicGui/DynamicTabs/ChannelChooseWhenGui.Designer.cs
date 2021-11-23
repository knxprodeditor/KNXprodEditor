
namespace knxprod_ns
{
    partial class ChannelChooseWhenGui
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
            this.comboBoxCcwTest = new System.Windows.Forms.ComboBox();
            this.buttonChannelChooseWhenSave = new System.Windows.Forms.Button();
            this.checkBoxCcwDefault = new System.Windows.Forms.CheckBox();
            this.label367 = new System.Windows.Forms.Label();
            this.textCcwInternalDescription = new System.Windows.Forms.TextBox();
            this.label369 = new System.Windows.Forms.Label();
            this.label370 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxCcwTest
            // 
            this.comboBoxCcwTest.FormattingEnabled = true;
            this.comboBoxCcwTest.Location = new System.Drawing.Point(110, 3);
            this.comboBoxCcwTest.Name = "comboBoxCcwTest";
            this.comboBoxCcwTest.Size = new System.Drawing.Size(257, 21);
            this.comboBoxCcwTest.TabIndex = 25;
            // 
            // buttonChannelChooseWhenSave
            // 
            this.buttonChannelChooseWhenSave.Location = new System.Drawing.Point(7, 92);
            this.buttonChannelChooseWhenSave.Name = "buttonChannelChooseWhenSave";
            this.buttonChannelChooseWhenSave.Size = new System.Drawing.Size(75, 23);
            this.buttonChannelChooseWhenSave.TabIndex = 24;
            this.buttonChannelChooseWhenSave.Text = "speichern";
            this.buttonChannelChooseWhenSave.UseVisualStyleBackColor = true;
            this.buttonChannelChooseWhenSave.Click += new System.EventHandler(this.buttonChannelChooseWhenSave_Click);
            // 
            // checkBoxCcwDefault
            // 
            this.checkBoxCcwDefault.AutoSize = true;
            this.checkBoxCcwDefault.Location = new System.Drawing.Point(110, 31);
            this.checkBoxCcwDefault.Name = "checkBoxCcwDefault";
            this.checkBoxCcwDefault.Size = new System.Drawing.Size(15, 14);
            this.checkBoxCcwDefault.TabIndex = 23;
            this.checkBoxCcwDefault.UseVisualStyleBackColor = true;
            // 
            // label367
            // 
            this.label367.AutoSize = true;
            this.label367.Location = new System.Drawing.Point(4, 32);
            this.label367.Name = "label367";
            this.label367.Size = new System.Drawing.Size(41, 13);
            this.label367.TabIndex = 22;
            this.label367.Text = "Default";
            // 
            // textCcwInternalDescription
            // 
            this.textCcwInternalDescription.Location = new System.Drawing.Point(110, 55);
            this.textCcwInternalDescription.Name = "textCcwInternalDescription";
            this.textCcwInternalDescription.Size = new System.Drawing.Size(257, 20);
            this.textCcwInternalDescription.TabIndex = 21;
            // 
            // label369
            // 
            this.label369.AutoSize = true;
            this.label369.Location = new System.Drawing.Point(4, 58);
            this.label369.Name = "label369";
            this.label369.Size = new System.Drawing.Size(95, 13);
            this.label369.TabIndex = 20;
            this.label369.Text = "InternalDescription";
            // 
            // label370
            // 
            this.label370.AutoSize = true;
            this.label370.Location = new System.Drawing.Point(4, 6);
            this.label370.Name = "label370";
            this.label370.Size = new System.Drawing.Size(28, 13);
            this.label370.TabIndex = 19;
            this.label370.Text = "Test";
            // 
            // ChannelChooseWhenGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxCcwTest);
            this.Controls.Add(this.buttonChannelChooseWhenSave);
            this.Controls.Add(this.checkBoxCcwDefault);
            this.Controls.Add(this.label367);
            this.Controls.Add(this.textCcwInternalDescription);
            this.Controls.Add(this.label369);
            this.Controls.Add(this.label370);
            this.Name = "ChannelChooseWhenGui";
            this.Size = new System.Drawing.Size(376, 120);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCcwTest;
        private System.Windows.Forms.Button buttonChannelChooseWhenSave;
        private System.Windows.Forms.CheckBox checkBoxCcwDefault;
        private System.Windows.Forms.Label label367;
        private System.Windows.Forms.TextBox textCcwInternalDescription;
        private System.Windows.Forms.Label label369;
        private System.Windows.Forms.Label label370;
    }
}
