using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileSchoolRegisterAppApi.Controllers;
using MobileSchoolRegisterAppApi.Tests.Contexts;
using MobileSchoolRegisterAppApi.Tests.MockRepos;
using Repository.IRepo;
using Repository.Models;

namespace MobileSchoolRegisterAppApi.Tests.Controllers
{
    [TestClass]
    public class CoursesControllerTest
    {
        private TestSchoolRegisterContext testSchoolRegisterContext;
        private ICourseRepo courseRepo;
        private CoursesController coursesController;

        [TestMethod]
        public void GetCourse_ShouldReturnOk()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            courseRepo = new MockCourseRepo(testSchoolRegisterContext);
            coursesController = new CoursesController(courseRepo);
            PopulateCourseFields();
            
            //Act
            var actionResult = coursesController.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Course>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }

        [TestMethod]
        public void GetCourse_ShouldReturnNotFound()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            courseRepo = new MockCourseRepo(testSchoolRegisterContext);
            coursesController = new CoursesController(courseRepo);
            PopulateCourseFields();
            
            //Act
            IHttpActionResult actionResult = coursesController.Get(10);

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetCourse_ShouldReturnBadRequest()
        {
            //Arrange
            coursesController = new CoursesController(courseRepo);

            //Act
            IHttpActionResult actionResult = coursesController.Get(null);

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));

        }

        [TestMethod]
        public void GetCourses_ShouldReturnOk()
        {
            //Arrange 
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            courseRepo = new MockCourseRepo(testSchoolRegisterContext);
            coursesController = new CoursesController(courseRepo);
            PopulateCourseFields();
            
            //Act

            IHttpActionResult actionResult = coursesController.GetCourses();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<Course>>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            for (int i = 0; i <= 1; i++)
            {
                Assert.AreEqual(i + 1, contentResult.Content.ElementAt(i).Id);
            }
        }

        [TestMethod]
        public void PutCourseShouldReturnAccepted()
        {
            // Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            courseRepo = new MockCourseRepo(testSchoolRegisterContext);
            coursesController = new CoursesController(courseRepo);
            PopulateCourseFields();

            // Act
            IHttpActionResult actionResult = coursesController.Put(1, new Course{Id = 1});
            var contentResult = actionResult as NegotiatedContentResult<Course>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
        }
        [TestMethod]
        public void PutCourseShouldReturnBadRequestDifferentId()
        {
            // Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            courseRepo = new MockCourseRepo(testSchoolRegisterContext);
            coursesController = new CoursesController(courseRepo);

            //Act
            IHttpActionResult actionResult = coursesController.Put(99, new Course { Id = 1 });

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }
        [TestMethod]
        public void PutCourseShouldReturnNotFound()
        {
            // Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            courseRepo = new MockCourseRepo(testSchoolRegisterContext);
            coursesController = new CoursesController(courseRepo);

            //Act
            IHttpActionResult actionResult = coursesController.Put(1, new Course { Id = 1 });

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }







        private void PopulateCourseFields()
        {
            testSchoolRegisterContext.Courses.Add(new Course { Id = 1, Name = "Mathematics", StudentsGroupId = 1, TeacherId = "sampleTeacherId" });
            testSchoolRegisterContext.Courses.Add(new Course { Id = 2, Name = "Mathematics", StudentsGroupId = 1, TeacherId = "sampleTeacherId" });
            testSchoolRegisterContext.Courses.Add(new Course { Id = 3, Name = "Mathematics", StudentsGroupId = 1, TeacherId = "sampleTeacherId" });
        }
    }
}
