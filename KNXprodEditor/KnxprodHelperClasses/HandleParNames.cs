using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace knxprod_ns
{
    public partial class HandleParNames
    {
        /// <summary>
        /// Wandelt alle Sonderzeichen in ACSII Code mit führendem Punkt (bspw. _ = .5F)
        /// </summary>
        /// <param name="name">der Parametername in Klartext</param>
        /// <returns>Namensteil der ID in knxprod Schreibweise</returns>
        public static string ExchangeNameToId(string name)
        {
            string id ="";
            foreach (char c in name)
            {
                int unicode = c;
                if (unicode < 48 || (unicode > 57 && unicode < 65) || (unicode > 90 && unicode < 97) || unicode > 122) // wenn Sonderzeichen
                {
                    id += ".";
                    id += unicode.ToString("X2");
                }
                else
                {
                    id += c;
                }
            }
            return id;
        }
    }
}