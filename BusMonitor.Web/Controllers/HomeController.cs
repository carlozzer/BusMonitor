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
        
        [HttpGet]
        [Produces("text/html")]
        public IActionResult Index( )
        {
            //TimeTable model = ModelWithToken( string.Empty );
            //return View( model );
            return Content("cat is mandatory");
        }

        
        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
