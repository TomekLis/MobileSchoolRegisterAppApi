using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileSchoolRegisterAppApi.Controllers;
using MobileSchoolRegisterAppApi.Tests.Contexts;
using MobileSchoolRegisterAppApi.Tests.MockRepos;
using Moq;
using Repository.IRepo;
using Repository.Models;
using Repository.Models.DTOs.Student;

namespace MobileSchoolRegisterAppApi.Tests.Controllers
{
    /// <summary>
    /// Summary description for StudentsControllerTest
    /// </summary>
    [TestClass]
    public class StudentsControllerTest
    {
        private TestSchoolRegisterContext testSchoolRegisterContext;
        private IStudentRepo studentRepo;
        private StudentsController studentsController;

        [TestMethod]
        public void GetStudent_ShouldReturnOk()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            studentRepo = new MockStudentRepo(testSchoolRegisterContext);
            studentsController = new StudentsController(studentRepo);
            PopulateStudentFields();

            //Act
            var actionResult = studentsController.GetStudent("sampleStudentId");
            var contentResult = actionResult as OkNegotiatedContentResult<StudentBasicDto>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("sampleStudentId", contentResult.Content.Id);
        }

        [TestMethod]
        public void GetStudent_ShouldReturnNotFound()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            studentRepo = new MockStudentRepo(testSchoolRegisterContext);
            studentsController = new StudentsController(studentRepo);
            PopulateStudentFields();

            //Act
            var actionResult = studentsController.GetStudent("notExistingStudentId");


            //Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetStudent_ShouldReturnForbidden()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            studentRepo = new MockStudentRepo(testSchoolRegisterContext);
            studentsController = new StudentsController(studentRepo);
            PopulateStudentFields();

            //Act
            var actionResult = studentsController.GetStudent("unauthenticatedStudentId") as ResponseMessageResult;


            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(actionResult.Response.StatusCode, HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public void GetStudent_ShouldReturnBadRequest()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            studentRepo = new MockStudentRepo(testSchoolRegisterContext);
            studentsController = new StudentsController(studentRepo);

            //Act
            IHttpActionResult actionResult = studentsController.GetStudent(null);

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));

        }

        [TestMethod]
        public void PutStudent__ShouldReturnOk()
        {
            // Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            studentRepo = new MockStudentRepo(testSchoolRegisterContext);
            studentsController = new StudentsController(studentRepo);
            PopulateStudentFields();

            //Act
            var actionResult = studentsController.PutStudent("sampleStudentId", new Student {Id = "sampleStudentId"});
            var contentResult = actionResult as OkNegotiatedContentResult<Student>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("sampleStudentId", contentResult.Content.Id);
        }

        [TestMethod]
        public void PutStudent_ShouldReturnBadRequestDifferentId()
        {
            // Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            studentRepo = new MockStudentRepo(testSchoolRegisterContext);
            studentsController = new StudentsController(studentRepo);

            //Act
            IHttpActionResult actionResult =
                studentsController.PutStudent("notExistingStudentId", new Student {Id = "sampleStudentId"});

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void PutStudent_ShouldReturnNotFound()
        {
            // Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            studentRepo = new MockStudentRepo(testSchoolRegisterContext);
            studentsController = new StudentsController(studentRepo);

            //Act
            IHttpActionResult actionResult =
                studentsController.PutStudent("sampleStudentId", new Student {Id = "sampleStudentId"});

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutStudent_ShouldReturnForbidden()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            studentRepo = new MockStudentRepo(testSchoolRegisterContext);
            studentsController = new StudentsController(studentRepo);
            PopulateStudentFields();

            //Act
            var actionResult =
                studentsController.PutStudent("unauthenticatedStudentId", new Student {Id = "unauthenticatedStudentId"})
                    as ResponseMessageResult;


            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(actionResult.Response.StatusCode, HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public void DeleteStudent__ShouldReturnOk()
        {
            // Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            studentRepo = new MockStudentRepo(testSchoolRegisterContext);
            studentsController = new StudentsController(studentRepo);
            PopulateStudentFields();

            //Act
            var actionResult = studentsController.GetStudent("sampleStudentId");
            var contentResult = actionResult as OkNegotiatedContentResult<StudentBasicDto>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("sampleStudentId", contentResult.Content.Id);
        }

        [TestMethod]
        public void DeleteStudent_ShouldReturnNotFound()
        {
            // Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            studentRepo = new MockStudentRepo(testSchoolRegisterContext);
            studentsController = new StudentsController(studentRepo);

            //Act
            IHttpActionResult actionResult = studentsController.DeleteStudent("sampleStudentId");

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteStudent_ShouldReturnForbidden()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            studentRepo = new MockStudentRepo(testSchoolRegisterContext);
            studentsController = new StudentsController(studentRepo);
            PopulateStudentFields();

            //Act
            var actionResult = studentsController.DeleteStudent("unauthenticatedStudentId") as ResponseMessageResult;


            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(actionResult.Response.StatusCode, HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public void GetCoursesByStudentId__ShouldReturnOk()
        {
            // Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            studentRepo = new MockStudentRepo(testSchoolRegisterContext);
            studentsController = new StudentsController(studentRepo);
            PopulateStudentFields();

            //Act
            var actionResult = studentsController.GetStudent("sampleStudentId");
            var contentResult = actionResult as OkNegotiatedContentResult<StudentBasicDto>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("sampleStudentId", contentResult.Content.Id);
        }

        [TestMethod]
        public void GetCoursesByStudentId_ShouldReturnNotFound()
        {
            // Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            studentRepo = new MockStudentRepo(testSchoolRegisterContext);
            studentsController = new StudentsController(studentRepo);

            //Act
            IHttpActionResult actionResult = studentsController.GetCoursesByStudentId("sampleStudentId");

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetCoursesByStudentId_ShouldReturnForbidden()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            studentRepo = new MockStudentRepo(testSchoolRegisterContext);
            studentsController = new StudentsController(studentRepo);
            PopulateStudentFields();

            //Act
            var actionResult =
                studentsController.GetCoursesByStudentId("unauthenticatedStudentId") as ResponseMessageResult;


            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(actionResult.Response.StatusCode, HttpStatusCode.Forbidden);
        }

        private void PopulateStudentFields()
        {
            var claim = new Claim("test", "sampleStudentId");
            var mockIdentity =
                Mock.Of<ClaimsIdentity>(ci => ci.FindFirst(It.IsAny<string>()) == claim);

            studentsController.User = Mock.Of<IPrincipal>(ip => ip.Identity == mockIdentity);


            var course1 = new Course {Id = 1, Name = "Mathematics", StudentsGroupId = 1};
            var course2 = new Course {Id = 2, Name = "PE", StudentsGroupId = 1,};
            var course3 = new Course {Id = 3, Name = "Literature", StudentsGroupId = 1};

            var studentGroup = new StudentGroup
            {
                Id = 1,
                Grade = 1,
                Name = "SampleStudentGroup",
                Courses = new List<Course>()
                {
                    course1,
                    course2,
                    course3
                }
            };

            testSchoolRegisterContext.Courses.Add(course1);
            testSchoolRegisterContext.Courses.Add(course2);
            testSchoolRegisterContext.Courses.Add(course3);
            testSchoolRegisterContext.Students.Add(new Student()
            {
                Id = "sampleStudentId",
                FirstName = "John",
                LastName = "Doe",
                StudentGroup = studentGroup
            });
            testSchoolRegisterContext.Students.Add(new Student()
            {
                Id = "unauthenticatedStudentId",
                FirstName = "Jane",
                LastName = "Doe",
                StudentGroup = studentGroup
            });

        }
    }
}
