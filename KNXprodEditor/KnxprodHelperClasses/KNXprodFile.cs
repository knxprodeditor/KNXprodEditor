using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnxProd.Model;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Reflection;
using System.Timers;

namespace knxprod_ns
{
    public class KNXprodFile
    {
        // hierein werden die gesamten Daten der einzelnen XML Files geladen
        public static KNX knxprodKnxMaster;
        public static KNX knxprodCatalog;
        public static KNX knxprodHardware;
        public static KNX knxprodApplicationProgram;

        // Merker der Namespaces (Verionen) der XML Dateien
        private static string originalKnxMasterNamespace;
        private static string originalCatalogNamespace;
        private static string originalHardwareNamespace;
        private static string originalAppProgramNamespace;

        // Die Ordnernamen, in denen die XML Daten entpackt und zum exoprt zwischengespeichert werden
        public static string unzippedKnxprodPath = @".\unzippedKnxprod\"; // hierhin wird die knxprod Datei entpackt
        public static string knxprodExportPath = @".\exportedKnxprod\"; // hier werden die XML Daten gespeichert, bevor sie als knxprod verpackt werden

        // Merker für den Pfad der aktuell geöffneten knxprod
        public static string selectedKnxprodToOpenPath;

        // Merker und Hilfsvariablen
        public static string[] knxprodSubDirs;
        private static string knxprodSubDirName;
        
        

        /// <summary>
        /// Die knx_master.xml, Catalog.xml und Hardware.xml öffnen (bisher immer nur eine Datei dieser Art pro knxprod gesehen)
        /// </summary>
        /// <param name="path"></param>
        public static void OpenStandardKnxProdFiles(string path)
        {
            knxprodSubDirs = Directory.GetDirectories(path);

            // knx_master.xml lesen
            XmlSerializer knxMasterSerializer = new XmlSerializer(typeof(KNX));
            XDocument knxMasterDoc = XDocument.Load(path + @"\knx_master.xml");
            originalKnxMasterNamespace = knxMasterDoc.Root.Name.NamespaceName;
            rewriteNamespace(knxMasterDoc.Root, "http://knx.org/xml/project/20");
            rewriteLastAttribute(knxMasterDoc.Root, "http://knx.org/xml/project/20");
            knxprodKnxMaster = DeserializeBase64.DeserializeWithBinaryData<KNX>(knxMasterDoc);

            DirectoryInfo dir = new DirectoryInfo(knxprodSubDirs[0]);
            knxprodSubDirName = dir.Name; //in der Hoffnung, dass immer nur ein Ordner in der knxprod enthalten ist...

            // Catalog.xml lesen
            XmlSerializer catalogSerializer = new XmlSerializer(typeof(KNX));
            XDocument catalogDoc = XDocument.Load(knxprodSubDirs[0] + @"\Catalog.xml");
            originalCatalogNamespace = catalogDoc.Root.Name.NamespaceName;
            rewriteNamespace(catalogDoc.Root, "http://knx.org/xml/project/20");
            rewriteLastAttribute(catalogDoc.Root, "http://knx.org/xml/project/20");
            knxprodCatalog = (KNX)catalogSerializer.Deserialize(catalogDoc.CreateReader());

            FormKNXprodEditor.ClearCatHardAppTabPages(); //alle Tabs löschen

            FormKNXprodEditor.FillSelectableLanguages();
            KnxMasterLanguage.SetupLanguageKnxMaster((FormKNXprodEditor.GetSelectedLanguageComboBoxItem().Tag as LanguageData_t).Identifier);
            CatalogGui.mCatalogGui.InitializeCatalogTreeView();

            // Hardware.xml lesen
            XmlSerializer hardwareSerializer = new XmlSerializer(typeof(KNX));
            var hardwareDoc = XDocument.Load(knxprodSubDirs[0] + @"\Hardware.xml");
            originalHardwareNamespace = hardwareDoc.Root.Name.NamespaceName;
            rewriteNamespace(hardwareDoc.Root, "http://knx.org/xml/project/20");
            rewriteLastAttribute(hardwareDoc.Root, "http://knx.org/xml/project/20");
            knxprodHardware = (KNX)hardwareSerializer.Deserialize(hardwareDoc.CreateReader());

            // alle eventuell vorher geladenen Daten auf null setzen, damit die neu geöffneten Daten geladen werden
            // nötig, falls in einer laufenden Session die gleiche knxprod noch einmal geöffnet wird
            HardwareGui.loadedProductId = null;
            HardwareGui.loadedHardware2ProgramId = null;
            loadedKnxprodApplicationProgramRefId = null;
        }


        private static string loadedKnxprodApplicationProgramRefId; // Merker, welches ApplicationProgram aktuell geladen ist
        
        /// <summary>
        /// Das Application Program anhand der in der Hardware selektierten ID öffnen
        /// </summary>
        public static void ReadApplicationProgramFile()
        {
            // ApplicationProgram lesen
            XmlSerializer appProgramSerializer = new XmlSerializer(typeof(KNX));
            XDocument appProgramDoc = XDocument.Load(knxprodSubDirs[0] + @"\" + HardwareGui.selectedApplicationProgramRefId + ".xml");
            originalAppProgramNamespace = appProgramDoc.Root.Name.NamespaceName;
            rewriteNamespace(appProgramDoc.Root, "http://knx.org/xml/project/20");
            rewriteLastAttribute(appProgramDoc.Root, "http://knx.org/xml/project/20");
            KNX tempKnxprodApplicationProgram = DeserializeBase64.DeserializeWithBinaryData<KNX>(appProgramDoc);
            if (HardwareGui.selectedApplicationProgramRefId != loadedKnxprodApplicationProgramRefId)
            {
                knxprodApplicationProgram = tempKnxprodApplicationProgram;
                ApplicationProgramGui.InitializeApplicationTab();
                loadedKnxprodApplicationProgramRefId = HardwareGui.selectedApplicationProgramRefId;
            }
        }

        /*
         * Namespace der Quell XML Dateien ändern.
         * Annahme: die neueste Schema Version ist abwärtkompatibel
         * daher wird hier die Schema Version der XSD eingetragen
         * der Deserializer benötigt die korrekte Schema Version
         * Quelle: https://stackoverflow.com/questions/11103615/how-to-change-xml-namespace-of-certain-element
         */
        private static void rewriteNamespace(XElement el, string xmlNamespace)
        {
            XNamespace aw = xmlNamespace; //  "http://knx.org/xml/project/20";
            el.Name = (aw + el.Name.LocalName) as XName;

            if (!el.HasElements)
                return;

            foreach (XElement d in el.Elements())
                rewriteNamespace(d, xmlNamespace);
        }


        private static void rewriteLastAttribute(XElement el, string xmlNamespace)
        {
            if (el.LastAttribute != null)
            {
                el.LastAttribute.Value = xmlNamespace;//"http://knx.org/xml/project/20";
            }
        }

        

        /// <summary>
        /// den Pfad der ConverterEngine.dll eingeben lassen und in den Settings ablegen
        /// </summary>
        public static void SaveConverterEngineDllPath()
        {
            OpenFileDialog openFileDialogConvEngine = new OpenFileDialog
            { 
                InitialDirectory = @"C:\Program Files (x86)\ETS5\CV",
                Title = "Browse ConverterEngine.dll",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "dll",
                Filter = "Knx.Ets.Converter.ConverterEngine.dll file|Knx.Ets.Converter.ConverterEngine.dll",
                FilterIndex = 2,
                
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            if (openFileDialogConvEngine.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialogConvEngine.FileName.Contains("Knx.Ets.Converter.ConverterEngine.dll"))
                {

                    string converterEnginePath = openFileDialogConvEngine.FileName;

                    Properties.Settings.Default.ConverterEnginePath = converterEnginePath;
                    Properties.Settings.Default.Save(); // Saves settings in application configuration file
                    FormKNXprodEditor.SetTextBoxCurrentConverterEnginePath(converterEnginePath);
                }
                else
                {
                    MessageBox.Show("Flasche Datei ausgewählt!", "ConverterEngine Auswahl",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        

        /// <summary>
        /// Der komplette Ablauf der Dateizusammenstellung bs zur Zip Datei Erstellung
        /// </summary>
        /// <param name="knxProdSavePath">der Pfad an dem die .knxprod Datei gespeichert werden soll</param>
        public static void CreateKnxprodFile(string knxProdSavePath)
        {
            SaveKnxprodFilesToFolder(knxprodExportPath);

            if (!SignKnxprod(knxprodExportPath))
            {
                DialogResult dialogResult = MessageBox.Show("Die Signierung der knxprod konnte nicht durchgeführt werden. Die Datei Knx.Ets.Converter.ConverterEngine.dll konnte nicht gefunden werden. " +
                    "\nSoll eine knxprod Datei ohne Signierung erstellt werden?", "knxprod Signierungsfehler",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            if (File.Exists(knxProdSavePath))
            {
                try
                {
                    File.Delete(knxProdSavePath);
                }
                catch (IOException ioEx)
                {
                    MessageBox.Show(ioEx.Message);
                    return;
                }
            }
            ZipFile.CreateFromDirectory(knxprodExportPath, knxProdSavePath);

            FormKNXprodEditor.StartKnxprodFileSavedLabelTimer();
        }

        

        /// <summary>
        /// stellt alle XML Dateien aus dem Speicher und den geöffeneten Dateien her
        /// </summary>
        /// <param name="path">der Pfad, in dem die Daten gespeichert werden sollen</param>
        public static void SaveKnxprodFilesToFolder(string path)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true); //löschen alter Daten aus dem Ordner .\unzippedKnxprod
                }
                catch (IOException ioEx)
                {
                    MessageBox.Show("Bitte Ordner schließen oder Verküpfung löschen:" + ioEx.Source + ioEx.Message);
                }
            }


            DirectoryInfo di = Directory.CreateDirectory(path + knxprodSubDirName);

            XmlSerializer knxprodSerializer = new XmlSerializer(typeof(KNX));

            /* // Hier wurde Anfangs die knx_master aus den Daten geschrieben, hat aber zu Problemen geführt, da nicht alle Daten in den deserialisierten Daten enthalten waren
             * // Problem festgestellt mit SB_RTR_LCD_AirQ_RH.vd1 mittels ETS 5.7.2 umgewandelt zu knxprod
             * // Abhilfe: die knx_master.xml wird aus den Original Daten vollständig kopiert
             * // -> ist kein Problem, da in der knx_master.xml nichts verändert wird
             * 
            TextWriter knxprodMasterWriter = new StreamWriter(knxprodExportPath + "knx_master.xml");
            knxprodSerializer.Serialize(knxprodMasterWriter, knxprodKnxMaster);
            knxprodMasterWriter.Close();
            RewriteNamespaceXmlFile(knxprodExportPath + "knx_master.xml", originalKnxMasterNamespace);
            */

            File.Copy(unzippedKnxprodPath + "knx_master.xml", path + "knx_master.xml");

            TextWriter knxprodCatalogWriter = new StreamWriter(di.FullName + "\\Catalog.xml");
            knxprodSerializer.Serialize(knxprodCatalogWriter, knxprodCatalog);
            knxprodCatalogWriter.Close();
            RewriteNamespaceXmlFile(di.FullName + "\\Catalog.xml", originalCatalogNamespace);

            TextWriter knxprodHardwareWriter = new StreamWriter(di.FullName + "\\Hardware.xml");
            knxprodSerializer.Serialize(knxprodHardwareWriter, knxprodHardware);
            knxprodHardwareWriter.Close();
            RewriteNamespaceXmlFile(di.FullName + "\\Hardware.xml", originalHardwareNamespace);

            if (knxprodApplicationProgram != null) // wenn ein ApplicationProgram ausgewählt wurde
            {
                TextWriter knxprodAppProgWriter = new StreamWriter(di.FullName + "\\" + HardwareGui.selectedApplicationProgramRefId + ".xml");
                knxprodSerializer.Serialize(knxprodAppProgWriter, knxprodApplicationProgram);
                knxprodAppProgWriter.Close();
                RewriteNamespaceXmlFile(di.FullName + "\\" + HardwareGui.selectedApplicationProgramRefId + ".xml", originalAppProgramNamespace);
            }

            // kopieren der unveränderten ApplicationProgramms an den angegebenen Pfad
            CopyNotSelectedApplicationPrograms(path);
        }

        /// <summary>
        /// Ersetzt den Namespace (Version) der XML Datei
        /// </summary>
        /// <param name="filePath">die Datei, die verändert werden soll</param>
        /// <param name="newNamespace">der Namespace, der in die Datei geschriebenw erden soll</param>
        private static void RewriteNamespaceXmlFile(string filePath, string newNamespace)
        {
            System.Xml.XmlDocument XMLOriginalDoc = new XmlDocument();
            System.Xml.XmlDocument XMLFinalDoc = new XmlDocument();

            // load an xml document with namespace to be changed.
            XMLOriginalDoc.Load(filePath);

            // Now load original document into new document, and modify namespace in the new document
            XMLFinalDoc.LoadXml(XMLOriginalDoc.OuterXml.Replace(XMLOriginalDoc.DocumentElement.NamespaceURI, newNamespace));

            // Save the document at the same path as original
            XMLFinalDoc.Save(filePath);
        }

        /// <summary>
        /// Dieser Abschnitt wurde aus dem Beispiel von https://github.com/KARDUINO/SignKnxProd übernommen
        /// </summary>
        /// <param name="signDir">das zu signierdende Verzeichnis</param>
        private static bool SignKnxprod(string signDir)
        {

            string dllLocation = Properties.Settings.Default.ConverterEnginePath;

            if (!System.IO.File.Exists(dllLocation))
            {
                return false;
            }

            var asm = Assembly.LoadFrom(dllLocation);
            // leider ist die aktuelle Version mit der funktionierenden Version Nummerngleich...
            // ToDo: herausfinden, wie man die funktionierende DLL detektiert
            // Version version = asm.GetName().Version;
            // Version versionOk = new Version(5, 6, 241, 33672);
            // int cmp = version.CompareTo(versionOk);

            var type = asm.GetType("Knx.Ets.Converter.ConverterEngine.ConverterEngine");

            var mi = type.GetMethod("SignOutputFiles", BindingFlags.Static | BindingFlags.NonPublic);
            mi.Invoke(null, new object[] { signDir });

            return true;
        }


        /// <summary>
        /// kopieren der nicht angewählten ApplicationProgram XML Dateien
        /// </summary>
        /// <param name="path">Pfad an den die Daten kopiert werden sollen</param>        
        private static void CopyNotSelectedApplicationPrograms(string path)
        {
            List<string> ApplicationProgramRefs = new List<string>();
            foreach (ManufacturerData_tManufacturer manufacturer in knxprodHardware.ManufacturerData)
            {
                foreach (Hardware_t hardware in manufacturer.Hardware)
                {
                    foreach (Hardware2Program_t Hardware2Program in hardware.Hardware2Programs)
                    {
                        foreach(ApplicationProgramRef_t appProgRef in Hardware2Program.ApplicationProgramRef)
                        {
                            if (!ApplicationProgramRefs.Contains(appProgRef.RefId))
                            {
                                ApplicationProgramRefs.Add(appProgRef.RefId);
                            }
                        }
                    }
                }
            }

            foreach (string appProg in ApplicationProgramRefs)
            {
                if (ApplicationProgramGui.selectedApplicationProgram != null)
                {
                    if (appProg != ApplicationProgramGui.selectedApplicationProgram.Id)
                    {
                        File.Copy(unzippedKnxprodPath + knxprodSubDirName + "\\" + appProg + ".xml", path + knxprodSubDirName + "\\" + appProg + ".xml");
                    }
                }
                else // wenn noch kein ApplicationProgramm ausgewählt wurde, soll einfach alles kopiert werden
                {
                    File.Copy(unzippedKnxprodPath + knxprodSubDirName + "\\" + appProg + ".xml", path + knxprodSubDirName + "\\" + appProg + ".xml");
                }
            }
        }




    }
}