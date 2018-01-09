using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileSchoolRegisterAppApi.Controllers;
using MobileSchoolRegisterAppApi.Tests.Contexts;
using MobileSchoolRegisterAppApi.Tests.MockRepos;
using Moq;
using Repository.IRepo;
using Repository.Models;
using Repository.Models.DTOs.Teacher;

namespace MobileSchoolRegisterAppApi.Tests.Controllers
{
    [TestClass]
    public class TeachersControllerTest
    {
        private TestSchoolRegisterContext testSchoolRegisterContext;
        private ITeacherRepo teacherRepo;
        private TeachersController teachersController;

        [TestMethod]
        public void GetTeacher_ShouldReturnOks()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            teacherRepo = new MockTeacherRepo(testSchoolRegisterContext);
            teachersController = new TeachersController(teacherRepo);
            PopulateTeacherFields();

            //Act
            var actionResult = teachersController.GetTeacher("sampleTeacherId");
            var contentResult = actionResult as OkNegotiatedContentResult<TeacherBasicDto>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("sampleTeacherId", contentResult.Content.Id);
        }

        private void PopulateTeacherFields()
        {
            var claim = new Claim("test", "sampleTeacherId");
            var mockIdentity =
                Mock.Of<ClaimsIdentity>(ci => ci.FindFirst(It.IsAny<string>()) == claim);

            teachersController.User = Mock.Of<IPrincipal>(ip => ip.Identity == mockIdentity);

            var course1 = new Course { Id = 1, Name = "Mathematics", StudentsGroupId = 1, TeacherId = "sampleTeacherId" };
            var course2 = new Course { Id = 2, Name = "PE", StudentsGroupId = 1, TeacherId = "sampleTeacherId" };
            var course3 = new Course { Id = 3, Name = "Literature", StudentsGroupId = 1, TeacherId = "sampleTeacherId" };
            testSchoolRegisterContext.Courses.Add(course1);
            testSchoolRegisterContext.Courses.Add(course2);
            testSchoolRegisterContext.Courses.Add(course3);
            testSchoolRegisterContext.Teachers.Add(new Teacher()
            {
                Id = "sampleTeacherId",
                FirstName = "John",
                LastName = "Doe",
                Courses = new List<Course>()
                {
                    course1,
                    course2,
                    course3
                }
            });
        }
    }
}
