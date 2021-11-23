
namespace knxprod_ns
{
    partial class ContextMenuHandler
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
            this.contextMenuAddDeleteCopyPaste = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuAddDeleteCopyPaste.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuAddDeleteCopyPaste
            // 
            this.contextMenuAddDeleteCopyPaste.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuAdd,
            this.toolStripMenuDelete,
            this.toolStripMenuCopy,
            this.toolStripMenuPaste});
            this.contextMenuAddDeleteCopyPaste.Name = "contextMenuParameterTypes";
            this.contextMenuAddDeleteCopyPaste.Size = new System.Drawing.Size(181, 114);
            this.contextMenuAddDeleteCopyPaste.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuAddDeleteCopyPasteItemClicked);
            // 
            // toolStripMenuAdd
            // 
            this.toolStripMenuAdd.Name = "toolStripMenuAdd";
            this.toolStripMenuAdd.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuAdd.Text = "hinzufügen";
            // 
            // toolStripMenuDelete
            // 
            this.toolStripMenuDelete.Name = "toolStripMenuDelete";
            this.toolStripMenuDelete.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuDelete.Text = "löschen";
            // 
            // toolStripMenuCopy
            // 
            this.toolStripMenuCopy.Name = "toolStripMenuCopy";
            this.toolStripMenuCopy.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuCopy.Text = "kopieren";
            // 
            // toolStripMenuPaste
            // 
            this.toolStripMenuPaste.Name = "toolStripMenuPaste";
            this.toolStripMenuPaste.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuPaste.Text = "einfügen";
            // 
            // ContextMenuHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ContextMenuHandler";
            this.contextMenuAddDeleteCopyPaste.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuAddDeleteCopyPaste;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuDelete;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuCopy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuPaste;
    }
}
