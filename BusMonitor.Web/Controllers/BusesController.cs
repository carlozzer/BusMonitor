using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMonitor.BLL.Tables;
using BusMonitor.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusMonitor.Web.Controllers
{
    public class BusesController : Controller
    {
        public IActionResult Index( string cat )
        {
            string root_url = $"{this.HttpContext.Request.Scheme}://{this.HttpContext.Request.Host.Value}";

            TimeTable model = TablesBLL.ModelWithToken( cat , root_url );
            model = TablesBLL.ArrivalTimes( cat , model.EMTToken , root_url );
            return View( model );
        }
    }
}