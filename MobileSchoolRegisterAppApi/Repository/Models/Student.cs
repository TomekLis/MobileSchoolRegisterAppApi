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
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Student> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}