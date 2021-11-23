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
    public partial class LoadProceduresGui : UserControl
    {
        private static LoadProceduresGui mLoadProceduresGui;

        public LoadProceduresGui()
        {
            InitializeComponent();
            mLoadProceduresGui = this;
        }

        public static void InitializeLoadProceduresTreeView()
        {
            mLoadProceduresGui.treeViewLoadProcedures.Nodes.Clear();
            if (ApplicationProgramGui.selectedApplicationProgram.Static.LoadProcedures != null)
            {
                foreach (LoadProcedures_tLoadProcedure loadProcedure in ApplicationProgramGui.selectedApplicationProgram.Static.LoadProcedures)
                {
                    TreeNode parentNode = mLoadProceduresGui.treeViewLoadProcedures.Nodes.Add("LoadProcedure");
                    parentNode.Tag = loadProcedure;

                    if (loadProcedure.Items != null)
                    {
                        foreach (object item in loadProcedure.Items)
                        {
                            mLoadProceduresGui.ReadLdCtrlObjects(parentNode, item);
                        }
                    }
                }
                if (mLoadProceduresGui.treeViewLoadProcedures.Nodes[0] != null)
                {
                    mLoadProceduresGui.treeViewLoadProcedures.Nodes[0].Expand();
                }
            }
        }

        private void ReadLdCtrlObjects(TreeNode parentNode, object item)
        {
            if (item is LdCtrlAbsSegment_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlAbsSegment");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlAbsSegment_t), childNode);
            }
            else if (item is LdCtrlClearCachedObjectTypes_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlClearCachedObjectTypes");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlClearCachedObjectTypes_t), childNode);
            }
            else if (item is LdCtrlClearLCFilterTable_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlClearLCFilterTable");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlClearLCFilterTable_t), childNode);
            }
            else if (item is LdCtrlCompareMem_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlCompareMem");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlCompareMem_t), childNode);
            }
            else if (item is LdCtrlCompareProp_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlCompareProp");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlCompareProp_t), childNode);
            }
            else if (item is LdCtrlCompareRelMem_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlCompareRelMem");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlCompareRelMem_t), childNode);
            }
            else if (item is LdCtrlConnect_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlConnect");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlConnect_t), childNode);
            }
            else if (item is LdCtrlDeclarePropDesc_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlDeclarePropDesc");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlDeclarePropDesc_t), childNode);
            }
            else if (item is LdCtrlDelay_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlDelay");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlDelay_t), childNode);
            }
            else if (item is LdCtrlDisconnect_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlDisconnect");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlDisconnect_t), childNode);
            }
            else if (item is LdCtrlInvokeFunctionProp_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlInvokeFunctionProp");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlInvokeFunctionProp_t), childNode);
            }
            else if (item is LdCtrlLoad_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlLoad");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlLoad_t), childNode);
            }
            else if (item is LdCtrlLoadCompleted_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlLoadCompleted");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlLoadCompleted_t), childNode);
            }
            else if (item is LdCtrlLoadImageMem_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlLoadImageMem");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlLoadImageMem_t), childNode);
            }
            else if (item is LdCtrlLoadImageProp_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlLoadImageProp");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlLoadImageProp_t), childNode);
            }
            else if (item is LdCtrlLoadImageRelMem_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlLoadImageRelMem");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlLoadImageRelMem_t), childNode);
            }
            else if (item is LdCtrlMapError_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlMapError");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlMapError_t), childNode);
            }
            else if (item is LdCtrlMasterReset_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlMasterReset");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlMasterReset_t), childNode);
            }
            else if (item is LdCtrlMaxLength_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlMaxLength");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlMaxLength_t), childNode);
            }
            else if (item is LdCtrlMerge_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlMerge");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlMerge_t), childNode);
            }
            else if (item is LdCtrlProgressText_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlProgressText");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlProgressText_t), childNode);
            }
            else if (item is LdCtrlReadFunctionProp_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlReadFunctionProp");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlReadFunctionProp_t), childNode);
            }
            else if (item is LdCtrlRelSegment_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlRelSegment");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlRelSegment_t), childNode);
            }
            else if (item is LdCtrlRestart_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlRestart");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlRestart_t), childNode);
            }
            else if (item is LdCtrlSetControlVariable_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlSetControlVariable");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlSetControlVariable_t), childNode);
            }
            else if (item is LdCtrlTaskCtrl1_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlTaskCtrl1");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlTaskCtrl1_t), childNode);
            }
            else if (item is LdCtrlTaskCtrl2_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlTaskCtrl2");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlTaskCtrl2_t), childNode);
            }
            else if (item is LdCtrlTaskPtr_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlTaskPtr");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlTaskPtr_t), childNode);
            }
            else if (item is LdCtrlTaskSegment_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlTaskSegment");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlTaskSegment_t), childNode);
            }
            else if (item is LdCtrlUnload_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlUnload");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlUnload_t), childNode);
            }
            else if (item is LdCtrlWriteMem_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlWriteMem");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlWriteMem_t), childNode);
            }
            else if (item is LdCtrlWriteProp_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlWriteProp");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlWriteProp_t), childNode);
            }
            else if (item is LdCtrlWriteRelMem_t)
            {
                TreeNode childNode = parentNode.Nodes.Add("LdCtrlWriteRelMem");
                childNode.Tag = item;
                FillTreeViewLoadProcedureOnError((item as LdCtrlWriteRelMem_t), childNode);
            }
            else if (item is LdCtrlBaseChoose_t)
            {
                ReadLdCtrlBaseChoose_t(parentNode, item as LdCtrlBaseChoose_t);
            }
        }

        private void ReadLdCtrlBaseChoose_t(TreeNode parentNode, LdCtrlBaseChoose_t ldCtrlBaseChoose)
        {
            TreeNode childNode = parentNode.Nodes.Add("LdCtrlBaseChoose");
            childNode.Tag = ldCtrlBaseChoose;
            foreach (LdCtrlBaseChoose_tWhen whenItem in ldCtrlBaseChoose.when)
            {
                ReadLdCtrlBaseChoose_tWhen(childNode, whenItem);
            }
        }

        private void ReadLdCtrlBaseChoose_tWhen(TreeNode parentNode, LdCtrlBaseChoose_tWhen ldCtrlBaseChooseWhen)
        {
            TreeNode childNode = parentNode.Nodes.Add("LdCtrlBaseChooseWhen");
            childNode.Tag = ldCtrlBaseChooseWhen;
            foreach (object item in ldCtrlBaseChooseWhen.Items)
            {
                ReadLdCtrlObjects(childNode, item);
            }
        }

        private void FillTreeViewLoadProcedureOnError(LdCtrlBase_t ldCtrlBase, TreeNode parentNode)
        {
            if (ldCtrlBase.OnError != null)
            {
                foreach (LdCtrlBase_tOnError onError in ldCtrlBase.OnError)
                {
                    TreeNode childNode = parentNode.Nodes.Add("on Error cause: " + onError.Cause);
                }
            }
        }

        private void treeViewLoadProcedures_AfterSelect(object sender, TreeViewEventArgs e)
        {
            object selectedItem = treeViewLoadProcedures.SelectedNode.Tag;
            RemoveAllLoadProceduresTabs();

            if (selectedItem is LoadProcedures_tLoadProcedure)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLoadProcedure);
                checkBoxLoadProcedureMergeIdSpecified.Checked = (selectedItem as LoadProcedures_tLoadProcedure).MergeIdSpecified;
                numericLoadProcedureMergeId.Value = (selectedItem as LoadProcedures_tLoadProcedure).MergeId;
            }
            else if (selectedItem is LdCtrlAbsSegment_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlAbsSegment);
                checkBoxLdCtrlAbsSegmentLsmIdxSpecified.Checked = (selectedItem as LdCtrlAbsSegment_t).LsmIdxSpecified;
                numericLdCtrlAbsSegmentLsmIdx.Value = (selectedItem as LdCtrlAbsSegment_t).LsmIdx;
                checkBoxLdCtrlAbsSegmentObjTypeSpecified.Checked = (selectedItem as LdCtrlAbsSegment_t).ObjTypeSpecified;
                numericLdCtrlAbsSegmentObjType.Value = (selectedItem as LdCtrlAbsSegment_t).ObjType;
                numericLdCtrlAbsSegmentOccurence.Value = (selectedItem as LdCtrlAbsSegment_t).Occurrence;
                numericLdCtrlAbsSegmentSegType.Value = (selectedItem as LdCtrlAbsSegment_t).SegType;
                numericLdCtrlAbsSegmentAddress.Value = (selectedItem as LdCtrlAbsSegment_t).Address;
                numericLdCtrlAbsSegmentSize.Value = (selectedItem as LdCtrlAbsSegment_t).Size;
                numericLdCtrlAbsSegmentAccess.Value = (selectedItem as LdCtrlAbsSegment_t).Access;
                numericLdCtrlAbsSegmentMemType.Value = (selectedItem as LdCtrlAbsSegment_t).MemType;
                numericLdCtrlAbsSegmentSegFlags.Value = (selectedItem as LdCtrlAbsSegment_t).SegFlags;
                comboBoxLdCtrlAbsSegmentAppliesTo.SelectedItem = (selectedItem as LdCtrlAbsSegment_t).AppliesTo.ToString();
                textBoxLdCtrlAbsSegmentInternalDescription.Text = (selectedItem as LdCtrlAbsSegment_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlClearCachedObjectTypes_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlClearCachedObjectTypes);
                comboBoxLdCtrlClearCachedObjectTypesAppliesTo.SelectedItem = (selectedItem as LdCtrlClearCachedObjectTypes_t).AppliesTo.ToString();
                textBoxLdCtrlClearCachedObjectTypesInternalDescription.Text = (selectedItem as LdCtrlClearCachedObjectTypes_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlClearLCFilterTable_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlClearLCFilterTable);
                checkBoxLcCtrlClearLcFilterTableUseFunctionProp.Checked = (selectedItem as LdCtrlClearLCFilterTable_t).UseFunctionProp;
                comboBoxLcCtrlClearLcFilterTableAppliesTo.SelectedItem = (selectedItem as LdCtrlClearLCFilterTable_t).AppliesTo.ToString();
                textBoxLcCtrlClearLcFilterTableInternalDescription.Text = (selectedItem as LdCtrlClearLCFilterTable_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlCompareMem_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlCompareMem);
                comboBoxLdCtrlCompareMemAddressSpace.SelectedItem = (selectedItem as LdCtrlCompareMem_t).AddressSpace.ToString();
                numericLdCtrlCompareMemAddress.Value = (selectedItem as LdCtrlCompareMem_t).Address;
                numericLdCtrlCompareMemSize.Value = (selectedItem as LdCtrlCompareMem_t).Size;
                checkBoxLdCtrlCompareMemAllowCachedValue.Checked = (selectedItem as LdCtrlCompareMem_t).AllowCachedValue;
                HandleKnxDataTypes.ReadKNXType(listBoxLdCtrlCompareMemInlineData, (selectedItem as LdCtrlCompareMem_t).InlineData);
                HandleKnxDataTypes.ReadKNXType(listBoxLdCtrlCompareMemMask, (selectedItem as LdCtrlCompareMem_t).Mask);
                textBoxLdCtrlCompareMemRange.Text = (selectedItem as LdCtrlCompareMem_t).Range;
                checkBoxLdCtrlCompareMemInvert.Checked = (selectedItem as LdCtrlCompareMem_t).Invert;
                numericLdCtrlCompareMemRetryInterval.Value = (selectedItem as LdCtrlCompareMem_t).RetryInterval;
                numericLdCtrlCompareMemTimeOut.Value = (selectedItem as LdCtrlCompareMem_t).TimeOut;
                comboBoxLdCtrlCompareMemAppliesTo.SelectedItem = (selectedItem as LdCtrlCompareMem_t).AppliesTo.ToString();
                textBoxLdCtrlCompareMemInternalDescription.Text = (selectedItem as LdCtrlCompareMem_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlCompareProp_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlCompareProp);
                checkBoxLdCtrlComparePropObjIdxSpecified.Checked = (selectedItem as LdCtrlCompareProp_t).ObjIdxSpecified;
                numericLdCtrlComparePropObjIdx.Value = (selectedItem as LdCtrlCompareProp_t).ObjIdx;
                checkBoxLdCtrlComparePropObjTypeSpecified.Checked = (selectedItem as LdCtrlCompareProp_t).ObjTypeSpecified;
                numericLdCtrlComparePropObjType.Value = (selectedItem as LdCtrlCompareProp_t).ObjType;
                numericLdCtrlComparePropOccurence.Value = (selectedItem as LdCtrlCompareProp_t).Occurrence;
                numericLdCtrlComparePropPropId.Value = (selectedItem as LdCtrlCompareProp_t).PropId;
                numericLdCtrlComparePropStartElement.Value = (selectedItem as LdCtrlCompareProp_t).StartElement;
                numericLdCtrlComparePropCount.Value = (selectedItem as LdCtrlCompareProp_t).Count;
                checkBoxLdCtrlComparePropAllowCachedValue.Checked = (selectedItem as LdCtrlCompareProp_t).AllowCachedValue;
                HandleKnxDataTypes.ReadKNXType(listBoxLdCtrlComparePropInlineData, (selectedItem as LdCtrlCompareProp_t).InlineData);
                HandleKnxDataTypes.ReadKNXType(listBoxLdCtrlComparePropMask, (selectedItem as LdCtrlCompareProp_t).Mask);
                textBoxLdCtrlComparePropRange.Text = (selectedItem as LdCtrlCompareProp_t).Range;
                checkBoxLdCtrlComparePropInvert.Checked = (selectedItem as LdCtrlCompareProp_t).Invert;
                numericLdCtrlComparePropRetryInterval.Value = (selectedItem as LdCtrlCompareProp_t).RetryInterval;
                numericLdCtrlComparePropTimeOut.Value = (selectedItem as LdCtrlCompareProp_t).TimeOut;
                comboBoxLdCtrlComparePropAppliesTo.SelectedItem = (selectedItem as LdCtrlCompareProp_t).AppliesTo.ToString();
                textBoxLdCtrlComparePropInternalDescription.Text = (selectedItem as LdCtrlCompareProp_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlCompareRelMem_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlCompareRelMem);
                checkBoxLdCtrlCompareRelMemObjIdxSpecified.Checked = (selectedItem as LdCtrlCompareRelMem_t).ObjIdxSpecified;
                numericLdCtrlCompareRelMemObjIdx.Value = (selectedItem as LdCtrlCompareRelMem_t).ObjIdx;
                checkBoxLdCtrlCompareRelMemObjTypeSpecified.Checked = (selectedItem as LdCtrlCompareRelMem_t).ObjTypeSpecified;
                numericLdCtrlCompareRelMemObjType.Value = (selectedItem as LdCtrlCompareRelMem_t).ObjType;
                numericLdCtrlCompareRelMemOccurence.Value = (selectedItem as LdCtrlCompareRelMem_t).Occurrence;
                numericLdCtrlCompareRelMemOffset.Value = (selectedItem as LdCtrlCompareRelMem_t).Offset;
                numericLdCtrlCompareRelMemSize.Value = (selectedItem as LdCtrlCompareRelMem_t).Size;
                checkBoxLdCtrlCompareRelMemAllowChachedValue.Checked = (selectedItem as LdCtrlCompareRelMem_t).AllowCachedValue;
                HandleKnxDataTypes.ReadKNXType(listBoxLdCtrlCompareRelMemInlineData, (selectedItem as LdCtrlCompareRelMem_t).InlineData);
                HandleKnxDataTypes.ReadKNXType(listBoxLdCtrlCompareRelMemMask, (selectedItem as LdCtrlCompareRelMem_t).Mask);
                textBoxLdCtrlCompareRelMemRange.Text = (selectedItem as LdCtrlCompareRelMem_t).Range;
                checkBoxLdCtrlCompareRelMemInvert.Checked = (selectedItem as LdCtrlCompareRelMem_t).Invert;
                numericLdCtrlCompareRelMemRetryInterval.Value = (selectedItem as LdCtrlCompareRelMem_t).RetryInterval;
                numericLdCtrlCompareRelMemTimeOut.Value = (selectedItem as LdCtrlCompareRelMem_t).TimeOut;
                comboBoxLdCtrlCompareRelMemAppliesTo.SelectedItem = (selectedItem as LdCtrlCompareRelMem_t).AppliesTo.ToString();
                textBoxLdCtrlCompareRelMemInternalDescription.Text = (selectedItem as LdCtrlCompareRelMem_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlConnect_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlConnect);
                comboBoxLdCtrlConnectAppliesTo.SelectedItem = (selectedItem as LdCtrlConnect_t).AppliesTo.ToString();
                textBoxLdCtrlConnectInternalDescription.Text = (selectedItem as LdCtrlConnect_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlDeclarePropDesc_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlDeclarePropDesc);
                checkBoxLdCtrlDeclarePropDescObjIdxSpecified.Checked = (selectedItem as LdCtrlDeclarePropDesc_t).ObjIdxSpecified;
                numericLdCtrlDeclarePropDescObjIdx.Value = (selectedItem as LdCtrlDeclarePropDesc_t).ObjIdx;
                checkBoxLdCtrlDeclarePropDescObjTypeSpecified.Checked = (selectedItem as LdCtrlDeclarePropDesc_t).ObjTypeSpecified;
                numericLdCtrlDeclarePropDescObjType.Value = (selectedItem as LdCtrlDeclarePropDesc_t).ObjType;
                numericLdCtrlDeclarePropDescOccurence.Value = (selectedItem as LdCtrlDeclarePropDesc_t).Occurrence;
                numericLdCtrlDeclarePropDescPropId.Value = (selectedItem as LdCtrlDeclarePropDesc_t).PropId;
                numericLdCtrlDeclarePropDescMaxElements.Value = (selectedItem as LdCtrlDeclarePropDesc_t).MaxElements;
                numericLdCtrlDeclarePropDescReadAccess.Value = (selectedItem as LdCtrlDeclarePropDesc_t).ReadAccess;
                numericLdCtrlDeclarePropDescWriteAccess.Value = (selectedItem as LdCtrlDeclarePropDesc_t).WriteAccess;
                checkBoxLdCtrlDeclarePropDescWritable.Checked = (selectedItem as LdCtrlDeclarePropDesc_t).Writable;
                comboBoxLdCtrlDeclarePropDescAppliesTo.SelectedItem = (selectedItem as LdCtrlDeclarePropDesc_t).AppliesTo.ToString();
                textBoxLdCtrlDeclarePropDescInternalDescription.Text = (selectedItem as LdCtrlDeclarePropDesc_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlDelay_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlDelay);
                numericLdCtrlDelayMilliSeconds.Value = (selectedItem as LdCtrlDelay_t).MilliSeconds;
                comboBoxLdCtrlDelayAppliesTo.SelectedItem = (selectedItem as LdCtrlDelay_t).AppliesTo.ToString();
                textBoxLdCtrlDelayInternalDescription.Text = (selectedItem as LdCtrlDelay_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlDisconnect_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlDisconnect);
                comboBoxLdCtrlDisconnectAppliesTo.SelectedItem = (selectedItem as LdCtrlDisconnect_t).AppliesTo.ToString();
                textBoxLdCtrlDisconnectInternalDescription.Text = (selectedItem as LdCtrlDisconnect_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlInvokeFunctionProp_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlInvokeFunctionProp);
                checkBoxLdCtrlInvokeFunctionPropObjIdxSpecified.Checked = (selectedItem as LdCtrlInvokeFunctionProp_t).ObjIdxSpecified;
                numericLdCtrlInvokeFunctionPropObjIdx.Value = (selectedItem as LdCtrlInvokeFunctionProp_t).ObjIdx;
                checkBoxLdCtrlInvokeFunctionPropObjTypeSpecified.Checked = (selectedItem as LdCtrlInvokeFunctionProp_t).ObjTypeSpecified;
                numericLdCtrlInvokeFunctionPropObjType.Value = (selectedItem as LdCtrlInvokeFunctionProp_t).ObjType;
                numericLdCtrlInvokeFunctionPropOccurence.Value = (selectedItem as LdCtrlInvokeFunctionProp_t).Occurrence;
                numericLdCtrlInvokeFunctionPropPropId.Value = (selectedItem as LdCtrlInvokeFunctionProp_t).PropId;
                HandleKnxDataTypes.ReadKNXType(listBoxLdCtrlInvokeFunctionPropInlineData, (selectedItem as LdCtrlInvokeFunctionProp_t).InlineData);
                comboBoxLdCtrlInvokeFunctionPropAppliesTo.SelectedItem = (selectedItem as LdCtrlInvokeFunctionProp_t).AppliesTo.ToString();
                textBoxLdCtrlInvokeFunctionPropInternalDescription.Text = (selectedItem as LdCtrlInvokeFunctionProp_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlLoad_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlLoad);
                checkBoxLdCtrlLoadLsmIdxSpecified.Checked = (selectedItem as LdCtrlLoad_t).LsmIdxSpecified;
                numericLdCtrlLoadLsmIdx.Value = (selectedItem as LdCtrlLoad_t).LsmIdx;
                checkBoxLdCtrlLoadObjTypeSpecified.Checked = (selectedItem as LdCtrlLoad_t).ObjTypeSpecified;
                numericLdCtrlLoadObjType.Value = (selectedItem as LdCtrlLoad_t).ObjType;
                numericLdCtrlLoadOccurence.Value = (selectedItem as LdCtrlLoad_t).Occurrence;
                comboBoxLdCtrlLoadAppliesTo.SelectedItem = (selectedItem as LdCtrlLoad_t).AppliesTo.ToString();
                textBoxLdCtrlLoadInternalDescription.Text = (selectedItem as LdCtrlLoad_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlLoadCompleted_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlLoadCompleted);
                checkBoxLdCtrlLoadCompletedLsmIdxSpecified.Checked = (selectedItem as LdCtrlLoadCompleted_t).LsmIdxSpecified;
                numericLdCtrlLoadCompletedLsmIdx.Value = (selectedItem as LdCtrlLoadCompleted_t).LsmIdx;
                checkBoxLdCtrlLoadCompletedObjTypeSpecified.Checked = (selectedItem as LdCtrlLoadCompleted_t).ObjTypeSpecified;
                numericLdCtrlLoadCompletedObjType.Value = (selectedItem as LdCtrlLoadCompleted_t).ObjType;
                numericLdCtrlLoadCompletedOccurence.Value = (selectedItem as LdCtrlLoadCompleted_t).Occurrence;
                comboBoxLdCtrlLoadCompletedAppliesTo.SelectedItem = (selectedItem as LdCtrlLoadCompleted_t).AppliesTo.ToString();
                textBoxLdCtrlLoadCompletedInternalDescription.Text = (selectedItem as LdCtrlLoadCompleted_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlLoadImageMem_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlLoadImageMem);
                comboBoxLdCtrlLoadImageMemAddressSpace.SelectedItem = (selectedItem as LdCtrlLoadImageMem_t).AddressSpace.ToString();
                numericLdCtrlLoadImageMemAddress.Value = (selectedItem as LdCtrlLoadImageMem_t).Address;
                numericLdCtrlLoadImageMemSize.Value = (selectedItem as LdCtrlLoadImageMem_t).Size;
                comboBoxLdCtrlLoadImageMemAppliesTo.SelectedItem = (selectedItem as LdCtrlLoadImageMem_t).AppliesTo.ToString();
                textBoxLdCtrlLoadImageMemInternalDescription.Text = (selectedItem as LdCtrlLoadImageMem_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlLoadImageProp_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlLoadImageProp);
                checkBoxLdCtrlLoadImagePropObjIdxSpecified.Checked = (selectedItem as LdCtrlLoadImageProp_t).ObjIdxSpecified;
                numericLdCtrlLoadImagePropObjIdx.Value = (selectedItem as LdCtrlLoadImageProp_t).ObjIdx;
                checkBoxLdCtrlLoadImagePropObjTypeSpecified.Checked = (selectedItem as LdCtrlLoadImageProp_t).ObjTypeSpecified;
                numericLdCtrlLoadImagePropObjType.Value = (selectedItem as LdCtrlLoadImageProp_t).ObjType;
                numericLdCtrlLoadImagePropOccurence.Value = (selectedItem as LdCtrlLoadImageProp_t).Occurrence;
                numericLdCtrlLoadImagePropPropId.Value = (selectedItem as LdCtrlLoadImageProp_t).PropId;
                numericLdCtrlLoadImagePropStartElement.Value = (selectedItem as LdCtrlLoadImageProp_t).StartElement;
                numericLdCtrlLoadImagePropCount.Value = (selectedItem as LdCtrlLoadImageProp_t).Count;
                comboBoxLdCtrlLoadImagePropAppliesTo.SelectedItem = (selectedItem as LdCtrlLoadImageProp_t).AppliesTo.ToString();
                textBoxLdCtrlLoadImagePropInternalDescription.Text = (selectedItem as LdCtrlLoadImageProp_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlLoadImageRelMem_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlLoadImageRelMem);
                checkBoxLdCtrlLoadImageRelMemObjIdySpecified.Checked = (selectedItem as LdCtrlLoadImageRelMem_t).ObjIdxSpecified;
                numericLdCtrlLoadImageRelMemObjIdx.Value = (selectedItem as LdCtrlLoadImageRelMem_t).ObjIdx;
                checkBoxLdCtrlLoadImageRelMemObjTypeSpecified.Checked = (selectedItem as LdCtrlLoadImageRelMem_t).ObjTypeSpecified;
                numericLdCtrlLoadImageRelMemObjType.Value = (selectedItem as LdCtrlLoadImageRelMem_t).ObjType;
                numericLdCtrlLoadImageRelMemOccurence.Value = (selectedItem as LdCtrlLoadImageRelMem_t).Occurrence;
                numericLdCtrlLoadImageRelMemOffset.Value = (selectedItem as LdCtrlLoadImageRelMem_t).Offset;
                numericLdCtrlLoadImageRelMemSize.Value = (selectedItem as LdCtrlLoadImageRelMem_t).Size;
                comboBoxLdCtrlLoadImageRelMemAppliesTo.SelectedItem = (selectedItem as LdCtrlLoadImageRelMem_t).AppliesTo.ToString();
                textBoxLdCtrlLoadImageRelMemInternalDescription.Text = (selectedItem as LdCtrlLoadImageRelMem_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlMapError_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlMapError);
                numericLdCtrlMapErrorLdCtrlFilter.Value = (selectedItem as LdCtrlMapError_t).LdCtrlFilter;
                numericLdCtrlMapErrorOriginalError.Value = (selectedItem as LdCtrlMapError_t).OriginalError;
                numericLdCtrlMapErrorMappedError.Value = (selectedItem as LdCtrlMapError_t).MappedError;
                comboBoxLdCtrlMapErrorAppliesTo.SelectedItem = (selectedItem as LdCtrlMapError_t).AppliesTo.ToString();
                textBoxLdCtrlMapErrorInternalDescription.Text = (selectedItem as LdCtrlMapError_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlMasterReset_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlMasterReset);
                numericLdCtrlMasterResetEreaseCode.Value = (selectedItem as LdCtrlMasterReset_t).EraseCode;
                numericLdCtrlMasterResetChannelNumber.Value = (selectedItem as LdCtrlMasterReset_t).ChannelNumber;
                comboBoxLdCtrlMasterResetAppliesTo.SelectedItem = (selectedItem as LdCtrlMasterReset_t).AppliesTo.ToString();
                textBoxLdCtrlMasterResetInternalDescription.Text = (selectedItem as LdCtrlMasterReset_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlMaxLength_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlMaxLength);
                checkBoxLdCtrlMaxLengthIsmIdxSpecified.Checked = (selectedItem as LdCtrlMaxLength_t).LsmIdxSpecified;
                numericLdCtrlMaxLengthLsmIdx.Value = (selectedItem as LdCtrlMaxLength_t).LsmIdx;
                checkBoxLdCtrlMaxLengthObjTypeSpecified.Checked = (selectedItem as LdCtrlMaxLength_t).ObjTypeSpecified;
                numericLdCtrlMaxLengthObjType.Value = (selectedItem as LdCtrlMaxLength_t).ObjType;
                numericLdCtrlMaxLengthOccurence.Value = (selectedItem as LdCtrlMaxLength_t).Occurrence;
                numericLdCtrlMaxLengthSize.Value = (selectedItem as LdCtrlMaxLength_t).Size;
                comboBoxLdCtrlMaxLengthAppliesTo.SelectedItem = (selectedItem as LdCtrlMaxLength_t).AppliesTo.ToString();
                textBoxLdCtrlMaxLengthInternalDescription.Text = (selectedItem as LdCtrlMaxLength_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlMerge_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlMerge);
                numericLdCtrlMergeMergeId.Value = (selectedItem as LdCtrlMerge_t).MergeId;
                comboBoxLdCtrlMergeAppliesTo.SelectedItem = (selectedItem as LdCtrlMerge_t).AppliesTo.ToString();
                textBoxLdCtrlMergeInternalDescription.Text = (selectedItem as LdCtrlMerge_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlProgressText_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlProgressText);
                checkBoxLdCtrlProgressTextTextIdSpecified.Checked = (selectedItem as LdCtrlProgressText_t).TextIdSpecified;
                numericLdCtrlProgressTextTextId.Value = (selectedItem as LdCtrlProgressText_t).TextId;
                textBoxLdCtrlProgressTextMessageRef.Text = (selectedItem as LdCtrlProgressText_t).MessageRef;
                comboBoxLdCtrlProgressTextAppliesTo.SelectedItem = (selectedItem as LdCtrlProgressText_t).AppliesTo.ToString();
                textBoxLdCtrlProgressTextInternalDescription.Text = (selectedItem as LdCtrlProgressText_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlReadFunctionProp_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlReadFunctionProp);
                checkBoxLdCtrlReadFunctionPropObjIdxSpecified.Checked = (selectedItem as LdCtrlReadFunctionProp_t).ObjIdxSpecified;
                numericLdCtrlReadFunctionPropObjIdx.Value = (selectedItem as LdCtrlReadFunctionProp_t).ObjIdx;
                checkBoxLdCtrlReadFunctionPropObjTypeSpecified.Checked = (selectedItem as LdCtrlReadFunctionProp_t).ObjTypeSpecified;
                numericLdCtrlReadFunctionPropObjType.Value = (selectedItem as LdCtrlReadFunctionProp_t).ObjType;
                numericLdCtrlReadFunctionPropOccurence.Value = (selectedItem as LdCtrlReadFunctionProp_t).Occurrence;
                numericLdCtrlReadFunctionPropPropId.Value = (selectedItem as LdCtrlReadFunctionProp_t).PropId;
                comboBoxLdCtrlReadFunctionPropAppliesTo.SelectedItem = (selectedItem as LdCtrlReadFunctionProp_t).AppliesTo.ToString();
                textBoxLdCtrlReadFunctionPropInternalDescription.Text = (selectedItem as LdCtrlReadFunctionProp_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlRelSegment_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlRelSegment);
                checkBoxLdCtrlRelSegmentLsmIdxSpecified.Checked = (selectedItem as LdCtrlRelSegment_t).LsmIdxSpecified;
                numericLdCtrlRelSegmentLsmIdx.Value = (selectedItem as LdCtrlRelSegment_t).LsmIdx;
                checkBoxLdCtrlRelSegmentObjTypeSpecified.Checked = (selectedItem as LdCtrlRelSegment_t).ObjTypeSpecified;
                numericLdCtrlRelSegmentObjType.Value = (selectedItem as LdCtrlRelSegment_t).ObjType;
                numericLdCtrlRelSegmentOccurence.Value = (selectedItem as LdCtrlRelSegment_t).Occurrence;
                numericLdCtrlRelSegmentSize.Value = (selectedItem as LdCtrlRelSegment_t).Size;
                numericLdCtrlRelSegmentMode.Value = (selectedItem as LdCtrlRelSegment_t).Mode;
                numericLdCtrlRelSegmentFill.Value = (selectedItem as LdCtrlRelSegment_t).Fill;
                comboBoxLdCtrlRelSegmentAppliesTo.SelectedItem = (selectedItem as LdCtrlRelSegment_t).AppliesTo.ToString();
                textBoxLdCtrlRelSegmentInternalDescription.Text = (selectedItem as LdCtrlRelSegment_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlRestart_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlRestart);
                comboBoxLdCtrlRestartAppliesTo.SelectedItem = (selectedItem as LdCtrlRestart_t).AppliesTo.ToString();
                textBoxLdCtrlRestartInternalDescription.Text = (selectedItem as LdCtrlRestart_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlSetControlVariable_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlSetControlVariable);
                comboBoxLdCtrlSetControlVariableName.SelectedItem = (selectedItem as LdCtrlSetControlVariable_t).Name.ToString();
                checkBoxLdCtrlSetControlVariableValue.Checked = (selectedItem as LdCtrlSetControlVariable_t).Value;
                comboBoxLdCtrlSetControlVariableAppliesTo.SelectedItem = (selectedItem as LdCtrlSetControlVariable_t).AppliesTo.ToString();
                textBoxLdCtrlSetControlVariableInternalDescription.Text = (selectedItem as LdCtrlSetControlVariable_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlTaskCtrl1_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlTaskCtrl1);
                checkBoxLdCtrlTaskCtrl1LsmIdxSpecified.Checked = (selectedItem as LdCtrlTaskCtrl1_t).LsmIdxSpecified;
                numericLdCtrlTaskCtrl1LsmIdx.Value = (selectedItem as LdCtrlTaskCtrl1_t).LsmIdx;
                checkBoxLdCtrlTaskCtrl1ObjTypeSpecified.Checked = (selectedItem as LdCtrlTaskCtrl1_t).ObjTypeSpecified;
                numericLdCtrlTaskCtrl1ObjType.Value = (selectedItem as LdCtrlTaskCtrl1_t).ObjType;
                numericLdCtrlTaskCtrl1Occurence.Value = (selectedItem as LdCtrlTaskCtrl1_t).Occurrence;
                numericLdCtrlTaskCtrl1Address.Value = (selectedItem as LdCtrlTaskCtrl1_t).Address;
                numericLdCtrlTaskCtrl1Count.Value = (selectedItem as LdCtrlTaskCtrl1_t).Count;
                comboBoxLdCtrlTaskCtrl1AppliesTo.SelectedItem = (selectedItem as LdCtrlTaskCtrl1_t).AppliesTo.ToString();
                textBoxLdCtrlTaskCtrl1InternalDescription.Text = (selectedItem as LdCtrlTaskCtrl1_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlTaskCtrl2_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlTaskCtrl2);
                checkBoxLdCtrlTaskCtrl2LsmIdxSpecified.Checked = (selectedItem as LdCtrlTaskCtrl2_t).LsmIdxSpecified;
                numericLdCtrlTaskCtrl2LsmIdx.Value = (selectedItem as LdCtrlTaskCtrl2_t).LsmIdx;
                checkBoxLdCtrlTaskCtrl2ObjTypeSpecified.Checked = (selectedItem as LdCtrlTaskCtrl2_t).ObjTypeSpecified;
                numericLdCtrlTaskCtrl2ObjType.Value = (selectedItem as LdCtrlTaskCtrl2_t).ObjType;
                numericLdCtrlTaskCtrl2Occurence.Value = (selectedItem as LdCtrlTaskCtrl2_t).Occurrence;
                numericLdCtrlTaskCtrl2Callback.Value = (selectedItem as LdCtrlTaskCtrl2_t).Callback;
                numericLdCtrlTaskCtrl2Address.Value = (selectedItem as LdCtrlTaskCtrl2_t).Address;
                numericLdCtrlTaskCtrl2Seg0.Value = (selectedItem as LdCtrlTaskCtrl2_t).Seg0;
                numericLdCtrlTaskCtrl2Seg1.Value = (selectedItem as LdCtrlTaskCtrl2_t).Seg1;
                comboBoxLdCtrlTaskCtrl2AppliesTo.SelectedItem = (selectedItem as LdCtrlTaskCtrl2_t).AppliesTo.ToString();
                textBoxLdCtrlTaskCtrl2InternalDescription.Text = (selectedItem as LdCtrlTaskCtrl2_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlTaskPtr_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlTaskPtr);
                checkBoxLdCtrlTaskPtrLsmIdxSpecified.Checked = (selectedItem as LdCtrlTaskPtr_t).LsmIdxSpecified;
                numericLdCtrlTaskPtrLsmIdx.Value = (selectedItem as LdCtrlTaskPtr_t).LsmIdx;
                checkBoxLdCtrlTaskPtrObjTypeSpecified.Checked = (selectedItem as LdCtrlTaskPtr_t).ObjTypeSpecified;
                numericLdCtrlTaskPtrObjType.Value = (selectedItem as LdCtrlTaskPtr_t).ObjType;
                numericLdCtrlTaskPtrOccurence.Value = (selectedItem as LdCtrlTaskPtr_t).Occurrence;
                numericLdCtrlTaskPtrInitPtr.Value = (selectedItem as LdCtrlTaskPtr_t).InitPtr;
                numericLdCtrlTaskPtrSavePtr.Value = (selectedItem as LdCtrlTaskPtr_t).SavePtr;
                numericLdCtrlTaskPtrSerialPtr.Value = (selectedItem as LdCtrlTaskPtr_t).SerialPtr;
                comboBoxLdCtrlTaskPtrAppliesTo.SelectedItem = (selectedItem as LdCtrlTaskPtr_t).AppliesTo.ToString();
                textBoxLdCtrlTaskPtrInternalDescription.Text = (selectedItem as LdCtrlTaskPtr_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlTaskSegment_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlTaskSegment);
                checkBoxLdCtrlTaskSegmentLsmIdxSpecified.Checked = (selectedItem as LdCtrlTaskSegment_t).LsmIdxSpecified;
                numericLdCtrlTaskSegmentLsmIdx.Value = (selectedItem as LdCtrlTaskSegment_t).LsmIdx;
                checkBoxLdCtrlTaskSegmentObjTypeSpecified.Checked = (selectedItem as LdCtrlTaskSegment_t).ObjTypeSpecified;
                numericLdCtrlTaskSegmentObjType.Value = (selectedItem as LdCtrlTaskSegment_t).ObjType;
                numericLdCtrlTaskSegmentOccurence.Value = (selectedItem as LdCtrlTaskSegment_t).Occurrence;
                numericLdCtrlTaskSegmentAddress.Value = (selectedItem as LdCtrlTaskSegment_t).Address;
                comboBoxLdCtrlTaskSegmentAppliesTo.SelectedItem = (selectedItem as LdCtrlTaskSegment_t).AppliesTo.ToString();
                textBoxLdCtrlTaskSegmentInternalDescription.Text = (selectedItem as LdCtrlTaskSegment_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlUnload_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlUnload);
                checkBoxLdCtrlUnloadLsmIdxSpecified.Checked = (selectedItem as LdCtrlUnload_t).LsmIdxSpecified;
                numericLdCtrlUnloadLsmIdx.Value = (selectedItem as LdCtrlUnload_t).LsmIdx;
                checkBoxLdCtrlUnloadObjTypeSpecified.Checked = (selectedItem as LdCtrlUnload_t).ObjTypeSpecified;
                numericLdCtrlUnloadObjType.Value = (selectedItem as LdCtrlUnload_t).ObjType;
                numericLdCtrlUnloadOccurence.Value = (selectedItem as LdCtrlUnload_t).Occurrence;
                comboBoxLdCtrlUnloadAppliesTo.SelectedItem = (selectedItem as LdCtrlUnload_t).AppliesTo.ToString();
                textBoxLdCtrlUnloadInternalDescription.Text = (selectedItem as LdCtrlUnload_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlWriteMem_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlWriteMem);
                comboBoxLdCtrlWriteMemAddressSpace.SelectedItem = (selectedItem as LdCtrlWriteMem_t).AddressSpace.ToString();
                numericLdCtrlWriteMemAddress.Value = (selectedItem as LdCtrlWriteMem_t).Address;
                numericLdCtrlWriteMemSize.Value = (selectedItem as LdCtrlWriteMem_t).Size;
                checkBoxLdCtrlWriteMemVerify.Checked = (selectedItem as LdCtrlWriteMem_t).Verify;
                HandleKnxDataTypes.ReadKNXType(listBoxLdCtrlWriteMemInlineData, (selectedItem as LdCtrlWriteMem_t).InlineData);
                comboBoxLdCtrlWriteMemAppliesTo.SelectedItem = (selectedItem as LdCtrlWriteMem_t).AppliesTo.ToString();
                textBoxLdCtrlWriteMemInternalDescription.Text = (selectedItem as LdCtrlWriteMem_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlWriteProp_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlWriteProp);
                checkBoxLdCtrlWritePropObjIdxSpecified.Checked = (selectedItem as LdCtrlWriteProp_t).ObjIdxSpecified;
                numericLdCtrlWritePropObjIdx.Value = (selectedItem as LdCtrlWriteProp_t).ObjIdx;
                checkBoxLdCtrlWritePropOjjTypeSpecified.Checked = (selectedItem as LdCtrlWriteProp_t).ObjTypeSpecified;
                numericLdCtrlWritePropObjType.Value = (selectedItem as LdCtrlWriteProp_t).ObjType;
                numericLdCtrlWritePropOccurence.Value = (selectedItem as LdCtrlWriteProp_t).Occurrence;
                numericLdCtrlWritePropPropId.Value = (selectedItem as LdCtrlWriteProp_t).PropId;
                numericLdCtrlWritePropStartElement.Value = (selectedItem as LdCtrlWriteProp_t).StartElement;
                numericLdCtrlWritePropCount.Value = (selectedItem as LdCtrlWriteProp_t).Count;
                checkBoxLdCtrlWritePropVerify.Checked = (selectedItem as LdCtrlWriteProp_t).Verify;
                HandleKnxDataTypes.ReadKNXType(listBoxLdCtrlWritePropInlineData, (selectedItem as LdCtrlWriteProp_t).InlineData);
                comboBoxLdCtrlWritePropAppliesTo.SelectedItem = (selectedItem as LdCtrlWriteProp_t).AppliesTo.ToString();
                textBoxLdCtrlWritePropInternalDescription.Text = (selectedItem as LdCtrlWriteProp_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlWriteRelMem_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlWriteRelMem);
                checkBoxLdCtrlWriteRelMemObjIdxSpecified.Checked = (selectedItem as LdCtrlWriteRelMem_t).ObjIdxSpecified;
                numericLdCtrlWriteRelMemObjIdx.Value = (selectedItem as LdCtrlWriteRelMem_t).ObjIdx;
                checkBoxLdCtrlWriteRelMemObjTypeSpecified.Checked = (selectedItem as LdCtrlWriteRelMem_t).ObjTypeSpecified;
                numericLdCtrlWriteRelMemObjType.Value = (selectedItem as LdCtrlWriteRelMem_t).ObjType;
                numericLdCtrlWriteRelMemOccurence.Value = (selectedItem as LdCtrlWriteRelMem_t).Occurrence;
                numericLdCtrlWriteRelMemOffset.Value = (selectedItem as LdCtrlWriteRelMem_t).Offset;
                numericLdCtrlWriteRelMemSize.Value = (selectedItem as LdCtrlWriteRelMem_t).Size;
                checkBoxLdCtrlWriteRelMemVerify.Checked = (selectedItem as LdCtrlWriteRelMem_t).Verify;
                HandleKnxDataTypes.ReadKNXType(listBoxLdCtrlWriteRelMemInlineData, (selectedItem as LdCtrlWriteRelMem_t).InlineData);
                comboBoxLdCtrlWriteRelMemAppliesTo.SelectedItem = (selectedItem as LdCtrlWriteRelMem_t).AppliesTo.ToString();
                textBoxLdCtrlWriteRelMemInternalDescription.Text = (selectedItem as LdCtrlWriteRelMem_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlBaseChoose_t)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlBaseChoose);
                textBoxLdCtrlBaseChooseParamRefId.Text = (selectedItem as LdCtrlBaseChoose_t).ParamRefId;
                textBoxLdCtrlBaseChooseInternalDescription.Text = (selectedItem as LdCtrlBaseChoose_t).InternalDescription;
            }
            else if (selectedItem is LdCtrlBaseChoose_tWhen)
            {
                tabControlLoadProcedures.TabPages.Add(tabPageLdCtrlBaseChooseWhen);
            }
        }

        private void RemoveAllLoadProceduresTabs()
        {
            while (tabControlLoadProcedures.TabPages.Count > 0)
            {
                tabControlLoadProcedures.TabPages.Clear();
            }
        }
    }
}
