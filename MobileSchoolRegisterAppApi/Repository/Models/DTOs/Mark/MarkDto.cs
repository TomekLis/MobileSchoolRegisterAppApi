using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Models.DTOs.StudentActivity;

namespace Repository.Models.DTOs.Mark
{
    public class MarkDto : StudentActivityDto
    {
        public MarkValue MarkValue { get; set; }
        public Importance Importance { get; set; }

    }
}