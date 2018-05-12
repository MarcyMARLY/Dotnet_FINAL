using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Data;
using MeetingPlanner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingPlanner.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CityreportController : Controller
    {
        private readonly BaseContext context;
        public CityreportController(BaseContext baseContext)
        {
            context = baseContext;
        }
        [HttpGet("{stDay}/{endDay}/{city}")]
        public int Get(DateTime stDay, DateTime endDay, string city)
        {

            var collection = context.Histories.Where(x => x.date >= stDay && x.date <= endDay && x.city == city);
            var total = 1;
            foreach(History col in collection)
            {
                total += col.price * col.numOfTickets;
            }
            return total;
        }
    }
}