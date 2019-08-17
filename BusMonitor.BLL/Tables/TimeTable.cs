using BusMonitor.BLL.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace BusMonitor.BLL.Tables
{
    public class TimeTable {

        public string           Temp        { get; set; }
        public List<BusLine>    Lines       { get; set; }
        public string           EMTToken    { get; set; }
        public string           Category    { get; set; }

    }



}
