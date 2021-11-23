
namespace knxprod_ns
{
    partial class ComObjectParameterChooseGui
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
            this.buttonComObjectParameterChooseSave = new System.Windows.Forms.Button();
            this.checkBoxCopcWithParameterRefRef = new System.Windows.Forms.CheckBox();
            this.label669 = new System.Windows.Forms.Label();
            this.textCopcParamRefId = new System.Windows.Forms.TextBox();
            this.label114 = new System.Windows.Forms.Label();
            this.textCopcInternalDescription = new System.Windows.Forms.TextBox();
            this.label113 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonComObjectParameterChooseSave
            // 
            this.buttonComObjectParameterChooseSave.Location = new System.Drawing.Point(3, 82);
            this.buttonComObjectParameterChooseSave.Name = "buttonComObjectParameterChooseSave";
            this.buttonComObjectParameterChooseSave.Size = new System.Drawing.Size(75, 23);
            this.buttonComObjectParameterChooseSave.TabIndex = 15;
            this.buttonComObjectParameterChooseSave.Text = "speichern";
            this.buttonComObjectParameterChooseSave.UseVisualStyleBackColor = true;
            this.buttonComObjectParameterChooseSave.Click += new System.EventHandler(this.buttonComObjectParameterChooseSave_Click);
            // 
            // checkBoxCopcWithParameterRefRef
            // 
            this.checkBoxCopcWithParameterRefRef.AutoSize = true;
            this.checkBoxCopcWithParameterRefRef.Location = new System.Drawing.Point(125, 56);
            this.checkBoxCopcWithParameterRefRef.Name = "checkBoxCopcWithParameterRefRef";
            this.checkBoxCopcWithParameterRefRef.Size = new System.Drawing.Size(15, 14);
            this.checkBoxCopcWithParameterRefRef.TabIndex = 14;
            this.checkBoxCopcWithParameterRefRef.UseVisualStyleBackColor = true;
            // 
            // label669
            // 
            this.label669.AutoSize = true;
            this.label669.Location = new System.Drawing.Point(3, 57);
            this.label669.Name = "label669";
            this.label669.Size = new System.Drawing.Size(114, 13);
            this.label669.TabIndex = 13;
            this.label669.Text = "With ParameterRefRef";
            // 
            // textCopcParamRefId
            // 
            this.textCopcParamRefId.Location = new System.Drawing.Point(125, 3);
            this.textCopcParamRefId.Name = "textCopcParamRefId";
            this.textCopcParamRefId.ReadOnly = true;
            this.textCopcParamRefId.Size = new System.Drawing.Size(252, 20);
            this.textCopcParamRefId.TabIndex = 12;
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(3, 32);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(95, 13);
            this.label114.TabIndex = 11;
            this.label114.Text = "InternalDescription";
            // 
            // textCopcInternalDescription
            // 
            this.textCopcInternalDescription.Location = new System.Drawing.Point(125, 29);
            this.textCopcInternalDescription.Name = "textCopcInternalDescription";
            this.textCopcInternalDescription.Size = new System.Drawing.Size(252, 20);
            this.textCopcInternalDescription.TabIndex = 10;
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Location = new System.Drawing.Point(3, 6);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(63, 13);
            this.label113.TabIndex = 9;
            this.label113.Text = "ParamRefId";
            // 
            // ComObjectParameterChooseGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonComObjectParameterChooseSave);
            this.Controls.Add(this.checkBoxCopcWithParameterRefRef);
            this.Controls.Add(this.label669);
            this.Controls.Add(this.textCopcParamRefId);
            this.Controls.Add(this.label114);
            this.Controls.Add(this.textCopcInternalDescription);
            this.Controls.Add(this.label113);
            this.Name = "ComObjectParameterChooseGui";
            this.Size = new System.Drawing.Size(389, 119);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonComObjectParameterChooseSave;
        private System.Windows.Forms.CheckBox checkBoxCopcWithParameterRefRef;
        private System.Windows.Forms.Label label669;
        private System.Windows.Forms.TextBox textCopcParamRefId;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.TextBox textCopcInternalDescription;
        private System.Windows.Forms.Label label113;
    }
}
