using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Data;
using MeetingPlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingPlanner.Data
{
    public class EventRepository : IRepository
    {
        private BaseContext context;
        public EventRepository(BaseContext baseContext)
        {
            context = baseContext;
        }
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> Get()
        {
            var collection = context.Events;
            return collection.ToList();
        }

        public IActionResult Get(int id)
        {
            var eventT = this.context.Events.Where(x => x.id == id);
            
            return new ObjectResult(eventT);
        }

        public IActionResult Post(Event eventT)
        {
            throw new NotImplementedException();
        }

        public IActionResult Put(int id, Event eventT)
        {
            throw new NotImplementedException();
        }
    }
}
