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

                // primera linea es head
                if ( idx > 0 ) {

                    BusLine bl = new BusLine()
                    {

                        Stop = line.SafeSplit(",").SafeIndexer(0),
                        Line = line.SafeSplit(",").SafeIndexer(1),
                        Desc = line.SafeSplit(",").SafeIndexer(2),

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
            //model.Lines = new List<BusLine>();
            //model.Lines.Add(new BusLine() { Stop="2705",Line="C2",Time="+20min" , Desc= "Isaac Peral, Hospital Clínico dirección Moncloa" });

            EMTClient cli = new EMTClient();
            model.EMTToken = cli.Login("carlozzer@gmail.com", "carlo33er@GMAIL.COM");


            return View( model );
        }


        public IActionResult TimeArrivalBus( string token , string stop , string line )
        {
            EMTClient cli = new EMTClient();
            // "2705", "C2"
            int seconds = cli.TimeArrivalBus( stop , line , token);
            string time = $"{(seconds / 60).ToString("00")}:{(seconds % 60).ToString("00")}";

            return Content( time );
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
