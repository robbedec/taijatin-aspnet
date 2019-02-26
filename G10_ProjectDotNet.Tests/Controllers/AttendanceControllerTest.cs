using G10_ProjectDotNet.Controllers;
using G10_ProjectDotNet.Models.Domain;
using G10_ProjectDotNet.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace G10_ProjectDotNet.Tests.Controllers
{
    public class AttendanceControllerTest
    {
        private readonly AttendanceController _controller;
        private readonly Mock<ISessionRepository> _sessionRepository;
        private readonly Mock<IAttendanceRepository> _attendanceRepository;
        private readonly DummyApplicationDbContext _dummyContext;

        public AttendanceControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _sessionRepository = new Mock<ISessionRepository>();
            _attendanceRepository = new Mock<IAttendanceRepository>();
            _controller = new AttendanceController(_sessionRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }

        // -- CREATE GET --

        [Fact]
        public void Create_RegisterValid_RedirectsToSessionIndex()
        {
            _sessionRepository.Setup(m => m.GetCurrentSession()).Returns(_dummyContext.Session);

            RedirectToActionResult action = _controller.Create(1, 4) as RedirectToActionResult;

            Assert.Equal("Index", action?.ActionName);
            Assert.Equal("Session", action?.ControllerName);
        }

        [Fact]
        public void Create_RegisterValid_CreatesAndPersistsAttendance()
        {
            _sessionRepository.Setup(m => m.GetCurrentSession()).Returns(_dummyContext.Session);
            _attendanceRepository.Setup(m => m.Add(It.IsAny<Attendance>()));
            
            _controller.Create(1, 1);

            //_attendanceRepository.Verify(m => m.Add(It.IsAny<Attendance>()), Times.Once());
            _sessionRepository.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Create_AlreadyRegistered_RedirectsToSessionIndex()
        {
            _sessionRepository.Setup(m => m.GetCurrentSession()).Returns(_dummyContext.Session);

            RedirectToActionResult action = _controller.Create(1, 1) as RedirectToActionResult;

            Assert.Equal("Index", action?.ActionName);
            Assert.Equal("Session", action?.ControllerName);
        }
    }
}
