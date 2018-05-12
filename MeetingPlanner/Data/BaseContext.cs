
using MeetingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Data
{
    public class BaseContext: DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        { }
        public DbSet<City> Cities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Busket> Buskets { get; set; }
        public DbSet<History> Histories { get; set; }

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<Busket>().ToTable("Buskets");
            modelBuilder.Entity<History>().ToTable("Histories");
        }
    }
}
