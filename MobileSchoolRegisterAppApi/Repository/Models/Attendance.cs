using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Attendance : StudentActivity
    {
        //TODO: check whether parent constructor gets called without declaration in child class
        public Attendance() : base()
        {

        }
        public bool WasPresent { get; set; }
    }
}