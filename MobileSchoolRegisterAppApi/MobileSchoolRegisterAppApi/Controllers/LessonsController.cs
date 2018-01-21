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
using Repository.Models.DTOs.DaySchedule;
using Repository.Models.DTOs.Lesson;
using Repository.Models.DTOs.StudentActivity;

namespace MobileSchoolRegisterAppApi.Controllers
{
    public class LessonsController : ApiController
    {
        private readonly ILessonRepo _repo;

        public LessonsController(ILessonRepo repo)
        {
            _repo = repo;
        }

        // GET: api/Lessons/5
        [ResponseType(typeof(LessonDto))]
        public IHttpActionResult Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Lesson lessonEntity = _repo.GetLessonById((int)id);
            if (lessonEntity == null)
            {
                return NotFound();
            }
            var lesson = new LessonDto()
            {
                Id = lessonEntity.Id,
                Date = lessonEntity.Date,
                CourseId = lessonEntity.CourseId,
                studentActivities = new List<StudentActivityDto>()
            };
            return Ok(lesson);
        }

        // PUT: api/Lessons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int? id, Lesson lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id == null || id != lesson.Id)
            {
                return BadRequest();
            }
            if (!LessonExists((int)id))
            {
                return NotFound();
            }
            _repo.MarkAsModified(lesson);
            _repo.SaveChanges();
            return Content(HttpStatusCode.Accepted, lesson);
        }

        //    // POST: api/Lessons
        [ResponseType(typeof(Lesson))]
        public IHttpActionResult PostLesson(Lesson lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.AddLesson(lesson);
            _repo.SaveChanges();

            return Ok(lesson);
        }

        // DELETE: api/Lessons/5
        [ResponseType(typeof(Lesson))]
        public IHttpActionResult DeleteLesson(int id)
        {
            if (!LessonExists(id))
            {
                return NotFound();
            }

            _repo.DeleteLesson(id);
            _repo.SaveChanges();

            return Ok();
        }

        private bool LessonExists(int id)
        {
            return _repo.GetLessons().Count(e => e.Id == id) > 0;
        }
    }
}