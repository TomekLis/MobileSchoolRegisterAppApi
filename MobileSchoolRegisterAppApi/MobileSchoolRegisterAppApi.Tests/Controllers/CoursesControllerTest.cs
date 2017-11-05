using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileSchoolRegisterAppApi.Controllers;
using Repository.IRepo;
using Repository.Models;
using Repository.Repo;

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
            courseRepo = new CourseRepo(testSchoolRegisterContext);
            coursesController = new CoursesController(courseRepo);
            PopulateCourseFields();
            
            //Act
            var actionResult = coursesController.GetCourse(1);
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
            courseRepo = new CourseRepo(testSchoolRegisterContext);
            coursesController = new CoursesController(courseRepo);
            PopulateCourseFields();
            
            //Act
            IHttpActionResult actionResult = coursesController.GetCourse(10);

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetCourse_ShouldReturnBadRequest()
        {
            //Arrange
            coursesController = new CoursesController(courseRepo);

            //Act
            IHttpActionResult actionResult = coursesController.GetCourse(null);

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetCourses_ShouldReturnOk()
        {
            //Arrange 
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            courseRepo = new CourseRepo(testSchoolRegisterContext);
            coursesController = new CoursesController(courseRepo);
            PopulateCourseFields();
            //asldmalksd
            //Act
            IQueryable<Course> actionResult = coursesController.GetCourses();
        }


        private void PopulateCourseFields()
        {
            testSchoolRegisterContext.Courses.Add(new Course { Id = 1, Name = "Mathematics", StudentsGroupId = 1, TeacherId = "sampleTeacherId" });
            testSchoolRegisterContext.Courses.Add(new Course { Id = 2, Name = "Mathematics", StudentsGroupId = 1, TeacherId = "sampleTeacherId" });
            testSchoolRegisterContext.Courses.Add(new Course { Id = 3, Name = "Mathematics", StudentsGroupId = 1, TeacherId = "sampleTeacherId" });
        }
    }
}
