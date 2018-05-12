using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MeetingPlanner.Controllers;

namespace XUnitTestProject1
{
    public class UnitTest2

    {
        private readonly HistoryTestController eventT;

        public UnitTest2()
        {
            eventT = new HistoryTestController();
        }

        [Fact]
        public void Test1()
        {
            var res = eventT.isPrice(-100);
            Assert.False(res, " price");
        }

        [Fact]
        public void Test2()
        {
            var res = eventT.isNumOfTickets(-100);
            Assert.False(res, " tickets");
        }
    }
}
