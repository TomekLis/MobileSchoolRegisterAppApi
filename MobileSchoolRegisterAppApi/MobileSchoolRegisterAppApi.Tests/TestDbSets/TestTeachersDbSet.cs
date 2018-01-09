using System.Data.Entity;
using System.Linq;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests.TestDbSets
{
    internal class TestTeachersDbSet : TestDbSet<Teacher>
    {
        public override Teacher Find(params object[] keyValues)
        {
            return this.SingleOrDefault(teacher => teacher.Id == (string)keyValues.Single());
        }

    }
}