
namespace knxprod_ns
{
    partial class AssignGui
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
            this.checkBoxAssignSourceParamRefRef = new System.Windows.Forms.CheckBox();
            this.checkBoxAssignTargetParamRefRef = new System.Windows.Forms.CheckBox();
            this.label134 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.textAssInternalDescription = new System.Windows.Forms.TextBox();
            this.textAssValue = new System.Windows.Forms.TextBox();
            this.textAssSourceParamRefRef = new System.Windows.Forms.TextBox();
            this.textAssTargetParamRefRef = new System.Windows.Forms.TextBox();
            this.label132 = new System.Windows.Forms.Label();
            this.label115 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBoxAssignSourceParamRefRef
            // 
            this.checkBoxAssignSourceParamRefRef.AutoSize = true;
            this.checkBoxAssignSourceParamRefRef.Location = new System.Drawing.Point(372, 31);
            this.checkBoxAssignSourceParamRefRef.Name = "checkBoxAssignSourceParamRefRef";
            this.checkBoxAssignSourceParamRefRef.Size = new System.Drawing.Size(66, 17);
            this.checkBoxAssignSourceParamRefRef.TabIndex = 21;
            this.checkBoxAssignSourceParamRefRef.Text = "auflösen";
            this.checkBoxAssignSourceParamRefRef.UseVisualStyleBackColor = true;
            this.checkBoxAssignSourceParamRefRef.CheckedChanged += new System.EventHandler(this.CheckBoxAssignSourceParamRefRef_CheckedChanged);
            // 
            // checkBoxAssignTargetParamRefRef
            // 
            this.checkBoxAssignTargetParamRefRef.AutoSize = true;
            this.checkBoxAssignTargetParamRefRef.Location = new System.Drawing.Point(372, 5);
            this.checkBoxAssignTargetParamRefRef.Name = "checkBoxAssignTargetParamRefRef";
            this.checkBoxAssignTargetParamRefRef.Size = new System.Drawing.Size(66, 17);
            this.checkBoxAssignTargetParamRefRef.TabIndex = 20;
            this.checkBoxAssignTargetParamRefRef.Text = "auflösen";
            this.checkBoxAssignTargetParamRefRef.UseVisualStyleBackColor = true;
            this.checkBoxAssignTargetParamRefRef.CheckedChanged += new System.EventHandler(this.CheckBoxAssignTargetParamRefRef_CheckedChanged);
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Location = new System.Drawing.Point(2, 32);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(105, 13);
            this.label134.TabIndex = 19;
            this.label134.Text = "SourceParamRefRef";
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Location = new System.Drawing.Point(2, 84);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(95, 13);
            this.label133.TabIndex = 18;
            this.label133.Text = "InternalDescription";
            // 
            // textAssInternalDescription
            // 
            this.textAssInternalDescription.Location = new System.Drawing.Point(108, 81);
            this.textAssInternalDescription.Name = "textAssInternalDescription";
            this.textAssInternalDescription.Size = new System.Drawing.Size(257, 20);
            this.textAssInternalDescription.TabIndex = 17;
            // 
            // textAssValue
            // 
            this.textAssValue.Location = new System.Drawing.Point(108, 55);
            this.textAssValue.Name = "textAssValue";
            this.textAssValue.Size = new System.Drawing.Size(257, 20);
            this.textAssValue.TabIndex = 16;
            // 
            // textAssSourceParamRefRef
            // 
            this.textAssSourceParamRefRef.Location = new System.Drawing.Point(108, 29);
            this.textAssSourceParamRefRef.Name = "textAssSourceParamRefRef";
            this.textAssSourceParamRefRef.Size = new System.Drawing.Size(257, 20);
            this.textAssSourceParamRefRef.TabIndex = 15;
            // 
            // textAssTargetParamRefRef
            // 
            this.textAssTargetParamRefRef.Location = new System.Drawing.Point(108, 3);
            this.textAssTargetParamRefRef.Name = "textAssTargetParamRefRef";
            this.textAssTargetParamRefRef.Size = new System.Drawing.Size(257, 20);
            this.textAssTargetParamRefRef.TabIndex = 14;
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Location = new System.Drawing.Point(2, 58);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(34, 13);
            this.label132.TabIndex = 13;
            this.label132.Text = "Value";
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(2, 6);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(102, 13);
            this.label115.TabIndex = 12;
            this.label115.Text = "TargetParamRefRef";
            // 
            // AssignGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxAssignSourceParamRefRef);
            this.Controls.Add(this.checkBoxAssignTargetParamRefRef);
            this.Controls.Add(this.label134);
            this.Controls.Add(this.label133);
            this.Controls.Add(this.textAssInternalDescription);
            this.Controls.Add(this.textAssValue);
            this.Controls.Add(this.textAssSourceParamRefRef);
            this.Controls.Add(this.textAssTargetParamRefRef);
            this.Controls.Add(this.label132);
            this.Controls.Add(this.label115);
            this.Name = "AssignGui";
            this.Size = new System.Drawing.Size(446, 115);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxAssignSourceParamRefRef;
        private System.Windows.Forms.CheckBox checkBoxAssignTargetParamRefRef;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.TextBox textAssInternalDescription;
        private System.Windows.Forms.TextBox textAssValue;
        private System.Windows.Forms.TextBox textAssSourceParamRefRef;
        private System.Windows.Forms.TextBox textAssTargetParamRefRef;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.Label label115;
    }
}
