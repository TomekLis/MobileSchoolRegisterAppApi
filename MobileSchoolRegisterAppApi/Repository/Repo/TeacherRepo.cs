using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Repository.IRepo;
using Repository.Models;

namespace Repository.Repo
{
    public class TeacherRepo : DbContext, ITeacherRepo
    {
        private readonly ISchoolRegisterContext _db;

        public TeacherRepo(ISchoolRegisterContext db)
        {
            _db = db;
        }
        public IQueryable<Teacher> GeTeachers()
        {
            return _db.Teachers.AsNoTracking();
        }

        public Teacher GetTeacherById(string id)
        {
            Teacher teacher = _db.Teachers.Find(id);
            return teacher;
        }

        public IEnumerable<Student> GetTeacherStudents(Teacher teacher)
        {
            var students = from s in _db.Students
                           join sg in _db.StudentGroups on s.StudentGroup.Id equals sg.Id
                           join dbCourse in _db.Courses on sg.Id equals dbCourse.StudentGroup.Id
                           join dbTeacher in _db.Teachers on dbCourse.Teacher.Id equals dbTeacher.Id
                           where dbTeacher.Id == teacher.Id
                           select s;
            
            return students.Distinct();
        }


        public void DeleteTeacher(string id)
        {
            Teacher teacher = _db.Teachers.Find(id);
            _db.Teachers.Remove(teacher);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
        public void MarkAsModified(Teacher teacher)
        {
            _db.Entry(teacher).State = EntityState.Modified;
        }

    }
}