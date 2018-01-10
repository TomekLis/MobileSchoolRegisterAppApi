using System;
using System.Net;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileSchoolRegisterAppApi.Controllers;
using MobileSchoolRegisterAppApi.Tests.Contexts;
using MobileSchoolRegisterAppApi.Tests.MockRepos;
using Repository.IRepo;
using Repository.Models;
using Repository.Models.DTOs.Mark;

namespace MobileSchoolRegisterAppApi.Tests.Controllers
{
    [TestClass]
    public class MarkControllerTest
    {
        private TestSchoolRegisterContext testSchoolRegisterContext;
        private IMarkRepo markRepo;
        private MarksController marksController;

        [TestMethod]
        public void GetMark_ShouldReturnOk()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            markRepo = new MockMarkRepo(testSchoolRegisterContext);
            marksController = new MarksController(markRepo);
            PopulateMarkFields();

            //Act
            var actionResult = marksController.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<MarkDto>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }

        [TestMethod]
        public void PostMark_ShouldReturnOk()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            markRepo = new MockMarkRepo(testSchoolRegisterContext);
            marksController = new MarksController(markRepo);
            PopulateMarkFields();
            Mark mark = new Mark() { Id = 1 };

            //Act
            var actionResult = marksController.PostMark(mark);

            // Assert
            Assert.IsNotNull(actionResult);
        }
        [TestMethod]
        public void PuttMark_ShouldReturnOk()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            markRepo = new MockMarkRepo(testSchoolRegisterContext);
            marksController = new MarksController(markRepo);
            PopulateMarkFields();

            Mark mark = new Mark() {Id = 1};

            //Act
            var actionResult = marksController.Put(1, mark);

            // Assert
            Assert.IsNotNull(actionResult);
        }
        [TestMethod]
        public void DeletetMark_ShouldReturnOk()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            markRepo = new MockMarkRepo(testSchoolRegisterContext);
            marksController = new MarksController(markRepo);
            PopulateMarkFields();

            //Act
            var actionResult = marksController.DeleteMark(1);

            // Assert
            Assert.IsNotNull(actionResult);
        }

        private void PopulateMarkFields()
        {
            var mark1 = new Mark { Id = 1};
            var mark2 = new Mark { Id = 2};
            var mark3 = new Mark { Id = 3};

            testSchoolRegisterContext.Marks.Add(mark1);
            testSchoolRegisterContext.Marks.Add(mark2);
            testSchoolRegisterContext.Marks.Add(mark3);
        }


    }
}
