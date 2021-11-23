
namespace knxprod_ns
{
    partial class ComObjectRefRefGui
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
            this.buttonComObjRefRefSave = new System.Windows.Forms.Button();
            this.label45 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.textCorrInternalDescription = new System.Windows.Forms.TextBox();
            this.textCorrRefId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonComObjRefRefSave
            // 
            this.buttonComObjRefRefSave.Location = new System.Drawing.Point(7, 56);
            this.buttonComObjRefRefSave.Name = "buttonComObjRefRefSave";
            this.buttonComObjRefRefSave.Size = new System.Drawing.Size(75, 23);
            this.buttonComObjRefRefSave.TabIndex = 9;
            this.buttonComObjRefRefSave.Text = "speichern";
            this.buttonComObjRefRefSave.UseVisualStyleBackColor = true;
            this.buttonComObjRefRefSave.Click += new System.EventHandler(this.buttonComObjRefRefSave_Click);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(4, 32);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(95, 13);
            this.label45.TabIndex = 8;
            this.label45.Text = "InternalDescription";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(4, 6);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(33, 13);
            this.label39.TabIndex = 7;
            this.label39.Text = "RefId";
            // 
            // textCorrInternalDescription
            // 
            this.textCorrInternalDescription.Location = new System.Drawing.Point(108, 29);
            this.textCorrInternalDescription.Name = "textCorrInternalDescription";
            this.textCorrInternalDescription.Size = new System.Drawing.Size(281, 20);
            this.textCorrInternalDescription.TabIndex = 6;
            // 
            // textCorrRefId
            // 
            this.textCorrRefId.Location = new System.Drawing.Point(108, 3);
            this.textCorrRefId.Name = "textCorrRefId";
            this.textCorrRefId.ReadOnly = true;
            this.textCorrRefId.Size = new System.Drawing.Size(281, 20);
            this.textCorrRefId.TabIndex = 5;
            // 
            // ComObjectRefRefGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonComObjRefRefSave);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.textCorrInternalDescription);
            this.Controls.Add(this.textCorrRefId);
            this.Name = "ComObjectRefRefGui";
            this.Size = new System.Drawing.Size(398, 89);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonComObjRefRefSave;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox textCorrInternalDescription;
        private System.Windows.Forms.TextBox textCorrRefId;
    }
}
