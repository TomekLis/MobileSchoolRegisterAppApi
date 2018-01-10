using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.IRepo;
using Repository.Models;

namespace Repository.Repo
{
    public class LessonRepo : DbContext, ILessonRepo
    {
        private readonly ISchoolRegisterContext _db;

        public LessonRepo(ISchoolRegisterContext db)
        {
            _db = db;
        }
        public IQueryable<Lesson> GetLessons()
        {
            //_db.Database.Log = message => Trace.WriteLine(message);
            return _db.Lessons.AsNoTracking();
        }

        public Lesson GetLessonById(int id)
        {
            Lesson lesson = _db.Lessons.Find(id);
            return lesson;
        }

        public void DeleteLesson(int id)
        {
            Lesson lesson = _db.Lessons.Find(id);
            _db.Lessons.Remove(lesson);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void AddLesson(Lesson lesson)
        {
            _db.Lessons.Add(lesson);
        }


        public void MarkAsModified(Lesson lesson)
        {
            Entry(lesson).State = EntityState.Modified;
        }
    }
}
