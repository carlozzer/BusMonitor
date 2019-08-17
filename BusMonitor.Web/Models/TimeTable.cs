using BusMonitor.BLL.EMT;
using BusMonitor.BLL.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BusMonitor.Web.Models
{
    public class TimeTable
    {
        public string           Temp        { get; set; }
        public List<BusLine>    Lines       { get; set; }
        public string           EMTToken    { get; set; }
        public string           Category    { get; set; }

        #region PUBLIC STATIC

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

        #endregion

        #region BUILD MODEL

        public static TimeTable ModelWithToken ( string cat ) {

            TimeTable model = ReadCSV( cat );
            
            EMTClient cli = new EMTClient();
            model.EMTToken = cli.Login("carlozzer@gmail.com", "carlo33er@GMAIL.COM");

            return model;

        }

        #endregion
    }
}
