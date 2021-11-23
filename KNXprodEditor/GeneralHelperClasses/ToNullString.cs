using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Mit dieser String bzw. Object Extension kann man einen String oder ein Object in String umwandeln auch wenn er null ist.
 */

namespace knxprod_ns
{
    public static class Extensions
    {

        public static string ToNullString(this string str)
        {
            return (str != null) ? str : "";
        }

        public static string ToNullString(this object obj)
        {
            return (obj != null) ? obj.ToString() : "";
        }

        public static string NullIfEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str) ? str : null;
        }
    }
}
