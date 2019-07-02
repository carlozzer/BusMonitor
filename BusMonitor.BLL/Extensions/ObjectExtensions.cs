using BusMonitor.BLL.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusMonitor.BLL.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToXmlString(this object obj)
        {
            StringBuilder ret = new StringBuilder();

            if ( obj != null) {
                
                Template.Try ( () => {

                    XmlSerializer serializer = new XmlSerializer(obj.GetType());

                    using ( StringWriter writer = new StringWriter(ret) ) {

                        serializer.Serialize( writer , obj );
                    }

                });
            }

            return ret.ToString();
        }

        public static string ToStringOKorNULL ( this object obj ) {

            string ret = "NULL";

            if ( obj != null) {
                
                ret = "OK";
                
            }

            return ret;
        }
    }
}
