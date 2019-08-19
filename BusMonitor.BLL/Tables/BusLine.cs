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
        
        public string Desc  { get; set; }
        public int    Seconds { get; set; }
        public string Time { get {

            string ret = string.Empty;

            if ( this.Seconds != 0 ) {

                if ( this.Seconds != 999999 ) {

                    ret = $"{(this.Seconds / 60).ToString("00")}:{(this.Seconds % 60).ToString("00")}";

                } else {

                    ret = "+20:00";
                }

            } else {

                ret = "---";
            }

            return ret;
        }} 


    }
}
