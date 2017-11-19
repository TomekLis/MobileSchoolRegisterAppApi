using System.Data.Entity;
using Repository.IRepo;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests.Contexts
{
    class TestSchoolRegisterContext : ISchoolRegisterContext
    {
        public TestSchoolRegisterContext()
        {
            this.Courses = new TestCoursesDbSet();
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
        public int SaveChanges()
        {
            return 0;
        }

        public Database Database { get; }
    }
}
