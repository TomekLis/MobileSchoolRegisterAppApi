using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Repository.IRepo;
using Repository.Models;
using Repository.Models.Contexts;
using Repository.Models.DTOs.Course;
using Repository.Models.DTOs.DaySchedule;
using Repository.Models.DTOs.Teacher;
using WebGrease.Css.Extensions;

namespace MobileSchoolRegisterAppApi.Controllers
{
    [Authorize]
    public class TeachersController : ApiController
    {
        private readonly ITeacherRepo _repo;

        public TeachersController(ITeacherRepo repo)
        {
            _repo = repo;
        }


        // GET: api/Teachers/5
        [ResponseType(typeof(Teacher))]
        public IHttpActionResult GetTeacher(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            Teacher teacherEntity = _repo.GetTeacherById(id);

            if (teacherEntity == null)
            {
                return NotFound();
            }
            if (!HasAccesToTeacher(id))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

            var teacher = new TeacherBasicDto()
            {
                Id = teacherEntity.Id,
                UserName = teacherEntity.UserName,
                FullName = teacherEntity.FullName,
                FirstName = teacherEntity.FirstName,
                LastName = teacherEntity.LastName,
                Email = teacherEntity.Email,
                PhoneNumber = teacherEntity.PhoneNumber
            };

            return Ok(teacher);
        }

        // PUT: api/Teachers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeacher(string id, Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(id) || id != teacher.Id)
            {
                return BadRequest();
            }

            if (!TeacherExists(id))
            {
                return NotFound();
            }
            if (!HasAccesToTeacher(id))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            _repo.MarkAsModified(teacher);
            _repo.SaveChanges();
            
            return Ok(teacher);
        }

        // DELETE: api/Teachers/5
        [ResponseType(typeof(Teacher))]
        public IHttpActionResult DeleteTeacher(string id)
        {
            if (!TeacherExists(id))
            {
                return NotFound();
            }
            if (!HasAccesToTeacher(id))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            _repo.DeleteTeacher(id);
            _repo.SaveChanges();
            return Ok();
        }

        [Route("teachers/GetCoursesByTeacherId/{id}")]
        [ResponseType(typeof(IQueryable<CourseDto>))]
        public IHttpActionResult GetCoursesByTeacherId(string id)
        {
            if (!TeacherExists(id))
            {
                return NotFound();
            }
            if (!HasAccesToTeacher(id))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            Teacher teacherEntity = _repo.GetTeacherById(id);

            if (teacherEntity == null)
            {
                return NotFound();
            }
            var courses = teacherEntity.Courses.Select(c =>
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

        private bool TeacherExists(string id)
        {
            return _repo.GeTeachers().Count(e => e.Id == id) > 0;
        }

        private bool HasAccesToTeacher(string id)
        {
            return User.Identity.GetUserId() == id;
        }
    }
}