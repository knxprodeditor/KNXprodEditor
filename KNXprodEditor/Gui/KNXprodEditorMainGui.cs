using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using KnxProd.Model;

namespace knxprod_ns
{
    public partial class FormKNXprodEditor : Form
    {
        public static FormKNXprodEditor mFormKNXprodEditor;

        public FormKNXprodEditor()
        {
            InitializeComponent();
            while (tabControlCatHardApp.TabPages.Count > 0)
            {
                tabControlCatHardApp.TabPages.RemoveAt(0);
            }
            this.WindowState = FormWindowState.Maximized;
            this.Text = Config.AppVersion; // die aktuelle Version in die Titelleiste des Fensters schreiben

            mFormKNXprodEditor = this;
            tabCatalogFile.Controls.Add(new CatalogGui());
            tabHardwareFile.Controls.Add(new HardwareGui());
            tabApplicationFile.Controls.Add(new ApplicationProgramGui());
        }

        public static void FillSelectableLanguages()
        {
            while (mFormKNXprodEditor.comboBoxSelectedLanguage.Items.Count > 0)
            {
                mFormKNXprodEditor.comboBoxSelectedLanguage.Items.RemoveAt(0);
            }
            foreach (var manufacturer in KNXprodFile.knxprodCatalog.ManufacturerData)
            {
                // Auswahl der verfügbaren Sprachen laden (Annahme, dass in Catalog.xml, Hardware.xml und Applikation*.xml die selben Sprachen enthalten sind
                foreach (var language in manufacturer.Languages)
                {
                    ComboBoxItem newComboBoxItem = new ComboBoxItem(language.Identifier, language);
                    mFormKNXprodEditor.comboBoxSelectedLanguage.Items.Add(newComboBoxItem);
                    if (language.Identifier == "de-DE")
                    {
                        mFormKNXprodEditor.comboBoxSelectedLanguage.SelectedItem = newComboBoxItem;
                    }
                }
            }
            if (mFormKNXprodEditor.comboBoxSelectedLanguage.SelectedIndex == -1)
            {
                mFormKNXprodEditor.comboBoxSelectedLanguage.SelectedIndex = 0;
            }
            KnxMasterLanguage.SetupLanguageKnxMaster((GetSelectedLanguageComboBoxItem().Tag as LanguageData_t).Identifier);
        }

        /// <summary>
        /// die ausgewählte Sprache wurde verändert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSelectedLanguage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            KnxMasterLanguage.SetupLanguageKnxMaster(((comboBoxSelectedLanguage.SelectedItem as ComboBoxItem).Tag as LanguageData_t).Identifier);
            CatalogGui.mCatalogGui.SetupCatalogTab();
            HardwareGui.mHardwareGui.SetupHardwareTab();
            ApplicationProgramGui.mApplicationProgramGui.SetupApplicationTab();
        }

        public static ComboBoxItem GetSelectedLanguageComboBoxItem()
        {
            return mFormKNXprodEditor.comboBoxSelectedLanguage.SelectedItem as ComboBoxItem;
        }

        /// <summary>
        /// Der Button "Datei öffnen" zum laden einer knxprod Datei
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void knxprodFileOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogKnxprod = new OpenFileDialog
            {
                //InitialDirectory = @"C:\",
                Title = "Browse knxprod Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "knxprod",
                Filter = "knxprod files (*.knxprod)|*.knxprod",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true
            };

            if (openFileDialogKnxprod.ShowDialog() == DialogResult.OK)
            {

                textFileSelection.Text = openFileDialogKnxprod.FileName;
                KNXprodFile.selectedKnxprodToOpenPath = openFileDialogKnxprod.FileName;
                if (Directory.Exists(KNXprodFile.unzippedKnxprodPath))
                {
                    try
                    {
                        Directory.Delete(KNXprodFile.unzippedKnxprodPath, true); //löschen alter Daten aus dem Ordner .\unzippedKnxprod
                    }
                    catch (IOException ioEx)
                    {
                        MessageBox.Show("Bitte Ordner schließen oder Verküpfung löschen:" + ioEx.Source + ioEx.Message);
                    }
                }

                ZipFile.ExtractToDirectory(KNXprodFile.selectedKnxprodToOpenPath, KNXprodFile.unzippedKnxprodPath);

                KNXprodFile.OpenStandardKnxProdFiles(KNXprodFile.unzippedKnxprodPath);
            }
        }

        /// <summary>
        /// Der Button "Datei speichern unter"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void knxprodFileSaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (KNXprodFile.knxprodKnxMaster == null)
            {
                MessageBox.Show("Bitte eine knprod Datei laden", "knxprod Datei Fehler");
                return;
            }

            SaveFileDialog saveFileDialogKnxprod = new SaveFileDialog
            {
                //InitialDirectory = @"C:\",
                Title = "Save knxprod File",
                //CheckFileExists = true,
                //CheckPathExists = true,
                DefaultExt = "knxprod",
                Filter = "knxprod files (*.knxprod)|*.knxprod",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (saveFileDialogKnxprod.ShowDialog() == DialogResult.OK)
            {
                KNXprodFile.CreateKnxprodFile(saveFileDialogKnxprod.FileName);
            }

            // für das erneute Speichern den Pfad ablegen
            textFileSelection.Text = saveFileDialogKnxprod.FileName;
            KNXprodFile.selectedKnxprodToOpenPath = saveFileDialogKnxprod.FileName;
        }

        /// <summary>
        /// Der Button "ConverterEngine Pfad"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConverterEngineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KNXprodFile.SaveConverterEngineDllPath();
        }

        /// <summary>
        /// die Textbox an dem ConverterEngine Auswahl Button mit dem aktuellen Pfad füllen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>    
        private void ConverterEngineToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolStripTextBoxCurrentConverterEnginePath.Text = "aktueller Pfad: " + Properties.Settings.Default.ConverterEnginePath;
        }

        public static void SetTextBoxCurrentConverterEnginePath(string currentConverterEnginePath)
        {
            mFormKNXprodEditor.toolStripTextBoxCurrentConverterEnginePath.Text = "aktueller Pfad: " + currentConverterEnginePath;
        }

        /// <summary>
        /// Das Click Event der Textbox, in der der Pfad zur ConverterEngine.dll angezeigt wird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripTextBoxCurrentConverterEnginePath_Click(object sender, EventArgs e)
        {
            KNXprodFile.SaveConverterEngineDllPath();
        }

        /// <summary>
        /// Der Button "speichern" (speichern an dem Pfad, von dem geöffnet wurde)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void knxprodFileSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KNXprodFile.CreateKnxprodFile(KNXprodFile.selectedKnxprodToOpenPath);
        }



        /// <summary>
        /// Startet einen Timer, um das Label mit der "gespeichert..." Beschriftung nach kurzer Zeit wieder auszublenden
        /// </summary>
        private static System.Timers.Timer knxprodFileSavedLabelTimer;
        public static void StartKnxprodFileSavedLabelTimer()
        {
            // Create a timer with a two second interval.
            knxprodFileSavedLabelTimer = new System.Timers.Timer(4000);
            // Hook up the Elapsed event for the timer. 
            knxprodFileSavedLabelTimer.Elapsed += mFormKNXprodEditor.OnTimedEvent;
            knxprodFileSavedLabelTimer.Enabled = true;

            // Das Label ist normalerweise unsichtbar, aber soll nun für 4 Sekunden sichtbar gemacht werden
            mFormKNXprodEditor.labelKnxprodFileSaved.Visible = true;
        }

        /// <summary>
        /// Durch einen Threadwechsel beim Abarbeiten des Timers muss die GUI Veränderung in eienr extra Funktion durchgeführt werden.
        /// </summary>
        /// <returns>true</returns>
        private bool StopKnxprodFileSavedLabelTimer()
        {
            labelKnxprodFileSaved.Visible = false;
            knxprodFileSavedLabelTimer.Enabled = false;
            return true;
        }

        /// <summary>
        /// Timer Interrupt Routine, wird nach 4s aufgerufen
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            this.Invoke((Func<bool>)StopKnxprodFileSavedLabelTimer);
        }

        public static void ClearCatHardAppTabPages()
        {
            while (mFormKNXprodEditor.tabControlCatHardApp.TabCount > 0) //alle Tabs löschen
            {
                mFormKNXprodEditor.tabControlCatHardApp.TabPages.Clear();
            }
        }
        
        /*******************************************************************************************************************/
        // Catalog Tab
        public static void AddCatalogFileTab()
        {
            if (!mFormKNXprodEditor.tabControlCatHardApp.TabPages.Contains(mFormKNXprodEditor.tabCatalogFile))
            {
                mFormKNXprodEditor.tabControlCatHardApp.TabPages.Add(mFormKNXprodEditor.tabCatalogFile);
            }
        }

        /*******************************************************************************************************************/
        // Hardware Tab
        public static void AddHardwareFileTab()
        {
            if (!mFormKNXprodEditor.tabControlCatHardApp.TabPages.Contains(mFormKNXprodEditor.tabHardwareFile))
            {
                mFormKNXprodEditor.tabControlCatHardApp.TabPages.Add(mFormKNXprodEditor.tabHardwareFile);
            }
        }


        /*******************************************************************************************************************/
        // Application Tab
        public static void AddApplicationFileTab()
        {
            if (!mFormKNXprodEditor.tabControlCatHardApp.TabPages.Contains(mFormKNXprodEditor.tabApplicationFile))
            {
                mFormKNXprodEditor.tabControlCatHardApp.TabPages.Add(mFormKNXprodEditor.tabApplicationFile);
            }
        }

        public static void SelectApplicationFileTab()
        {
            if (mFormKNXprodEditor.tabControlCatHardApp.TabPages.Contains(mFormKNXprodEditor.tabApplicationFile))
            {
                mFormKNXprodEditor.tabControlCatHardApp.SelectTab(mFormKNXprodEditor.tabApplicationFile);
            }
                
        }

        public static bool CheckContainsApplicationFileTab()
        {
            if (mFormKNXprodEditor.tabControlCatHardApp.TabPages.Contains(mFormKNXprodEditor.tabApplicationFile))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Shortcut zum speichern der Daten mittel STRG + s Tastenkombination
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                KNXprodFile.CreateKnxprodFile(KNXprodFile.selectedKnxprodToOpenPath);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}


