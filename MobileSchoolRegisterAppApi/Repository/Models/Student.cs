using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Repository.Models
{
    public class Student : User
    {
        public int StudentsGroupId { get; set; }
        public virtual StudentGroup StudentGroup{ get; set; }
        public int? Age { get; set; }
    }
}