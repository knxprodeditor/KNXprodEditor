using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnxProd.Model;

namespace knxprod_ns
{
    public partial class ContextMenuHandler : UserControl
    {
        private static ContextMenuHandler mContextMenuHandler;

        public ContextMenuHandler()
        {
            InitializeComponent();
            mContextMenuHandler = this;
        }

        // Merker ob der Treeview der Parametertypes oder Enumeration zuletzt geklickt wurde, um die korrekte Aktion des contextMenu zu veranlassen
        public static TreeView contextMenuLastClickedTreeView;

        /// <summary>
        /// Handler, wenn auf ein contextMenü Item geklickt wurde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void contextMenuAddDeleteCopyPasteItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (contextMenuLastClickedTreeView == ParameterTypesGui.GetParaTypesTreeView())
            {
                if (e.ClickedItem == this.toolStripMenuAdd)
                {
                    ParameterTypesGui.AddParameterType();
                }
                if (e.ClickedItem == this.toolStripMenuDelete)
                {
                    ParameterTypesGui.DeleteParameterType();
                }
                if (e.ClickedItem == toolStripMenuCopy)
                {
                    ParameterTypesGui.CopyParameterType();
                }
            }

            if (contextMenuLastClickedTreeView == ParameterTypesGui.GetTypeResEnumTreeView())
            {
                if (e.ClickedItem == this.toolStripMenuAdd)
                {
                    ParameterTypesGui.AddEnumItem();
                }
                if (e.ClickedItem == this.toolStripMenuDelete)
                {
                    ParameterTypesGui.DeleteEnumItem();
                }
                if (e.ClickedItem == toolStripMenuCopy)
                {
                    ParameterTypesGui.CopyEnumItem();
                }
            }

            if (contextMenuLastClickedTreeView == ComObjectsGui.GetCommObjectsTreeView())
            {
                if (e.ClickedItem == this.toolStripMenuAdd)
                {
                    ComObjectsGui.AddComObject();
                }
                if (e.ClickedItem == this.toolStripMenuDelete)
                {
                    ComObjectsGui.DeleteComObject();
                }
                if (e.ClickedItem == toolStripMenuCopy)
                {
                    ComObjectsGui.CopyComObject();
                }
            }

            if (contextMenuLastClickedTreeView == ParameterGui.GetParameterTreeView())
            {
                if (e.ClickedItem == this.toolStripMenuAdd)
                {
                    ParameterGui.AddParameter();
                }
                if (e.ClickedItem == this.toolStripMenuDelete)
                {
                    ParameterGui.DeleteParameter();
                }
                if (e.ClickedItem == toolStripMenuCopy)
                {
                    ParameterGui.CopyParameter();
                }
            }
            if (contextMenuLastClickedTreeView == ParameterGui.GetParUnionParametersTreeView())
            {
                if (e.ClickedItem == this.toolStripMenuAdd)
                {
                    ParameterGui.AddUnionParameter();
                }
                if (e.ClickedItem == this.toolStripMenuDelete)
                {
                    ParameterGui.DeleteUnionParameter();
                }
                if (e.ClickedItem == toolStripMenuCopy)
                {
                    ParameterGui.CopyUnionParameter();
                }
            }
        }

        public static void ShowContextMenuAtMousePosition()
        {
            mContextMenuHandler.contextMenuAddDeleteCopyPaste.Show(Cursor.Position.X, Cursor.Position.Y);
        }

       
        public static void SetToolStripMenuAddVisible(bool value)
        {
            mContextMenuHandler.toolStripMenuAdd.Visible = value;
        }

        public static void SettoolStripMenuPasteVisible(bool value)
        {
            mContextMenuHandler.toolStripMenuPaste.Visible = value;
        }
        public static void SettoolStripMenuDeleteVisible(bool value)
        {
            mContextMenuHandler.toolStripMenuDelete.Visible = value;
        }
        public static void SettoolStripMenuCopyVisible(bool value)
        {
            mContextMenuHandler.toolStripMenuCopy.Visible = value;
        }
    }
}
