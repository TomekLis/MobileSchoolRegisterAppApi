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
    public class StudentRepo : DbContext, IStudentRepo
    {
        private readonly ISchoolRegisterContext _db;

        public StudentRepo(ISchoolRegisterContext db)
        {
            _db = db;
        }
        public IQueryable<Student> GetStudents()
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            return _db.Students.AsNoTracking();
        }

        public Student GetStudentById(string id)
        {
            Student student = _db.Students.Find(id);
            return student;
        }

        public void DeleteStudent(string id)
        {
            DeleteRelatedStudentActivities(id);
            Student student = _db.Students.Find(id);
            _db.Students.Remove(student);
        }

        private void DeleteRelatedStudentActivities(string id)
        {
            var list = _db.StudentActivities.Where(sa => sa.StudentId == id);
            foreach (var activity in list)
            {
                _db.StudentActivities.Remove(activity);
            }
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void MarkAsModified(Student student)
        {
            Entry(student).State = EntityState.Modified;
        }

        public void GetCoursesRelatedToStudent(Student teacher)
        {
            Entry(teacher).Collection<Course>(t => t.StudentGroup.Courses).Load();
        }

        public void GetCoursesRelatedToTeacher(Student student)
        {
            Entry(student).Collection<Course>(t => t.StudentGroup.Courses).Load();
        }
    }
}