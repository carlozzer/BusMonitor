using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMonitor.Web.Models
{
    public class TimeTable
    {
        public string Temp { get; set; }
     
        public List<TimeLine> Lines { get; set; }
        public string EMTToken { get; set; }
    }
}
