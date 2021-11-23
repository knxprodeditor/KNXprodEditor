using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnxProd.Model;

namespace knxprod_ns
{
    public class LanguageProcessing
    {
        LanguageData_tTranslationUnit translationUnitField;

        public LanguageProcessing()
        {

        }

        public LanguageData_tTranslationUnit TranslationUnit
        {
            get
            {
                return this.translationUnitField;
            }
            set
            {
                this.translationUnitField = value;
            }
        }

        /*
         * alle Texte aus der Translation hintereinander in einem String zurückgeben
         */
        public string ReadTranslation(string refId)
        {
            string translationText = null;
            LanguageData_tTranslationUnitTranslationElement translationElement = translationUnitField.TranslationElement.ToList().Find(x => x.RefId == refId);
            if (translationElement != null)
            {
                foreach (LanguageData_tTranslationUnitTranslationElementTranslation translation in translationElement.Translation.ToList())
                {
                    translationText += translation.Text + " ";
                }
            }
            return translationText;
        }

        public string ReadTranslation(string refId, string attr)
        {
            string translationText = null;
            if (translationUnitField != null)
            {
                LanguageData_tTranslationUnitTranslationElement translationElement = translationUnitField.TranslationElement.ToList().Find(x => x.RefId == refId);
                if (translationElement != null)
                {
                    foreach (LanguageData_tTranslationUnitTranslationElementTranslation translation in translationElement.Translation.ToList())
                    {
                        if (translation.AttributeName == attr)
                        {
                            translationText += translation.Text;
                        }
                    }
                }
            }
            return translationText;
        }

        /// <summary>
        /// Füllt die Übersetzungen einer ID in ein DataGridView
        /// </summary>
        // Übergabeparameter:
        //      languageData = der Einstiegspunkt der knxprod <Languages>
        //      dgv = zu befüllendes DataGridView
        //      refId = die in den Translations zu suchende ID 
        //      attr = Attribut, welches in das DataGridView gefüllt werden soll (z.B. "Name", "Text", "FuntionText", ...) 

        public static void FillLanguageDataGridView(LanguageData_t[] languageData, DataGridView dgv, string refId, string attr)
        {
            dgv.Rows.Clear();
            if (languageData != null)
            {
                foreach (LanguageData_t language in languageData)
                {
                    LanguageData_tTranslationUnitTranslationElement translationElement = null;
                    string translationText = "";
                    foreach (var transUnit in language.TranslationUnit)
                    {
                        if (transUnit.TranslationElement != null)
                        {
                            translationElement = transUnit.TranslationElement.ToList().Find(x => x.RefId == refId);
                            if (translationElement != null)
                            {
                                break;
                            }
                        }
                    }

                    if (translationElement != null)
                    {
                        foreach (LanguageData_tTranslationUnitTranslationElementTranslation translation in translationElement.Translation.ToList())
                        {
                            if (translation.AttributeName == attr)
                            {
                                translationText += translation.Text;
                            }
                        }
                    }

                    dgv.Rows.Add(language.Identifier, translationText);
                }
            }
        }




        /// <summary>
        /// Überträgt die Übersetzungen aus einem DataGridView zu einer ID in die knxprod Daten
        /// </summary>
        /// <param name="languageData">die geladene LanguageData</param>
        /// <param name="dgv">der DataGridView, welcher die Daten enthält</param>
        /// <param name="refId">die zu bearbeitende RefId</param>
        /// <param name="attr">das Attribut, welches bearbeitet werden soll (z.B. "Text")</param>
        public static void WriteLanguageData(LanguageData_t[] languageData, DataGridView dgv, string refId, string attr, string translationUntitId)
        {
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                LanguageData_tTranslationUnitTranslationElement translationElement = null;
                LanguageData_t language = languageData.ToList().Find(x => x.Identifier == dgvRow.Cells[0].Value.ToString());
                
                // das TranslationElement suchen
                foreach (LanguageData_tTranslationUnit transUnit in language.TranslationUnit)
                {
                    if (transUnit.TranslationElement != null)
                    {
                        translationElement = transUnit.TranslationElement.ToList().Find(x => x.RefId == refId);
                        if (translationElement != null)
                        {
                            break;
                        }
                    }
                }

                if (translationElement != null)
                {
                    bool attributeFound = false;
                    //Element gefunden, bedeutet, ID hat bereits eine Übersetzung und wird überschrieben
                    foreach (LanguageData_tTranslationUnitTranslationElementTranslation translation in translationElement.Translation.ToList())
                    {
                        if (translation.AttributeName == attr)
                        {
                            attributeFound = true;

                            if (dgvRow.Cells[1].Value != null && dgvRow.Cells[1].Value.ToString() != "")
                            {
                                translation.Text = dgvRow.Cells[1].Value.ToString();
                            }
                            else
                            {
                                translation.Text = "";
                            }
                        }
                    }
                    // das Attribut (z.B. "FunctionText") wurde nicht gefunden und muss daher neu angelegt werden
                    if(attributeFound == false)
                    {
                        LanguageData_tTranslationUnitTranslationElementTranslation newTranslationElementTranslation = new LanguageData_tTranslationUnitTranslationElementTranslation();
                        translationElement.Translation = HandleArrayFunctions.Add(translationElement.Translation, newTranslationElementTranslation);
                        newTranslationElementTranslation.AttributeName = attr;

                        if (dgvRow.Cells[1].Value != null && dgvRow.Cells[1].Value.ToString() != "")
                        {
                            newTranslationElementTranslation.Text = dgvRow.Cells[1].Value.ToString();
                        }
                        else
                        {
                            newTranslationElementTranslation.Text = "";
                        }
                    }
                }
                else
                {
                    //Element wurde nicht gefunden, daher wird es neu angelegt
                    LanguageData_tTranslationUnitTranslationElement newTransUnitElement = new LanguageData_tTranslationUnitTranslationElement();
                    LanguageData_tTranslationUnitTranslationElementTranslation[] newTransUnitElementTranslation = new LanguageData_tTranslationUnitTranslationElementTranslation[0];
                    LanguageData_tTranslationUnitTranslationElementTranslation newElementTranslation = new LanguageData_tTranslationUnitTranslationElementTranslation();
                    newTransUnitElement.Translation = newTransUnitElementTranslation;
                    newTransUnitElement.RefId = refId;

                    newElementTranslation.AttributeName = attr;
                    newElementTranslation.Text = dgvRow.Cells[1].Value.ToString();

                    if (newElementTranslation.Text != "")
                    {
                        //Translation an TranslationElement anhängen
                        newTransUnitElement.Translation = HandleArrayFunctions.Add(newTransUnitElement.Translation, newElementTranslation); 

                        LanguageData_tTranslationUnit selectedTransUnit = language.TranslationUnit.ToList().Find(x => x.RefId == translationUntitId);
                        if (selectedTransUnit == null)
                        {
                            LanguageData_tTranslationUnit newTranslationUnit = new LanguageData_tTranslationUnit();
                            language.TranslationUnit = HandleArrayFunctions.Add(language.TranslationUnit, newTranslationUnit);
                            newTranslationUnit.RefId = translationUntitId;
                            selectedTransUnit = newTranslationUnit;
                        }

                        if (selectedTransUnit.TranslationElement == null)
                        {
                            selectedTransUnit.TranslationElement = new LanguageData_tTranslationUnitTranslationElement[0];
                        }
                        //Das Translation Element an die Translation Unit anhängen
                        selectedTransUnit.TranslationElement = HandleArrayFunctions.Add(selectedTransUnit.TranslationElement, newTransUnitElement); 
                    }
                }
            }
        }

        /// <summary>
        /// Fügt nur die Spachen in ein dataGridView ein (für die Erstellung eines neuen Elements)
        /// </summary>
        /// <param name="languageData"></param>
        /// <param name="dgv"></param>
        public static void PrepareLanguageGridView(LanguageData_t[] languageData, DataGridView dgv)
        {
            dgv.Rows.Clear();
            foreach (LanguageData_t language in languageData)
            {
                dgv.Rows.Add(language.Identifier, "");
            }
        }

        /// <summary>
        /// Tauscht die alte RefId eines TranslationElementes gegen die neue RefId aus
        /// </summary>
        /// <param name="languageData"></param>
        /// <param name="oldRefId"></param>
        /// <param name="newRefId"></param>
        public static void ChangeTansElementRefId(LanguageData_t[] languageData, string oldRefId, string newRefId)
        {
            foreach (LanguageData_t language in languageData)
            {
                LanguageData_tTranslationUnitTranslationElement translationElement = null;
                foreach (LanguageData_tTranslationUnit transUnit in language.TranslationUnit)
                {
                    if (transUnit.TranslationElement != null)
                    {
                        translationElement = transUnit.TranslationElement.ToList().Find(x => x.RefId == oldRefId);
                        if (translationElement != null)
                        {
                            break;
                        }
                    }
                }

                if (translationElement != null)
                {
                    translationElement.RefId = newRefId;
                }
            }
        }

        /// <summary>
        /// Tauscht die alte RefId einer TranslationUnit gegen die neue RefId aus
        /// </summary>
        /// <param name="languageData"></param>
        /// <param name="oldRefId"></param>
        /// <param name="newRefId"></param>
        public static void ChangeTansUnitRefId(LanguageData_t[] languageData, string oldRefId, string newRefId)
        {
            foreach (LanguageData_t language in languageData)
            {
                foreach (LanguageData_tTranslationUnit transUnit in language.TranslationUnit)
                {
                    if (transUnit.RefId == oldRefId)
                    {
                        transUnit.RefId = newRefId;
                    }
                }
            }
        }

        /// <summary>
        /// Löscht die Lange Daten zur angegebenen RefId
        /// </summary>
        /// <param name="languageData"></param>
        /// <param name="refId"></param>
        public static void DeleteLanguageData(LanguageData_t[] languageData, string refId)
        {
            foreach (LanguageData_t language in languageData)
            {
                foreach (LanguageData_tTranslationUnit transUnit in language.TranslationUnit)
                {
                    if (transUnit.TranslationElement != null)
                    {
                        int translationElementIndex = transUnit.TranslationElement.ToList().FindIndex(x => x.RefId == refId);
                        if (translationElementIndex >= 0)
                        {
                            // Element gefunden, nun löschen
                            LanguageData_tTranslationUnit selectedTransUnit = language.TranslationUnit.ToList().Find(x => x.RefId == ApplicationProgramGui.selectedApplicationProgram.Id); //die Translation Unit hat die ID des Application Programs

                            selectedTransUnit.TranslationElement = HandleArrayFunctions.Delete(selectedTransUnit.TranslationElement, translationElementIndex); //Das Translation Element aus der Translation Unit löschen
                        }
                    }
                }
            }
        }
    }
}
