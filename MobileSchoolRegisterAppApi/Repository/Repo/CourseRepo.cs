using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Repository.IRepo;
using Repository.Models;

namespace Repository.Repo
{
    public class CourseRepo : DbContext, ICourseRepo
    {
        private readonly ISchoolRegisterContext _db;

        public CourseRepo(ISchoolRegisterContext db)
        {
            _db = db;
        }
        public IQueryable<Course> GetCourses()
        {
            //_db.Database.Log = message => Trace.WriteLine(message);
            return _db.Courses.AsNoTracking();
        }

        public Course GetCourseById(int id)
        {
            Course course = _db.Courses.Find(id);
            return course;
        }

        public void DeleteCourse(int id)
        {
            Course course = _db.Courses.Find(id);
            _db.Courses.Remove(course);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void AddCourse(Course course)
        {
            _db.Courses.Add(course);
        }

        public void MarkAsModified(Course course)
        {
            _db.Entry(course).State = EntityState.Modified;
        }
    }
}