using System.Data.Entity;
using System.Linq;
using Repository.IRepo;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests.MockRepos
{
    public class MockCourseRepo : ICourseRepo
    {
        private readonly ISchoolRegisterContext _db;

        public MockCourseRepo(ISchoolRegisterContext db)
        {
            _db = db;
        }
        public DbSet<Course> Courses { get; set; }

        public IQueryable<Course> GetCourses()
        {
            return _db.Courses;
        }

        public Course GetCourseById(int id)
        {
           return _db.Courses.Find(id);
        }

        public void DeleteCourse(int id){}

        public void SaveChanges(){}

        public void AddCourse(Course course){}

        public void MarkAsModified(Course course){}
    }
}