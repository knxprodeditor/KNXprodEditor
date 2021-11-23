
namespace knxprod_ns
{
    partial class ChannelChooseGui
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
            this.textCcParamRefId = new System.Windows.Forms.TextBox();
            this.textCcInternalDescription = new System.Windows.Forms.TextBox();
            this.label117 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textCcParamRefId
            // 
            this.textCcParamRefId.Location = new System.Drawing.Point(107, 3);
            this.textCcParamRefId.Name = "textCcParamRefId";
            this.textCcParamRefId.Size = new System.Drawing.Size(261, 20);
            this.textCcParamRefId.TabIndex = 59;
            // 
            // textCcInternalDescription
            // 
            this.textCcInternalDescription.Location = new System.Drawing.Point(107, 29);
            this.textCcInternalDescription.Name = "textCcInternalDescription";
            this.textCcInternalDescription.Size = new System.Drawing.Size(261, 20);
            this.textCcInternalDescription.TabIndex = 58;
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Location = new System.Drawing.Point(3, 32);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(95, 13);
            this.label117.TabIndex = 57;
            this.label117.Text = "InternalDescription";
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Location = new System.Drawing.Point(3, 6);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(63, 13);
            this.label118.TabIndex = 56;
            this.label118.Text = "ParamRefId";
            // 
            // ChannelChooseGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textCcParamRefId);
            this.Controls.Add(this.textCcInternalDescription);
            this.Controls.Add(this.label117);
            this.Controls.Add(this.label118);
            this.Name = "ChannelChooseGui";
            this.Size = new System.Drawing.Size(386, 57);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textCcParamRefId;
        private System.Windows.Forms.TextBox textCcInternalDescription;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.Label label118;
    }
}
