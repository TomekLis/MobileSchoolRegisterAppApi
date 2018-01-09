using System.Data.Entity;
using System.Linq;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests.TestDbSets
{
    internal class TestMarksDbSet : TestDbSet<Mark>
    {
        public override Mark Find(params object[] keyValues)
        {
            return this.SingleOrDefault(x => x.Id == (int)keyValues.Single());
        }

    }
}