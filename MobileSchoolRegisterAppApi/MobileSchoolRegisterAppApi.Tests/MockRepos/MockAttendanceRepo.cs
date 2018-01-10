using System.Data.Entity;
using System.Linq;
using Repository.IRepo;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests.MockRepos
{
    public class MockAttendanceRepo : IAttendanceRepo
    {
        private readonly ISchoolRegisterContext _db;

        public MockAttendanceRepo(ISchoolRegisterContext db)
        {
            _db = db;
        }
        public DbSet<Attendance> Attendances { get; set; }

        public IQueryable<Attendance> GetAttendances()
        {
            return _db.Attendances;
        }

        public Attendance GetAttendanceById(int id)
        {
            return _db.Attendances.Find(id);
        }

        public void DeleteAttendance(int id) { }

        public void SaveChanges() { }

        public void AddAttendance(Attendance attendance) { }

        public void MarkAsModified(Attendance attendance) { }
    }
}