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
using Repository.Models;
using Repository.Models.Contexts;

namespace MobileSchoolRegisterAppApi.Controllers
{
    public class StudentGroupsController : ApiController
    {
        private SchoolRegisterContext db = new SchoolRegisterContext();

        // GET: api/StudentGroups
        public IQueryable<StudentGroup> GetStudentGroups()
        {
            return db.StudentGroups;
        }

        // GET: api/StudentGroups/5
        [ResponseType(typeof(StudentGroup))]
        public IHttpActionResult GetStudentGroup(int id)
        {
            StudentGroup studentGroup = db.StudentGroups.Find(id);
            if (studentGroup == null)
            {
                return NotFound();
            }

            return Ok(studentGroup);
        }

        // PUT: api/StudentGroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentGroup(int id, StudentGroup studentGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentGroup.Id)
            {
                return BadRequest();
            }

            db.Entry(studentGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentGroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/StudentGroups
        [ResponseType(typeof(StudentGroup))]
        public IHttpActionResult PostStudentGroup(StudentGroup studentGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentGroups.Add(studentGroup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = studentGroup.Id }, studentGroup);
        }

        // DELETE: api/StudentGroups/5
        [ResponseType(typeof(StudentGroup))]
        public IHttpActionResult DeleteStudentGroup(int id)
        {
            StudentGroup studentGroup = db.StudentGroups.Find(id);
            if (studentGroup == null)
            {
                return NotFound();
            }

            db.StudentGroups.Remove(studentGroup);
            db.SaveChanges();

            return Ok(studentGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentGroupExists(int id)
        {
            return db.StudentGroups.Count(e => e.Id == id) > 0;
        }
    }
}