﻿using BusMonitor.BLL.Clients;
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

        public static TimeTable ArrivalTimes( string category , string token , string root_url ) {

            TimeTable model = ReadCSV( category , root_url );
            model.EMTToken = token;
            model.Category = category;

            EMTClient   emt = new EMTClient(); // buses
            AEMETClient met = new AEMETClient(); // weather

            model.Stops.SafeForEach( stop => {

                string[] lines = model.LinesByStop( stop );
                List<BusLine> times = emt.TimeArrivalBus( stop , lines , model.EMTToken );
                model.UpdateTimes ( times );

            });

            model.Temp = met.ReadTemp().ToString();

            return model;

        }

        #endregion

        #region READ TABLE

        public static TimeTable ReadCSV( string cat , string root_url ) {

            TimeTable ret = new TimeTable();

            ret.Category = cat.Safe();

            WebClient client = new WebClient();

            string url = $"{root_url}/csv/buses.csv";
            //string url = "http://busmon.westeurope.cloudapp.azure.com/csv/buses.csv";

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
                            Desc = line.SafeSplit("|").SafeIndexer(3).RemoveDiacritics()

                        };

                        ret.Lines.Add ( bl );

                    }
                }
                

            });

            //var fileContents = System.IO.File.ReadAllText( .MapPath(@"~/csv/buses.csv"));

            return ret;

        }

        public static TimeTable ModelWithToken ( string cat , string root_url ) {

            TimeTable model = ReadCSV( cat , root_url );
            
            EMTClient cli = new EMTClient();
            model.EMTToken = cli.Login("carlozzer@gmail.com", "carlo33er@GMAIL.COM");

            return model;

        }

        #endregion
    }
}
