using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
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
        public void GetTeacher_ShouldReturnOk()
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

        [TestMethod]
        public void GetTeacher_ShouldReturnNotFound()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            teacherRepo = new MockTeacherRepo(testSchoolRegisterContext);
            teachersController = new TeachersController(teacherRepo);
            PopulateTeacherFields();

            //Act
            var actionResult = teachersController.GetTeacher("notExistingTeacherId");


            //Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetTeacher_ShouldReturnForbidden()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            teacherRepo = new MockTeacherRepo(testSchoolRegisterContext);
            teachersController = new TeachersController(teacherRepo);
            PopulateTeacherFields();

            //Act
            var actionResult = teachersController.GetTeacher("unauthenticatedTeacherId") as ResponseMessageResult;


            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(actionResult.Response.StatusCode, HttpStatusCode.Forbidden);
        }
        [TestMethod]
        public void GetTeacher_ShouldReturnBadRequest()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            teacherRepo = new MockTeacherRepo(testSchoolRegisterContext);
            teachersController = new TeachersController(teacherRepo);

            //Act
            IHttpActionResult actionResult = teachersController.GetTeacher(null);

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));

        }

        [TestMethod]
        public void PutTeacher__ShouldReturnOk()
        {
            // Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            teacherRepo = new MockTeacherRepo(testSchoolRegisterContext);
            teachersController = new TeachersController(teacherRepo);
            PopulateTeacherFields();

            //Act
            var actionResult = teachersController.PutTeacher("sampleTeacherId", new Teacher { Id = "sampleTeacherId" });
            var contentResult = actionResult as OkNegotiatedContentResult<Teacher>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("sampleTeacherId", contentResult.Content.Id);
        }
        [TestMethod]
        public void PutTeacher_ShouldReturnBadRequestDifferentId()
        {
            // Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            teacherRepo = new MockTeacherRepo(testSchoolRegisterContext);
            teachersController = new TeachersController(teacherRepo);

            //Act
            IHttpActionResult actionResult = teachersController.PutTeacher("notExistingTeacherId", new Teacher { Id = "sampleTeacherId" });

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }
        [TestMethod]
        public void PutTeacher_ShouldReturnNotFound()
        {
            // Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            teacherRepo = new MockTeacherRepo(testSchoolRegisterContext);
            teachersController = new TeachersController(teacherRepo);

            //Act
            IHttpActionResult actionResult = teachersController.PutTeacher("sampleTeacherId", new Teacher { Id = "sampleTeacherId" });

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
        [TestMethod]
        public void PutTeacher_ShouldReturnForbidden()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            teacherRepo = new MockTeacherRepo(testSchoolRegisterContext);
            teachersController = new TeachersController(teacherRepo);
            PopulateTeacherFields();

            //Act
            var actionResult = teachersController.PutTeacher("unauthenticatedTeacherId", new Teacher { Id = "unauthenticatedTeacherId" }) as ResponseMessageResult;


            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(actionResult.Response.StatusCode, HttpStatusCode.Forbidden);
        }
        [TestMethod]
        public void DeleteTeacher__ShouldReturnOk()
        {
            // Arrange
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

        [TestMethod]
        public void DeleteTeacher_ShouldReturnNotFound()
        {
            // Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            teacherRepo = new MockTeacherRepo(testSchoolRegisterContext);
            teachersController = new TeachersController(teacherRepo);

            //Act
            IHttpActionResult actionResult = teachersController.DeleteTeacher("sampleTeacherId");

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
        [TestMethod]
        public void DeleteTeacher_ShouldReturnForbidden()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            teacherRepo = new MockTeacherRepo(testSchoolRegisterContext);
            teachersController = new TeachersController(teacherRepo);
            PopulateTeacherFields();

            //Act
            var actionResult = teachersController.DeleteTeacher("unauthenticatedTeacherId") as ResponseMessageResult;


            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(actionResult.Response.StatusCode, HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public void GetCoursesByTeacherId__ShouldReturnOk()
        {
            // Arrange
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

        [TestMethod]
        public void GetCoursesByTeacherId_ShouldReturnNotFound()
        {
            // Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            teacherRepo = new MockTeacherRepo(testSchoolRegisterContext);
            teachersController = new TeachersController(teacherRepo);

            //Act
            IHttpActionResult actionResult = teachersController.GetCoursesByTeacherId("sampleTeacherId");

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
        [TestMethod]
        public void GetCoursesByTeacherId_ShouldReturnForbidden()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            teacherRepo = new MockTeacherRepo(testSchoolRegisterContext);
            teachersController = new TeachersController(teacherRepo);
            PopulateTeacherFields();

            //Act
            var actionResult = teachersController.GetCoursesByTeacherId("unauthenticatedTeacherId") as ResponseMessageResult;


            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(actionResult.Response.StatusCode, HttpStatusCode.Forbidden);
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
            testSchoolRegisterContext.Teachers.Add(new Teacher()
            {
                Id = "unauthenticatedTeacherId",
                FirstName = "Jane",
                LastName = "Doe"
            });

        }
    }
}
