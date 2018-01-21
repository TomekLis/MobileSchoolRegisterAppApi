using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.IRepo;
using Repository.Models;

namespace Repository.Repo
{
    public class AttendanceRepo : DbContext, IAttendanceRepo
    {
        private readonly ISchoolRegisterContext _db;

        public AttendanceRepo(ISchoolRegisterContext db)
        {
            _db = db;
        }
        public IQueryable<Attendance> GetAttendances()
        {
            //_db.Database.Log = message => Trace.WriteLine(message);
            return _db.Attendances.AsNoTracking();
        }

        public Attendance GetAttendanceById(int id)
        {
            Attendance attendance = _db.Attendances.Find(id);
            return attendance;
        }

        public void DeleteAttendance(int id)
        {
            Attendance attendance = _db.Attendances.Find(id);
            _db.Attendances.Remove(attendance);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void AddAttendance(Attendance attendance)
        {
            _db.Attendances.Add(attendance);
        }

        public void MarkAsModified(Attendance attendance)
        {
            _db.Entry(attendance).State = EntityState.Modified;
        }
    }
}
