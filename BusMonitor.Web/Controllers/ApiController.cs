using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMonitor.BLL.AEMET;
using BusMonitor.BLL.EMT;
using BusMonitor.BLL.Extensions;
using BusMonitor.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusMonitor.Web.Controllers
{
    
    [ApiController]
    public class ApiController : ControllerBase
    {

        #region ESTIMATED BUS ARRIVAL TIME


        string RenderEstimatedTime( int seconds ) {

            string ret = string.Empty;

            if ( seconds != 0 ) {

                if ( seconds != 999999 ) {

                    ret = $"{(seconds / 60).ToString("00")}:{(seconds % 60).ToString("00")}";

                } else {

                    ret = "+20:00";
                }

            } else {

                ret = "---";
            }

            return ret;
        }

        TimeTable UpdateTable( TimeTable source , Dictionary<string,int> times ) {

            TimeTable ret = new TimeTable();

            if ( source != null ) {

                ret.Category = source.Category;
                ret.EMTToken = source.EMTToken;
                ret.Lines    = new List<BusLine>();
                ret.Temp     = source.Temp;
                
                source.Lines.SafeForEach( source_item => {

                    if ( times.ContainsKey( source_item.Line ) ) {

                        // updates
                        source_item.Time = RenderEstimatedTime( times[source_item.Line] );
                        ret.Lines.Add ( source_item );

                    } else {

                        ret.Lines.Add( source_item );
                    }

                });
            }

            return ret;
        }

        #endregion

        [HttpGet]
        [Route("api/arrival/cat/{category}/token/{token}")]
        public TimeTable Arrival ( string category , string token )
        {
            Console.Out.WriteLine("=====================");
            Console.Out.WriteLine( $"START TimeArrivalBus ( {category} , {token} )");
            Console.Out.WriteLine("=====================");

            TimeTable model = TimeTable.ReadCSV( category );

            EMTClient   emt = new EMTClient(); // buses
            AEMETClient met = new AEMETClient(); // weather

            IEnumerable<IGrouping<string,BusLine>> stops = model.Lines.GroupBy<BusLine,string>( b => b.Stop );

            stops.SafeForEach( stop => {

                string[] lines = stop.ToList().Select( s => s.Line ).ToArray();
                Dictionary<string,int> times = emt.TimeArrivalBus( stop.Key , lines , token);
                model = UpdateTable( model , times );
                
            });

            model.Temp = met.ReadTemp().ToString();

            Console.Out.WriteLine("=====================");
            Console.Out.WriteLine("TimeArrivalBus");
            Console.Out.WriteLine("=====================");
            Console.Out.WriteLine( model.ToXmlString() );
            Console.Out.WriteLine("=====================");

            Console.Out.WriteLine("=====================");
            Console.Out.WriteLine( $"END TimeArrivalBus ( {category} , {token} )");
            Console.Out.WriteLine("=====================");

            return model;
        }

    }
}