using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnxProd.Model;

namespace knxprod_ns
{
    public partial class ApplicationGui : UserControl
    {
        private static ApplicationGui mApplicationGui;

        public ApplicationGui()
        {
            InitializeComponent();
            mApplicationGui = this;
        }

        /********************************************************************************************************************************/
        // Application View

        public static void FillApplicationPage(ApplicationProgram_t applicationProgram)
        {
            DynamicTabControl.AddApplicationTab();

            // Die Id muss in den Teil des Manufacturers mit der AppVersion und den Identifier des Application Programms zerlegt werden
            int applicationIdPosition = applicationProgram.Id.IndexOf("_A-") + 11;
            mApplicationGui.textAppIdManufacturerVersion.Text = applicationProgram.Id.Substring(0, applicationIdPosition);
            mApplicationGui.numericAppIdIdentifier.Value = int.Parse(applicationProgram.Id.Substring(applicationIdPosition, 4), System.Globalization.NumberStyles.HexNumber);

            mApplicationGui.numericAppApplicationNumber.Value = applicationProgram.ApplicationNumber;

            // Die ApplicationVersion wird in der ETS als "Programmversion" angezeigt.
            // Allerdings wird der Wert dezimal in der xml abgelegt, in der ETS wird der hexadezimale Wert geteilt durch 10 angezeigt
            decimal appVersion = int.Parse(applicationProgram.ApplicationVersion.ToString("X"));
            appVersion = appVersion / 10;
            mApplicationGui.numericAppApplicationVersion.Value = appVersion;

            mApplicationGui.comboBoxAppProgramType.SelectedItem = HandleKnxDataTypes.ReadKNXType(applicationProgram.ProgramType);
            mApplicationGui.comboBoxAppMaskVersion.SelectedItem = applicationProgram.MaskVersion;
            mApplicationGui.textAppVisibleDescription.Text = applicationProgram.VisibleDescription;
            mApplicationGui.textAppName.Text = applicationProgram.Name;
            mApplicationGui.comboBoxAppLoadProcedureStyle.SelectedItem = HandleKnxDataTypes.ReadKNXType(applicationProgram.LoadProcedureStyle);
            mApplicationGui.numericAppPeiType.Value = applicationProgram.PeiType;
            mApplicationGui.checkBoxAppHelpTopicSpecified.Checked = applicationProgram.HelpTopicSpecified;
            mApplicationGui.numericAppHelpTopic.Value = applicationProgram.HelpTopic;
            mApplicationGui.textAppHelpFile.Text = applicationProgram.HelpFile;
            mApplicationGui.textAppContextHelpFile.Text = applicationProgram.ContextHelpFile;
            mApplicationGui.textAppIconFile.Text = applicationProgram.IconFile;
            mApplicationGui.textAppDefaultLanguage.Text = applicationProgram.DefaultLanguage;
            mApplicationGui.checkBoxAppDynamicTableManagement.Checked = applicationProgram.DynamicTableManagement;
            mApplicationGui.checkBoxAppLinkable.Checked = applicationProgram.Linkable;
            mApplicationGui.checkBoxAppIsSecureEnabled.Checked = applicationProgram.IsSecureEnabled;
            mApplicationGui.textAppMinEtsVersion.Text = applicationProgram.MinEtsVersion;
            mApplicationGui.textAppOriginalManufacturer.Text = applicationProgram.OriginalManufacturer;
            mApplicationGui.checkBoxAppPreEts4Style.Checked = applicationProgram.PreEts4Style;
            mApplicationGui.checkBoxAppConvertedFromPreEts4Data.Checked = applicationProgram.ConvertedFromPreEts4Data;
            mApplicationGui.checkBoxAppCreatedFromLegacySchemaVersion.Checked = applicationProgram.CreatedFromLegacySchemaVersion;
            mApplicationGui.comboBoxAppIPConfig.SelectedItem = HandleKnxDataTypes.ReadKNXType(applicationProgram.IPConfig);
            mApplicationGui.numericAppAdditionalAdressesCount.Value = applicationProgram.AdditionalAddressesCount;
            mApplicationGui.numericAppMaxUsersEntries.Value = applicationProgram.MaxUserEntries;
            mApplicationGui.numericAppMaxTunnelingUserEntries.Value = applicationProgram.MaxTunnelingUserEntries;
            mApplicationGui.numericAppMaxSecurityIndividualAddressEntries.Value = applicationProgram.MaxSecurityIndividualAddressEntries;
            mApplicationGui.numericAppMaxSecurityGroupKeyTableEntries.Value = applicationProgram.MaxSecurityGroupKeyTableEntries;
            mApplicationGui.numericAppMaxSecurityP2PKeyTableEntries.Value = applicationProgram.MaxSecurityP2PKeyTableEntries;
            mApplicationGui.numericAppMaxSecurityProxyGroupKeyTableEntries.Value = applicationProgram.MaxSecurityProxyGroupKeyTableEntries;
            mApplicationGui.numericAppMaxSecurityProxyIndividualAddressTableEntries.Value = applicationProgram.MaxSecurityProxyIndividualAddressTableEntries;
            mApplicationGui.numericAppNonRegRelevantDataVersion.Value = applicationProgram.NonRegRelevantDataVersion;
            mApplicationGui.checkBoxAppBroken.Checked = applicationProgram.Broken;
            mApplicationGui.checkBoxAppDownloadInfoComplete.Checked = applicationProgram.DownloadInfoIncomplete;
            mApplicationGui.textAppReplacesVersions.Text = applicationProgram.ReplacesVersions.ToNullString();
            HandleKnxDataTypes.ReadKNXType(mApplicationGui.listAppHash, applicationProgram.Hash);
            mApplicationGui.textAppInternalDescription.Text = applicationProgram.InternalDescription;
            mApplicationGui.textAppSemantics.Text = applicationProgram.Semantics;

            mApplicationGui.dgvAppProgAppTranslations.Rows.Clear();
            LanguageProcessing.FillLanguageDataGridView(ApplicationProgramGui.selectedApplicationManufacturer.Languages, mApplicationGui.dgvAppProgAppTranslations, applicationProgram.Id, "Name");
        }

        /********************************************************************************************************************************/
        // Application Edit

        private void buttonApplicationSave_Click(object sender, EventArgs e)
        {
            ApplicationProgram_t newApplicationProgram = DynamicTreeViewGui.GetSelectedTreeNode().Tag as ApplicationProgram_t;

            newApplicationProgram.ProgramType = HandleGuiDataTypes.ReadApplicationProgramType(comboBoxAppProgramType.SelectedItem.ToString());
            newApplicationProgram.MaskVersion = comboBoxAppMaskVersion.SelectedItem.ToString();
            newApplicationProgram.VisibleDescription = Extensions.NullIfEmpty(textAppVisibleDescription.Text);
            newApplicationProgram.Name = Extensions.NullIfEmpty(textAppName.Text);
            newApplicationProgram.LoadProcedureStyle = HandleGuiDataTypes.ReadLoadProcedureStyle(comboBoxAppLoadProcedureStyle.SelectedItem.ToString());
            newApplicationProgram.PeiType = (byte)numericAppPeiType.Value;
            newApplicationProgram.HelpTopicSpecified = checkBoxAppHelpTopicSpecified.Checked;
            newApplicationProgram.HelpTopic = (uint)numericAppHelpTopic.Value;
            newApplicationProgram.HelpFile = Extensions.NullIfEmpty(textAppHelpFile.Text);
            newApplicationProgram.ContextHelpFile = Extensions.NullIfEmpty(textAppContextHelpFile.Text);
            newApplicationProgram.IconFile = Extensions.NullIfEmpty(textAppIconFile.Text);
            newApplicationProgram.DefaultLanguage = Extensions.NullIfEmpty(textAppDefaultLanguage.Text);
            newApplicationProgram.DynamicTableManagement = checkBoxAppDynamicTableManagement.Checked;
            newApplicationProgram.Linkable = checkBoxAppLinkable.Checked;
            newApplicationProgram.IsSecureEnabled = checkBoxAppIsSecureEnabled.Checked;
            newApplicationProgram.MinEtsVersion = Extensions.NullIfEmpty(textAppMinEtsVersion.Text);
            newApplicationProgram.OriginalManufacturer = Extensions.NullIfEmpty(textAppOriginalManufacturer.Text);
            newApplicationProgram.PreEts4Style = checkBoxAppPreEts4Style.Checked;
            newApplicationProgram.ConvertedFromPreEts4Data = checkBoxAppConvertedFromPreEts4Data.Checked;
            newApplicationProgram.CreatedFromLegacySchemaVersion = checkBoxAppCreatedFromLegacySchemaVersion.Checked;
            newApplicationProgram.IPConfig = HandleGuiDataTypes.ReadApplicationProgramIPConfig(comboBoxAppIPConfig.SelectedItem.ToString());
            newApplicationProgram.AdditionalAddressesCount = (int)numericAppAdditionalAdressesCount.Value;
            newApplicationProgram.MaxUserEntries = (ushort)numericAppMaxUsersEntries.Value;
            newApplicationProgram.MaxTunnelingUserEntries = (ushort)numericAppMaxTunnelingUserEntries.Value;
            newApplicationProgram.MaxSecurityIndividualAddressEntries = (ushort)numericAppMaxSecurityIndividualAddressEntries.Value;
            newApplicationProgram.MaxSecurityGroupKeyTableEntries = (ushort)numericAppMaxSecurityGroupKeyTableEntries.Value;
            newApplicationProgram.MaxSecurityP2PKeyTableEntries = (ushort)numericAppMaxSecurityP2PKeyTableEntries.Value;
            newApplicationProgram.MaxSecurityProxyGroupKeyTableEntries = (ushort)numericAppMaxSecurityProxyGroupKeyTableEntries.Value;
            newApplicationProgram.MaxSecurityProxyIndividualAddressTableEntries = (ushort)numericAppMaxSecurityProxyIndividualAddressTableEntries.Value;
            newApplicationProgram.NonRegRelevantDataVersion = (ushort)numericAppNonRegRelevantDataVersion.Value;
            newApplicationProgram.Broken = checkBoxAppBroken.Checked;
            newApplicationProgram.DownloadInfoIncomplete = checkBoxAppDownloadInfoComplete.Checked;
            newApplicationProgram.ReplacesVersions = Extensions.NullIfEmpty(textAppReplacesVersions.Text);
            newApplicationProgram.Hash = HandleGuiDataTypes.ReadGuiType(listAppHash);
            newApplicationProgram.InternalDescription = Extensions.NullIfEmpty(textAppInternalDescription.Text);
            newApplicationProgram.Semantics = Extensions.NullIfEmpty(textAppSemantics.Text);

            LanguageProcessing.WriteLanguageData(ApplicationProgramGui.selectedApplicationManufacturer.Languages, dgvAppProgAppTranslations, newApplicationProgram.Id, "Name", ApplicationProgramGui.selectedApplicationProgram.Id);


            // Die Operationen zur Anpassung der ApplicationProgram Id müssen zuletzt durchgeführt werden, da die Dateien gespeichert und wieder geöffnet werden müssen
            ushort appNumber = (ushort)numericAppApplicationNumber.Value;

            // Die ApplicationVersion wird in der ETS als "Programmversion" angezeigt.
            // Allerdings wird der Wert dezimal in der xml abgelegt, in der ETS wird der hexadezimale Wert geteilt durch 10 angezeigt
            string appVersionString = ((int)(numericAppApplicationVersion.Value * 10)).ToString();
            byte appVersion = byte.Parse(appVersionString, System.Globalization.NumberStyles.HexNumber);

            // Die Application Id besteht aus dem Manufacturer, der ApplicationNumer in dezimal, der ApplicationVersion in dezimal und einer Id in hexadezimal
            string newAppId = ApplicationProgramGui.selectedApplicationManufacturer.RefId + "_A-" + appNumber.ToString("X4") + "-" + appVersion.ToString("X2") + "-" + ((int)numericAppIdIdentifier.Value).ToString("X4");
            string oldAppId = ApplicationProgramGui.selectedApplicationProgram.Id;
            if (newAppId != oldAppId)
            {
                newApplicationProgram.ApplicationNumber = appNumber;
                newApplicationProgram.ApplicationVersion = appVersion;
                ChangeApplicationId(ApplicationProgramGui.selectedApplicationManufacturer.RefId, oldAppId, newAppId);
            }
        }

        /// <summary>
        /// Die Application Id besteht aus dem Manufacturer, der ApplicationNumer in dezimal, der ApplicationVersion in dezimal und einer Id in hexadezimal
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <param name="applicationNumber"></param>
        /// <param name="applicationVersion"></param>
        /// <param name="ApplicationIdIdentifier"></param>
        private void ChangeApplicationId(string manufacturerId, string oldAppId, string newAppId)
        {
            // Das Ändern des Manufacturers ist sehr aufwändig, da es sich durch alle Dateien in der gesamten knxprod zieht
            // Daher wird dieses in der Textdateiverarbeitung gemacht und anschließend die bearbeiteten Dateien aus dem unzippedKnxprod Ordner neu geladen

            DialogResult result = MessageBox.Show("Soll die Application Id wirklich geändert werden?\n" +
                "Alle Daten werden zwischengespeichert, aber alle Tabs werden geschlossen.\n" +
                "Das eventuell veränderte ApplicationProgramm und die Hardware muss neu ausgewählt werden."
                , "Application Id Änderung Sicherheitsabfrage",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                // den alten ApplicationProgram Identifier wieder herstellen
                int applicationIdPosition = ApplicationProgramGui.selectedApplicationProgram.Id.IndexOf("_A-") + 11;
                textAppIdManufacturerVersion.Text = ApplicationProgramGui.selectedApplicationProgram.Id.Substring(0, applicationIdPosition);
                numericAppIdIdentifier.Value = int.Parse(ApplicationProgramGui.selectedApplicationProgram.Id.Substring(applicationIdPosition), System.Globalization.NumberStyles.HexNumber);
                // die alte ApplicationNumber und ApplicationVersion wieder herstellen
                numericAppApplicationNumber.Value = ApplicationProgramGui.selectedApplicationProgram.ApplicationNumber;

                // Die ApplicationVersion wird in der ETS als "Programmversion" angezeigt.
                // Allerdings wird der Wert dezimal in der xml abgelegt, in der ETS wird der hexadezimale Wert geteilt durch 10 angezeigt
                decimal appVersion = int.Parse(ApplicationProgramGui.selectedApplicationProgram.ApplicationVersion.ToString("X"));
                appVersion = appVersion / 10;
                numericAppApplicationVersion.Value = appVersion;

                return;
            }

            // die neue Application Id zusammenbauen (ohne Manufacturer Id, da diese nicht in der Hardware2Program Id vorkommt)
            string stringToReplace = oldAppId.Substring(ApplicationProgramGui.selectedApplicationProgram.Id.IndexOf("_A-") + 3);
            string replaceString = newAppId.Substring(ApplicationProgramGui.selectedApplicationProgram.Id.IndexOf("_A-") + 3);


            KNXprodFile.SaveKnxprodFilesToFolder(KNXprodFile.knxprodExportPath); // alle bisherigen Daten im ExportPath zwischenspeichern
            DirectoryHelper.DirectoryDelete(KNXprodFile.unzippedKnxprodPath); // den unzipped Ordner löschen
            DirectoryHelper.DirectoryCopy(KNXprodFile.knxprodExportPath, KNXprodFile.unzippedKnxprodPath, true); // alle zwischengespeicherten Daten aus dem ExportPath in den unzippedPath kopieren

            foreach (var directory in KNXprodFile.knxprodSubDirs)
            {
                // nur den Ordnernamen herausfiltern
                int posFoldername = directory.LastIndexOf("\\");
                string dirName = directory.Substring(posFoldername + 1);

                if (dirName == manufacturerId) // wenn der Ordnername in der knxprod mit dem Manufacturer übereinstimmt
                {
                    DirectoryInfo dir = new DirectoryInfo(directory);

                    // auch der Ordner Name muss geändert werden, dazu wird ein neuer Ordner erstellt und der alten später gelöscht
                    //string newDirName = dir.FullName.Replace(oldAppId, newAppId);
                    //Directory.CreateDirectory(newDirName);

                    foreach (FileInfo file in dir.GetFiles())
                    {
                        string oldFileName = file.FullName;
                        string newFileName = oldFileName.Replace(stringToReplace, replaceString);

                        string text = File.ReadAllText(oldFileName);
                        text = text.Replace(stringToReplace, replaceString); // in der Datei alle vorkommenden Manufacturer IDs ersetzen
                        File.WriteAllText(newFileName, text);
                    }

                    //dir.Delete(true); // alten Ordner (mit der alten Manufacturer Id) löschen
                }
            }

            KNXprodFile.OpenStandardKnxProdFiles(KNXprodFile.unzippedKnxprodPath); // die Dateien aus dem exportPath neu einlesen

            // das erste Element als aktiven Knoten auswählen (meinstens der veränderte Manufacturer)
            CatalogGui.mTreeView.SelectedNode = CatalogGui.mTreeView.TopNode;
        }
    }
}
