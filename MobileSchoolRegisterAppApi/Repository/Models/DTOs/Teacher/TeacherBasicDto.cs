using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository.Models.DTOs.Teacher
{
    public class TeacherBasicDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string  FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}