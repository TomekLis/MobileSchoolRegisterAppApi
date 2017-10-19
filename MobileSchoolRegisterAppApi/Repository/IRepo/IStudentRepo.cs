using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepo
{
    interface IStudentRepo
    {
        IQueryable<Student> GetStudents();
        Student GetStudentById(string id);
        void DeleteStudent(string id);
        void SaveChanges();
        void AddStudent(Student student);
        void EditStudent(Student student);
    }
}
