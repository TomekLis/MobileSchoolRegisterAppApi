using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Models.DTOs.Lesson;

namespace Repository.Models.DTOs.StudentActivity
{
    public class StudentActivityDto
    {
        public int Id { get; set; }
        public LessonDto Lesson{get; set; }
    }
}