using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class DaySchedule
    {
        [Key]
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }  
    }
}