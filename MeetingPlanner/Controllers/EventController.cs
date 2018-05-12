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
    public class EventController : Controller
    {
        private readonly BaseContext context;
        public EventController(BaseContext baseContext)
        {
            context = baseContext;
        }
        [HttpGet(Name = "getEvents")]
        public IEnumerable<Event> Get()
        {
            var collection = context.Events;
            return collection.ToList();
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var eventT = this.context.Events.Where(x => x.id == id);
            if (eventT == null)
            {
                return NotFound();
            }
            return new ObjectResult(eventT);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Event eventT)
        {
            if(eventT == null)
            {
                return BadRequest();
            }
            if(!(eventT.price>-1 && eventT.price < 12000))
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var newEvent = new Event
                {
                    id = eventT.id,
                    name = eventT.name,
                    city = eventT.city,
                    date = eventT.date,
                    price = eventT.price
                };
                this.context.Events.Add(newEvent);
                context.SaveChanges();

                return Ok();
            }else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] Event eventT)
        {
            var newEvent = context.Events.FirstOrDefault(x => x.id == id);
            if(newEvent == null)
            {
                return NotFound();
            }
            newEvent.id = eventT.id;
            newEvent.name = eventT.name;
            newEvent.city = eventT.city;
            newEvent.date = eventT.date;
            newEvent.price = eventT.price;

            context.Events.Update(newEvent);
            context.SaveChanges();
            return NoContent();    

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var eventT = context.Events.FirstOrDefault(x => x.id == id);
            context.Events.Remove(eventT);
            context.SaveChanges();
            return NoContent();
        }
    }
}