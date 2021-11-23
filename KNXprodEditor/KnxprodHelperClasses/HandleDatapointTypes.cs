using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnxProd.Model;

namespace knxprod_ns
{
    class HandleDatapointTypes
    {
        public class DatapointListBoxData
        {
            public string DPST { get; set; }
            public string DptText { get; set; }
            public uint SizeInBit { get; set; }
        }

        private static List<DatapointListBoxData> knxMasterDatapointData = new List<DatapointListBoxData>();

        public static void FillDptList()
        {
            foreach (DatapointType_t datapointType in KNXprodFile.knxprodKnxMaster.MasterData.DatapointTypes)
            {
                foreach (DatapointType_tDatapointSubtype datapointSubtype in datapointType.DatapointSubtypes)
                {

                    string dptTranslationText = KnxMasterLanguage.languageProcessingKnxMaster.ReadTranslation(datapointSubtype.Id, "Text");

                    string showText = datapointType.Id.Replace("-", " ") + "." + datapointSubtype.Number.ToString("D3") + " " + dptTranslationText; // aus z.B. DPT-1 und Nummer 2 wird DPT 1.002 Boolesch

                    knxMasterDatapointData.Add(new DatapointListBoxData() { DPST = datapointSubtype.Id, DptText = showText, SizeInBit = datapointType.SizeInBit });
                }
            }
        }


        public static void FillDptInListbox(ListBox listbox, string[] data)
        {
            listbox.DisplayMember = "DptText";
            listbox.DataSource = knxMasterDatapointData;

            if (data != null)
            {
                int index = 0;
                bool datapointFound = false;

                foreach (DatapointListBoxData listBoxDataItem in knxMasterDatapointData)
                {
                    foreach (string dpst in data)
                    {
                        if (listBoxDataItem.DPST == dpst)
                        {
                            // Listbox Item selektieren
                            listbox.SelectedIndex = index;
                            datapointFound = true;
                            break;
                        }
                    }
                    if (datapointFound)
                    {
                        break;
                    }
                    index++;
                }
            }
            else
            {
                listbox.SelectedIndex = -1;
            }
        }
    }
}
