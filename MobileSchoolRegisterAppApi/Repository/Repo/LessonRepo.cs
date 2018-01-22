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

        public void AddLesson(Lesson passedLesson)
        {
            Lesson lesson = new Lesson();
            if (passedLesson.Course?.Id !=null)
            {
                lesson.Course = _db.Courses.Find(passedLesson.Course.Id);
                lesson.Date = DateTime.Today;
                _db.Lessons.Add(lesson);
                _db.SaveChanges();

                foreach (var passedStudent in passedLesson.Course.StudentGroup.Students)
                {
                    var attendance = new Attendance();
                    attendance.WasPresent = passedStudent.Attendances.FirstOrDefault() != null ? passedStudent.Attendances.FirstOrDefault().WasPresent : false;
                    var student = _db.Students.Find(passedStudent.Id);
                    attendance.Student = student;
                    attendance.Lesson = lesson;
                    _db.Attendances.Add(attendance);
                    _db.SaveChanges();
                }
            }
        }


        public void MarkAsModified(Lesson lesson)
        {
            _db.Entry(lesson).State = EntityState.Modified;
        }
    }
}
