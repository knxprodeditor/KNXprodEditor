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
    public partial class HardwareGui : UserControl
    {
        public static HardwareGui mHardwareGui;

        public HardwareGui()
        {
            InitializeComponent();
            mHardwareGui = this;
        }

        /***********************************************************************************************************/
        // Hardware View

        public static string selectedProductRefId;
        public static string selectedHardware2ProgramRefId;
        public static string selectedApplicationProgramRefId;
        private static ManufacturerData_tManufacturer selectedHardwareManufacturer;
        public static LanguageData_tTranslationUnit selectedTranslationUnitHardware;
        public static LanguageProcessing languageProcessingHardware = new LanguageProcessing();
        public static string loadedProductId; // Merker welches Produkt in der Hardware aktuell geladen ist
        public static string loadedHardware2ProgramId; // Merker welches Produkt in der Hardware aktuell geladen ist

        public void InitializeHardwareTreeView()
        {
            FormKNXprodEditor.AddHardwareFileTab();

            // wenn die Hardware.xml noch nicht geladen ist
            if (selectedProductRefId != loadedProductId || selectedHardware2ProgramRefId != loadedHardware2ProgramId)
            {
                SetupHardwareTab();
                loadedProductId = selectedProductRefId;
                loadedHardware2ProgramId = selectedHardware2ProgramRefId;
            }
        }

        public void SetupHardwareTab()
        {
                string selectedLanguageString = KnxMasterLanguage.selectedLanguageString;
                while (treeViewHardware.GetNodeCount(false) > 0)
                {
                    treeViewHardware.Nodes.RemoveAt(0);
                }
                foreach (var manufacturer in KNXprodFile.knxprodHardware.ManufacturerData)
                {
                    if (manufacturer.Languages != null)
                    {
                        LanguageData_t selectdLanguageApplication = manufacturer.Languages.ToList().Find(x => x.Identifier == selectedLanguageString);
                        if (selectdLanguageApplication != null)
                        {
                            selectedTranslationUnitHardware = selectdLanguageApplication.TranslationUnit.ToList().Find(x => x.RefId == selectedProductRefId);
                            languageProcessingHardware.TranslationUnit = selectedTranslationUnitHardware;
                        }
                        else
                        {
                            languageProcessingHardware.TranslationUnit = null;
                        }
                    }
                    else
                    {
                        languageProcessingHardware.TranslationUnit = null;
                    }
                    FillHardwareTreeView(manufacturer);
                }
                treeViewHardware.ExpandAll(); //Alle Zweige ausklappen, da die Anzahl meinstens überschaubar ist
                if (treeViewHardware.Nodes[0] != null)
                {
                    treeViewHardware.SelectedNode = treeViewHardware.Nodes[0];
                }
            
        }

        private void FillHardwareTreeView(ManufacturerData_tManufacturer manufacturer)
        {
            TreeNode hardwareNode = new TreeNode();
            foreach (var hardware in manufacturer.Hardware)
            {
                foreach (var product in hardware.Products)
                {
                    if (product.Id == selectedProductRefId)
                    {
                        TreeNode manufacturerNode = treeViewHardware.Nodes.Add("Manufacturer: " + manufacturer.RefId);
                        manufacturerNode.Tag = manufacturer;

                        hardwareNode = manufacturerNode.Nodes.Add(hardware.Name);
                        hardwareNode.Tag = hardware;

                        TreeNode productNode = new TreeNode();
                        string HardwareProductId = languageProcessingHardware.ReadTranslation(product.Id, "Text");
                        if (HardwareProductId != null)
                        {
                            productNode = hardwareNode.Nodes.Add(HardwareProductId);
                        }
                        else // falls keine Übersetzung vorhanden ist, wird der Text des products verwendet
                        {
                            productNode = hardwareNode.Nodes.Add(product.Text);
                        }

                        productNode.Tag = product;
                        if (product.Baggages != null)
                        {
                            foreach (var productBaggage in product.Baggages)
                            {
                                var baggageNode = productNode.Nodes.Add(productBaggage.RefId);
                                baggageNode.Tag = productBaggage;
                            }
                        }
                        if (product.Attributes != null)
                        {
                            foreach (var productAttribute in product.Attributes)
                            {
                                var attributeNode = productNode.Nodes.Add(productAttribute.Id);
                                attributeNode.Tag = productAttribute;
                            }
                        }
                        if (product.RegistrationInfo != null)
                        {
                            TreeNode registrationInfoNode = productNode.Nodes.Add("Registration");
                            registrationInfoNode.Tag = product.RegistrationInfo;
                        }
                        break;
                    }
                }
                foreach (var hardware2Program in hardware.Hardware2Programs)
                {
                    if (hardware2Program.Id == selectedHardware2ProgramRefId)
                    {
                        TreeNode hardware2ProgramNode = hardwareNode.Nodes.Add("Hardware2Program: " + hardware2Program.Id);
                        hardware2ProgramNode.Tag = hardware2Program;
                        foreach (var applicationProgramRef in hardware2Program.ApplicationProgramRef)
                        {
                            var applicationProgramRefNode = hardware2ProgramNode.Nodes.Add("ApplikationProgram: " + applicationProgramRef.RefId);  //ToDo: aus language vom Gerät aufzulösen
                            applicationProgramRefNode.Tag = applicationProgramRef;
                        }
                        if (hardware2Program.RegistrationInfo != null)
                        {
                            TreeNode registrationInfoNode = hardware2ProgramNode.Nodes.Add("Registration");
                            registrationInfoNode.Tag = hardware2Program.RegistrationInfo;
                        }
                    }
                }
            }
        }

        private TreeNode lastSelectedTreeViewHardwareNode;

        private void TreeViewHardware_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Highlight new node
            e.Node.BackColor = SystemColors.Highlight;
            e.Node.ForeColor = SystemColors.HighlightText;
            if (lastSelectedTreeViewHardwareNode != null)
            {
                // Set colors to normal for old node
                lastSelectedTreeViewHardwareNode.BackColor = SystemColors.Window;
                lastSelectedTreeViewHardwareNode.ForeColor = SystemColors.WindowText;
            }
            lastSelectedTreeViewHardwareNode = e.Node;


            //Alle Nodes aufgrund Ihres Tags abhandeln
            if (e.Node.Tag is ManufacturerData_tManufacturer)
            {
                RemoveAllHardwarePages();
                FillManufacturerPage(e.Node.Tag as ManufacturerData_tManufacturer);
            }
            else if (e.Node.Tag is Hardware_t)
            {
                RemoveAllHardwarePages();
                FillHardwarePage(e.Node.Tag as Hardware_t);
            }
            else if (e.Node.Tag is ApplicationProgramRef_t)
            {
                RemoveAllHardwarePages();
                FillApplicationProgramPage(e.Node.Tag as ApplicationProgramRef_t);
                selectedApplicationProgramRefId = (e.Node.Tag as ApplicationProgramRef_t).RefId;
                KNXprodFile.ReadApplicationProgramFile();
            }
            else if (e.Node.Tag is Hardware2Program_t)
            {
                RemoveAllHardwarePages();
                FillHardware2Program(e.Node.Tag as Hardware2Program_t);
            }
            else if (e.Node.Tag is Hardware_tProduct)
            {
                RemoveAllHardwarePages();
                FillProductPage(e.Node.Tag as Hardware_tProduct);
            }
            else if (e.Node.Tag is RegistrationInfo_t)
            {
                RemoveAllHardwarePages();
                FillRegistrationPage(e.Node.Tag as RegistrationInfo_t);
            }
            else if (e.Node.Tag is Hardware_tProductBaggage)
            {
                RemoveAllHardwarePages();
                FillProductBaggagePage(e.Node.Tag as Hardware_tProductBaggage);
            }
            else if (e.Node.Tag is Hardware_tProductAttribute)
            {
                RemoveAllHardwarePages();
                FillProductAttributePage(e.Node.Tag as Hardware_tProductAttribute);
            }
            else
            {
                MessageBox.Show("Datentyp nicht für Detailanzeige vorbereitet. Das Element hat den Typ " + e.Node.Tag.GetType().ToString(), "ComObject when Fehler",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveAllHardwarePages()
        {
            while (tabControlHardware.TabPages.Count > 0)
            {
                tabControlHardware.TabPages.Clear();
            }
        }

        private void FillManufacturerPage(ManufacturerData_tManufacturer manufacturer)
        {
            if (!tabControlHardware.TabPages.Contains(this.tabHardwareManufacturer))
            {
                tabControlHardware.TabPages.Add(this.tabHardwareManufacturer);
            }
            textHardManRefId.Text = manufacturer.RefId;
        }

        private void FillHardwarePage(Hardware_t hardware)
        {
            if (!tabControlHardware.TabPages.Contains(this.tabHardware))
            {
                tabControlHardware.TabPages.Add(this.tabHardware);
            }
            textHardId.Text = hardware.Id;
            textHardName.Text = hardware.Name;
            textHardSerialNumber.Text = hardware.SerialNumber;
            numericHardVersionNumber.Value = hardware.VersionNumber;
            checkBoxHardBusCurrentSpecified.Checked = hardware.BusCurrentSpecified;
            numericHardBusCurrent.Value = (decimal)hardware.BusCurrent;
            checkBoxBoxHardTP256Specified.Checked = hardware.Tp256Specified;
            checkBoxHardTP256.Checked = hardware.Tp256;
            checkBoxHardIsAccessory.Checked = hardware.IsAccessory;
            checkBoxHardHasIndividualAddress.Checked = hardware.HasIndividualAddress;
            checkBoxHardHasApplicationProgram.Checked = hardware.HasApplicationProgram;
            checkBoxHardHasApplicationProgram2.Checked = hardware.HasApplicationProgram2;
            checkBoxHardIsPowerSupply.Checked = hardware.IsPowerSupply;
            checkBoxHardIsChoke.Checked = hardware.IsChoke;
            checkBoxHardIsCoupler.Checked = hardware.IsCoupler;
            checkBoxHardIsPowerLineRepeater.Checked = hardware.IsPowerLineRepeater;
            checkBoxHardIsPowerLineSignalFilter.Checked = hardware.IsPowerLineSignalFilter;
            checkBoxHardIsCable.Checked = hardware.IsCable;
            checkBoxHardIsIPEnabled.Checked = hardware.IsIPEnabled;
            checkBoxHardIsRFRetransmitter.Checked = hardware.IsRFRetransmitter;
            textHardOriginalManufacturer.Text = hardware.OriginalManufacturer;
            checkBoxHardrFRxCapabilitiesSpecified.Checked = hardware.RFRxCapabilitiesSpecified;
            comboBoxHardrFRxCapabilities.SelectedItem = HandleKnxDataTypes.ReadKNXType(hardware.RFRxCapabilities);
            checkBoxHardrFTxCapabilitiesSpecified.Checked = hardware.RFTxCapabilitiesSpecified;
            comboBoxHardrFTxCapabilities.SelectedItem = HandleKnxDataTypes.ReadKNXType(hardware.RFTxCapabilities);
            checkBoxHardNoDownloadWithoutPlugin.Checked = hardware.NoDownloadWithoutPlugin;
            numericHardNonRegRelevantDataVersion.Value = hardware.NonRegRelevantDataVersion;
            textHardInternalDescription.Text = hardware.InternalDescription;
        }

        private void FillApplicationProgramPage(ApplicationProgramRef_t applicationProgram)
        {
            if (!tabControlHardware.TabPages.Contains(this.tabApplicationProgram))
            {
                tabControlHardware.TabPages.Add(this.tabApplicationProgram);
            }
            textAppProgRefId.Text = applicationProgram.RefId;
        }

        private void FillHardware2Program(Hardware2Program_t hardware2Program)
        {
            if (!tabControlHardware.TabPages.Contains(this.tabHardware2Program))
            {
                tabControlHardware.TabPages.Add(this.tabHardware2Program);
            }

            // die Hardware2ProgramRefId in den Teil aus Manufacturer und Hardware und den individellen Teil zerlegen
            // der Manufacturer und die Hardware Id können an anderer Stelle verändert werden
            int indexOfHard2ProgDivider = hardware2Program.Id.IndexOf("_HP-");
            int lengthOfHard2ProgId = hardware2Program.Id.Length;
            textHardware2ProgramIdManufacturerHardware.Text = hardware2Program.Id.Substring(0, indexOfHard2ProgDivider + 4);
            textHardware2ProgramIdIdentifier.Text = hardware2Program.Id.Substring(indexOfHard2ProgDivider + 4, lengthOfHard2ProgId - indexOfHard2ProgDivider - 4);

            HandleKnxDataTypes.ReadKNXType(listH2pMediumTypes, hardware2Program.MediumTypes);
            HandleKnxDataTypes.ReadKNXType(listH2pHash, hardware2Program.Hash);
            HandleKnxDataTypes.ReadKNXType(listH2pCheckSums, hardware2Program.CheckSums);
            HandleKnxDataTypes.ReadKNXType(listH2pLoadedImage, hardware2Program.LoadedImage);
            HandleKnxDataTypes.ReadCouplerCapability(listH2pCouplerCapabilities, hardware2Program.CouplerCapabilities);
        }

        private void FillProductPage(Hardware_tProduct product)
        {
            if (!tabControlHardware.TabPages.Contains(this.tabProduct))
            {
                tabControlHardware.TabPages.Add(this.tabProduct);
            }

            // die ProductId in den Teil aus Manufacturer und Hardware und den individellen Teil zerlegen
            // der Manufacturer und die Hardware Id können an anderer Stelle verändert werden
            int indexOfProdDivider = product.Id.IndexOf("_P-");
            int lengthOfProdId = product.Id.Length;
            textHardwareProductIdManufacturerHardware.Text = product.Id.Substring(0, indexOfProdDivider + 3);
            textHardwareProductIdIdentifier.Text = product.Id.Substring(indexOfProdDivider + 3, lengthOfProdId - indexOfProdDivider - 3);

            textProdText.Text = product.Text;
            textProdOrderNumber.Text = product.OrderNumber;
            checkBoxProdIsRailMounted.Checked = product.IsRailMounted;
            checkBoxProdWidthInMillimeterSpecified.Checked = product.WidthInMillimeterSpecified;
            numericProdWidthInMillimeter.Value = (decimal)product.WidthInMillimeter;
            textProdVisibleDescription.Text = product.VisibleDescription;
            textProdDefaultLanguage.Text = product.DefaultLanguage;
            numericProdNonRegRelevantDataVersion.Value = product.NonRegRelevantDataVersion;
            HandleKnxDataTypes.ReadKNXType(listProdHash, product.Hash);
            textProdInternalDescription.Text = product.InternalDescription;

            dgvHardwareProduct.Rows.Clear();

            selectedHardwareManufacturer = FindSelectedHardwareManufacturer(product.Id);
            LanguageProcessing.FillLanguageDataGridView(selectedHardwareManufacturer.Languages, dgvHardwareProduct, product.Id, "Text");
        }

        private void FillRegistrationPage(RegistrationInfo_t registrationInfo)
        {
            if (!tabControlHardware.TabPages.Contains(this.tabHardwareRegistration))
            {
                tabControlHardware.TabPages.Add(this.tabHardwareRegistration);
            }

            comboBoxHardwareRegRegistrationStatus.SelectedItem = registrationInfo.RegistrationStatus.ToString();
            textHardwareRegRegistrationNumber.Text = registrationInfo.RegistrationNumber;
            textHardwareRegOriginalRegistrationNumber.Text = registrationInfo.OriginalRegistrationNumber;
            checkBoxHardwareRegRegistrationDateSpecified.Checked = registrationInfo.RegistrationDateSpecified;
            if (registrationInfo.RegistrationDate > dateTimeHardwareRegRegistrationDate.MinDate && registrationInfo.RegistrationDate < dateTimeHardwareRegRegistrationDate.MaxDate)
            {
                dateTimeHardwareRegRegistrationDate.Value = registrationInfo.RegistrationDate;
            }
            else
            {
                dateTimeHardwareRegRegistrationDate.Value = dateTimeHardwareRegRegistrationDate.MinDate;
            }
            HandleKnxDataTypes.ReadKNXType(listBoxHardwareRegRegistrationSignature, registrationInfo.RegistrationSignature);
            comboBoxHardwareRegRegistrationKey.SelectedItem = registrationInfo.RegistrationKey;
        }

        private void FillProductBaggagePage(Hardware_tProductBaggage productBaggage)
        {
            if (!tabControlHardware.TabPages.Contains(this.tabProductBaggage))
            {
                tabControlHardware.TabPages.Add(this.tabProductBaggage);
            }
            textPBagRefId.Text = productBaggage.RefId;
        }

        private void FillProductAttributePage(Hardware_tProductAttribute productAttribute)
        {
            if (!tabControlHardware.TabPages.Contains(this.tabProductAttribute))
            {
                tabControlHardware.TabPages.Add(this.tabProductAttribute);
            }
            textPAttrId.Text = productAttribute.Id;
            textPAttrName.Text = HandleKnxDataTypes.ReadKNXType(productAttribute.Name);
            textPAttrValue.Text = productAttribute.Value;
        }

        /// <summary>
        /// Findet den Manufacturer zur angegebenen Hardware oder Product Id
        /// </summary>
        /// <param name="id">Hardware oder Product Id</param>
        /// <returns>zugehöriger Manufacturer</returns>
        private ManufacturerData_tManufacturer FindSelectedHardwareManufacturer(string id)
        {
            foreach (ManufacturerData_tManufacturer manufacturer in KNXprodFile.knxprodHardware.ManufacturerData)
            {
                foreach (var hardware in manufacturer.Hardware)
                {
                    if (hardware.Id == id)
                    {
                        return manufacturer;
                    }
                    foreach (var product in hardware.Products)
                    {
                        if (product.Id == id)
                        {
                            return manufacturer;
                        }
                    }
                }
            }
            return null;
        }


        /****************************************************************************************************************************/
        // Hardware Edit

        private void buttonHardwareSave_Click(object sender, EventArgs e)
        {
            Hardware_t newHardware = treeViewHardware.SelectedNode.Tag as Hardware_t;

            newHardware.Name = Extensions.NullIfEmpty(textHardName.Text);
            newHardware.SerialNumber = Extensions.NullIfEmpty(textHardSerialNumber.Text);
            newHardware.VersionNumber = (ushort)numericHardVersionNumber.Value;
            newHardware.BusCurrentSpecified = checkBoxHardBusCurrentSpecified.Checked;
            newHardware.BusCurrent = (float)numericHardBusCurrent.Value;
            newHardware.Tp256Specified = checkBoxBoxHardTP256Specified.Checked;
            newHardware.Tp256 = checkBoxHardTP256.Checked;
            newHardware.IsAccessory = checkBoxHardIsAccessory.Checked;
            newHardware.HasIndividualAddress = checkBoxHardHasIndividualAddress.Checked;
            newHardware.HasApplicationProgram = checkBoxHardHasApplicationProgram.Checked;
            newHardware.HasApplicationProgram2 = checkBoxHardHasApplicationProgram2.Checked;
            newHardware.IsPowerSupply = checkBoxHardIsPowerSupply.Checked;
            newHardware.IsChoke = checkBoxHardIsChoke.Checked;
            newHardware.IsCoupler = checkBoxHardIsCoupler.Checked;
            newHardware.IsPowerLineRepeater = checkBoxHardIsPowerLineRepeater.Checked;
            newHardware.IsPowerLineSignalFilter = checkBoxHardIsPowerLineSignalFilter.Checked;
            newHardware.IsCable = checkBoxHardIsCable.Checked;
            newHardware.IsIPEnabled = checkBoxHardIsIPEnabled.Checked;
            newHardware.IsRFRetransmitter = checkBoxHardIsRFRetransmitter.Checked;
            newHardware.OriginalManufacturer = Extensions.NullIfEmpty(textHardOriginalManufacturer.Text);
            newHardware.RFRxCapabilitiesSpecified = checkBoxHardrFRxCapabilitiesSpecified.Checked;
            newHardware.RFRxCapabilities = HandleGuiDataTypes.ReadRFRxCapabilities(comboBoxHardrFRxCapabilities.SelectedItem.ToString());
            newHardware.RFTxCapabilitiesSpecified = checkBoxHardrFTxCapabilitiesSpecified.Checked;
            newHardware.RFTxCapabilities = HandleGuiDataTypes.ReadRFTxCapabilities(comboBoxHardrFTxCapabilities.SelectedItem.ToString());
            newHardware.NoDownloadWithoutPlugin = checkBoxHardNoDownloadWithoutPlugin.Checked;
            newHardware.NonRegRelevantDataVersion = (ushort)numericHardNonRegRelevantDataVersion.Value;
            newHardware.InternalDescription = Extensions.NullIfEmpty(textHardInternalDescription.Text);

            treeViewHardware.SelectedNode.Text = newHardware.Name;
        }

        private void buttonProductSave_Click(object sender, EventArgs e)
        {
            Hardware_tProduct newHardwareProduct = treeViewHardware.SelectedNode.Tag as Hardware_tProduct;
            string newProductId = textHardwareProductIdManufacturerHardware.Text + textHardwareProductIdIdentifier.Text;
            LanguageProcessing.ChangeTansUnitRefId(selectedHardwareManufacturer.Languages, newHardwareProduct.Id, newProductId);
            LanguageProcessing.ChangeTansElementRefId(selectedHardwareManufacturer.Languages, newHardwareProduct.Id, newProductId);
            newHardwareProduct.Id = newProductId;
            CatalogGui.ChangeProductIdInCatalog(newHardwareProduct.Id);
            newHardwareProduct.Text = Extensions.NullIfEmpty(textProdText.Text);
            newHardwareProduct.OrderNumber = Extensions.NullIfEmpty(textProdOrderNumber.Text);
            newHardwareProduct.IsRailMounted = checkBoxProdIsRailMounted.Checked;
            newHardwareProduct.WidthInMillimeterSpecified = checkBoxProdWidthInMillimeterSpecified.Checked;
            newHardwareProduct.WidthInMillimeter = (float)numericProdWidthInMillimeter.Value;
            newHardwareProduct.VisibleDescription = Extensions.NullIfEmpty(textProdVisibleDescription.Text);
            newHardwareProduct.DefaultLanguage = Extensions.NullIfEmpty(textProdDefaultLanguage.Text);
            newHardwareProduct.NonRegRelevantDataVersion = (ushort)numericProdNonRegRelevantDataVersion.Value;
            newHardwareProduct.Hash = HandleGuiDataTypes.ReadGuiType(listProdHash);
            newHardwareProduct.InternalDescription = Extensions.NullIfEmpty(textProdInternalDescription.Text);

            LanguageProcessing.WriteLanguageData(selectedHardwareManufacturer.Languages, dgvHardwareProduct, newHardwareProduct.Id, "Text", newHardwareProduct.Id);

            string HardwareProductId = languageProcessingHardware.ReadTranslation(newHardwareProduct.Id, "Text");
            if (HardwareProductId != null)
            {
                treeViewHardware.SelectedNode.Text = HardwareProductId;
            }
            else // falls keien Übersetzung vorhanden ist, wird der Text des products verwendet
            {
                treeViewHardware.SelectedNode.Text = newHardwareProduct.Text;
            }
        }

        private void buttonHardware2ProgramSave_Click(object sender, EventArgs e)
        {
            Hardware2Program_t newHardware2Program = treeViewHardware.SelectedNode.Tag as Hardware2Program_t;
            //newHardware2Program.Id = textHardware2ProgramIdManufacturerHardware.Text + textHardware2ProgramIdIdentifier.Text;
            //ChangeHardware2ProgramIdInCatalogItemId(newHardware2Program.Id);
            newHardware2Program.MediumTypes = HandleGuiDataTypes.ReadListBox(listH2pMediumTypes);
            newHardware2Program.Hash = HandleGuiDataTypes.ReadGuiType(listH2pHash);
            newHardware2Program.CheckSums = HandleGuiDataTypes.ReadGuiType(listH2pCheckSums);
            newHardware2Program.LoadedImage = HandleGuiDataTypes.ReadGuiType(listH2pLoadedImage);
            newHardware2Program.CouplerCapabilities = HandleGuiDataTypes.ReadCouplerCapability(listH2pCouplerCapabilities);
        }
    }
}
