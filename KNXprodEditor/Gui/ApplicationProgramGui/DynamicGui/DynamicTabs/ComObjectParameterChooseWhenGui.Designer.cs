
namespace knxprod_ns
{
    partial class ComObjectParameterChooseWhenGui
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
            this.comboBoxCopcwTest = new System.Windows.Forms.ComboBox();
            this.buttonComObjParChooseWhenSave = new System.Windows.Forms.Button();
            this.checkBoxCopcwDefault = new System.Windows.Forms.CheckBox();
            this.label135 = new System.Windows.Forms.Label();
            this.textCopcwInternalDescription = new System.Windows.Forms.TextBox();
            this.label368 = new System.Windows.Forms.Label();
            this.label371 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxCopcwTest
            // 
            this.comboBoxCopcwTest.FormattingEnabled = true;
            this.comboBoxCopcwTest.Location = new System.Drawing.Point(109, 3);
            this.comboBoxCopcwTest.Name = "comboBoxCopcwTest";
            this.comboBoxCopcwTest.Size = new System.Drawing.Size(257, 21);
            this.comboBoxCopcwTest.TabIndex = 31;
            // 
            // buttonComObjParChooseWhenSave
            // 
            this.buttonComObjParChooseWhenSave.Location = new System.Drawing.Point(6, 90);
            this.buttonComObjParChooseWhenSave.Name = "buttonComObjParChooseWhenSave";
            this.buttonComObjParChooseWhenSave.Size = new System.Drawing.Size(75, 23);
            this.buttonComObjParChooseWhenSave.TabIndex = 30;
            this.buttonComObjParChooseWhenSave.Text = "speichern";
            this.buttonComObjParChooseWhenSave.UseVisualStyleBackColor = true;
            this.buttonComObjParChooseWhenSave.Click += new System.EventHandler(this.buttonComObjParChooseWhenSave_Click);
            // 
            // checkBoxCopcwDefault
            // 
            this.checkBoxCopcwDefault.AutoSize = true;
            this.checkBoxCopcwDefault.Location = new System.Drawing.Point(109, 32);
            this.checkBoxCopcwDefault.Name = "checkBoxCopcwDefault";
            this.checkBoxCopcwDefault.Size = new System.Drawing.Size(15, 14);
            this.checkBoxCopcwDefault.TabIndex = 29;
            this.checkBoxCopcwDefault.UseVisualStyleBackColor = true;
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Location = new System.Drawing.Point(3, 32);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(41, 13);
            this.label135.TabIndex = 28;
            this.label135.Text = "Default";
            // 
            // textCopcwInternalDescription
            // 
            this.textCopcwInternalDescription.Location = new System.Drawing.Point(109, 55);
            this.textCopcwInternalDescription.Name = "textCopcwInternalDescription";
            this.textCopcwInternalDescription.Size = new System.Drawing.Size(257, 20);
            this.textCopcwInternalDescription.TabIndex = 27;
            // 
            // label368
            // 
            this.label368.AutoSize = true;
            this.label368.Location = new System.Drawing.Point(3, 58);
            this.label368.Name = "label368";
            this.label368.Size = new System.Drawing.Size(95, 13);
            this.label368.TabIndex = 26;
            this.label368.Text = "InternalDescription";
            // 
            // label371
            // 
            this.label371.AutoSize = true;
            this.label371.Location = new System.Drawing.Point(3, 6);
            this.label371.Name = "label371";
            this.label371.Size = new System.Drawing.Size(28, 13);
            this.label371.TabIndex = 25;
            this.label371.Text = "Test";
            // 
            // ComObjectParameterChooseWhenGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxCopcwTest);
            this.Controls.Add(this.buttonComObjParChooseWhenSave);
            this.Controls.Add(this.checkBoxCopcwDefault);
            this.Controls.Add(this.label135);
            this.Controls.Add(this.textCopcwInternalDescription);
            this.Controls.Add(this.label368);
            this.Controls.Add(this.label371);
            this.Name = "ComObjectParameterChooseWhenGui";
            this.Size = new System.Drawing.Size(381, 122);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCopcwTest;
        private System.Windows.Forms.Button buttonComObjParChooseWhenSave;
        private System.Windows.Forms.CheckBox checkBoxCopcwDefault;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.TextBox textCopcwInternalDescription;
        private System.Windows.Forms.Label label368;
        private System.Windows.Forms.Label label371;
    }
}
