using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Lesson
    {
        public Lesson()
        {
            this.StudentActivities = new HashSet<StudentActivity>();
        }
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<StudentActivity> StudentActivities { get; set; }
    }
}