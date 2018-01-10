using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Repository.IRepo;
using Repository.Models;

namespace Repository.Repo
{
    public class MarkRepo : DbContext, IMarkRepo
    {
        private readonly ISchoolRegisterContext _db;

        public MarkRepo(ISchoolRegisterContext db)
        {
            _db = db;
        }
        public IQueryable<Mark> GetMarks()
        {
            //_db.Database.Log = message => Trace.WriteLine(message);
            return _db.Marks.AsNoTracking();
        }

        public Mark GetMarkById(int id)
        {
            Mark mark = _db.Marks.Find(id);
            return mark;
        }

        public void DeleteMark(int id)
        {
            Mark mark = _db.Marks.Find(id);
            _db.Marks.Remove(mark);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void AddMark(Mark mark)
        {
            _db.Marks.Add(mark);
        }
        public void MarkAsModified(Mark mark)
        {
            Entry(mark).State = EntityState.Modified;
        }
    }
}