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
using Repository.IRepo;
using Repository.Models;
using Repository.Models.Contexts;

namespace MobileSchoolRegisterAppApi.Controllers
{
    public class MarksController : ApiController
    {
        private SchoolRegisterContext db = new SchoolRegisterContext();

        private readonly IMarkRepo _repo;

        public MarksController(IMarkRepo repo)
        {
            _repo = repo;
        }
        // GET: api/Marks
        public IQueryable<Mark> GetStudentActivities()
        {
            return db.Marks;
        }

        // GET: api/Marks/5
        [ResponseType(typeof(Mark))]
        public IHttpActionResult GetMark(int id)
        {
            Mark mark = db.Marks.Find(id) as Mark;
            if (mark == null)
            {
                return NotFound();
            }

            return Ok(mark);
        }

        // PUT: api/Marks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMark(int id, Mark mark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mark.Id)
            {
                return BadRequest();
            }

            db.Entry(mark).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarkExists(id))
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

        // POST: api/Marks
        [ResponseType(typeof(Mark))]
        public IHttpActionResult PostMark(Mark mark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentActivities.Add(mark);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mark.Id }, mark);
        }

        // DELETE: api/Marks/5
        [ResponseType(typeof(Mark))]
        public IHttpActionResult DeleteMark(int id)
        {
            Mark mark = db.StudentActivities.Find(id) as Mark;
            if (mark == null)
            {
                return NotFound();
            }

            db.StudentActivities.Remove(mark);
            db.SaveChanges();

            return Ok(mark);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MarkExists(int id)
        {
            return db.StudentActivities.Count(e => e.Id == id) > 0;
        }
    }
}