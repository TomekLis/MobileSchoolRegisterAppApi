using System.Linq;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests.TestDbSets
{
    class TestCoursesDbSet : TestDbSet<Course>
    {
        public override Course Find(params object[] keyValues)
        {
            return this.SingleOrDefault(course => course.Id == (int) keyValues.Single());
        }
    }
}
