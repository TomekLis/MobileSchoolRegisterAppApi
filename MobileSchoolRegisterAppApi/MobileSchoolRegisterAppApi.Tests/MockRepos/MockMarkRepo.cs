using System.Data.Entity;
using System.Linq;
using Repository.IRepo;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests.MockRepos
{
    public class MockMarkRepo : IMarkRepo
    {
        private readonly ISchoolRegisterContext _db;

        public MockMarkRepo(ISchoolRegisterContext db)
        {
            _db = db;
        }
        public DbSet<Mark> Marks { get; set; }

        public IQueryable<Mark> GetMarks()
        {
            return _db.Marks;
        }

        public Mark GetMarkById(int id)
        {
            return _db.Marks.Find(id);
        }

        public void DeleteMark(int id) { }

        public void SaveChanges() { }

        public void AddMark(Mark mark) { }

        public void MarkAsModified(Mark mark) { }
    }
}