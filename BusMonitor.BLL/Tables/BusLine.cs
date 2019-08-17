using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Tables
{
    public class BusLine
    {
        public string Stop  { get; set; }
        public string Line  { get; set; }
        public string Time  { get; set; }
        public string Desc  { get; set; }
    }
}
