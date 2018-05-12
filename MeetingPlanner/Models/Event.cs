using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingPlanner.Models
{
    public class Event
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public DateTime date { get; set; }
        [Range(0, 1200, ErrorMessage = "Smth wrong with age")]
        public int price { get; set; }
    }
}
