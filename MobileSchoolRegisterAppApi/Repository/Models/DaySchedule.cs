using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository.Models
    public enum Day
    {
        Monday, Tuesday, Wednesday, Thursday, Friday
    }
    public class DaySchedule
    {
        [Key]
        public int Id { get; set; }
        public Day Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}