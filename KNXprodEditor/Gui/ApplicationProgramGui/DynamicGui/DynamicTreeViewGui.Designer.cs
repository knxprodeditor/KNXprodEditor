
namespace knxprod_ns
{
    partial class DynamicTreeViewGui
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DynamicTreeViewGui));
            this.expandParaKoTreeView = new System.Windows.Forms.CheckBox();
            this.ParaKoTreeView = new System.Windows.Forms.TreeView();
            this.contextMenuAppParCo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuAddApplicationChannel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuAddChannnelIndependentBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuAddParameterBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuAddComObject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuAddParameter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuAddChoose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuAddWhen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuAppParCoDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuCopyDynamicObject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuPasteDynamicObject = new System.Windows.Forms.ToolStripMenuItem();
            this.DynamicTreeViewImageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuAppParCo.SuspendLayout();
            this.SuspendLayout();
            // 
            // expandParaKoTreeView
            // 
            this.expandParaKoTreeView.AutoSize = true;
            this.expandParaKoTreeView.Location = new System.Drawing.Point(3, 3);
            this.expandParaKoTreeView.Name = "expandParaKoTreeView";
            this.expandParaKoTreeView.Size = new System.Drawing.Size(105, 17);
            this.expandParaKoTreeView.TabIndex = 5;
            this.expandParaKoTreeView.Text = "alles ausklappen";
            this.expandParaKoTreeView.UseVisualStyleBackColor = true;
            this.expandParaKoTreeView.CheckedChanged += new System.EventHandler(this.expandParaKoTreeView_CheckedChanged);
            // 
            // ParaKoTreeView
            // 
            this.ParaKoTreeView.AllowDrop = true;
            this.ParaKoTreeView.ImageIndex = 0;
            this.ParaKoTreeView.ImageList = this.DynamicTreeViewImageList;
            this.ParaKoTreeView.Location = new System.Drawing.Point(3, 22);
            this.ParaKoTreeView.Name = "ParaKoTreeView";
            this.ParaKoTreeView.SelectedImageIndex = 0;
            this.ParaKoTreeView.Size = new System.Drawing.Size(400, 593);
            this.ParaKoTreeView.TabIndex = 4;
            this.ParaKoTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ParaKoTreeView_ItemDrag);
            this.ParaKoTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ParaKoTreeView_AfterSelect);
            this.ParaKoTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ParaKoTreeView_DragDrop);
            this.ParaKoTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.ParaKoTreeView_DragEnter);
            this.ParaKoTreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.ParaKoTreeView_DragOver);
            this.ParaKoTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ParaKoTreeView_MouseDown);
            // 
            // contextMenuAppParCo
            // 
            this.contextMenuAppParCo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuAddApplicationChannel,
            this.toolStripMenuAddChannnelIndependentBlock,
            this.toolStripMenuAddParameterBlock,
            this.toolStripMenuAddComObject,
            this.toolStripMenuAddParameter,
            this.toolStripMenuAddChoose,
            this.toolStripMenuAddWhen,
            this.toolStripSeparator1,
            this.toolStripMenuAppParCoDelete,
            this.toolStripMenuCopyDynamicObject,
            this.toolStripMenuPasteDynamicObject});
            this.contextMenuAppParCo.Name = "contextMenuParameterTypes";
            this.contextMenuAppParCo.Size = new System.Drawing.Size(285, 230);
            // 
            // toolStripMenuAddApplicationChannel
            // 
            this.toolStripMenuAddApplicationChannel.Name = "toolStripMenuAddApplicationChannel";
            this.toolStripMenuAddApplicationChannel.Size = new System.Drawing.Size(284, 22);
            this.toolStripMenuAddApplicationChannel.Text = "ApplicationChannel hinzufügen";
            this.toolStripMenuAddApplicationChannel.Click += new System.EventHandler(this.toolStripMenuAddApplicationChannel_Click);
            // 
            // toolStripMenuAddChannnelIndependentBlock
            // 
            this.toolStripMenuAddChannnelIndependentBlock.Name = "toolStripMenuAddChannnelIndependentBlock";
            this.toolStripMenuAddChannnelIndependentBlock.Size = new System.Drawing.Size(284, 22);
            this.toolStripMenuAddChannnelIndependentBlock.Text = "ChannnelIndependentBlock hinzufügen";
            this.toolStripMenuAddChannnelIndependentBlock.Click += new System.EventHandler(this.toolStripMenuAddChannnelIndependentBlock_Click);
            // 
            // toolStripMenuAddParameterBlock
            // 
            this.toolStripMenuAddParameterBlock.Name = "toolStripMenuAddParameterBlock";
            this.toolStripMenuAddParameterBlock.Size = new System.Drawing.Size(284, 22);
            this.toolStripMenuAddParameterBlock.Text = "ParameterBlock hinzufügen";
            this.toolStripMenuAddParameterBlock.Click += new System.EventHandler(this.toolStripMenuAddParameterBlock_Click);
            // 
            // toolStripMenuAddComObject
            // 
            this.toolStripMenuAddComObject.Name = "toolStripMenuAddComObject";
            this.toolStripMenuAddComObject.Size = new System.Drawing.Size(284, 22);
            this.toolStripMenuAddComObject.Text = "ComObject hinzufügen";
            this.toolStripMenuAddComObject.Click += new System.EventHandler(this.toolStripMenuAddComObject_Click);
            // 
            // toolStripMenuAddParameter
            // 
            this.toolStripMenuAddParameter.Name = "toolStripMenuAddParameter";
            this.toolStripMenuAddParameter.Size = new System.Drawing.Size(284, 22);
            this.toolStripMenuAddParameter.Text = "Parameter hinzufügen";
            this.toolStripMenuAddParameter.Click += new System.EventHandler(this.toolStripMenuAddParameter_Click);
            // 
            // toolStripMenuAddChoose
            // 
            this.toolStripMenuAddChoose.Name = "toolStripMenuAddChoose";
            this.toolStripMenuAddChoose.Size = new System.Drawing.Size(284, 22);
            this.toolStripMenuAddChoose.Text = "Choose hinzufügen";
            this.toolStripMenuAddChoose.Click += new System.EventHandler(this.toolStripMenuAddChoose_Click);
            // 
            // toolStripMenuAddWhen
            // 
            this.toolStripMenuAddWhen.Name = "toolStripMenuAddWhen";
            this.toolStripMenuAddWhen.Size = new System.Drawing.Size(284, 22);
            this.toolStripMenuAddWhen.Text = "When hinzufügen";
            this.toolStripMenuAddWhen.Click += new System.EventHandler(this.toolStripMenuAddWhen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(281, 6);
            // 
            // toolStripMenuAppParCoDelete
            // 
            this.toolStripMenuAppParCoDelete.Name = "toolStripMenuAppParCoDelete";
            this.toolStripMenuAppParCoDelete.Size = new System.Drawing.Size(284, 22);
            this.toolStripMenuAppParCoDelete.Text = "löschen";
            this.toolStripMenuAppParCoDelete.Click += new System.EventHandler(this.toolStripMenuAppParCoDelete_Click);
            // 
            // toolStripMenuCopyDynamicObject
            // 
            this.toolStripMenuCopyDynamicObject.Name = "toolStripMenuCopyDynamicObject";
            this.toolStripMenuCopyDynamicObject.Size = new System.Drawing.Size(284, 22);
            this.toolStripMenuCopyDynamicObject.Text = "kopieren";
            this.toolStripMenuCopyDynamicObject.Click += new System.EventHandler(this.toolStripMenuCopyDynamicObject_Click);
            // 
            // toolStripMenuPasteDynamicObject
            // 
            this.toolStripMenuPasteDynamicObject.Name = "toolStripMenuPasteDynamicObject";
            this.toolStripMenuPasteDynamicObject.Size = new System.Drawing.Size(284, 22);
            this.toolStripMenuPasteDynamicObject.Text = "einfügen";
            this.toolStripMenuPasteDynamicObject.Click += new System.EventHandler(this.toolStripMenuPasteDynamicObject_Click);
            // 
            // DynamicTreeViewImageList
            // 
            this.DynamicTreeViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("DynamicTreeViewImageList.ImageStream")));
            this.DynamicTreeViewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.DynamicTreeViewImageList.Images.SetKeyName(0, "application");
            this.DynamicTreeViewImageList.Images.SetKeyName(1, "channel.png");
            this.DynamicTreeViewImageList.Images.SetKeyName(2, "parameterBlock.png");
            this.DynamicTreeViewImageList.Images.SetKeyName(3, "parameter");
            this.DynamicTreeViewImageList.Images.SetKeyName(4, "commObject");
            this.DynamicTreeViewImageList.Images.SetKeyName(5, "choose");
            this.DynamicTreeViewImageList.Images.SetKeyName(6, "when.png");
            this.DynamicTreeViewImageList.Images.SetKeyName(7, "ComObjectParameterBlock.png");
            this.DynamicTreeViewImageList.Images.SetKeyName(8, "separator.png");
            // 
            // DynamicTreeViewGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.expandParaKoTreeView);
            this.Controls.Add(this.ParaKoTreeView);
            this.Name = "DynamicTreeViewGui";
            this.Size = new System.Drawing.Size(407, 620);
            this.contextMenuAppParCo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox expandParaKoTreeView;
        private System.Windows.Forms.TreeView ParaKoTreeView;
        private System.Windows.Forms.ContextMenuStrip contextMenuAppParCo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAddApplicationChannel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAddChannnelIndependentBlock;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAddParameterBlock;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAddComObject;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAddParameter;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAddChoose;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAddWhen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAppParCoDelete;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuCopyDynamicObject;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuPasteDynamicObject;
        private System.Windows.Forms.ImageList DynamicTreeViewImageList;
    }
}
