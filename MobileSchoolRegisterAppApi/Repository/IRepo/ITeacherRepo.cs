using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Models;

namespace Repository.IRepo
{
    public interface ITeacherRepo
    {
        IQueryable<Teacher> GeTeachers();
        Teacher GeTeacherById(string id); 
        void DeleteTeacher(string id);
        void SaveChanges();
        void MarkAsModified(Teacher teacher);

    }
}