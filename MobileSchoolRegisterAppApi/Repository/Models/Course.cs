using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Course
    {
        public Course()
        {
            this.DaySchedules = new HashSet<DaySchedule>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public string TeacherId { get; set; }
        public int? StudentsGroupId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual StudentGroup StudentGroup { get; set; }
        public virtual ICollection<DaySchedule> DaySchedules { get; set; }
    }
}