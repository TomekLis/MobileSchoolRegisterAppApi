using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StudentsGroupId { get; set; }
        public virtual StudentGroup StudentGroup{ get; set; }
    }
}