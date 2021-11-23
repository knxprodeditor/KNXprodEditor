using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace knxprod_ns
{
    public partial class DynamicTabControl : UserControl
    {
        public static DynamicTabControl mDynamicTabControl;

        public DynamicTabControl()
        {
            InitializeComponent();
            mDynamicTabControl = this;
            tabApplication.Controls.Add(new ApplicationGui());
            tabParameterRefRef.Controls.Add(new ParameterRefRefGui());
            tabParameterRef.Controls.Add(new ParameterRefGui());
            tabParameter.Controls.Add(new AppParameterGui());
            tabUnionParameter.Controls.Add(new UnionParameterGui());
            tabApplicationChannel.Controls.Add(new ApplicationChannelGui());
            tabComObject.Controls.Add(new ComObjectGui());
            tabComObjectRef.Controls.Add(new ComObjectRefGui());
            tabComObjectRefRef.Controls.Add(new ComObjectRefRefGui());
            tabChannelChoose.Controls.Add(new ChannelChooseGui());
            tabComObjectParameterBlock.Controls.Add(new ComObjectParameterBlockGui());
            tabComObjectParameterChoose.Controls.Add(new ComObjectParameterChooseGui());
            tabAssign.Controls.Add(new AssignGui());
            tabChannelChooseWhen.Controls.Add(new ChannelChooseWhenGui());
            tabComObjectParameterChooseWhen.Controls.Add(new ComObjectParameterChooseWhenGui());
            tabParameterSeparator.Controls.Add(new ParameterSeparatorGui());
        }

        public static void RemoveAllParamsCoTabs()
        {
            while (mDynamicTabControl.tabControlParamsKO.TabPages.Count > 0)
            {
                    mDynamicTabControl.tabControlParamsKO.TabPages.Clear();
            }
        }

        public static void RemoveTabAtIndex(int index)
        {
            mDynamicTabControl.tabControlParamsKO.TabPages.RemoveAt(index);
        }

        /***********************************************************************************************************************************/
        // Application Tab
        public static void AddApplicationTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabApplication))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabApplication);
            }
        }

        /***********************************************************************************************************************************/
        // ApplicationChannel Tab
        public static void AddApplicationChannelTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabApplicationChannel))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabApplicationChannel);
            }
        }

        /***********************************************************************************************************************************/
        // IndependentChannel Tab
        public static void AddIndependentChannelTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabChannelIndependent))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabChannelIndependent);
            }
        }

        /***********************************************************************************************************************************/
        // Parameter Tab
        public static void AddParameterTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabParameter))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabParameter);
            }
        }

        public static void RemoveParameterTab()
        {
            if (mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabParameter))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Remove(mDynamicTabControl.tabParameter);
            }
        }

        public static void SelectParameterTab()
        {
            mDynamicTabControl.tabControlParamsKO.SelectTab(mDynamicTabControl.tabParameter);
        }

        /***********************************************************************************************************************************/
        // UnionParameter Tab
        public static void AddUnionParameterTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabUnionParameter))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabUnionParameter);
            }
        }

        public static void RemoveUnionParameterTab()
        {
            if (mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabUnionParameter))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Remove(mDynamicTabControl.tabUnionParameter);
            }
        }

        public static void SelectUnionParameterTab()
        {
            mDynamicTabControl.tabControlParamsKO.SelectTab(mDynamicTabControl.tabUnionParameter);
        }

        /***********************************************************************************************************************************/
        // ComObjectParameterChoose Tab
        public static void AddComObjectParameterChooseTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabComObjectParameterChoose))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabComObjectParameterChoose);
            }
        }

        /***********************************************************************************************************************************/
        // ParameterRef Tab
        public static void AddParameterRefTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabParameterRef))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabParameterRef);
            }
        }

        public static void RemoveParameterRefTab()
        {
            if (mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabParameterRef))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Remove(mDynamicTabControl.tabParameterRef);
            }
        }

        public static void SelectParameterRefTab()
        {
            if (mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabParameterRef))
            {
                mDynamicTabControl.tabControlParamsKO.SelectTab(mDynamicTabControl.tabParameterRef);
            }
        }

        /***********************************************************************************************************************************/
        // ChannelChoose Tab
        public static void AddChannelChooseTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabChannelChoose))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabChannelChoose);
            }
        }

        /***********************************************************************************************************************************/
        // ComObjectParameterBlock Tab
        public static void AddComObjectParameterBlockTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabComObjectParameterBlock))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabComObjectParameterBlock);
            }
        }

        /***********************************************************************************************************************************/
        // ParameterRefRef Tab
        public static void AddParameterRefRefTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabParameterRefRef))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabParameterRefRef);
            }
        }

        /***********************************************************************************************************************************/
        // ChannelChooseWhen Tab
        public static void AddChannelChooseWhenTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabChannelChooseWhen))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabChannelChooseWhen);
            }
        }

        /***********************************************************************************************************************************/
        // ComObject Tab
        public static void AddComObjectTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabComObject))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabComObject);
            }
        }

        public static void RemoveComObjectTab()
        {
            if (mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabComObject))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Remove(mDynamicTabControl.tabComObject);
            }
        }

        public static void SelectComObjectTab()
        {
            if (mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabComObject))
            {
                mDynamicTabControl.tabControlParamsKO.SelectTab(mDynamicTabControl.tabComObject);
            }
        }

        /***********************************************************************************************************************************/
        // ComObjectRef Tab
        public static void AddComObjectRefTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabComObjectRef))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabComObjectRef);
            }
        }

        public static void SelectComObjectRefTab()
        {
            if (mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabComObjectRef))
            {
                mDynamicTabControl.tabControlParamsKO.SelectTab(mDynamicTabControl.tabComObjectRef);
            }
        }

        public static void AddComObjectRefRefTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabComObjectRefRef))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabComObjectRefRef);
            }
        }

        /***********************************************************************************************************************************/
        // ComObjectParameterChooseWhen Tab
        public static void AddComObjectParameterChooseWhenTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabComObjectParameterChooseWhen))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabComObjectParameterChooseWhen);
            }
        }

        /***********************************************************************************************************************************/
        // ParameterSeparator Tab
        public static void AddParameterSeparatorTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabParameterSeparator))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabParameterSeparator);
            }
        }

        /***********************************************************************************************************************************/
        // ChannelIndependent Tab
        public static void AddChannelIndependentTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabChannelIndependent))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabChannelIndependent);
            }
        }

        /***********************************************************************************************************************************/
        // Assign Tab
        public static void AddAssignTab()
        {
            if (!mDynamicTabControl.tabControlParamsKO.TabPages.Contains(mDynamicTabControl.tabAssign))
            {
                mDynamicTabControl.tabControlParamsKO.TabPages.Add(mDynamicTabControl.tabAssign);
            }
        }
    }
}
