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
    public class HistoryController : Controller
    {
        private readonly BaseContext context;
        public HistoryController(BaseContext baseContext)
        {
            context = baseContext;
        }
        [HttpGet(Name = "getHistories")]
        public IEnumerable<History> Get()
        {
            var collection = context.Histories;
            return collection.ToList();
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var history = this.context.Histories.Where(x => x.id == id);
            if (history == null)
            {
                return NotFound();
            }
            return new ObjectResult(history);
        }
        [HttpPost]
        public IActionResult Post([FromBody] History history)
        {
            if (history == null)
            {
                return BadRequest();
            }
            var oldHistory = context.Histories.FirstOrDefault(x => x.id == history.id);
            if (oldHistory != null)
            {
                oldHistory.id = history.id;
                oldHistory.name = history.name;
                oldHistory.city = history.city;
                oldHistory.date = history.date;
                oldHistory.price = history.price;
                oldHistory.numOfTickets += history.numOfTickets;

                context.Histories.Update(oldHistory);
                context.SaveChanges();
                return NoContent();

            }
            else
            {

                var newHistory = new History
                {
                    id = history.id,
                    name = history.name,
                    city = history.city,
                    date = history.date,
                    price = history.price,
                    numOfTickets = history.numOfTickets
                };
                this.context.Histories.Add(newHistory);
                context.SaveChanges();

                return CreatedAtRoute("getHistories", new
                {
                    id = history.id,
                    name = history.name,
                    city = history.city,
                    date = history.date,
                    price = history.price,
                    numOfTickets = history.numOfTickets
                }, newHistory);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] History history)
        {
            var newHistory = context.Histories.FirstOrDefault(x => x.id == id);
            if (newHistory == null)
            {
                return NotFound();
            }
            newHistory.id = history.id;
            newHistory.name = history.name;
            newHistory.city = history.city;
            newHistory.date = history.date;
            newHistory.price = history.price;
            newHistory.numOfTickets = history.numOfTickets;

            context.Histories.Update(newHistory);
            context.SaveChanges();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var history = context.Histories.FirstOrDefault(x => x.id == id);
            context.Histories.Remove(history);
            context.SaveChanges();
            return NoContent();
        }

    }
}