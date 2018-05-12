using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MeetingPlanner.Controllers;
using Final.Data;
using MeetingPlanner.Models;
using System.Net;

namespace XUnitTestProject1
{
    public class EventTest
    {
        private readonly EventController eventT;
        private BaseContext basecontext = null;

        public EventTest()
        {
            eventT = new EventController(basecontext);
        }
        [Fact]
        public void Test1() {
            var idT = 1001;
            var nameT = "Fake";
            var cityT = "Astana";
            var dateT = new DateTime(1, 1, 1, 1, 1, 1);
            var priceT = -1;

            var fake = new Event { id = idT, name = nameT, city = cityT, date = dateT, price = priceT };
            Assert.NotEqual(eventT.Post(fake).ToString(), HttpStatusCode.OK.ToString());
        }
        [Fact]
        public void Test2()
        {
            var idT = 1001;
            var nameT = "Fake";
            var cityT = "Astana";
            var dateT = new DateTime(1, 1, 1, 1, 1, 1);
            var priceT = 130000;

            var fake = new Event { id = idT, name = nameT, city = cityT, date = dateT, price = priceT };
            Assert.NotEqual(eventT.Post(fake).ToString(), HttpStatusCode.OK.ToString());
        }


    }
}
