using System.Data.Entity;
using System.Linq;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests.TestDbSets
{
    internal class TestStudentsDbSet : TestDbSet<Student>
    {
        public override Student Find(params object[] keyValues)
        {
            return this.SingleOrDefault(student => student.Id == (string)keyValues.Single());
        }

    }
}