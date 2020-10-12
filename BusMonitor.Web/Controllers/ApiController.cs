using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMonitor.BLL.Extensions;
using BusMonitor.BLL.Tables;
using BusMonitor.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusMonitor.Web.Controllers
{
    
    [ApiController]
    public class ApiController : ControllerBase
    {
        [HttpGet]
        [Route("api/arrival/cat/{category}/token/{token}/{time}")]
        public TimeTable Arrival ( string category , string token , string time )
        {
            Console.Out.WriteLine("=====================");
            Console.Out.WriteLine( $"START TimeArrivalBus ( {category} , {token} , {time} )");
            Console.Out.WriteLine("=====================");

            TimeTable model = TablesBLL.ArrivalTimes( category , token );

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