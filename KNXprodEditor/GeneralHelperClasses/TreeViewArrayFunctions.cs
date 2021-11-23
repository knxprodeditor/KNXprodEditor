using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace knxprod_ns
{
    class TreeViewArrayFunctions
    {
        private T[] AddArrayTreeView<T>(T[] array, T newValue, out TreeNode childNode)
        {
            TreeNode test = new TreeNode();
            childNode = test;

            return HandleArrayFunctions.Add(array, newValue);
        }


        /// <summary>
        /// einen Eintrag aus einem Array beliebigen Typs und dem dazugehörigen TreeView löschen
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">aus dem Array wird das Objekt gelöscht</param>
        /// <param name="index">an diesem Index wird das Objekt gelöscht</param>
        /// <param name="treeView">der Treeview, in dem das zu löschende Objekt aufgeführt ist</param>
        /// <returns></returns>
        public static T[] DeleteArrayTreeView<T>(T[] array, int index, TreeView treeView)
        {
            DeleteTreeNodeByTag(array[index], treeView);

            int newLength = array.Length - 1;

            T[] result = new T[newLength];

            if (newLength < 1)
            {
                return result;
            }

            for (int i = 0, j = 0; i < array.Length; i++, j++)
            {
                if (i == index)
                {
                    j--;
                }
                else
                {
                    result[j] = array[i];
                }
            }
            return result;
        }

        /// <summary>
        /// löscht einen TreeNode anhand eines Tags
        /// </summary>
        /// <param name="tag">der zu löschende Tag</param>
        /// <param name="treeView">der TreeView in dem gesucht und gelöscht werden soll</param>
        private static void DeleteTreeNodeByTag(object tag, TreeView treeView)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                recDeleteTreeNodeByTag(node, tag, treeView);
            }
        }


        private static void recDeleteTreeNodeByTag(TreeNode treeNode, object tag, TreeView treeView)
        {
            if (treeNode != null)
            {
                if (treeNode.Tag == tag)
                {
                    treeView.Nodes.Remove(treeNode);
                }
                else
                {
                    foreach (TreeNode node in treeNode.Nodes)
                    {
                        recDeleteTreeNodeByTag(node, tag, treeView);
                    }
                }
            }
        }

        /// <summary>
        /// Sucht einen TreeNode anhand eines Tags
        /// </summary>
        /// <param name="tag">der zu suchende Tag</param>
        /// <param name="treeView">der TreeView in dem gesucht werden soll</param>
        /// <returns>den gefundenen TreeNode oder null</returns>
        public static TreeNode SearchTreeNodeByTag(object tag, TreeView treeView)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                TreeNode foundNode = recSearchTreeNodeByTag(node, tag, treeView);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }
            return null;
        }

        // rekusive Funktion für die Funktion SearchTreeNodeByTag
        private static TreeNode recSearchTreeNodeByTag(TreeNode treeNode, object tag, TreeView treeView)
        {
            if (treeNode != null)
            {
                if (treeNode.Tag == tag)
                {
                    return treeNode;
                }
                else
                {
                    foreach (TreeNode node in treeNode.Nodes)
                    {
                        TreeNode foundNode = recSearchTreeNodeByTag(node, tag, treeView);
                        if (foundNode != null)
                        {
                            return foundNode;
                        }
                    }
                }
            }
            return null;
        }
    }
}
