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
using Repository.Models.DTOs.Attendance;
using Repository.Models.DTOs.Course;
using Repository.Models.DTOs.DaySchedule;
using Repository.Models.DTOs.Lesson;
using Repository.Models.DTOs.Mark;
using Repository.Models.DTOs.Student;
using Repository.Models.DTOs.StudentActivity;
using Repository.Models.DTOs.Teacher;
using WebGrease.Css.Extensions;

namespace MobileSchoolRegisterAppApi.Controllers
{
    [Authorize]
    public class TeachersController : ApiController
    {
        private readonly ITeacherRepo _repo;
        private readonly IAttendanceRepo _attendanceRepo;
        private readonly IMarkRepo _markRepo;


        public TeachersController(ITeacherRepo repo, IAttendanceRepo attendanceRepo, IMarkRepo markRepo)
        {
            _repo = repo;
            _attendanceRepo = attendanceRepo;
            _markRepo = markRepo;
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
                PhoneNumber = teacherEntity.PhoneNumber,
                UpcomingCourses = teacherEntity.Courses.Select(c => new CourseDto()
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
                })
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

        [Route("teachers/GetStudentsByTeacherId/{id}")]
        [ResponseType(typeof(IQueryable<CourseDto>))]
        public IHttpActionResult GetStudentsByTeacherId(string id)
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
            var students = _repo.GetTeacherStudents(teacherEntity).Select(s => new StudentBasicDto()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                UserName = s.UserName,
                PhoneNumber = s.PhoneNumber
            }
            );
            var attendances = _attendanceRepo.GetAttendances().ToList();
            var marks = _markRepo.GetMarks().ToList();
            
            List<StudentBasicDto> studentList = students.ToList();
            for (int i = 0; i < studentList.Count; i++)
            {
                var attendancesForStudent = new List<Attendance>();
                var marksForStudent = new List<Mark>();

                var studentBasicDto = studentList[i];
                var studentAttendances = attendances.Where(a => a.Student.Id == studentBasicDto.Id).ToList();
                attendancesForStudent.AddRange(studentAttendances);
                studentBasicDto.Attendances = attendancesForStudent.Select(a => new AttendanceDto()
                {
                    Id = a.Id,
                    WasPresent = a.WasPresent,
                    Lesson = new LessonDto()
                    {
                        Id = a.Lesson != null ? a.Lesson.Id : (int?)null,
                        Date = a.Lesson != null ? a.Lesson.Date : (DateTime?)null
                    }
                }).ToList();
                var studentMarks = marks.Where(a => a.Student.Id == studentBasicDto.Id).ToList();
                marksForStudent.AddRange(studentMarks);
                studentBasicDto.Marks = marksForStudent.Select(m => new MarkDto()
                {
                    Id = m.Id,
                    Importance = m.Importance,
                    MarkValue = m.MarkValue,
                    Lesson = new LessonDto()
                    {
                        Id = m.Lesson != null ? m.Lesson.Id : (int?) null,
                        Date = m.Lesson != null ? m.Lesson.Date : (DateTime?) null
                    }
                }).ToList();

                studentList[i] = studentBasicDto;
                
            }
            
            //s.Marks = _repo.GetStudentMarks(s).Select(m => new MarkDto()
            //    {
            //        Importance = m.Importance,
            //        MarkValue = m.MarkValue,
            //        Lesson = new LessonDto()
            //        {
            //            Id = m.Lesson != null ? m.Lesson.Id : (int?)null,
            //            Date = m.Lesson != null ? m.Lesson.Date : (DateTime?)null
            //        }
            //    }),
              //s.Attendances = _attendanceRepo.GetAttendances()/*.Where(x=> x.Student.Id == s.Id)*/.Select(a => new AttendanceDto()
              //  {
              //      Id = a.Id,
              //      WasPresent = a.WasPresent,
              //      Lesson = new LessonDto()
              //      {
              //          Id = a.Lesson != null ? a.Lesson.Id : (int?)null,
              //          Date = a.Lesson != null ? a.Lesson.Date : (DateTime?)null
              //      }
              //  }));
            return Ok(studentList);
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