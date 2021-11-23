
namespace knxprod_ns
{
    partial class AppParameterGui
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
            this.label442 = new System.Windows.Forms.Label();
            this.label440 = new System.Windows.Forms.Label();
            this.comboBoxPParameterCollection = new System.Windows.Forms.ComboBox();
            this.numericPParameterTypeSizeInBit = new System.Windows.Forms.NumericUpDown();
            this.buttonParCoParameterSave = new System.Windows.Forms.Button();
            this.comboBoxPParameterTypeName = new System.Windows.Forms.ComboBox();
            this.label322 = new System.Windows.Forms.Label();
            this.dgvAppProgParTranslations = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label306 = new System.Windows.Forms.Label();
            this.label223 = new System.Windows.Forms.Label();
            this.listPSelectableParameters = new System.Windows.Forms.ListBox();
            this.label222 = new System.Windows.Forms.Label();
            this.label172 = new System.Windows.Forms.Label();
            this.listPParameterTypeParams = new System.Windows.Forms.ListBox();
            this.label171 = new System.Windows.Forms.Label();
            this.textPLegacyPatchAlways = new System.Windows.Forms.TextBox();
            this.tabControlParItem = new System.Windows.Forms.TabControl();
            this.tabParMemory = new System.Windows.Forms.TabPage();
            this.label326 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.textParMemAddress = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.textPMBitOffset = new System.Windows.Forms.TextBox();
            this.textPMOffset = new System.Windows.Forms.TextBox();
            this.label310 = new System.Windows.Forms.Label();
            this.textPMCodeSegment = new System.Windows.Forms.TextBox();
            this.textParBaseAddress = new System.Windows.Forms.TextBox();
            this.tabParProperty = new System.Windows.Forms.TabPage();
            this.label85 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.textPPBitOffset = new System.Windows.Forms.TextBox();
            this.textPPOffset = new System.Windows.Forms.TextBox();
            this.textPPObjectType = new System.Windows.Forms.TextBox();
            this.textPPOccurence = new System.Windows.Forms.TextBox();
            this.textPPPropertyId = new System.Windows.Forms.TextBox();
            this.textPPObjectIndex = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.textPInternalDescription = new System.Windows.Forms.TextBox();
            this.textPCustomerAdjustable = new System.Windows.Forms.TextBox();
            this.textPInitialValue = new System.Windows.Forms.TextBox();
            this.textPValue = new System.Windows.Forms.TextBox();
            this.textPAccess = new System.Windows.Forms.TextBox();
            this.textPSuffixText = new System.Windows.Forms.TextBox();
            this.textPText = new System.Windows.Forms.TextBox();
            this.textPParameterType = new System.Windows.Forms.TextBox();
            this.textPName = new System.Windows.Forms.TextBox();
            this.textPId = new System.Windows.Forms.TextBox();
            this.label69 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericPParameterTypeSizeInBit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppProgParTranslations)).BeginInit();
            this.tabControlParItem.SuspendLayout();
            this.tabParMemory.SuspendLayout();
            this.tabParProperty.SuspendLayout();
            this.SuspendLayout();
            // 
            // label442
            // 
            this.label442.AutoSize = true;
            this.label442.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label442.ForeColor = System.Drawing.Color.Red;
            this.label442.Location = new System.Drawing.Point(392, 34);
            this.label442.Name = "label442";
            this.label442.Size = new System.Drawing.Size(383, 13);
            this.label442.TabIndex = 237;
            this.label442.Text = "Parameter Eigenschaften sind nur im Parameter Tab zu verändern!";
            // 
            // label440
            // 
            this.label440.AutoSize = true;
            this.label440.Location = new System.Drawing.Point(3, 7);
            this.label440.Name = "label440";
            this.label440.Size = new System.Drawing.Size(55, 13);
            this.label440.TabIndex = 236;
            this.label440.Text = "Parameter";
            // 
            // comboBoxPParameterCollection
            // 
            this.comboBoxPParameterCollection.FormattingEnabled = true;
            this.comboBoxPParameterCollection.Location = new System.Drawing.Point(122, 3);
            this.comboBoxPParameterCollection.Name = "comboBoxPParameterCollection";
            this.comboBoxPParameterCollection.Size = new System.Drawing.Size(261, 21);
            this.comboBoxPParameterCollection.TabIndex = 235;
            this.comboBoxPParameterCollection.SelectionChangeCommitted += new System.EventHandler(this.comboBoxPParameterCollection_SelectionChangeCommitted);
            // 
            // numericPParameterTypeSizeInBit
            // 
            this.numericPParameterTypeSizeInBit.Location = new System.Drawing.Point(525, 210);
            this.numericPParameterTypeSizeInBit.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numericPParameterTypeSizeInBit.Name = "numericPParameterTypeSizeInBit";
            this.numericPParameterTypeSizeInBit.Size = new System.Drawing.Size(249, 20);
            this.numericPParameterTypeSizeInBit.TabIndex = 234;
            // 
            // buttonParCoParameterSave
            // 
            this.buttonParCoParameterSave.Location = new System.Drawing.Point(389, 3);
            this.buttonParCoParameterSave.Name = "buttonParCoParameterSave";
            this.buttonParCoParameterSave.Size = new System.Drawing.Size(75, 23);
            this.buttonParCoParameterSave.TabIndex = 233;
            this.buttonParCoParameterSave.Text = "speichern";
            this.buttonParCoParameterSave.UseVisualStyleBackColor = true;
            this.buttonParCoParameterSave.Click += new System.EventHandler(this.buttonParCoParameterSave_Click);
            // 
            // comboBoxPParameterTypeName
            // 
            this.comboBoxPParameterTypeName.FormattingEnabled = true;
            this.comboBoxPParameterTypeName.Location = new System.Drawing.Point(525, 83);
            this.comboBoxPParameterTypeName.Name = "comboBoxPParameterTypeName";
            this.comboBoxPParameterTypeName.Size = new System.Drawing.Size(249, 21);
            this.comboBoxPParameterTypeName.TabIndex = 232;
            this.comboBoxPParameterTypeName.SelectionChangeCommitted += new System.EventHandler(this.comboBoxPParameterTypeName_SelectionChangeCommitted);
            // 
            // label322
            // 
            this.label322.AutoSize = true;
            this.label322.Location = new System.Drawing.Point(3, 372);
            this.label322.Name = "label322";
            this.label322.Size = new System.Drawing.Size(64, 13);
            this.label322.TabIndex = 231;
            this.label322.Text = "Translations";
            // 
            // dgvAppProgParTranslations
            // 
            this.dgvAppProgParTranslations.AllowUserToAddRows = false;
            this.dgvAppProgParTranslations.AllowUserToDeleteRows = false;
            this.dgvAppProgParTranslations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAppProgParTranslations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppProgParTranslations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dgvAppProgParTranslations.Location = new System.Drawing.Point(122, 372);
            this.dgvAppProgParTranslations.Name = "dgvAppProgParTranslations";
            this.dgvAppProgParTranslations.RowHeadersVisible = false;
            this.dgvAppProgParTranslations.Size = new System.Drawing.Size(261, 84);
            this.dgvAppProgParTranslations.TabIndex = 230;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Sprache";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 72;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Text";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 53;
            // 
            // label306
            // 
            this.label306.AutoSize = true;
            this.label306.Location = new System.Drawing.Point(404, 215);
            this.label306.Name = "label306";
            this.label306.Size = new System.Drawing.Size(116, 13);
            this.label306.TabIndex = 229;
            this.label306.Text = "ParametertypeSizeInBit";
            // 
            // label223
            // 
            this.label223.AutoSize = true;
            this.label223.Location = new System.Drawing.Point(404, 110);
            this.label223.Name = "label223";
            this.label223.Size = new System.Drawing.Size(110, 13);
            this.label223.TabIndex = 228;
            this.label223.Text = "SelectableParameters";
            // 
            // listPSelectableParameters
            // 
            this.listPSelectableParameters.FormattingEnabled = true;
            this.listPSelectableParameters.Location = new System.Drawing.Point(525, 110);
            this.listPSelectableParameters.Name = "listPSelectableParameters";
            this.listPSelectableParameters.Size = new System.Drawing.Size(249, 95);
            this.listPSelectableParameters.TabIndex = 227;
            // 
            // label222
            // 
            this.label222.AutoSize = true;
            this.label222.Location = new System.Drawing.Point(404, 86);
            this.label222.Name = "label222";
            this.label222.Size = new System.Drawing.Size(106, 13);
            this.label222.TabIndex = 226;
            this.label222.Text = "Parametertype Name";
            // 
            // label172
            // 
            this.label172.AutoSize = true;
            this.label172.Location = new System.Drawing.Point(3, 110);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(114, 13);
            this.label172.TabIndex = 225;
            this.label172.Text = "ParameterTypeParams";
            // 
            // listPParameterTypeParams
            // 
            this.listPParameterTypeParams.FormattingEnabled = true;
            this.listPParameterTypeParams.Location = new System.Drawing.Point(122, 109);
            this.listPParameterTypeParams.Name = "listPParameterTypeParams";
            this.listPParameterTypeParams.Size = new System.Drawing.Size(261, 43);
            this.listPParameterTypeParams.TabIndex = 224;
            // 
            // label171
            // 
            this.label171.AutoSize = true;
            this.label171.Location = new System.Drawing.Point(3, 349);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(103, 13);
            this.label171.TabIndex = 223;
            this.label171.Text = "LegacyPatchAlways";
            // 
            // textPLegacyPatchAlways
            // 
            this.textPLegacyPatchAlways.Location = new System.Drawing.Point(122, 346);
            this.textPLegacyPatchAlways.Name = "textPLegacyPatchAlways";
            this.textPLegacyPatchAlways.Size = new System.Drawing.Size(261, 20);
            this.textPLegacyPatchAlways.TabIndex = 222;
            // 
            // tabControlParItem
            // 
            this.tabControlParItem.Controls.Add(this.tabParMemory);
            this.tabControlParItem.Controls.Add(this.tabParProperty);
            this.tabControlParItem.Location = new System.Drawing.Point(6, 482);
            this.tabControlParItem.Name = "tabControlParItem";
            this.tabControlParItem.SelectedIndex = 0;
            this.tabControlParItem.Size = new System.Drawing.Size(377, 200);
            this.tabControlParItem.TabIndex = 221;
            // 
            // tabParMemory
            // 
            this.tabParMemory.Controls.Add(this.label326);
            this.tabParMemory.Controls.Add(this.label68);
            this.tabParMemory.Controls.Add(this.textParMemAddress);
            this.tabParMemory.Controls.Add(this.label67);
            this.tabParMemory.Controls.Add(this.label65);
            this.tabParMemory.Controls.Add(this.textPMBitOffset);
            this.tabParMemory.Controls.Add(this.textPMOffset);
            this.tabParMemory.Controls.Add(this.label310);
            this.tabParMemory.Controls.Add(this.textPMCodeSegment);
            this.tabParMemory.Controls.Add(this.textParBaseAddress);
            this.tabParMemory.Location = new System.Drawing.Point(4, 22);
            this.tabParMemory.Name = "tabParMemory";
            this.tabParMemory.Padding = new System.Windows.Forms.Padding(3);
            this.tabParMemory.Size = new System.Drawing.Size(369, 174);
            this.tabParMemory.TabIndex = 0;
            this.tabParMemory.Text = "Memory";
            this.tabParMemory.UseVisualStyleBackColor = true;
            // 
            // label326
            // 
            this.label326.AutoSize = true;
            this.label326.Location = new System.Drawing.Point(6, 143);
            this.label326.Name = "label326";
            this.label326.Size = new System.Drawing.Size(136, 13);
            this.label326.TabIndex = 114;
            this.label326.Text = "Parameter Memory Address";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(6, 61);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(47, 13);
            this.label68.TabIndex = 5;
            this.label68.Text = "BitOffset";
            // 
            // textParMemAddress
            // 
            this.textParMemAddress.Location = new System.Drawing.Point(148, 140);
            this.textParMemAddress.Name = "textParMemAddress";
            this.textParMemAddress.ReadOnly = true;
            this.textParMemAddress.Size = new System.Drawing.Size(177, 20);
            this.textParMemAddress.TabIndex = 113;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(6, 35);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(35, 13);
            this.label67.TabIndex = 4;
            this.label67.Text = "Offset";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(6, 9);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(74, 13);
            this.label65.TabIndex = 3;
            this.label65.Text = "CodeSegment";
            // 
            // textPMBitOffset
            // 
            this.textPMBitOffset.Location = new System.Drawing.Point(112, 58);
            this.textPMBitOffset.Name = "textPMBitOffset";
            this.textPMBitOffset.ReadOnly = true;
            this.textPMBitOffset.Size = new System.Drawing.Size(251, 20);
            this.textPMBitOffset.TabIndex = 2;
            // 
            // textPMOffset
            // 
            this.textPMOffset.Location = new System.Drawing.Point(112, 32);
            this.textPMOffset.Name = "textPMOffset";
            this.textPMOffset.ReadOnly = true;
            this.textPMOffset.Size = new System.Drawing.Size(251, 20);
            this.textPMOffset.TabIndex = 1;
            // 
            // label310
            // 
            this.label310.AutoSize = true;
            this.label310.Location = new System.Drawing.Point(6, 114);
            this.label310.Name = "label310";
            this.label310.Size = new System.Drawing.Size(109, 13);
            this.label310.TabIndex = 7;
            this.label310.Text = "Memory BaseAddress";
            // 
            // textPMCodeSegment
            // 
            this.textPMCodeSegment.Location = new System.Drawing.Point(112, 6);
            this.textPMCodeSegment.Name = "textPMCodeSegment";
            this.textPMCodeSegment.ReadOnly = true;
            this.textPMCodeSegment.Size = new System.Drawing.Size(251, 20);
            this.textPMCodeSegment.TabIndex = 0;
            // 
            // textParBaseAddress
            // 
            this.textParBaseAddress.Location = new System.Drawing.Point(148, 111);
            this.textParBaseAddress.Name = "textParBaseAddress";
            this.textParBaseAddress.ReadOnly = true;
            this.textParBaseAddress.Size = new System.Drawing.Size(177, 20);
            this.textParBaseAddress.TabIndex = 6;
            // 
            // tabParProperty
            // 
            this.tabParProperty.Controls.Add(this.label85);
            this.tabParProperty.Controls.Add(this.label91);
            this.tabParProperty.Controls.Add(this.label88);
            this.tabParProperty.Controls.Add(this.label87);
            this.tabParProperty.Controls.Add(this.label86);
            this.tabParProperty.Controls.Add(this.label84);
            this.tabParProperty.Controls.Add(this.textPPBitOffset);
            this.tabParProperty.Controls.Add(this.textPPOffset);
            this.tabParProperty.Controls.Add(this.textPPObjectType);
            this.tabParProperty.Controls.Add(this.textPPOccurence);
            this.tabParProperty.Controls.Add(this.textPPPropertyId);
            this.tabParProperty.Controls.Add(this.textPPObjectIndex);
            this.tabParProperty.Location = new System.Drawing.Point(4, 22);
            this.tabParProperty.Name = "tabParProperty";
            this.tabParProperty.Padding = new System.Windows.Forms.Padding(3);
            this.tabParProperty.Size = new System.Drawing.Size(369, 174);
            this.tabParProperty.TabIndex = 1;
            this.tabParProperty.Text = "Property";
            this.tabParProperty.UseVisualStyleBackColor = true;
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(6, 139);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(47, 13);
            this.label85.TabIndex = 16;
            this.label85.Text = "BitOffset";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(6, 113);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(35, 13);
            this.label91.TabIndex = 13;
            this.label91.Text = "Offset";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(6, 35);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(62, 13);
            this.label88.TabIndex = 12;
            this.label88.Text = "ObjectType";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(6, 61);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(60, 13);
            this.label87.TabIndex = 11;
            this.label87.Text = "Occurence";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(6, 87);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(55, 13);
            this.label86.TabIndex = 10;
            this.label86.Text = "PropertyId";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(6, 9);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(64, 13);
            this.label84.TabIndex = 8;
            this.label84.Text = "ObjectIndex";
            // 
            // textPPBitOffset
            // 
            this.textPPBitOffset.Location = new System.Drawing.Point(112, 136);
            this.textPPBitOffset.Name = "textPPBitOffset";
            this.textPPBitOffset.Size = new System.Drawing.Size(251, 20);
            this.textPPBitOffset.TabIndex = 7;
            // 
            // textPPOffset
            // 
            this.textPPOffset.Location = new System.Drawing.Point(113, 110);
            this.textPPOffset.Name = "textPPOffset";
            this.textPPOffset.Size = new System.Drawing.Size(251, 20);
            this.textPPOffset.TabIndex = 4;
            // 
            // textPPObjectType
            // 
            this.textPPObjectType.Location = new System.Drawing.Point(113, 32);
            this.textPPObjectType.Name = "textPPObjectType";
            this.textPPObjectType.Size = new System.Drawing.Size(251, 20);
            this.textPPObjectType.TabIndex = 3;
            // 
            // textPPOccurence
            // 
            this.textPPOccurence.Location = new System.Drawing.Point(113, 58);
            this.textPPOccurence.Name = "textPPOccurence";
            this.textPPOccurence.Size = new System.Drawing.Size(251, 20);
            this.textPPOccurence.TabIndex = 2;
            // 
            // textPPPropertyId
            // 
            this.textPPPropertyId.Location = new System.Drawing.Point(113, 84);
            this.textPPPropertyId.Name = "textPPPropertyId";
            this.textPPPropertyId.Size = new System.Drawing.Size(251, 20);
            this.textPPPropertyId.TabIndex = 1;
            // 
            // textPPObjectIndex
            // 
            this.textPPObjectIndex.Location = new System.Drawing.Point(113, 6);
            this.textPPObjectIndex.Name = "textPPObjectIndex";
            this.textPPObjectIndex.Size = new System.Drawing.Size(251, 20);
            this.textPPObjectIndex.TabIndex = 0;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(3, 323);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(95, 13);
            this.label51.TabIndex = 220;
            this.label51.Text = "InternalDescription";
            // 
            // textPInternalDescription
            // 
            this.textPInternalDescription.Location = new System.Drawing.Point(122, 320);
            this.textPInternalDescription.Name = "textPInternalDescription";
            this.textPInternalDescription.Size = new System.Drawing.Size(261, 20);
            this.textPInternalDescription.TabIndex = 219;
            // 
            // textPCustomerAdjustable
            // 
            this.textPCustomerAdjustable.Location = new System.Drawing.Point(122, 293);
            this.textPCustomerAdjustable.Name = "textPCustomerAdjustable";
            this.textPCustomerAdjustable.Size = new System.Drawing.Size(261, 20);
            this.textPCustomerAdjustable.TabIndex = 218;
            // 
            // textPInitialValue
            // 
            this.textPInitialValue.Location = new System.Drawing.Point(122, 266);
            this.textPInitialValue.Name = "textPInitialValue";
            this.textPInitialValue.Size = new System.Drawing.Size(261, 20);
            this.textPInitialValue.TabIndex = 217;
            // 
            // textPValue
            // 
            this.textPValue.Location = new System.Drawing.Point(122, 239);
            this.textPValue.Name = "textPValue";
            this.textPValue.Size = new System.Drawing.Size(261, 20);
            this.textPValue.TabIndex = 216;
            // 
            // textPAccess
            // 
            this.textPAccess.Location = new System.Drawing.Point(122, 212);
            this.textPAccess.Name = "textPAccess";
            this.textPAccess.Size = new System.Drawing.Size(261, 20);
            this.textPAccess.TabIndex = 215;
            // 
            // textPSuffixText
            // 
            this.textPSuffixText.Location = new System.Drawing.Point(122, 185);
            this.textPSuffixText.Name = "textPSuffixText";
            this.textPSuffixText.Size = new System.Drawing.Size(261, 20);
            this.textPSuffixText.TabIndex = 214;
            // 
            // textPText
            // 
            this.textPText.Location = new System.Drawing.Point(122, 158);
            this.textPText.Name = "textPText";
            this.textPText.Size = new System.Drawing.Size(261, 20);
            this.textPText.TabIndex = 213;
            // 
            // textPParameterType
            // 
            this.textPParameterType.Location = new System.Drawing.Point(122, 83);
            this.textPParameterType.Name = "textPParameterType";
            this.textPParameterType.Size = new System.Drawing.Size(261, 20);
            this.textPParameterType.TabIndex = 212;
            // 
            // textPName
            // 
            this.textPName.Location = new System.Drawing.Point(122, 56);
            this.textPName.Name = "textPName";
            this.textPName.Size = new System.Drawing.Size(261, 20);
            this.textPName.TabIndex = 211;
            // 
            // textPId
            // 
            this.textPId.Location = new System.Drawing.Point(122, 30);
            this.textPId.Name = "textPId";
            this.textPId.Size = new System.Drawing.Size(261, 20);
            this.textPId.TabIndex = 210;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(3, 296);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(100, 13);
            this.label69.TabIndex = 209;
            this.label69.Text = "CustomerAdjustable";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(3, 269);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(58, 13);
            this.label70.TabIndex = 208;
            this.label70.Text = "InitialValue";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(3, 242);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(34, 13);
            this.label71.TabIndex = 207;
            this.label71.Text = "Value";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(3, 212);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(42, 13);
            this.label78.TabIndex = 206;
            this.label78.Text = "Access";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(3, 188);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(54, 13);
            this.label79.TabIndex = 205;
            this.label79.Text = "SuffixText";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(3, 161);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(28, 13);
            this.label80.TabIndex = 204;
            this.label80.Text = "Text";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(3, 86);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(79, 13);
            this.label81.TabIndex = 203;
            this.label81.Text = "ParameterType";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(3, 59);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(35, 13);
            this.label82.TabIndex = 202;
            this.label82.Text = "Name";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(3, 33);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(16, 13);
            this.label83.TabIndex = 201;
            this.label83.Text = "Id";
            // 
            // AppParameterGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label442);
            this.Controls.Add(this.label440);
            this.Controls.Add(this.comboBoxPParameterCollection);
            this.Controls.Add(this.numericPParameterTypeSizeInBit);
            this.Controls.Add(this.buttonParCoParameterSave);
            this.Controls.Add(this.comboBoxPParameterTypeName);
            this.Controls.Add(this.label322);
            this.Controls.Add(this.dgvAppProgParTranslations);
            this.Controls.Add(this.label306);
            this.Controls.Add(this.label223);
            this.Controls.Add(this.listPSelectableParameters);
            this.Controls.Add(this.label222);
            this.Controls.Add(this.label172);
            this.Controls.Add(this.listPParameterTypeParams);
            this.Controls.Add(this.label171);
            this.Controls.Add(this.textPLegacyPatchAlways);
            this.Controls.Add(this.tabControlParItem);
            this.Controls.Add(this.label51);
            this.Controls.Add(this.textPInternalDescription);
            this.Controls.Add(this.textPCustomerAdjustable);
            this.Controls.Add(this.textPInitialValue);
            this.Controls.Add(this.textPValue);
            this.Controls.Add(this.textPAccess);
            this.Controls.Add(this.textPSuffixText);
            this.Controls.Add(this.textPText);
            this.Controls.Add(this.textPParameterType);
            this.Controls.Add(this.textPName);
            this.Controls.Add(this.textPId);
            this.Controls.Add(this.label69);
            this.Controls.Add(this.label70);
            this.Controls.Add(this.label71);
            this.Controls.Add(this.label78);
            this.Controls.Add(this.label79);
            this.Controls.Add(this.label80);
            this.Controls.Add(this.label81);
            this.Controls.Add(this.label82);
            this.Controls.Add(this.label83);
            this.Name = "AppParameterGui";
            this.Size = new System.Drawing.Size(782, 827);
            ((System.ComponentModel.ISupportInitialize)(this.numericPParameterTypeSizeInBit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppProgParTranslations)).EndInit();
            this.tabControlParItem.ResumeLayout(false);
            this.tabParMemory.ResumeLayout(false);
            this.tabParMemory.PerformLayout();
            this.tabParProperty.ResumeLayout(false);
            this.tabParProperty.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label442;
        private System.Windows.Forms.Label label440;
        private System.Windows.Forms.ComboBox comboBoxPParameterCollection;
        private System.Windows.Forms.NumericUpDown numericPParameterTypeSizeInBit;
        private System.Windows.Forms.Button buttonParCoParameterSave;
        private System.Windows.Forms.ComboBox comboBoxPParameterTypeName;
        private System.Windows.Forms.Label label322;
        private System.Windows.Forms.DataGridView dgvAppProgParTranslations;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Label label306;
        private System.Windows.Forms.Label label223;
        private System.Windows.Forms.ListBox listPSelectableParameters;
        private System.Windows.Forms.Label label222;
        private System.Windows.Forms.Label label172;
        private System.Windows.Forms.ListBox listPParameterTypeParams;
        private System.Windows.Forms.Label label171;
        private System.Windows.Forms.TextBox textPLegacyPatchAlways;
        private System.Windows.Forms.TabControl tabControlParItem;
        private System.Windows.Forms.TabPage tabParMemory;
        private System.Windows.Forms.Label label326;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.TextBox textParMemAddress;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.TextBox textPMBitOffset;
        private System.Windows.Forms.TextBox textPMOffset;
        private System.Windows.Forms.Label label310;
        private System.Windows.Forms.TextBox textPMCodeSegment;
        private System.Windows.Forms.TextBox textParBaseAddress;
        private System.Windows.Forms.TabPage tabParProperty;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.TextBox textPPBitOffset;
        private System.Windows.Forms.TextBox textPPOffset;
        private System.Windows.Forms.TextBox textPPObjectType;
        private System.Windows.Forms.TextBox textPPOccurence;
        private System.Windows.Forms.TextBox textPPPropertyId;
        private System.Windows.Forms.TextBox textPPObjectIndex;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox textPInternalDescription;
        private System.Windows.Forms.TextBox textPCustomerAdjustable;
        private System.Windows.Forms.TextBox textPInitialValue;
        private System.Windows.Forms.TextBox textPValue;
        private System.Windows.Forms.TextBox textPAccess;
        private System.Windows.Forms.TextBox textPSuffixText;
        private System.Windows.Forms.TextBox textPText;
        private System.Windows.Forms.TextBox textPParameterType;
        private System.Windows.Forms.TextBox textPName;
        private System.Windows.Forms.TextBox textPId;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label83;
    }
}
