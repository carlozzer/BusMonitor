using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusMonitor.Web.Models;
using BusMonitor.BLL.EMT;
using System.Net;
using System.IO;
using System.Data;
using BusMonitor.BLL.Extensions;
using BusMonitor.BLL.AEMET;

namespace BusMonitor.Web.Controllers
{
    public class HomeController : Controller
    {
        #region CSV


        TimeTable ReadCSV() {

            TimeTable ret = new TimeTable();

            WebClient client = new WebClient();
            string url = $"{this.Request.Scheme}://{this.Request.Host.Value}/csv/buses.csv";
            Stream stream = client.OpenRead( url );
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();

            ret.Lines = new List<BusLine>();
            content.SafeSplit( Environment.NewLine ).SafeForEach( (line,idx) => {

                // # es comentario
                if ( !line.TrimStart().StartsWith("#") ) {

                    BusLine bl = new BusLine()
                    {

                        Stop = line.SafeSplit("|").SafeIndexer(0),
                        Line = line.SafeSplit("|").SafeIndexer(1),
                        Desc = line.SafeSplit("|").SafeIndexer(2),

                    };

                    ret.Lines.Add ( bl );
                }
                

            });

            //var fileContents = System.IO.File.ReadAllText( .MapPath(@"~/csv/buses.csv"));

            return ret;

        }

        #endregion


        public IActionResult Index()
        {
            TimeTable model = ReadCSV();
            
            EMTClient cli = new EMTClient();
            model.EMTToken = cli.Login("carlozzer@gmail.com", "carlo33er@GMAIL.COM");


            return View( model );
        }


        public IActionResult TimeArrivalBus( string token )
        {
            TimeTable model = ReadCSV();

            EMTClient   emt = new EMTClient(); // buses
            AEMETClient met = new AEMETClient(); // weather

            model.Lines.SafeForEach( line => {

                int seconds = emt.TimeArrivalBus( line.Stop , line.Line , token);
                line.Time = $"{(seconds / 60).ToString("00")}:{(seconds % 60).ToString("00")}";

            });

            model.Temp = met.ReadTemp().ToString();
            
            return Json( model );
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
