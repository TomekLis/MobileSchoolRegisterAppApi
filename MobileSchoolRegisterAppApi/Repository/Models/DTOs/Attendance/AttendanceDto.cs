using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Models.DTOs.StudentActivity;

namespace Repository.Models.DTOs.Attendance
{
    public class AttendanceDto : StudentActivityDto
    {
        public bool WasPresent { get; set; }
    }
}