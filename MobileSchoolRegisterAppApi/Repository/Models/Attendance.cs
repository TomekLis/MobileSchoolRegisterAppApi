using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Attendance : StudentActivity
    {
        public bool WasPresent { get; set; }
    }
}