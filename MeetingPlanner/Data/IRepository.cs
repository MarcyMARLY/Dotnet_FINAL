using MeetingPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingPlanner.Data
{
    public interface IRepository
    {
        IEnumerable<Event> Get();
        IActionResult Get(int id);
        IActionResult Post(Event eventT);
        IActionResult Put(int id, Event eventT);
        IActionResult Delete(int id);
    }
}
