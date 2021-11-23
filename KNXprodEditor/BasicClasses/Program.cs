using KnxProd.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using knxprod_ns;
using System.Windows.Forms;

namespace knxprod_ns
{
    public enum Images
    {
        application,
        channel,
        parameterBlock,
        parameter,
        comObject,
        choose,
        when,
        comObjectParameterBlock,
        separator
    }
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormKNXprodEditor());

        }
    }
}




