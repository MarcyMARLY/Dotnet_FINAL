using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Data
{
    public class DbInitilizer
    {
        public static void Initialize(BaseContext context)
        {
            context.Database.EnsureCreated();

            if (context.Cities.Any())
            {
                return;
            }
            if (context.Events.Any())
            {
                return;
            }
            if (context.Buskets.Any())
            {
                return;
            }
            if (context.Histories.Any())
            {
                return;
            }
            context.SaveChanges();
        }
    }
}
