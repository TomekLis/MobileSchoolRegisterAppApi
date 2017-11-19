using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepo
{
    public interface ISchoolRegisterContext
    {
        DbSet<Attendance> Attendances { get; set; }
        DbSet<Course>  Courses { get; set; }
        DbSet<DaySchedule> DaySchedules { get; set; }
        DbSet<Lesson> Lessons { get; set; }
        DbSet<Mark> Marks { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<StudentGroup> StudentGroups { get; set; }
        DbSet<StudentActivity> StudentActivities { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        int SaveChanges();
        Database Database { get; }
    }
}
