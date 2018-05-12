using System;
using Xunit;
using MeetingPlanner.Controllers;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        private readonly EventTestController eventT;

        public UnitTest1()
        {
            eventT = new EventTestController();
        }

        [Fact]
        public void Test1()
        {
            var res = eventT.isPrice(-2);
            Assert.False(res, " price");
        }
    }
}
