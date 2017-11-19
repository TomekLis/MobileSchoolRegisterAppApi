using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Repository.IRepo;

namespace Repository.Models.Contexts
{
    public class SchoolRegisterContext : IdentityDbContext, ISchoolRegisterContext
    {
        public SchoolRegisterContext() : base("DefaultConnection")
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public static SchoolRegisterContext Create()
        {
            return new SchoolRegisterContext();
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // required by Identity classes
            base.OnModelCreating(modelBuilder);

            // using System.Data.Entity.ModelConfiguration.Conventions;
            //Turnds off the convention that automatically creates plural name for table names
            // 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Turns off cascade deletion
            // CascadeDelete will be applied by Fluent API
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}