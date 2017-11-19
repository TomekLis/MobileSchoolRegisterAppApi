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
using System.Web.Mvc;
using Repository.IRepo;
using Repository.Models;
using Repository.Models.Contexts;

namespace MobileSchoolRegisterAppApi.Controllers
{
    public class CoursesController : ApiController
    {
        private readonly ICourseRepo _repo;

        public CoursesController(ICourseRepo repo)
        {
            _repo = repo;
        }

        // GET: api/Courses
        [ResponseType(typeof(IQueryable<Course>))]
        public IHttpActionResult GetCourses()
        {
            var courses = _repo.GetCourses();
            return Ok(courses);
        }

        // GET: api/Courses/5
        [ResponseType(typeof(Course))]
        public IHttpActionResult Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Course course = _repo.GetCourseById((int)id);
            if (course == null)
            {
                return NotFound();
            }
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

        //    // DELETE: api/Courses/5
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
    }
}