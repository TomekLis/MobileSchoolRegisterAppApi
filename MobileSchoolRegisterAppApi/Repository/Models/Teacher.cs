using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Repository.Models
{ 
    public class Teacher : User
    {
        public Teacher()
        {
            this.Courses = new HashSet<Course>();
        }
        [NotMapped]
        public string FullName => FirstName + " " + LastName;

        public virtual ICollection<Course> Courses { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Teacher> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}