using BusMonitor.BLL.Clients;
using BusMonitor.BLL.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BusMonitor.BLL.Tables
{
    public class TablesBLL
    {
        #region ARRIVAL TIME

        static string RenderEstimatedTime( int seconds ) {

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

        static TimeTable UpdateTable( TimeTable source , Dictionary<string,int> times ) {

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

        public static TimeTable ArrivalTimes( string category , string token ) {

            TimeTable model = ReadCSV( category );
            model.EMTToken = token;
            model.Category = category;

            EMTClient   emt = new EMTClient(); // buses
            AEMETClient met = new AEMETClient(); // weather

            IEnumerable<IGrouping<string,BusLine>> stops = model.Lines.GroupBy<BusLine,string>( b => b.Stop );

            stops.SafeForEach( stop => {

                string[] lines = stop.ToList().Select( s => s.Line ).ToArray();
                Dictionary<string,int> times = emt.TimeArrivalBus( stop.Key , lines , token);
                model = UpdateTable( model , times );
                
            });

            model.Temp = met.ReadTemp().ToString();

            return model;

        }

        #endregion

        #region READ TABLE

        public static TimeTable ReadCSV( string cat ) {

            TimeTable ret = new TimeTable();

            ret.Category = cat.Safe();

            WebClient client = new WebClient();

            //string url = $"{this.Request.Scheme}://{this.Request.Host.Value}/csv/buses.csv";
            string url = "http://busmon.westeurope.cloudapp.azure.com/csv/buses.csv";

            Stream stream = client.OpenRead( url );
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();

            ret.Lines = new List<BusLine>();
            content.SafeSplit( Environment.NewLine ).SafeForEach( (line,idx) => {

                // # es comentario
                if ( !line.TrimStart().StartsWith("#") ) {

                    string category = line.SafeSplit("|").SafeIndexer(0);
                    if ( category.SameText ( cat ) ) {

                        BusLine bl = new BusLine() {

                            Stop = line.SafeSplit("|").SafeIndexer(1),
                            Line = line.SafeSplit("|").SafeIndexer(2),
                            Desc = line.SafeSplit("|").SafeIndexer(3)

                        };

                        ret.Lines.Add ( bl );

                    }
                }
                

            });

            //var fileContents = System.IO.File.ReadAllText( .MapPath(@"~/csv/buses.csv"));

            return ret;

        }

        public static TimeTable ModelWithToken ( string cat ) {

            TimeTable model = ReadCSV( cat );
            
            EMTClient cli = new EMTClient();
            model.EMTToken = cli.Login("carlozzer@gmail.com", "carlo33er@GMAIL.COM");

            return model;

        }

        #endregion
    }
}
