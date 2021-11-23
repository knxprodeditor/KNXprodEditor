
namespace knxprod_ns
{
    partial class CodeGui
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
            this.tabControlCode = new System.Windows.Forms.TabControl();
            this.tabCodeRelativeSegment = new System.Windows.Forms.TabPage();
            this.numericCodeRelOffset = new System.Windows.Forms.NumericUpDown();
            this.numericCodeRelLoadStateMachine = new System.Windows.Forms.NumericUpDown();
            this.numericCodeRelSize = new System.Windows.Forms.NumericUpDown();
            this.label275 = new System.Windows.Forms.Label();
            this.label276 = new System.Windows.Forms.Label();
            this.label267 = new System.Windows.Forms.Label();
            this.textCodeRelInternalDescription = new System.Windows.Forms.TextBox();
            this.label266 = new System.Windows.Forms.Label();
            this.label265 = new System.Windows.Forms.Label();
            this.textCodeRelName = new System.Windows.Forms.TextBox();
            this.label264 = new System.Windows.Forms.Label();
            this.textCodeRelId = new System.Windows.Forms.TextBox();
            this.label263 = new System.Windows.Forms.Label();
            this.listCodeRelMask = new System.Windows.Forms.ListBox();
            this.label262 = new System.Windows.Forms.Label();
            this.listCodeRelData = new System.Windows.Forms.ListBox();
            this.tabCodeAbsoluteSegment = new System.Windows.Forms.TabPage();
            this.checkBoxCodeAbsUserMemory = new System.Windows.Forms.CheckBox();
            this.numericCodeAbsAddress = new System.Windows.Forms.NumericUpDown();
            this.comboBoxCodeAbsMemoryType = new System.Windows.Forms.ComboBox();
            this.numericCodeAbsSize = new System.Windows.Forms.NumericUpDown();
            this.label277 = new System.Windows.Forms.Label();
            this.label278 = new System.Windows.Forms.Label();
            this.label279 = new System.Windows.Forms.Label();
            this.label268 = new System.Windows.Forms.Label();
            this.textCodeAbsInternalDescription = new System.Windows.Forms.TextBox();
            this.label269 = new System.Windows.Forms.Label();
            this.label270 = new System.Windows.Forms.Label();
            this.textCodeAbsName = new System.Windows.Forms.TextBox();
            this.label271 = new System.Windows.Forms.Label();
            this.textCodeAbsId = new System.Windows.Forms.TextBox();
            this.label272 = new System.Windows.Forms.Label();
            this.listCodeAbsMask = new System.Windows.Forms.ListBox();
            this.label273 = new System.Windows.Forms.Label();
            this.listCodeAbsData = new System.Windows.Forms.ListBox();
            this.TreeViewCode = new System.Windows.Forms.TreeView();
            this.tabControlCode.SuspendLayout();
            this.tabCodeRelativeSegment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCodeRelOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCodeRelLoadStateMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCodeRelSize)).BeginInit();
            this.tabCodeAbsoluteSegment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCodeAbsAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCodeAbsSize)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlCode
            // 
            this.tabControlCode.Controls.Add(this.tabCodeRelativeSegment);
            this.tabControlCode.Controls.Add(this.tabCodeAbsoluteSegment);
            this.tabControlCode.Location = new System.Drawing.Point(382, 3);
            this.tabControlCode.Name = "tabControlCode";
            this.tabControlCode.SelectedIndex = 0;
            this.tabControlCode.Size = new System.Drawing.Size(493, 447);
            this.tabControlCode.TabIndex = 3;
            // 
            // tabCodeRelativeSegment
            // 
            this.tabCodeRelativeSegment.Controls.Add(this.numericCodeRelOffset);
            this.tabCodeRelativeSegment.Controls.Add(this.numericCodeRelLoadStateMachine);
            this.tabCodeRelativeSegment.Controls.Add(this.numericCodeRelSize);
            this.tabCodeRelativeSegment.Controls.Add(this.label275);
            this.tabCodeRelativeSegment.Controls.Add(this.label276);
            this.tabCodeRelativeSegment.Controls.Add(this.label267);
            this.tabCodeRelativeSegment.Controls.Add(this.textCodeRelInternalDescription);
            this.tabCodeRelativeSegment.Controls.Add(this.label266);
            this.tabCodeRelativeSegment.Controls.Add(this.label265);
            this.tabCodeRelativeSegment.Controls.Add(this.textCodeRelName);
            this.tabCodeRelativeSegment.Controls.Add(this.label264);
            this.tabCodeRelativeSegment.Controls.Add(this.textCodeRelId);
            this.tabCodeRelativeSegment.Controls.Add(this.label263);
            this.tabCodeRelativeSegment.Controls.Add(this.listCodeRelMask);
            this.tabCodeRelativeSegment.Controls.Add(this.label262);
            this.tabCodeRelativeSegment.Controls.Add(this.listCodeRelData);
            this.tabCodeRelativeSegment.Location = new System.Drawing.Point(4, 22);
            this.tabCodeRelativeSegment.Name = "tabCodeRelativeSegment";
            this.tabCodeRelativeSegment.Padding = new System.Windows.Forms.Padding(3);
            this.tabCodeRelativeSegment.Size = new System.Drawing.Size(485, 421);
            this.tabCodeRelativeSegment.TabIndex = 0;
            this.tabCodeRelativeSegment.Text = "RelativeSegment";
            this.tabCodeRelativeSegment.UseVisualStyleBackColor = true;
            // 
            // numericCodeRelOffset
            // 
            this.numericCodeRelOffset.Location = new System.Drawing.Point(113, 236);
            this.numericCodeRelOffset.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numericCodeRelOffset.Name = "numericCodeRelOffset";
            this.numericCodeRelOffset.Size = new System.Drawing.Size(291, 20);
            this.numericCodeRelOffset.TabIndex = 116;
            // 
            // numericCodeRelLoadStateMachine
            // 
            this.numericCodeRelLoadStateMachine.Location = new System.Drawing.Point(113, 210);
            this.numericCodeRelLoadStateMachine.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numericCodeRelLoadStateMachine.Name = "numericCodeRelLoadStateMachine";
            this.numericCodeRelLoadStateMachine.Size = new System.Drawing.Size(291, 20);
            this.numericCodeRelLoadStateMachine.TabIndex = 115;
            // 
            // numericCodeRelSize
            // 
            this.numericCodeRelSize.Location = new System.Drawing.Point(113, 158);
            this.numericCodeRelSize.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numericCodeRelSize.Name = "numericCodeRelSize";
            this.numericCodeRelSize.Size = new System.Drawing.Size(291, 20);
            this.numericCodeRelSize.TabIndex = 114;
            // 
            // label275
            // 
            this.label275.AutoSize = true;
            this.label275.Location = new System.Drawing.Point(6, 238);
            this.label275.Name = "label275";
            this.label275.Size = new System.Drawing.Size(35, 13);
            this.label275.TabIndex = 15;
            this.label275.Text = "Offset";
            // 
            // label276
            // 
            this.label276.AutoSize = true;
            this.label276.Location = new System.Drawing.Point(6, 212);
            this.label276.Name = "label276";
            this.label276.Size = new System.Drawing.Size(97, 13);
            this.label276.TabIndex = 13;
            this.label276.Text = "LoadStateMachine";
            // 
            // label267
            // 
            this.label267.AutoSize = true;
            this.label267.Location = new System.Drawing.Point(6, 186);
            this.label267.Name = "label267";
            this.label267.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label267.Size = new System.Drawing.Size(95, 13);
            this.label267.TabIndex = 11;
            this.label267.Text = "InternalDescription";
            // 
            // textCodeRelInternalDescription
            // 
            this.textCodeRelInternalDescription.Location = new System.Drawing.Point(113, 183);
            this.textCodeRelInternalDescription.Name = "textCodeRelInternalDescription";
            this.textCodeRelInternalDescription.Size = new System.Drawing.Size(291, 20);
            this.textCodeRelInternalDescription.TabIndex = 10;
            // 
            // label266
            // 
            this.label266.AutoSize = true;
            this.label266.Location = new System.Drawing.Point(6, 160);
            this.label266.Name = "label266";
            this.label266.Size = new System.Drawing.Size(27, 13);
            this.label266.TabIndex = 9;
            this.label266.Text = "Size";
            // 
            // label265
            // 
            this.label265.AutoSize = true;
            this.label265.Location = new System.Drawing.Point(6, 134);
            this.label265.Name = "label265";
            this.label265.Size = new System.Drawing.Size(35, 13);
            this.label265.TabIndex = 7;
            this.label265.Text = "Name";
            // 
            // textCodeRelName
            // 
            this.textCodeRelName.Location = new System.Drawing.Point(113, 131);
            this.textCodeRelName.Name = "textCodeRelName";
            this.textCodeRelName.Size = new System.Drawing.Size(291, 20);
            this.textCodeRelName.TabIndex = 6;
            // 
            // label264
            // 
            this.label264.AutoSize = true;
            this.label264.Location = new System.Drawing.Point(6, 108);
            this.label264.Name = "label264";
            this.label264.Size = new System.Drawing.Size(16, 13);
            this.label264.TabIndex = 5;
            this.label264.Text = "Id";
            // 
            // textCodeRelId
            // 
            this.textCodeRelId.Location = new System.Drawing.Point(113, 105);
            this.textCodeRelId.Name = "textCodeRelId";
            this.textCodeRelId.Size = new System.Drawing.Size(291, 20);
            this.textCodeRelId.TabIndex = 4;
            // 
            // label263
            // 
            this.label263.AutoSize = true;
            this.label263.Location = new System.Drawing.Point(6, 55);
            this.label263.Name = "label263";
            this.label263.Size = new System.Drawing.Size(33, 13);
            this.label263.TabIndex = 3;
            this.label263.Text = "Mask";
            // 
            // listCodeRelMask
            // 
            this.listCodeRelMask.FormattingEnabled = true;
            this.listCodeRelMask.Location = new System.Drawing.Point(113, 55);
            this.listCodeRelMask.Name = "listCodeRelMask";
            this.listCodeRelMask.Size = new System.Drawing.Size(291, 43);
            this.listCodeRelMask.TabIndex = 2;
            // 
            // label262
            // 
            this.label262.AutoSize = true;
            this.label262.Location = new System.Drawing.Point(6, 6);
            this.label262.Name = "label262";
            this.label262.Size = new System.Drawing.Size(30, 13);
            this.label262.TabIndex = 1;
            this.label262.Text = "Data";
            // 
            // listCodeRelData
            // 
            this.listCodeRelData.FormattingEnabled = true;
            this.listCodeRelData.Location = new System.Drawing.Point(113, 6);
            this.listCodeRelData.Name = "listCodeRelData";
            this.listCodeRelData.Size = new System.Drawing.Size(291, 43);
            this.listCodeRelData.TabIndex = 0;
            // 
            // tabCodeAbsoluteSegment
            // 
            this.tabCodeAbsoluteSegment.Controls.Add(this.checkBoxCodeAbsUserMemory);
            this.tabCodeAbsoluteSegment.Controls.Add(this.numericCodeAbsAddress);
            this.tabCodeAbsoluteSegment.Controls.Add(this.comboBoxCodeAbsMemoryType);
            this.tabCodeAbsoluteSegment.Controls.Add(this.numericCodeAbsSize);
            this.tabCodeAbsoluteSegment.Controls.Add(this.label277);
            this.tabCodeAbsoluteSegment.Controls.Add(this.label278);
            this.tabCodeAbsoluteSegment.Controls.Add(this.label279);
            this.tabCodeAbsoluteSegment.Controls.Add(this.label268);
            this.tabCodeAbsoluteSegment.Controls.Add(this.textCodeAbsInternalDescription);
            this.tabCodeAbsoluteSegment.Controls.Add(this.label269);
            this.tabCodeAbsoluteSegment.Controls.Add(this.label270);
            this.tabCodeAbsoluteSegment.Controls.Add(this.textCodeAbsName);
            this.tabCodeAbsoluteSegment.Controls.Add(this.label271);
            this.tabCodeAbsoluteSegment.Controls.Add(this.textCodeAbsId);
            this.tabCodeAbsoluteSegment.Controls.Add(this.label272);
            this.tabCodeAbsoluteSegment.Controls.Add(this.listCodeAbsMask);
            this.tabCodeAbsoluteSegment.Controls.Add(this.label273);
            this.tabCodeAbsoluteSegment.Controls.Add(this.listCodeAbsData);
            this.tabCodeAbsoluteSegment.Location = new System.Drawing.Point(4, 22);
            this.tabCodeAbsoluteSegment.Name = "tabCodeAbsoluteSegment";
            this.tabCodeAbsoluteSegment.Padding = new System.Windows.Forms.Padding(3);
            this.tabCodeAbsoluteSegment.Size = new System.Drawing.Size(485, 421);
            this.tabCodeAbsoluteSegment.TabIndex = 1;
            this.tabCodeAbsoluteSegment.Text = "AbsoluteSegment";
            this.tabCodeAbsoluteSegment.UseVisualStyleBackColor = true;
            // 
            // checkBoxCodeAbsUserMemory
            // 
            this.checkBoxCodeAbsUserMemory.AutoSize = true;
            this.checkBoxCodeAbsUserMemory.Location = new System.Drawing.Point(113, 264);
            this.checkBoxCodeAbsUserMemory.Name = "checkBoxCodeAbsUserMemory";
            this.checkBoxCodeAbsUserMemory.Size = new System.Drawing.Size(15, 14);
            this.checkBoxCodeAbsUserMemory.TabIndex = 118;
            this.checkBoxCodeAbsUserMemory.UseVisualStyleBackColor = true;
            // 
            // numericCodeAbsAddress
            // 
            this.numericCodeAbsAddress.Location = new System.Drawing.Point(113, 236);
            this.numericCodeAbsAddress.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numericCodeAbsAddress.Name = "numericCodeAbsAddress";
            this.numericCodeAbsAddress.Size = new System.Drawing.Size(291, 20);
            this.numericCodeAbsAddress.TabIndex = 117;
            // 
            // comboBoxCodeAbsMemoryType
            // 
            this.comboBoxCodeAbsMemoryType.FormattingEnabled = true;
            this.comboBoxCodeAbsMemoryType.Items.AddRange(new object[] {
            "RAM",
            "EEPROM",
            "FLASH"});
            this.comboBoxCodeAbsMemoryType.Location = new System.Drawing.Point(113, 209);
            this.comboBoxCodeAbsMemoryType.Name = "comboBoxCodeAbsMemoryType";
            this.comboBoxCodeAbsMemoryType.Size = new System.Drawing.Size(291, 21);
            this.comboBoxCodeAbsMemoryType.TabIndex = 116;
            // 
            // numericCodeAbsSize
            // 
            this.numericCodeAbsSize.Location = new System.Drawing.Point(113, 158);
            this.numericCodeAbsSize.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numericCodeAbsSize.Name = "numericCodeAbsSize";
            this.numericCodeAbsSize.Size = new System.Drawing.Size(291, 20);
            this.numericCodeAbsSize.TabIndex = 115;
            // 
            // label277
            // 
            this.label277.AutoSize = true;
            this.label277.Location = new System.Drawing.Point(6, 264);
            this.label277.Name = "label277";
            this.label277.Size = new System.Drawing.Size(66, 13);
            this.label277.TabIndex = 29;
            this.label277.Text = "UserMemory";
            // 
            // label278
            // 
            this.label278.AutoSize = true;
            this.label278.Location = new System.Drawing.Point(6, 238);
            this.label278.Name = "label278";
            this.label278.Size = new System.Drawing.Size(45, 13);
            this.label278.TabIndex = 27;
            this.label278.Text = "Address";
            // 
            // label279
            // 
            this.label279.AutoSize = true;
            this.label279.Location = new System.Drawing.Point(6, 212);
            this.label279.Name = "label279";
            this.label279.Size = new System.Drawing.Size(68, 13);
            this.label279.TabIndex = 25;
            this.label279.Text = "MemoryType";
            // 
            // label268
            // 
            this.label268.AutoSize = true;
            this.label268.Location = new System.Drawing.Point(6, 186);
            this.label268.Name = "label268";
            this.label268.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label268.Size = new System.Drawing.Size(95, 13);
            this.label268.TabIndex = 23;
            this.label268.Text = "InternalDescription";
            // 
            // textCodeAbsInternalDescription
            // 
            this.textCodeAbsInternalDescription.Location = new System.Drawing.Point(113, 183);
            this.textCodeAbsInternalDescription.Name = "textCodeAbsInternalDescription";
            this.textCodeAbsInternalDescription.Size = new System.Drawing.Size(291, 20);
            this.textCodeAbsInternalDescription.TabIndex = 22;
            // 
            // label269
            // 
            this.label269.AutoSize = true;
            this.label269.Location = new System.Drawing.Point(6, 160);
            this.label269.Name = "label269";
            this.label269.Size = new System.Drawing.Size(27, 13);
            this.label269.TabIndex = 21;
            this.label269.Text = "Size";
            // 
            // label270
            // 
            this.label270.AutoSize = true;
            this.label270.Location = new System.Drawing.Point(6, 134);
            this.label270.Name = "label270";
            this.label270.Size = new System.Drawing.Size(35, 13);
            this.label270.TabIndex = 19;
            this.label270.Text = "Name";
            // 
            // textCodeAbsName
            // 
            this.textCodeAbsName.Location = new System.Drawing.Point(113, 131);
            this.textCodeAbsName.Name = "textCodeAbsName";
            this.textCodeAbsName.Size = new System.Drawing.Size(291, 20);
            this.textCodeAbsName.TabIndex = 18;
            // 
            // label271
            // 
            this.label271.AutoSize = true;
            this.label271.Location = new System.Drawing.Point(6, 108);
            this.label271.Name = "label271";
            this.label271.Size = new System.Drawing.Size(16, 13);
            this.label271.TabIndex = 17;
            this.label271.Text = "Id";
            // 
            // textCodeAbsId
            // 
            this.textCodeAbsId.Location = new System.Drawing.Point(113, 105);
            this.textCodeAbsId.Name = "textCodeAbsId";
            this.textCodeAbsId.Size = new System.Drawing.Size(291, 20);
            this.textCodeAbsId.TabIndex = 16;
            // 
            // label272
            // 
            this.label272.AutoSize = true;
            this.label272.Location = new System.Drawing.Point(6, 55);
            this.label272.Name = "label272";
            this.label272.Size = new System.Drawing.Size(33, 13);
            this.label272.TabIndex = 15;
            this.label272.Text = "Mask";
            // 
            // listCodeAbsMask
            // 
            this.listCodeAbsMask.FormattingEnabled = true;
            this.listCodeAbsMask.Location = new System.Drawing.Point(113, 55);
            this.listCodeAbsMask.Name = "listCodeAbsMask";
            this.listCodeAbsMask.Size = new System.Drawing.Size(291, 43);
            this.listCodeAbsMask.TabIndex = 14;
            // 
            // label273
            // 
            this.label273.AutoSize = true;
            this.label273.Location = new System.Drawing.Point(6, 6);
            this.label273.Name = "label273";
            this.label273.Size = new System.Drawing.Size(30, 13);
            this.label273.TabIndex = 13;
            this.label273.Text = "Data";
            // 
            // listCodeAbsData
            // 
            this.listCodeAbsData.FormattingEnabled = true;
            this.listCodeAbsData.Location = new System.Drawing.Point(113, 6);
            this.listCodeAbsData.Name = "listCodeAbsData";
            this.listCodeAbsData.Size = new System.Drawing.Size(291, 43);
            this.listCodeAbsData.TabIndex = 12;
            // 
            // TreeViewCode
            // 
            this.TreeViewCode.Location = new System.Drawing.Point(3, 3);
            this.TreeViewCode.Name = "TreeViewCode";
            this.TreeViewCode.Size = new System.Drawing.Size(373, 447);
            this.TreeViewCode.TabIndex = 2;
            this.TreeViewCode.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewCode_AfterSelect);
            // 
            // CodeGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlCode);
            this.Controls.Add(this.TreeViewCode);
            this.Name = "CodeGui";
            this.Size = new System.Drawing.Size(1655, 856);
            this.tabControlCode.ResumeLayout(false);
            this.tabCodeRelativeSegment.ResumeLayout(false);
            this.tabCodeRelativeSegment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCodeRelOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCodeRelLoadStateMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCodeRelSize)).EndInit();
            this.tabCodeAbsoluteSegment.ResumeLayout(false);
            this.tabCodeAbsoluteSegment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCodeAbsAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCodeAbsSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlCode;
        private System.Windows.Forms.TabPage tabCodeRelativeSegment;
        private System.Windows.Forms.NumericUpDown numericCodeRelOffset;
        private System.Windows.Forms.NumericUpDown numericCodeRelLoadStateMachine;
        private System.Windows.Forms.NumericUpDown numericCodeRelSize;
        private System.Windows.Forms.Label label275;
        private System.Windows.Forms.Label label276;
        private System.Windows.Forms.Label label267;
        private System.Windows.Forms.TextBox textCodeRelInternalDescription;
        private System.Windows.Forms.Label label266;
        private System.Windows.Forms.Label label265;
        private System.Windows.Forms.TextBox textCodeRelName;
        private System.Windows.Forms.Label label264;
        private System.Windows.Forms.TextBox textCodeRelId;
        private System.Windows.Forms.Label label263;
        private System.Windows.Forms.ListBox listCodeRelMask;
        private System.Windows.Forms.Label label262;
        private System.Windows.Forms.ListBox listCodeRelData;
        private System.Windows.Forms.TabPage tabCodeAbsoluteSegment;
        private System.Windows.Forms.CheckBox checkBoxCodeAbsUserMemory;
        private System.Windows.Forms.NumericUpDown numericCodeAbsAddress;
        private System.Windows.Forms.ComboBox comboBoxCodeAbsMemoryType;
        private System.Windows.Forms.NumericUpDown numericCodeAbsSize;
        private System.Windows.Forms.Label label277;
        private System.Windows.Forms.Label label278;
        private System.Windows.Forms.Label label279;
        private System.Windows.Forms.Label label268;
        private System.Windows.Forms.TextBox textCodeAbsInternalDescription;
        private System.Windows.Forms.Label label269;
        private System.Windows.Forms.Label label270;
        private System.Windows.Forms.TextBox textCodeAbsName;
        private System.Windows.Forms.Label label271;
        private System.Windows.Forms.TextBox textCodeAbsId;
        private System.Windows.Forms.Label label272;
        private System.Windows.Forms.ListBox listCodeAbsMask;
        private System.Windows.Forms.Label label273;
        private System.Windows.Forms.ListBox listCodeAbsData;
        private System.Windows.Forms.TreeView TreeViewCode;
    }
}
