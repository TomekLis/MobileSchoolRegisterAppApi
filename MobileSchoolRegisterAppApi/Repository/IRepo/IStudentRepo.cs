using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepo
{
    public interface IStudentRepo
    {
        IQueryable<Student> GetStudents();
        Student GetStudentById(string id);
        void DeleteStudent(string id);
        void SaveChanges();
        void MarkAsModified(Student student);
        void GetCoursesRelatedToStudent(Student teacher);
    }
}
