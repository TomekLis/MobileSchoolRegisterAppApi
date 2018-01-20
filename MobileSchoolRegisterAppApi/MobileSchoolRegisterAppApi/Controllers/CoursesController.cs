using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Castle.Components.DictionaryAdapter.Xml;
using Repository.IRepo;
using Repository.Models;
using Repository.Models.Contexts;
using Repository.Models.DTOs.Course;
using Repository.Models.DTOs.DaySchedule;
using Repository.Models.DTOs.Lesson;
using Repository.Models.DTOs.Student;
using Repository.Models.DTOs.StudentActivity;

namespace MobileSchoolRegisterAppApi.Controllers
{
    [Authorize]
    public class CoursesController : ApiController
    {
        private readonly ICourseRepo _repo;

        public CoursesController(ICourseRepo repo)
        {
            _repo = repo;
        }

        // GET: api/Courses
        [ResponseType(typeof(IQueryable<CourseDto>))]
        public IHttpActionResult GetCourses()
        {
            var courses = _repo.GetCourses().Include(c => c.DaySchedules).Select(c =>
                new CourseDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    StudentsGroupId = c.StudentsGroupId,
                    Room = c.Room,
                    TeacherId = c.TeacherId,
                    DaySchedules = c.DaySchedules.Select(d => new DayScheduleDto()
                    {
                        Day = d.Day,
                        Id = d.Id,
                        EndTime = d.EndTime,
                        StartTime = d.StartTime
                    })
                });

            return Ok(courses);
        }

        // GET: api/Courses/5
        [ResponseType(typeof(CourseDto))]
        public IHttpActionResult Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Course courseEntity = _repo.GetCourseById((int)id);
            if (courseEntity == null)
            {
                return NotFound();
            }
            var course = new CourseDto()
            {
                Id = courseEntity.Id,
                Name = courseEntity.Name,
                Room = courseEntity.Room,
                StudentsGroupId = courseEntity.StudentsGroupId,
                TeacherId = courseEntity.TeacherId,
                DaySchedules = courseEntity.DaySchedules.Select(d => new DayScheduleDto()
                {
                    Day = d.Day,
                    Id = d.Id,
                    EndTime = d.EndTime,
                    StartTime = d.StartTime
                })
            };
            return Ok(course);
        }

        // PUT: api/Courses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int? id, Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id == null || id != course.Id)
            {
                return BadRequest();
            }
            if (!CourseExists((int)id))
            {
                return NotFound();
            }
            _repo.MarkAsModified(course);
            _repo.SaveChanges();
            return Content(HttpStatusCode.Accepted, course);
        }

        //    // POST: api/Courses
        [ResponseType(typeof(Course))]
        public IHttpActionResult PostCourse(Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repo.AddCourse(course);
            _repo.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [ResponseType(typeof(Course))]
        public IHttpActionResult DeleteCourse(int id)
        {
            if (!CourseExists(id))
            {
                return NotFound();
            }

            _repo.DeleteCourse(id);
            _repo.SaveChanges();

            return Ok();
        }

        private bool CourseExists(int id)
        {
            return _repo.GetCourses().Count(e => e.Id == id) > 0;
        }

        public IHttpActionResult GetLessonsByCourseId(int id)
        {
            if (!CourseExists(id))
            {
                return NotFound();
            }
            Course courseEntity = _repo.GetCourseById(id);

            if (courseEntity == null)
            {
                return NotFound();
            }
            var lessons = courseEntity.Lessons.Select(c =>
                new LessonDto()
                {
                    Id = c.Id,
                    CourseId = c.CourseId,
                    Date = c.Date,
                    studentActivities = c.StudentActivities.Select(d => new StudentActivityDto()
                    {
                        Id = d.Id
                    })
                });
            return Ok(lessons);

        }

        [Route("courses/GetStudentsByCourseId/{id}")]
        public IHttpActionResult GetStudentsByCourseId(int id)
        {
            if (!CourseExists(id))
            {
                return NotFound();
            }
            Course courseEntity = _repo.GetCourseById(id);

            var students = courseEntity.StudentGroup.Students.Select(s => new StudentBasicDto()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                UserName = s.UserName,
                PhoneNumber = s.PhoneNumber
            });
            return Ok(students);


        }
    }
}