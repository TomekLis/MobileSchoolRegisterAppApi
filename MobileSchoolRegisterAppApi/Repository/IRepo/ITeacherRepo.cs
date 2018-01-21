using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Models;

namespace Repository.IRepo
{
    public interface ITeacherRepo
    {
        IQueryable<Teacher> GeTeachers();
        Teacher GetTeacherById(string id);
        IEnumerable<Student> GetTeacherStudents(Teacher teacher);
        void DeleteTeacher(string id);
        void SaveChanges();
        void MarkAsModified(Teacher teacher);
    }
}