using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingPlanner.Controllers
{
    
    public class EventTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public bool isPrice(int val)
        {
            if (val > -1 && val <= 1200) return true;
            else return false;
        }
    }
}