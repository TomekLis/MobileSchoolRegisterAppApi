using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Models.DTOs.Attendance;
using Repository.Models.DTOs.Mark;

namespace Repository.Models.DTOs.Student
{
    public class StudentBasicDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<MarkDto> Marks { get; set; }
        public IEnumerable<AttendanceDto> Attendances { get; set; }
    }
}