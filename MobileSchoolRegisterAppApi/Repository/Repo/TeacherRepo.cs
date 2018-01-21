using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Repository.IRepo;
using Repository.Models;

namespace Repository.Repo
{
    public class TeacherRepo :DbContext, ITeacherRepo
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