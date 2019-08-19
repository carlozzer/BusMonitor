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

        public List<string> Stops {

            get {

                List<string> ret = new List<string>();

                if ( this.Lines.SafeCount() > 0 ) {

                    IEnumerable<IGrouping<string,BusLine>> groups = this.Lines.GroupBy<BusLine,string>( b => b.Stop );

                    groups.SafeForEach( group => {

                        ret.Add( group.Key );

                    });

                }

                return ret;
            }
        }


        public string[] LinesByStop( string stop ) {

            List<string> ret = new List<string>();

            if ( this.Lines.SafeCount() > 0 ) {

                ret = this.Lines.Where( l => l.Stop == stop ).Select<BusLine,string>( b => b.Line ).ToList();
            }

            return ret.ToArray();
        }

    }



}
