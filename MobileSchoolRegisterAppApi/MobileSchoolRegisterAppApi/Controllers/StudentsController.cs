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
using Microsoft.AspNet.Identity;
using Repository.IRepo;
using Repository.Models;
using Repository.Models.Contexts;
using Repository.Models.DTOs.Course;
using Repository.Models.DTOs.DaySchedule;
using Repository.Models.DTOs.Student;

namespace MobileSchoolRegisterAppApi.Controllers
{
    public class StudentsController : ApiController
    {
        private readonly IStudentRepo _repo;
        public StudentsController(IStudentRepo repo)
        {
            _repo = repo;
        }


        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            Student studentEntity = _repo.GetStudentById(id);

            if (studentEntity == null)
            {
                return NotFound();
            }
            if(!HasAccesToStudent(id))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

            var student = new StudentBasicDto()
            {
                Id = studentEntity.Id,
                UserName = studentEntity.UserName,
                FirstName = studentEntity.FirstName,
                LastName = studentEntity.LastName,
                Email = studentEntity.Email,
                PhoneNumber = studentEntity.PhoneNumber
            };

            return Ok(student);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(string id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(id) || id != student.Id)
            {
                return BadRequest();
            }

            if (!StudentExists(id))
            {
                return NotFound();
            }
            if (!HasAccesToStudent(id))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            _repo.MarkAsModified(student);
            _repo.SaveChanges();
            return Ok(student);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult DeleteStudent(string id)
        {
            if (!StudentExists(id))
            {
                return NotFound();
            }
            if (!HasAccesToStudent(id))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            _repo.DeleteStudent(id);
            _repo.SaveChanges();
            return Ok();
        }

        [Route("students/GetCoursesByStudentId/{id}")]
        [ResponseType(typeof(IQueryable<CourseDto>))]
        public IHttpActionResult GetCoursesByStudentId(string id)
        {
            if (!StudentExists(id))
            {
                return NotFound();
            }
            if (!HasAccesToStudent(id))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            Student studentEntity = _repo.GetStudentById(id);
            
            if (studentEntity == null)
            {
                return NotFound();
            }
            var courses = studentEntity.StudentGroup.Courses.Select(c =>
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

        private bool StudentExists(string id)
        {
            return _repo.GetStudents().Count(e => e.Id == id) > 0;
        }

        private bool HasAccesToStudent(string id)
        {
            return User.Identity.GetUserId() == id;
        }


    }
}