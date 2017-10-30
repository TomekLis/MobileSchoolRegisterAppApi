using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileSchoolRegisterAppApi.Controllers;

namespace MobileSchoolRegisterAppApi.Tests.Controllers
{
    [TestClass]
    public class CoursesControllerTest
    {
        [TestMethod]
        public void GetAllCourses_ShouldReturn()
        {
            var controller = new CoursesController(new TestSchoolRegisterContext());
        }
    }
}
