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
    public class ReportController : Controller
    {
        private readonly BaseContext context;
        public ReportController(BaseContext baseContext)
        {
            context = baseContext;
        }
        [HttpGet("{stDay}/{endDay}")]
        public IEnumerable<History> Get(DateTime stDay, DateTime endDay)
        {

            var collection = context.Histories.Where(x=>x.date>=stDay && x.date<=endDay);
            return collection.ToList();
        }
    }
}