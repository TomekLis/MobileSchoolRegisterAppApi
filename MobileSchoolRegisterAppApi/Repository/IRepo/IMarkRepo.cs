using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepo
{
    public interface IMarkRepo
    {
        IQueryable<Mark> GetMarks();
        Mark GetMarkById(int id);
        void DeleteMark(int id);
        void SaveChanges();
        void AddMark(Mark lesson);
        void MarkAsModified(Mark lesson);
    }
}
