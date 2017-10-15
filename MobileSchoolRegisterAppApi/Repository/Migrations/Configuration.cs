using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Repository.Models;
using Repository.Models.Contexts;

namespace Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.Contexts.SchoolRegisterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Models.Contexts.SchoolRegisterContext context)
        {
            // Uncomment to debug Seed method
            // if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            SeedTeachers(context);
            SeedStudentGroups(context);
            SeedCourses(context);
            SeedLessons(context);
            SeedStudents(context);
            SeedStudentActivities(context);
            SeedDaySchedules(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        private void SeedDaySchedules(SchoolRegisterContext context)
        {
            var CourseId = context.Set<Course>().Where(t => t.Name == "Mathematics for first grade").FirstOrDefault().Id;
            for (int i = 1; i <= 1; i++)
            {
                var daySchedule = new DaySchedule()
                {
                    Id = i,
                    Day = Day.Monday,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(1),
                    CourseId = CourseId
                };
                context.Set<DaySchedule>().AddOrUpdate(daySchedule);
            }
            context.SaveChanges();
        }

        //TODO: experiment a bit on that(inheritance)
        private void SeedStudentActivities(SchoolRegisterContext context)
        {
            var StudentId = context.Set<Student>().Where(s => s.FirstName == "Andy").FirstOrDefault().Id;
            var LessonId = context.Set<Lesson>().Where(t => t.Id == 1).FirstOrDefault().Id;
            for (int i = 1; i <= 1; i++)
            {
                var mark = new Mark()
                {
                    Id = i,
                    LessonId = LessonId,
                    StudentId = StudentId,
                    MarkValue = MarkValue.A,
                    Importance = Importance.ClassExam
                };
                context.Set<Mark>().AddOrUpdate(mark);
            }
            for (int i = 2; i <= 2; i++)
            {
                var attendance = new Attendance()
                {
                    Id = i,
                    LessonId = LessonId,
                    StudentId = StudentId,
                    WasPresent = true,
                };
                context.Set<Attendance>().AddOrUpdate(attendance);
            }
            context.SaveChanges();
        }
        private void SeedStudents(SchoolRegisterContext context)
        {
            var StudentGroupId = context.Set<StudentGroup>().Where(t => t.Name == "First grade").FirstOrDefault().Id;
            for (int i = 1; i <= 1; i++)
            {
                var student = new Student()
                {
                    Id = i,
                    FirstName = "Andy",
                    LastName = "Novak",
                    StudentsGroupId =  StudentGroupId
                };
                context.Set<Student>().AddOrUpdate(student);
            }
            context.SaveChanges();
        }

        private void SeedLessons(SchoolRegisterContext context)
        {
            var CourseId = context.Set<Course>().Where(t => t.Name == "Mathematics for first grade").FirstOrDefault().Id;
            for (int i = 1; i <= 1; i++)
            {
                var lesson = new Lesson()
                {
                    Id = i,
                    CourseId = CourseId,
                    Date = DateTime.Today,
                };
                context.Set<Lesson>().AddOrUpdate(lesson);
            }
            context.SaveChanges();
        }

        private void SeedStudentGroups(SchoolRegisterContext context)
        {
            for (int i = 1; i <= 1; i++)
            {
                var stdGrp = new StudentGroup()
                {
                    Id = i,
                    Grade = 1,
                    Name = "First grade"
                };
                context.Set<StudentGroup>().AddOrUpdate(stdGrp);
            }
            context.SaveChanges();
        }


        private void SeedTeachers(SchoolRegisterContext context)
        {
            var store = new UserStore<Teacher>(context);
            var manager = new UserManager<Teacher>(store);
            if (!context.Users.Any(u => u.UserName == "FirstTeacher"))
            {
                var user = new Teacher { UserName = "FirstTeacher", FirstName = "Adam", LastName = "Nowak" };
                var adminresult = manager.Create(user, "1234Abc");
            }
        }
        private void SeedCourses(SchoolRegisterContext context)
        {
            var TeacherId = context.Set<Teacher>().Where(t => t.UserName == "FirstTeacher").FirstOrDefault().Id;
            var StudentGroupId = context.Set<StudentGroup>().Where(sG => sG.Name == "First grade").FirstOrDefault().Id;
            for (int i = 1; i <= 1; i++)
            {
                var crs = new Course()
                {
                    Id = i,
                    Name = "Mathematics for first grade",
                    TeacherId = TeacherId,
                    Room = i.ToString(),
                    StudentsGroupId = StudentGroupId,
                };
                context.Set<Course>().AddOrUpdate(crs);
            }
            context.SaveChanges();
        }
    }
}
