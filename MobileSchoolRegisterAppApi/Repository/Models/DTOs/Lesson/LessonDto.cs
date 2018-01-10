using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Models.DTOs.StudentActivity;

namespace Repository.Models.DTOs.Lesson
{
    public class LessonDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? CourseId { get; set; }
        public IEnumerable<StudentActivityDto> studentActivities { get; set; }
    }
}