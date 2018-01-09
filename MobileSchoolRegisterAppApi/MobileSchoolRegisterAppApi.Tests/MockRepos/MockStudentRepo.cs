using System.Linq;
using MobileSchoolRegisterAppApi.Tests.Contexts;
using Repository.IRepo;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests.MockRepos
{
    internal class MockStudentRepo : IStudentRepo
    {
        private TestSchoolRegisterContext testSchoolRegisterContext;

        private readonly ISchoolRegisterContext _db;

        public MockStudentRepo(ISchoolRegisterContext db)
        {
            _db = db;
        }

        public IQueryable<Student> GetStudents()
        {
            return _db.Students;
        }

        public Student GetStudentById(string id)
        {
            return _db.Students.Find(id);
        }

        public void DeleteStudent(string id)
        { }

        public void SaveChanges()
        {
        }

        public void MarkAsModified(Student student)
        {
        }

        public void GetCoursesRelatedToStudent(Student student)
        {
        }

    }
}