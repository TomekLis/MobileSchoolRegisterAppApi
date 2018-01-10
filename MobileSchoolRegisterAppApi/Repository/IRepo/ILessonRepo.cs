using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepo
{
    public interface ILessonRepo
    {
        IQueryable<Lesson> GetLessons();
        Lesson GetLessonById(int id);
        void DeleteLesson(int id);
        void SaveChanges();
        void AddLesson(Lesson lesson);
        void MarkAsModified(Lesson lesson);
    }
}
