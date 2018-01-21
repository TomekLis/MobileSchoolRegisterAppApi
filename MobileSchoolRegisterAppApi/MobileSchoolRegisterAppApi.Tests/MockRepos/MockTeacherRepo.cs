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

        public Teacher GetTeacherById(string id)
        {
            return _db.Teachers.Find(id);
        }

        public IEnumerable<Student> GetTeacherStudents(Teacher teacher)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Mark> GetStudentMarks(string studentId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Attendance> GetStudentAttendances(string studentId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Mark> GetStudentMarks(Student student)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Attendance> GetStudentAttendances(Student student)
        {
            throw new NotImplementedException();
        }

        public void DeleteTeacher(string id)
        {        }

        public void SaveChanges()
        {
        }

        public void MarkAsModified(Teacher teacher)
        {
        }

        IQueryable<Student> ITeacherRepo.GetTeacherStudents(Teacher teacher)
        {
            throw new NotImplementedException();
        }
    }
}
