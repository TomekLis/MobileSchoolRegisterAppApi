﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Course
    {
        public Course()
            this.DaySchedules = new List<DaySchedule>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public int TeacherId { get; set; }
        public int StudentsGroupId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual StudentsGroup StudentsGroup { get; set; }
        public virtual ICollection<DaySchedule> DaySchedules { get; set; }
    }
}