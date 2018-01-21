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

    internal sealed class Configuration : DbMigrationsConfiguration<SchoolRegisterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Models.Contexts.SchoolRegisterContext context)
        {
            // Uncomment to debug Seed method
            //if (System.Diagnostics.Debugger.IsAttached == false)
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
            var CourseId = context.Set<Course>().FirstOrDefault(t => t.Name == "Maths").Id;
            for (int i = 1; i <= 5; i++)
            {
                var daySchedule = new DaySchedule()
                {
                    Id = i,
                    Day = GetDay(i),
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(1),
                    CourseId = CourseId
                };
                context.Set<DaySchedule>().AddOrUpdate(daySchedule);
            }
            context.SaveChanges();
        }

        private DayOfWeek GetDay(int i)
        {
            switch (i)
            {
                case 1:
                    return DayOfWeek.Monday;
                    break;
                case 2:
                    return DayOfWeek.Tuesday;
                    break;
                case 3:
                    return DayOfWeek.Wednesday;
                    break;
                case 4:
                    return DayOfWeek.Thursday;
                    break;
                case 5:
                    return DayOfWeek.Friday;
                    break;
                default:
                    return DayOfWeek.Friday;
                    break;
            }
        }

        //TODO: experiment a bit on that(inheritance)
        private void SeedStudentActivities(SchoolRegisterContext context)
        {
            var Student = context.Set<Student>().FirstOrDefault(s => s.FirstName == "Andy");
            var Lesson = context.Set<Lesson>().FirstOrDefault(t => t.Id == 1);
            for (int i = 1; i <= 1; i++)
            {
                var mark = new Mark()
                {
                    Id = i,
                    Student = Student,
                    Lesson = Lesson,
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
                    Student = Student,
                    Lesson = Lesson,
                    WasPresent = true,

                };
                context.Set<Attendance>().AddOrUpdate(attendance);
            }
            context.SaveChanges();
        }
        private void SeedStudents(SchoolRegisterContext context)
        {
           
            var studentGroup = context.Set<StudentGroup>().FirstOrDefault(t => t.Name == "First grade");
            var store = new UserStore<Student>(context);
            var manager = new UserManager<Student>(store);
            if (!context.Users.Any(u => u.UserName == "FirstStudent"))
            {
                var user = new Student() { UserName = "FirstStudent", FirstName = "Andy", LastName = "Kowalski", StudentGroup = studentGroup, StudentsGroupId = studentGroup.Id};
                var adminresult = manager.Create(user, "1234Abc");
            }
            if (!context.Users.Any(u => u.UserName == "SecondStudent"))
            {
                var user = new Student() { UserName = "SecondStudent", FirstName = "John", LastName = "Doe", StudentGroup = studentGroup, StudentsGroupId = studentGroup.Id };
                var adminresult = manager.Create(user, "1234Abc");
            }
            if (!context.Users.Any(u => u.UserName == "ThirdStudent"))
            {
                var user = new Student() { UserName = "ThirdStudent", FirstName = "Jane", LastName = "Doe", StudentGroup = studentGroup, StudentsGroupId = studentGroup.Id };
                var adminresult = manager.Create(user, "1234Abc");
            }


        }

        private void SeedLessons(SchoolRegisterContext context)
        {
            var CourseId = context.Set<Course>().FirstOrDefault(t => t.Name == "Maths").Id;
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
            var Teacher = context.Set<Teacher>().FirstOrDefault(t => t.UserName == "FirstTeacher");
            var StudentGroup = context.Set<StudentGroup>().FirstOrDefault(sg => sg.Name == "First grade");
            for (int i = 1; i <= 5; i++)
            {
                var crs = new Course()
                {
                    Id = i,
                    Name = GetCourseName(i),
                    TeacherId = Teacher.Id,
                    Room = i.ToString(),
                    StudentsGroupId = StudentGroup.Id,
                    StudentGroup = StudentGroup,
                    Teacher = Teacher
                };
                context.Set<Course>().AddOrUpdate(crs);
            }
            context.SaveChanges();
        }

        private string GetCourseName(int i)
        {
            switch (i)
            {
                case 1:
                    return "Maths";
                    break;
                case 2:
                    return "Literature";
                    break;
                case 3:
                    return "Spanish";
                    break;
                case 4:
                    return "PE";
                    break;
                case 5:
                    return "Science";
                    break;
                default:
                    return "Whatever";
                    break;
            }

        }
    }
}
