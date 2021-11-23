using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnxProd.Model;

namespace knxprod_ns
{
    class KnxMasterLanguage
    {
        public static LanguageProcessing languageProcessingKnxMaster = new LanguageProcessing();
        public static LanguageData_tTranslationUnit selectedTranslationUnitKnxMaster;
        public static string selectedLanguageString = null;

        public static void SetupLanguageKnxMaster(string languageIdentifier)
        {
            selectedLanguageString = languageIdentifier;

            LanguageData_t selectedLanguageKnxMaster = KNXprodFile.knxprodKnxMaster.MasterData.Languages.ToList().Find(x => x.Identifier == selectedLanguageString);
            if (selectedLanguageKnxMaster != null)
            {
                selectedTranslationUnitKnxMaster = selectedLanguageKnxMaster.TranslationUnit.ToList().Find(x => x.RefId == KNXprodFile.knxprodKnxMaster.MasterData.Id);
                languageProcessingKnxMaster.TranslationUnit = selectedTranslationUnitKnxMaster;
            }
            else
            {
                languageProcessingKnxMaster.TranslationUnit = null;
            }

            HandleDatapointTypes.FillDptList();
        }
    }
}
