using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests
{
    class TestCoursesDbSet : TestDbSet<Course>
    {
        public override Course Find(params object[] keyValues)
        {
            return this.SingleOrDefault(course => course.Id == (int) keyValues.Single());
        }
    }
}
