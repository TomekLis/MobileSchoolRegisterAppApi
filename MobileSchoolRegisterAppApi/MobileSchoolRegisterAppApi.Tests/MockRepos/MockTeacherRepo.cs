using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.IRepo;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests.MockRepos
{
    class MockTeacherRepo : ITeacherRepo
    {
        private readonly ISchoolRegisterContext _db;

        public MockTeacherRepo(ISchoolRegisterContext db)
        {
            _db = db;
        }
        public IQueryable<Teacher> GeTeachers()
        {
            return _db.Teachers;
        }

        public Teacher GeTeacherById(string id)
        {
            return _db.Teachers.Find(id);
        }

        public void DeleteTeacher(string id)
        {        }

        public void SaveChanges()
        {
        }

        public void MarkAsModified(Teacher teacher)
        {
        }

        public void GetCoursesRelatedToTeacher(Teacher teacher)
        {
        }
    }
}
