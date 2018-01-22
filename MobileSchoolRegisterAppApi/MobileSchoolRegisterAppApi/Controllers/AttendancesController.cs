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
using Repository.Models.DTOs.Attendance;
using Repository.Models.DTOs.DaySchedule;

namespace MobileSchoolRegisterAppApi.Controllers
{
    public class AttendancesController : ApiController
    {
        private readonly IAttendanceRepo _repo;

        public AttendancesController(IAttendanceRepo repo)
        {
            _repo = repo;
        }

        // GET: api/Attendances
        [ResponseType(typeof(IQueryable<AttendanceDto>))]
        public IHttpActionResult GetAttendances()
        {
            var attendances = _repo.GetAttendances().Select(c =>
                new AttendanceDto()
                {
                    Id = c.Id,
                    WasPresent = c.WasPresent
                });

            return Ok(attendances);
        }

        // GET: api/Attendances/5
        [ResponseType(typeof(AttendanceDto))]
        public IHttpActionResult Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Attendance attendanceEntity = _repo.GetAttendanceById((int)id);
            if (attendanceEntity == null)
            {
                return NotFound();
            }
            var attendance = new AttendanceDto()
            {
                Id = attendanceEntity.Id,
                WasPresent = attendanceEntity.WasPresent
            };
            return Ok(attendance);
        }

        // PUT: api/Attendances/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int? id, Attendance passedAttendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var attendance = _repo.GetAttendanceById(passedAttendance.Id);
            attendance.WasPresent = passedAttendance.WasPresent;
            if (id == null || id != attendance.Id)
            {
                return BadRequest();
            }
            if (!AttendanceExists((int)id))
            {
                return NotFound();
            }
            _repo.MarkAsModified(attendance);
            _repo.SaveChanges();
            return Content(HttpStatusCode.Accepted, attendance);
        }

        //    // POST: api/Attendances
        [ResponseType(typeof(Attendance))]
        public IHttpActionResult PostAttendance(Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repo.AddAttendance(attendance);
            _repo.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = attendance.Id }, attendance);
        }

        // DELETE: api/Attendances/5
        [ResponseType(typeof(Attendance))]
        public IHttpActionResult DeleteAttendance(int id)
        {
            if (!AttendanceExists(id))
            {
                return NotFound();
            }

            _repo.DeleteAttendance(id);
            _repo.SaveChanges();

            return Ok();
        }
        private bool AttendanceExists(int id)
        {
            return _repo.GetAttendances().Count(e => e.Id == id) > 0;
        }
    }
}