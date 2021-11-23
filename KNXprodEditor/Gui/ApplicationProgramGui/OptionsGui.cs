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
    public partial class OptionsGui : UserControl
    {
        private static OptionsGui mOptionsGui;

        public OptionsGui()
        {
            InitializeComponent();
            mOptionsGui = this;
        }

        public static void InitializeOptions()
        {
            ApplicationProgramStatic_tOptions options = ApplicationProgramGui.selectedApplicationProgram.Static.Options;
            if (options != null)
            {
                mOptionsGui.labelAppOptionsNoOptions.Visible = false;
                mOptionsGui.checkBoxAppOptionsPreferPartialDownloadIfApplicationLoaded.Checked = options.PreferPartialDownloadIfApplicationLoaded;
                mOptionsGui.checkBoxAppOptionsEasyCtrlModeModeStyleEmptyGroupComTables.Checked = options.EasyCtrlModeModeStyleEmptyGroupComTables;
                mOptionsGui.checkBoxAppOptionsSetObjectTableLengthAlwaysToOne.Checked = options.SetObjectTableLengthAlwaysToOne;
                mOptionsGui.checkBoxcheckBoxAppOptionsTextParameterEncodingSpecified.Checked = options.TextParameterEncodingSpecified;
                mOptionsGui.comboBoxAppOptionsTextParameterEncoding.SelectedItem = options.TextParameterEncoding.ToString();
                mOptionsGui.comboBoxAppOptionsTextParameterEncodingSelector.SelectedItem = options.TextParameterEncodingSelector.ToString();
                mOptionsGui.checkBoxAppOptionsTextParameterZeroTerminate.Checked = options.TextParameterZeroTerminate;
                mOptionsGui.comboBoxAppOptionsParameterByteOrder.SelectedItem = options.ParameterByteOrder;
                mOptionsGui.checkBoxAppOptionsPartialDownloadOnlyVisibleParameters.Checked = options.PartialDownloadOnlyVisibleParameters;
                mOptionsGui.checkBoxAppOptionsLegacyNoPartialDownload.Checked = options.LegacyNoPartialDownload;
                mOptionsGui.checkBoxAppOptionsLegacyNoOptimisticWrite.Checked = options.LegacyNoOptimisticWrite;
                mOptionsGui.checkBoxAppOptionsLegacyDoNotReportPropertyWriteErrors.Checked = options.LegacyDoNotReportPropertyWriteErrors;
                mOptionsGui.checkBoxAppOptionsLegacyNoBackgroundDownload.Checked = options.LegacyNoBackgroundDownload;
                mOptionsGui.checkBoxAppOptionsLegacyDoNotCheckManufacturerId.Checked = options.LegacyDoNotCheckManufacturerId;
                mOptionsGui.checkBoxAppOptionsLegacyAlwaysReloadAppIfCoVisibilityChanged.Checked = options.LegacyAlwaysReloadAppIfCoVisibilityChanged;
                mOptionsGui.checkBoxAppOptionsLegacyNeverReloadAppIfCoVisibilityChanged.Checked = options.LegacyNeverReloadAppIfCoVisibilityChanged;
                mOptionsGui.checkBoxAppOptionsLegacyDoNotSupportUndoDelete.Checked = options.LegacyDoNotSupportUndoDelete;
                mOptionsGui.checkBoxAppOptionsLegacyAllowPartialDownloadIfAp2Mismatch.Checked = options.LegacyAllowPartialDownloadIfAp2Mismatch;
                mOptionsGui.checkBoxAppOptionsLegacyKeepObjectTableGaps.Checked = options.LegacyKeepObjectTableGaps;
                mOptionsGui.checkBoxAppOptionsLegacyProxyCommunicationObjects.Checked = options.LegacyProxyCommunicationObjects;
                mOptionsGui.checkBoxAppOptionsDeviceInfoIgnoreRunState.Checked = options.DeviceInfoIgnoreRunState;
                mOptionsGui.checkBoxAppOptionsDeviceInfoIgnoreLoadedState.Checked = options.DeviceInfoIgnoreLoadedState;
                mOptionsGui.checkBoxAppOptionsDeviceCompareAllowCompatibleManufacturerId.Checked = options.DeviceCompareAllowCompatibleManufacturerId;
                mOptionsGui.checkBoxAppOptionsLineCoupler0912NewProgrammingStyle.Checked = options.LineCoupler0912NewProgrammingStyle;
                mOptionsGui.checkBoxAppOptionsMaxRoutingApduLengthSpecified.Checked = options.MaxRoutingApduLengthSpecified;
                mOptionsGui.numericAppOptionsMaxRoutingApduLength.Value = options.MaxRoutingApduLength;
                mOptionsGui.checkBoxcheckBoxAppOptionsComparableSpecified.Checked = options.ComparableSpecified;
                mOptionsGui.checkBoxAppOptionsComparable.Checked = options.Comparable;
                mOptionsGui.checkBoxcheckBoxAppOptionsReconstructableSpecified.Checked = options.ReconstructableSpecified;
                mOptionsGui.checkBoxAppOptionsReconstructable.Checked = options.Reconstructable;
                mOptionsGui.comboBoxAppOptionsDownloadInvisibleParameters.SelectedItem = options.DownloadInvisibleParameters;
                mOptionsGui.checkBoxAppOptionsSupportsExtendedMemoryServices.Checked = options.SupportsExtendedMemoryServices;
                mOptionsGui.checkBoxAppOptionsSupportsExtendedPropertyServices.Checked = options.SupportsExtendedPropertyServices;
                mOptionsGui.checkBoxAppOptionsSupportsIpSystemBroadcast.Checked = options.SupportsIpSystemBroadcast;
                mOptionsGui.checkBoxAppOptionsNotLoadableSpecified.Checked = options.NotLoadableSpecified;
                mOptionsGui.comboBoxAppOptionsNotLoadable.SelectedItem = options.NotLoadable;
                mOptionsGui.textBoxAppOptionsNotLoadableMessageRef.Text = options.NotLoadableMessageRef;
                mOptionsGui.checkBoxAppOptionsCustomerAdjustableParametersSpecified.Checked = options.CustomerAdjustableParametersSpecified;
                mOptionsGui.comboBoxAppOptionsCustomerAdjustableParameters.SelectedItem = options.CustomerAdjustableParameters;
                mOptionsGui.checkBoxAppOptionsMasterResetOnCRCMismatch.Checked = options.MasterResetOnCRCMismatch;
                mOptionsGui.checkBoxAppOptionsPromptBeforeFullDownload.Checked = options.PromptBeforeFullDownload;
                mOptionsGui.checkBoxAppOptionsLegacyPatchManufacturerIdInTaskSegment.Checked = options.LegacyPatchManufacturerIdInTaskSegment;
            }
            else
            {
                mOptionsGui.labelAppOptionsNoOptions.Visible = true;
            }
        }
    }
}
