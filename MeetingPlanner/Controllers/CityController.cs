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
    public class CityController : Controller
    {
        private readonly BaseContext context;
        public CityController(BaseContext baseContext)
        {
            context = baseContext;
        }

        [HttpGet(Name = "getCities")]
        public IEnumerable<City> Get()
        {
            var collection = context.Cities;
            return collection.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var city = this.context.Cities.Where(x => x.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            return new ObjectResult(city);
        }

        [HttpPost]
        public IActionResult Post([FromBody]City city)
        {
            if (city == null)
            {
                return BadRequest();
            }
            var newCity = new City
            {
                Id = city.Id,
                Name = city.Name

            };
            this.context.Cities.Add(newCity);
            context.SaveChanges();
            return CreatedAtRoute("getCities", new
            {
                Id = city.Id,
                Name = city.Name
            }, newCity);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var city = context.Cities.FirstOrDefault(x => x.Id == id);
            context.Cities.Remove(city);
            context.SaveChanges();
            return NoContent();
        }
    }
}