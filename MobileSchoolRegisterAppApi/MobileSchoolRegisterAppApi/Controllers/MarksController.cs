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
using Repository.Models.DTOs.Mark;

namespace MobileSchoolRegisterAppApi.Controllers
{
    public class MarksController : ApiController
    {
        private readonly IMarkRepo _repo;

        public MarksController(IMarkRepo repo)
        {
            _repo = repo;
        }

        // GET: api/Marks
        [ResponseType(typeof(IQueryable<MarkDto>))]
        public IHttpActionResult GetMarks()
        {
            var marks = _repo.GetMarks().Select(c =>
                new MarkDto()
                {
                    Id = c.Id,
                    MarkValue = c.MarkValue,
                    Importance = c.Importance
                });

            return Ok(marks);
        }

        // GET: api/Marks/5
        [ResponseType(typeof(MarkDto))]
        public IHttpActionResult Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Mark markEntity = _repo.GetMarkById((int)id);
            if (markEntity == null)
            {
                return NotFound();
            }
            var mark = new MarkDto()
            {
                Id = markEntity.Id,
                MarkValue = markEntity.MarkValue,
                Importance = markEntity.Importance
            };
            return Ok(mark);
        }

        // PUT: api/Marks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int? id, Mark mark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id == null || id != mark.Id)
            {
                return BadRequest();
            }
            if (!MarkExists((int)id))
            {
                return NotFound();
            }
            _repo.MarkAsModified(mark);
            _repo.SaveChanges();
            return Ok(mark);
        }

        //    // POST: api/Marks
        [ResponseType(typeof(Mark))]
        public IHttpActionResult PostMark(Mark mark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repo.AddMark(mark);
            _repo.SaveChanges();

            return Ok(mark);
        }

        // DELETE: api/Marks/5
        [ResponseType(typeof(Mark))]
        public IHttpActionResult DeleteMark(int id)
        {
            if (!MarkExists(id))
            {
                return NotFound();
            }

            _repo.DeleteMark(id);
            _repo.SaveChanges();

            return Ok();
        }
        private bool MarkExists(int id)
        {
            return _repo.GetMarks().Count(e => e.Id == id) > 0;
        }
    }
}