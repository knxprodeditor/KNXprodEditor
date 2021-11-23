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
    public partial class ApplicationProgramGui : UserControl
    {
        public static ApplicationProgramGui mApplicationProgramGui;


        public static ApplicationProgram_t selectedApplicationProgram;

        public static ManufacturerData_tManufacturer selectedApplicationManufacturer;

        public static LanguageData_tTranslationUnit selectedTranslationUnitApplication;

        public static LanguageProcessing languageProcessingApplication = new LanguageProcessing();


        public ApplicationProgramGui()
        {
            InitializeComponent();
            mApplicationProgramGui = this;
            tabAppDynamic.Controls.Add(new DynamicGuiMain());
            tabAppParametertypen.Controls.Add(new ParameterTypesGui());
            tabAppParameter.Controls.Add(new ParameterGui());
            tabComObjects.Controls.Add(new ComObjectsGui());
            tabAppCode.Controls.Add(new CodeGui());
            tabLoadProcedures.Controls.Add(new LoadProceduresGui()); ;
            tabAdressTable.Controls.Add(new AddressTableGui());
            tabAssociationTable.Controls.Add(new AssociationTableGui());
            tabOptions.Controls.Add(new OptionsGui());
            this.Controls.Add(new ContextMenuHandler());
        }

        public void SetupApplicationTab()
        {
            string selectedLanguageString = KnxMasterLanguage.selectedLanguageString;

            if(FormKNXprodEditor.CheckContainsApplicationFileTab())
            {
                DynamicTreeViewGui.mDynamicTreeViewGui.DeleteAllTreeNodes();

                foreach (var manufacturer in KNXprodFile.knxprodApplicationProgram.ManufacturerData)
                {
                    selectedApplicationManufacturer = manufacturer;
                    selectedApplicationProgram = manufacturer.ApplicationPrograms.ToList().Find(x => x.Id == HardwareGui.selectedApplicationProgramRefId);

                    LanguageData_t selectdLanguageApplication = manufacturer.Languages.ToList().Find(x => x.Identifier == selectedLanguageString);
                    if (selectdLanguageApplication != null)
                    {
                        selectedTranslationUnitApplication = selectdLanguageApplication.TranslationUnit.ToList().Find(x => x.RefId == HardwareGui.selectedApplicationProgramRefId);
                    }
                    else
                    {
                        MessageBox.Show("Fehler bei der Auswahl der Sprache! Es konnte für die Applikation die gewählte Sprache nicht gefunden werden.", "Fehler Sprache Applikationsprogramm",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    languageProcessingApplication.TranslationUnit = selectdLanguageApplication.TranslationUnit.ToList().Find(x => x.RefId == HardwareGui.selectedApplicationProgramRefId);

                    DynamicMemoryTable.InitEEPROMView();
                    ParameterTypesGui.InitializeParameterTypeTreeView();
                    DynamicTreeViewGui.InitializeParaKoTreeView();
                    CodeGui.InitializeCodeTreeView();
                    ComObjectsGui.InitializeCommObjectsTreeView();
                    ParameterGui.InitializeParameterTreeView();
                    LoadProceduresGui.InitializeLoadProceduresTreeView();
                    AddressTableGui.InitializeAddressTable();
                    AssociationTableGui.InitializeAssociationTable();
                    OptionsGui.InitializeOptions();

                    if (selectedApplicationProgram.ModuleDefs != null)
                    {
                        MessageBox.Show("ACHTUNG! Dieses knxprod ApplicationProgram bestitzt eine MdoduleDefs Sektion. Dieses wird aktuell im KNXprodEditor nicht ausgewertet und angezeigt!",
                            "ModuleDefs enthalten",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (selectedApplicationProgram.Static.ParameterCalculations != null)
                    {
                        MessageBox.Show("ACHTUNG! Dieses knxprod ApplicationProgram bestitzt eine ParameterCalculations Sektion. Dieses wird aktuell im KNXprodEditor nicht ausgewertet und angezeigt!",
                            "ParameterCalculations enthalten",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                DynamicTreeViewGui.mDynamicTreeViewGui.ExpandFirstTwoLayer();
            }
        }

        public static void InitializeApplicationTab()
        {
            FormKNXprodEditor.AddApplicationFileTab();

            /* hier muss die Application Page aufgerufen werden, als Workaround für einen Bug im DataGridView
            * -> es werden die Row Header nicht angezeigt, wenn die Page nicht sichbar ist, bevor das DataGridView gefüllt wird
            * Alternativ: erste Spalte des DataTable und damit auch DataGridView mit Adressen füllen
            */
            FormKNXprodEditor.SelectApplicationFileTab();

            Cursor.Current = Cursors.WaitCursor; //Zeige Wait cursor, weil das Füllen der Tabellen teilweise recht lange dauert
            mApplicationProgramGui.SetupApplicationTab();
            Cursor.Current = Cursors.Default; //Zeige wieder normalen Cursor, nachdem alle Tabellen gefüllt sind
        }

        public static void SelectAppParameterTab()
        {
            if (mApplicationProgramGui.tabControlAppProgMain.TabPages.Contains(mApplicationProgramGui.tabAppParameter))
            {
                mApplicationProgramGui.tabControlAppProgMain.SelectTab(mApplicationProgramGui.tabAppParameter);
            }
        }

        public static void SelectAppDynamicTab()
        {
            if (mApplicationProgramGui.tabControlAppProgMain.TabPages.Contains(mApplicationProgramGui.tabAppDynamic))
            {
                mApplicationProgramGui.tabControlAppProgMain.SelectTab(mApplicationProgramGui.tabAppDynamic);
            }
        }
        

    }
}
