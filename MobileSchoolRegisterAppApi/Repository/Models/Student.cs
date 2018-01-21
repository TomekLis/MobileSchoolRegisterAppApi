using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Repository.Models
{
    public class Student : User
    {
        public Student()
        {
            this.Marks = new HashSet<Mark>();
            this.Attendances = new HashSet<Attendance>();
        }

        public int? StudentsGroupId { get; set; }
        public virtual StudentGroup StudentGroup{ get; set; }
        public int? Age { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}