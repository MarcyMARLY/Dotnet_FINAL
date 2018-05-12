using Final.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BaseContext(
                serviceProvider.GetRequiredService<DbContextOptions<BaseContext>>()))
            {
                // Look for any movies.
                if (context.Histories.Any())
                {
                    return;   // DB has been seeded
                }
                if (context.Events.Any())
                {
                    return;   // DB has been seeded
                }
                if (context.Cities.Any())
                {
                    return;   // DB has been seeded
                }
                if (context.Buskets.Any())
                {
                    return;   // DB has been seeded
                }
                context.SaveChanges();

            }

        }
    }
}
