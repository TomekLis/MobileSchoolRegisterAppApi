using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class StudentActivity
    {   
        [Key]
        public int Id { get; set; }
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}