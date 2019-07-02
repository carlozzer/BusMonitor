using BusMonitor.BLL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Json
{
    public class JsonKeyValue
    {
        public string   Name    { get; set; }
        public string   Type    { get; set; }
        public object   Value   { get; set; }

        public JsonKeyValue ( Token token ) { 

            string lexema = token.ToString();

            this.Name = lexema.RemoveFirst().RemoveLast(); 
            this.Type = token.type.ToString();

        }
    }
}
