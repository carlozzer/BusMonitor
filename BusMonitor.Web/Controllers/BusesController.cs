using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMonitor.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusMonitor.Web.Controllers
{
    public class BusesController : Controller
    {
        public IActionResult Index( string cat )
        {
            TimeTable model = TimeTable.ModelWithToken( cat );
            return View( model );
        }
    }
}