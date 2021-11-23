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
    public partial class CatalogGui : UserControl
    {
        public static CatalogGui mCatalogGui;
        public static TreeView mTreeView;


        public CatalogGui()
        {
            InitializeComponent();
            mCatalogGui = this;
            mTreeView = treeViewCatalog;
        }


        /*********************************************************************************************************************/
        // CatalogView


        private static ManufacturerData_tManufacturer selectedCatalogManufacturer;
        public static CatalogSection_tCatalogItem selectedCatalogItem;

        public void SetupCatalogTab() //Einsprungsfunktion
        {
                treeViewCatalog.Nodes.Clear();

                foreach (var manufacturer in KNXprodFile.knxprodCatalog.ManufacturerData)
                {
                    var childNode = treeViewCatalog.Nodes.Add("Manufacturer: " + manufacturer.RefId);
                    childNode.Tag = manufacturer;
                    foreach (CatalogSection_t catalog in manufacturer.Catalog)
                    {
                        CatalogSectionTreeView(catalog, childNode);
                    }
                }
                treeViewCatalog.ExpandAll();
                if (treeViewCatalog.Nodes[0] != null)
                {
                    treeViewCatalog.SelectedNode = treeViewCatalog.Nodes[0];
                }           
        }
        
        public void InitializeCatalogTreeView() 
        {
            FormKNXprodEditor.AddCatalogFileTab();

            SetupCatalogTab();
        }
        

        private void CatalogSectionTreeView(CatalogSection_t catalogSection, TreeNode parentNode)
        {
            var childNode = parentNode.Nodes.Add(catalogSection.Name);
            childNode.Tag = catalogSection;
            if (catalogSection.CatalogItem != null)
            {
                foreach (var catalogItem in catalogSection.CatalogItem)
                {
                    TreeNode grandChildNode = childNode.Nodes.Add(catalogItem.Name); // Übersetzung nicht so einfach möglich, da für jedes CatalogItem eine Translation Unit besteht
                    grandChildNode.Tag = catalogItem;
                }
            }
            if (catalogSection.CatalogSection != null)
            {
                foreach (var catalogSectionItem in catalogSection.CatalogSection)
                {
                    CatalogSectionTreeView(catalogSectionItem, childNode);
                }
            }
        }

        private TreeNode lastSelectedTreeViewCatalogNode;

        private void TreeViewCatalog_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Highlight new node
            e.Node.BackColor = SystemColors.Highlight;
            e.Node.ForeColor = SystemColors.HighlightText;
            if (lastSelectedTreeViewCatalogNode != null)
            {
                // Set colors to normal for old node
                lastSelectedTreeViewCatalogNode.BackColor = SystemColors.Window;
                lastSelectedTreeViewCatalogNode.ForeColor = SystemColors.WindowText;
            }
            lastSelectedTreeViewCatalogNode = e.Node;

            if (e.Node.Tag is ManufacturerData_tManufacturer)
            {
                RemoveAllTabsCatalog();
                tabControlCatalog.TabPages.Add(this.tabManufacturer);

                foreach (MasterData_tManufacturer manufacturer in KNXprodFile.knxprodKnxMaster.MasterData.Manufacturers)
                {
                    ComboBoxItem newComboBoxItem = new ComboBoxItem(manufacturer.Name, manufacturer);
                    comboBoxCatalogManufacturer.Items.Add(newComboBoxItem);
                    if (manufacturer.Id == (e.Node.Tag as ManufacturerData_tManufacturer).RefId)
                    {
                        comboBoxCatalogManufacturer.SelectedItem = newComboBoxItem;
                    }
                }

                textCatManRefId.Text = (e.Node.Tag as ManufacturerData_tManufacturer).RefId;
            }

            if (e.Node.Tag is CatalogSection_t)
            {
                RemoveAllTabsCatalog();
                tabControlCatalog.TabPages.Add(this.tabCatalogSection);
                textCatSecId.Text = (e.Node.Tag as CatalogSection_t).Id;
                textCatSecName.Text = (e.Node.Tag as CatalogSection_t).Name;
                textCatSecNumber.Text = (e.Node.Tag as CatalogSection_t).Number.ToString();
                textCatSecVisibleDescription.Text = (e.Node.Tag as CatalogSection_t).VisibleDescription;
                textCatSecDefaultLanguage.Text = (e.Node.Tag as CatalogSection_t).DefaultLanguage;
                numericCatSecNonRegRelevantDataVersion.Value = (e.Node.Tag as CatalogSection_t).NonRegRelevantDataVersion;
                textCatSecInternalDescription.Text = (e.Node.Tag as CatalogSection_t).InternalDescription;

                dgvCatCatalogSection.Rows.Clear();

                selectedCatalogManufacturer = FindSelectedCatalogManufacturer((e.Node.Tag as CatalogSection_t).Id);

                LanguageProcessing.FillLanguageDataGridView(selectedCatalogManufacturer.Languages, dgvCatCatalogSection, (e.Node.Tag as CatalogSection_t).Id, "Name");
            }

            if (e.Node.Tag is CatalogSection_tCatalogItem)
            {
                RemoveAllTabsCatalog();
                tabControlCatalog.TabPages.Add(this.tabCatalogItem);
                textCatItmId.Text = (e.Node.Tag as CatalogSection_tCatalogItem).Id;
                textCatItmName.Text = (e.Node.Tag as CatalogSection_tCatalogItem).Name;
                numericCatItmNumber.Value = (e.Node.Tag as CatalogSection_tCatalogItem).Number;
                textCatItmVisibleDescription.Text = (e.Node.Tag as CatalogSection_tCatalogItem).VisibleDescription;
                textCatItmProductRefId.Text = (e.Node.Tag as CatalogSection_tCatalogItem).ProductRefId;
                textCatItmHardware2ProgramRefId.Text = (e.Node.Tag as CatalogSection_tCatalogItem).Hardware2ProgramRefId;
                textCatItmDefaultLanguage.Text = (e.Node.Tag as CatalogSection_tCatalogItem).DefaultLanguage;
                numericCatItmNonRegRelevantDataVersion.Value = (e.Node.Tag as CatalogSection_tCatalogItem).NonRegRelevantDataVersion;
                textCatItmInternalDescription.Text = (e.Node.Tag as CatalogSection_tCatalogItem).InternalDescription;

                dgvCatCatalogItem.Rows.Clear();

                selectedCatalogManufacturer = FindSelectedCatalogManufacturer((e.Node.Tag as CatalogSection_tCatalogItem).Id);

                LanguageProcessing.FillLanguageDataGridView(selectedCatalogManufacturer.Languages, dgvCatCatalogItem, (e.Node.Tag as CatalogSection_tCatalogItem).Id, "Name");

                // Speichern der gewählten Daten aus dem Catalog
                HardwareGui.selectedProductRefId = (e.Node.Tag as CatalogSection_tCatalogItem).ProductRefId;
                HardwareGui.selectedHardware2ProgramRefId = (e.Node.Tag as CatalogSection_tCatalogItem).Hardware2ProgramRefId;
                selectedCatalogItem = e.Node.Tag as CatalogSection_tCatalogItem;
                HardwareGui.mHardwareGui.InitializeHardwareTreeView();
            }
        }

        private void RemoveAllTabsCatalog()
        {
            while (tabControlCatalog.TabPages.Count > 0)
            {
                tabControlCatalog.TabPages.Clear();
            }
        }


        /// <summary>
        /// Findet den Manufacturer zur angegebenen CatalogSection oder CatalogItem Id
        /// </summary>
        /// <param name="id">CatalogSection oder CatalogItem Id</param>
        /// <returns>zugehöriger Manufacturer</returns>
        private ManufacturerData_tManufacturer FindSelectedCatalogManufacturer(string id)
        {
            foreach (ManufacturerData_tManufacturer manufacturer in KNXprodFile.knxprodCatalog.ManufacturerData)
            {
                foreach (CatalogSection_t catalogSection in manufacturer.Catalog)
                {
                    if (FindSelectedCatalogSectionManufacturer(catalogSection, id))
                    {
                        return manufacturer;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Rekursive Funktion für die Durchsuchung von CatalogSections nach der Id
        /// </summary>
        /// <param name="catalogSection">die zu durchsuchende catalogSection</param>
        /// <param name="id">die zu suchende Id der catalogSection oder catalogItem</param>
        /// <returns></returns>
        private bool FindSelectedCatalogSectionManufacturer(CatalogSection_t catalogSection, string id)
        {
            if (catalogSection.Id == id)
            {
                return true;
            }
            if (catalogSection.CatalogItem != null)
            {
                foreach (CatalogSection_tCatalogItem catalogItem in catalogSection.CatalogItem)
                {
                    if (catalogItem.Id == id)
                    {
                        return true;
                    }
                }
            }
            if (catalogSection.CatalogSection != null)
            {
                foreach (var catalogSectionItem in catalogSection.CatalogSection)
                {
                    if (catalogSectionItem.Id == id)
                    {
                        return true;
                    }
                    if (FindSelectedCatalogSectionManufacturer(catalogSectionItem, id)) // rekursiver Aufruf zum Druchsuchen aller catalogSections
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /****************************************************************************************************************************/
        // Catalog Edit

        private void comboBoxCatalogManufacturer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // Das Ändern des Manufacturers ist sehr aufwändig, da es sich durch alle Dateien in der gesamten knxprod zieht
            // Daher wird dieses in der Textdateiverarbeitung gemacht und anschließend die bearbeiteten Dateien aus dem unzippedKnxprod Ordner neu geladen

            DialogResult result = MessageBox.Show("Soll der Manufacturer wirklich geändert werden?\n" +
                "Alle Daten werden zwischengespeichert, aber alle Tabs werden geschlossen.\n" +
                "Das eventuell veränderte ApplicationProgramm und die Hardware muss neu ausgewählt werden."
                , "Manufacturer Änderung Sicherheitsabfrage",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                // den alten Manufacturer wieder als gewählten in der ComboBox anzeigen
                foreach (var comboBoxItem in comboBoxCatalogManufacturer.Items)
                {
                    if (((comboBoxItem as ComboBoxItem).Tag as MasterData_tManufacturer).Id == (treeViewCatalog.SelectedNode.Tag as ManufacturerData_tManufacturer).RefId)
                    {
                        comboBoxCatalogManufacturer.SelectedItem = comboBoxItem;
                        break;
                    }
                }
                return;
            }

            ManufacturerData_tManufacturer oldManufacturer = treeViewCatalog.SelectedNode.Tag as ManufacturerData_tManufacturer;
            MasterData_tManufacturer newManufacturer = (comboBoxCatalogManufacturer.SelectedItem as ComboBoxItem).Tag as MasterData_tManufacturer;

            KNXprodFile.SaveKnxprodFilesToFolder(KNXprodFile.knxprodExportPath); // alle bisherigen Daten im ExportPath zwischenspeichern
            DirectoryHelper.DirectoryDelete(KNXprodFile.unzippedKnxprodPath); // den unzipped Ordner löschen
            DirectoryHelper.DirectoryCopy(KNXprodFile.knxprodExportPath, KNXprodFile.unzippedKnxprodPath, true); // alle zwischengespeicherten Daten aus dem ExportPath in den unzippedPath kopieren

            foreach (var directory in KNXprodFile.knxprodSubDirs)
            {
                // nur den Ordnernamen herausfiltern
                int posFoldername = directory.LastIndexOf("\\");
                string dirName = directory.Substring(posFoldername + 1);

                if (dirName == oldManufacturer.RefId) // wenn der Ordnername in der knxprod mit dem (alten) Manufacturer übereinstimmt
                {
                    DirectoryInfo dir = new DirectoryInfo(directory);

                    // auch der Ordner Name muss geändert werden, dazu wird ein neuer Ordner erstellt und der alten später gelöscht
                    string newDirName = dir.FullName.Replace(oldManufacturer.RefId, newManufacturer.Id);
                    Directory.CreateDirectory(newDirName);

                    foreach (FileInfo file in dir.GetFiles())
                    {
                        string oldFileName = file.FullName;
                        string newFileName = oldFileName.Replace(oldManufacturer.RefId, newManufacturer.Id);

                        string text = File.ReadAllText(oldFileName);
                        text = text.Replace(oldManufacturer.RefId, newManufacturer.Id); // in der Datei alle vorkommenden Manufacturer IDs ersetzen
                        File.WriteAllText(newFileName, text);
                    }

                    dir.Delete(true); // alten Ordner (mit der alten Manufacturer Id) löschen
                }
            }

            KNXprodFile.OpenStandardKnxProdFiles(KNXprodFile.unzippedKnxprodPath); // die Dateien aus dem exportPath neu einlesen

            // das erste Element als aktiven Knoten auswählen (meinstens der veränderte Manufacturer)
            treeViewCatalog.SelectedNode = treeViewCatalog.TopNode;
        }

        private void buttonCatalogSectionSave_Click(object sender, EventArgs e)
        {
            CatalogSection_t newCatalogSection = treeViewCatalog.SelectedNode.Tag as CatalogSection_t;

            newCatalogSection.Name = Extensions.NullIfEmpty(textCatSecName.Text);
            newCatalogSection.Number = textCatSecNumber.Text;
            newCatalogSection.VisibleDescription = Extensions.NullIfEmpty(textCatSecVisibleDescription.Text);
            newCatalogSection.DefaultLanguage = Extensions.NullIfEmpty(textCatSecDefaultLanguage.Text);
            newCatalogSection.NonRegRelevantDataVersion = (ushort)numericCatSecNonRegRelevantDataVersion.Value;
            newCatalogSection.InternalDescription = Extensions.NullIfEmpty(textCatSecInternalDescription.Text);
            LanguageProcessing.WriteLanguageData(selectedCatalogManufacturer.Languages, dgvCatCatalogSection, newCatalogSection.Id, "Name", newCatalogSection.Id);

            treeViewCatalog.SelectedNode.Text = newCatalogSection.Name;
        }

        private void buttonCatalogItemSave_Click(object sender, EventArgs e)
        {
            CatalogSection_tCatalogItem newCatalogItem = treeViewCatalog.SelectedNode.Tag as CatalogSection_tCatalogItem;
            newCatalogItem.Name = Extensions.NullIfEmpty(textCatItmName.Text);
            newCatalogItem.Number = (int)numericCatItmNumber.Value;
            newCatalogItem.VisibleDescription = Extensions.NullIfEmpty(textCatItmVisibleDescription.Text);
            newCatalogItem.ProductRefId = Extensions.NullIfEmpty(textCatItmProductRefId.Text);
            newCatalogItem.Hardware2ProgramRefId = Extensions.NullIfEmpty(textCatItmHardware2ProgramRefId.Text);
            newCatalogItem.DefaultLanguage = Extensions.NullIfEmpty(textCatItmDefaultLanguage.Text);
            newCatalogItem.NonRegRelevantDataVersion = (ushort)numericCatItmNonRegRelevantDataVersion.Value;
            newCatalogItem.InternalDescription = Extensions.NullIfEmpty(textCatItmInternalDescription.Text);
            LanguageProcessing.WriteLanguageData(selectedCatalogManufacturer.Languages, dgvCatCatalogItem, newCatalogItem.Id, "Name", newCatalogItem.Id);

            treeViewCatalog.SelectedNode.Text = newCatalogItem.Name;
        }

        /// <summary>
        /// Im Catalog ist die Product Id mehrfach enthalten und kann hiermit verändert werden
        /// </summary>
        /// <param name="productId">die neue ProductId</param>
        public static void ChangeProductIdInCatalog(string productId)
        {
            // die neue ProductId als Referenz in den Catalog eintragen
            selectedCatalogItem.ProductRefId = productId;

            // die CatalogItem Id besteht aus der Hardware2Program Id, einem Teil der Product Id und "-1"
            int indexOfProductIdDivider = productId.IndexOf("_P-");
            string relevantPartOfProductId = productId.Substring(indexOfProductIdDivider + 3);

            int indexOfCatalogItemIdDivider = selectedCatalogItem.Id.IndexOf("_CI-");
            string unchangedPartOfCatalogItemId = selectedCatalogItem.Id.Substring(0, indexOfCatalogItemIdDivider + 4);

            string newCatalogItemId = unchangedPartOfCatalogItemId + relevantPartOfProductId + "-1";

            // Translations anpassen (Id ändern)
            LanguageProcessing.ChangeTansUnitRefId(selectedCatalogManufacturer.Languages, selectedCatalogItem.Id, newCatalogItemId);
            LanguageProcessing.ChangeTansElementRefId(selectedCatalogManufacturer.Languages, selectedCatalogItem.Id, newCatalogItemId);

            selectedCatalogItem.Id = newCatalogItemId;
        }

        private void ChangeHardware2ProgramIdInCatalogItemId(string hardware2ProgramId)
        {
            // die neue Hardware2Program Id als Referenz in den Catalog eintragen
            selectedCatalogItem.Hardware2ProgramRefId = hardware2ProgramId;

            int indexOfCatalogItemIdDivider = selectedCatalogItem.Id.IndexOf("_CI-");
            string unchangedPartOfCatalogItemId = selectedCatalogItem.Id.Substring(indexOfCatalogItemIdDivider);

            string newCatalogItemId = hardware2ProgramId + unchangedPartOfCatalogItemId;

            // Translations anpassen (Id ändern)
            LanguageProcessing.ChangeTansUnitRefId(selectedCatalogManufacturer.Languages, selectedCatalogItem.Id, newCatalogItemId);
            LanguageProcessing.ChangeTansElementRefId(selectedCatalogManufacturer.Languages, selectedCatalogItem.Id, newCatalogItemId);

            selectedCatalogItem.Id = newCatalogItemId;
        }
    }
}
