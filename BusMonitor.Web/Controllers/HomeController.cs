using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusMonitor.Web.Models;
using BusMonitor.BLL.EMT;

namespace BusMonitor.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            TimeTable model = new TimeTable();

            EMTClient cli = new EMTClient();
            model.EMTToken = cli.Login("carlozzer@gmail.com", "carlo33er@GMAIL.COM");

            model.Lines = new List<TimeLine>();
            model.Lines.Add(new TimeLine() { Stop="2705",Bus="C2",Time="+20min" , Desc= "Isaac Peral, Hospital Clínico dirección Moncloa" });
            //int seconds = cli.TimeArrivalBus("2705", "C2", token);
            //string line = $"2705 C2 {(seconds / 60).ToString("00")}:{(seconds % 60).ToString("00")}";

            return View( model );
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
