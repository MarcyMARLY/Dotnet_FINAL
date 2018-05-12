using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Data;
using MeetingPlanner.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeetingPlanner.Controllers
{
    [Route("api/[controller]")]
    public class BusketController : Controller
    {
        private readonly BaseContext context;
        public BusketController(BaseContext baseContext)
        {
            context = baseContext;
        }
        // GET: api/<controller>
        [HttpGet(Name = "getBuskets")]
        public IEnumerable<Busket> Get()
        {
            var collection = context.Buskets;
            return collection.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var newBusket = context.Buskets.Where(x => x.id == id);
            if (newBusket == null)
            {
                return NotFound();
            }
            return new ObjectResult(newBusket);

        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Busket busket)
        {
            if (busket == null)
            {
                return BadRequest();
            }
            // проверки
            if(context.Buskets.Count() != 0)
            {
                if (context.Buskets.First().city  != busket.city)
                {
                    return BadRequest();
                }
                else
                {
                    var newBusket = context.Buskets.FirstOrDefault(x => x.id == busket.id);
                    if(newBusket != null)
                    {
                        newBusket.id = busket.id;
                        newBusket.name = busket.name;
                        newBusket.city = busket.city;
                        newBusket.date = busket.date;
                        newBusket.price = busket.price;
                        newBusket.numOfTickets = newBusket.numOfTickets + busket.numOfTickets;

                        context.Buskets.Update(newBusket);
                        context.SaveChanges();
                        return NoContent();
                    }
                    else
                    {
                        var newB = new Busket
                        {
                            id = busket.id,
                            name = busket.name,
                            city = busket.city,
                            date = busket.date,
                            price = busket.price,
                            numOfTickets = busket.numOfTickets
                        };
                        context.Buskets.Add(newB);
                        context.SaveChanges();

                        return CreatedAtRoute("getBuskets", new
                        {
                            id = busket.id,
                            name = busket.name,
                            city = busket.city,
                            date = busket.date,
                            price = busket.price,
                            numOfTickets = busket.numOfTickets
                        }, newB);

                    }
                }
            }
            var newBus = new Busket
            {
                id = busket.id,
                name = busket.name,
                city = busket.city,
                date = busket.date,
                price = busket.price,
                numOfTickets = busket.numOfTickets
            };
            context.Buskets.Add(newBus);
            context.SaveChanges();

            return CreatedAtRoute("getBuskets", new
            {
                Id = busket.id,
                Name = busket.name,
                city = busket.city,
                date = busket.date,
                Price = busket.price,
                numOfTickets = busket.numOfTickets
            }, newBus);

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Busket busket)
        {
            var newBusket = context.Buskets.FirstOrDefault(x => x.id == id);
            if (newBusket == null)
            {
                return NotFound();
            }
            newBusket.id = busket.id;
            newBusket.name = busket.name;
            newBusket.city = busket.city;
            newBusket.date = busket.date;
            newBusket.price = busket.price;
            newBusket.numOfTickets = busket.numOfTickets;

            context.Buskets.Update(newBusket);
            context.SaveChanges();
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var busket = context.Buskets.FirstOrDefault(x => x.id == id);
            context.Buskets.Remove(busket);
            context.SaveChanges();
            return NoContent();

        }
    }
}
