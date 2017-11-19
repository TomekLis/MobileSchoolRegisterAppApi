using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepo
{
    public interface ICourseRepo
    {
        IQueryable<Course> GetCourses();
        Course GetCourseById(int id);
        void DeleteCourse(int id);
        void SaveChanges();
        void AddCourse(Course course);
        void MarkAsModified(Course course);

    }
}
