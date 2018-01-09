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
        void DeleteTeacher(string id);
        void SaveChanges();
        void MarkAsModified(Teacher teacher);
        void GetCoursesRelatedToTeacher(Teacher teacher);
    }
}