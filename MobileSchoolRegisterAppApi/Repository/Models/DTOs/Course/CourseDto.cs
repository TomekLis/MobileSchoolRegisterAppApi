using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Models.DTOs.DaySchedule;

namespace Repository.Models.DTOs.Course
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public string TeacherId { get; set; }
        public int? StudentsGroupId { get; set; }
        public  IEnumerable<DayScheduleDto> DaySchedules { get; set; }
    }
}