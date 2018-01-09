using System.Data.Entity;
using System.Linq;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests.TestDbSets
{
    internal class TestStudentGroupsDbSet : TestDbSet<StudentGroup>
    {
        public override StudentGroup Find(params object[] keyValues)
        {
            return this.SingleOrDefault(studentGroup => studentGroup.Id == (int)keyValues.Single());
        }

    }
}