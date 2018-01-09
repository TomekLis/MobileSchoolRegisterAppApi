using System.Data.Entity;
using System.Linq;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests.TestDbSets
{
    internal class TestStudentActivitiesDbSet : TestDbSet<StudentActivity>
    {
        public override StudentActivity Find(params object[] keyValues)
        {
            return this.SingleOrDefault(studentGroup => studentGroup.Id == (int)keyValues.Single());
        }

    }
}