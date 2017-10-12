using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class StudentsGroup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(1,8)]
        public int Grade { get; set; }
       }
}