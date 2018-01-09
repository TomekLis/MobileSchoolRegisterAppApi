using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Repository.Models
{
    public class StudentGroup
    {
        public StudentGroup()
        {
                this.Students = new HashSet<Student>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(1,8)]
        public int Grade { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

    }
}