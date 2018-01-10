using System.Data.Entity;
using System.Linq;
using Repository.IRepo;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests.MockRepos
{
    public class MockLessonRepo : ILessonRepo
    {
        private readonly ISchoolRegisterContext _db;

        public MockLessonRepo(ISchoolRegisterContext db)
        {
            _db = db;
        }
        public DbSet<Lesson> Lessons { get; set; }

        public IQueryable<Lesson> GetLessons()
        {
            return _db.Lessons;
        }

        public Lesson GetLessonById(int id)
        {
            return _db.Lessons.Find(id);
        }

        public void DeleteLesson(int id) { }

        public void SaveChanges() { }

        public void AddLesson(Lesson lesson) { }

        public void MarkAsModified(Lesson lesson) { }
    }
}