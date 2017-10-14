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
    public class StudentRepo : IStudentRepo
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

        public Student GetStudentById(int id)
        {
            Student student = _db.Students.Find(id);
            return student;
        }

        public void DeleteStudent(int id)
        {
            DeleteRelatedStudentActivities(id);
            Student student = _db.Students.Find(id);
            _db.Students.Remove(student);
        }

        private void DeleteRelatedStudentActivities(int id)
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

        public void AddStudent(Student student)
        {
            _db.Students.Add(student);
        }

        public void EditStudent(Student student)
        {
            _db.Entry(student).State = EntityState.Modified;
        }
    }
}