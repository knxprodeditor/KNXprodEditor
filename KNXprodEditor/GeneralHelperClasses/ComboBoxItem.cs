using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knxprod_ns
{
    public class ComboBoxItem
    {
        string _Contents;
        public string Contents
        {
            get { return _Contents; }
            set { _Contents = value; }
        }
        object _Tag;
        public object Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }
        public ComboBoxItem(string contents, object tag)
        {
            this._Contents = contents;
            this._Tag = tag;
        }

        public override string ToString() { return _Contents; }
    }


}
