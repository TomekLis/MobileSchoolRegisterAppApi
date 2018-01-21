using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using MobileSchoolRegisterAppApi.Tests.TestDbSets;
using Repository.IRepo;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests.Contexts
{
    class TestSchoolRegisterContext : IdentityDbContext, ISchoolRegisterContext
    {
        public TestSchoolRegisterContext()
        {
            this.Attendances = new TestAttendancesDbSet();
            this.Courses = new TestCoursesDbSet();
            this.DaySchedules = new TestDaySchedulesDbSet();
            this.Lessons = new TestLessonsDbSet();
            this.Marks = new TestMarksDbSet();
            this.Students = new TestStudentsDbSet();
            this.StudentGroups = new TestStudentGroupsDbSet();
            this.StudentActivities = new TestStudentActivitiesDbSet();
            this.Teachers = new TestTeachersDbSet();
        }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<DaySchedule> DaySchedules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<StudentActivity> StudentActivities { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public int SaveChangesCount { get; private set; }
    }
}
