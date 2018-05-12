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
    public class FilterController : Controller
    {
        private readonly BaseContext context;
        public FilterController(BaseContext baseContext)
        {
            context = baseContext;
        }
        [HttpGet]
        public IEnumerable<Event> Get()
        {

            var collection = context.Events;
            Console.WriteLine("new0");
            return collection.ToList();
        }
        [HttpGet("{city}/{stDay}/{endDay}/{stPrice}/{endPrice}")]
        public IEnumerable<Event> Get(string city, DateTime stDay, DateTime endDay,int stPrice, int endPrice)
        {
          
            var collection = context.Events;
            Console.WriteLine("newnew0");
            Console.WriteLine(stDay );

            if (city == null && (stDay== null && endDay == null) && (stPrice == 0 && endPrice ==0))
            {
                Console.WriteLine("newnew1");
                return collection.ToList();
            }
            if (city == null && (stDay.Equals(new DateTime(1900, 12, 31, 0, 0, 0)) && endDay.Equals(new DateTime(1900, 12, 31, 0, 0, 0))) && (stPrice >= 0 && endPrice > 0))
            {
                Console.WriteLine("newnew2");
                return collection.Where(x=>(x.price>=stPrice && x.price<=endPrice)).ToList();
            }
            if (city == null && (!stDay.Equals(new DateTime(1900,12,31,0,0,0)) && !endDay.Equals(new DateTime(1900, 12, 31, 0, 0, 0))) && (stPrice == 0 && endPrice == 0))
            {
                Console.WriteLine("newnew3");
                return collection.Where(x => (x.date >= stDay && x.date <= endDay)).ToList();
            }
            if (city != null && (stDay.Equals(new DateTime(1900, 12, 31, 0, 0, 0)) && endDay.Equals(new DateTime(1900, 12, 31, 0, 0, 0))) && (stPrice == 0 && endPrice == 0))
            {
                Console.WriteLine("newnew4");
                return collection.Where(x => (x.city == city )).ToList();
            }
            if (city == null && (!stDay.Equals(new DateTime(1900, 12, 31, 0, 0, 0)) && !endDay.Equals(new DateTime(1900, 12, 31, 0, 0, 0))) && (stPrice >= 0 && endPrice > 0))
            {
                Console.WriteLine("newnew5");
                return collection.Where(x => (x.date >= stDay && x.date <= endDay) && (x.price >= stPrice && x.price <= endPrice)).ToList();
            }
            if (city != null && (stDay.Equals(new DateTime(1900, 12, 31, 0, 0, 0)) && endDay.Equals(new DateTime(1900, 12, 31, 0, 0, 0))) && (stPrice >= 0 && endPrice > 0))
            {
                Console.WriteLine("newnew6");
                return collection.Where(x => (x.price >= stPrice && x.price <= endPrice) && (x.city == city)).ToList();
            }
            if (city != null && (!stDay.Equals(new DateTime(1900, 12, 31, 0, 0, 0)) && !endDay.Equals(new DateTime(1900, 12, 31, 0, 0, 0))) && (stPrice == 0 && endPrice == 0))

            {
                Console.WriteLine("newnew7");
                return collection.Where(x => (x.date >= stDay && x.date <= endDay) && (x.city == city)).ToList();
            }
            if (city != null && (!stDay.Equals(new DateTime(1900, 12, 31, 0, 0, 0)) && !endDay.Equals(new DateTime(1900, 12, 31, 0, 0, 0))) && (stPrice != -1 && endPrice != 0))
            {
                Console.WriteLine("newnew8");
                return collection.Where(x => (x.date >= stDay && x.date <= endDay) && (x.city == city) && (x.price >= stPrice && x.price <= endPrice)).ToList();
            }
            return collection.ToList();
        }

    }
}