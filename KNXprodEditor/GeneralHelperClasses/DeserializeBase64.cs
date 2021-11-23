using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace knxprod_ns
{
    public static class DeserializeBase64
    {
        public static T DeserializeWithBinaryData<T>(this XDocument xDoc)
        {
            //var xDoc = el.ToXmlDocument();
            using (var ms = new MemoryStream())
            {
                xDoc.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(ms);
            }
        }
    }
}
