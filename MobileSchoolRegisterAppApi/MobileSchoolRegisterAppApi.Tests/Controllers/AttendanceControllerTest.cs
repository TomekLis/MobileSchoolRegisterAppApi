using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileSchoolRegisterAppApi.Controllers;
using MobileSchoolRegisterAppApi.Tests.Contexts;
using MobileSchoolRegisterAppApi.Tests.MockRepos;
using Repository.IRepo;
using Repository.Models;
using Repository.Models.DTOs.Attendance;

namespace MobileSchoolRegisterAppApi.Tests.Controllers
{
    [TestClass]
    public class AttendanceControllerTest
    {
        private TestSchoolRegisterContext testSchoolRegisterContext;
        private IAttendanceRepo attendanceRepo;
        private AttendancesController attendancesController;

        [TestMethod]
        public void GetAttendance_ShouldReturnOk()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            attendanceRepo = new MockAttendanceRepo(testSchoolRegisterContext);
            attendancesController = new AttendancesController(attendanceRepo);
            PopulateAttendanceFields();

            //Act
            var actionResult = attendancesController.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<AttendanceDto>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }

        [TestMethod]
        public void PostAttendance_ShouldReturnOk()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            attendanceRepo = new MockAttendanceRepo(testSchoolRegisterContext);
            attendancesController = new AttendancesController(attendanceRepo);
            PopulateAttendanceFields();
            Attendance attendance = new Attendance() { Id = 1 };

            //Act
            var actionResult = attendancesController.PostAttendance(attendance);

            // Assert
            Assert.IsNotNull(actionResult);
        }
        [TestMethod]
        public void PuttAttendance_ShouldReturnOk()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            attendanceRepo = new MockAttendanceRepo(testSchoolRegisterContext);
            attendancesController = new AttendancesController(attendanceRepo);
            PopulateAttendanceFields();

            Attendance attendance = new Attendance() {Id = 1};

            //Act
            var actionResult = attendancesController.Put(1, attendance);

            // Assert
            Assert.IsNotNull(actionResult);
        }
        [TestMethod]
        public void DeletetAttendance_ShouldReturnOk()
        {
            //Arrange
            testSchoolRegisterContext = new TestSchoolRegisterContext();
            attendanceRepo = new MockAttendanceRepo(testSchoolRegisterContext);
            attendancesController = new AttendancesController(attendanceRepo);
            PopulateAttendanceFields();

            //Act
            var actionResult = attendancesController.DeleteAttendance(1);

            // Assert
            Assert.IsNotNull(actionResult);
        }

        private void PopulateAttendanceFields()
        {
            var attendance1 = new Attendance { Id = 1};
            var attendance2 = new Attendance { Id = 2};
            var attendance3 = new Attendance { Id = 3};

            testSchoolRegisterContext.Attendances.Add(attendance1);
            testSchoolRegisterContext.Attendances.Add(attendance2);
            testSchoolRegisterContext.Attendances.Add(attendance3);
        }


    }
}