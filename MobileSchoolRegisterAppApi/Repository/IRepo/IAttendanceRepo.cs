using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepo
{
    public interface IAttendanceRepo
    {
        IQueryable<Attendance> GetAttendances();
        Attendance GetAttendanceById(int id);
        void DeleteAttendance(int id);
        void SaveChanges();
        void AddAttendance(Attendance attendance);
        void MarkAsModified(Attendance attendance);
    }
}
