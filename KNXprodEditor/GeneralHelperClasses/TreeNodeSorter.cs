﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Quelle: https://stackoverflow.com/questions/50922465/how-to-sort-the-child-nodes-of-treeview

namespace knxprod_ns
{
    public class TreeNodeSorter : System.Collections.IComparer
    {
        public TreeNodeSorter() { }

        public int Compare(object x, object y)
        {
            TreeNode tx = x as TreeNode;
            TreeNode ty = y as TreeNode;

            string s1 = tx.Text;
            while (s1.Length > 0 && Char.IsDigit(s1.Last())) s1 = s1.TrimEnd(s1.Last());
            s1 = s1 + tx.Text.Substring(s1.Length).PadLeft(12, '0');

            string s2 = tx.Text;
            while (s2.Length > 0 && Char.IsDigit(s2.Last())) s2 = s2.TrimEnd(s2.Last());
            s2 = s2 + ty.Text.Substring(s2.Length).PadLeft(12, '0');

            return string.Compare(s1, s2);
        }
    }
}
