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
        private readonly Mock<IMemberRepository> _memberRepository;
        private readonly DummyApplicationDbContext _dummyContext;

        public AttendanceControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _sessionRepository = new Mock<ISessionRepository>();
            _memberRepository = new Mock<IMemberRepository>();
            _controller = new AttendanceController(_sessionRepository.Object, _memberRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }

        [Fact]
        public void Create_RegisterValid_RedirectsToSessionIndex()
        {
            _sessionRepository.Setup(m => m.GetByDateToday()).Returns(_dummyContext.Session);
            _memberRepository.Setup(m => m.GetById(5)).Returns(_dummyContext.Member2Dagen);

            RedirectToActionResult action = _controller.Create(5) as RedirectToActionResult;

            Assert.Equal("Index", action?.ActionName);
            Assert.Equal("Session", action?.ControllerName);
        }

        [Fact]
        public void Create_RegisterValid_CreatesAndPersistsAttendance()
        {
            _sessionRepository.Setup(m => m.GetByDateToday()).Returns(_dummyContext.Session);
            _memberRepository.Setup(m => m.GetById(1)).Returns(_dummyContext.Member1Dag);

            _controller.Create(1);

            _sessionRepository.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Create_AlreadyRegistered_RedirectsToSessionIndex()
        {
            _sessionRepository.Setup(m => m.GetByDateToday()).Returns(_dummyContext.Session);
            _memberRepository.Setup(m => m.GetById(1)).Returns(_dummyContext.Member1Dag);

            RedirectToActionResult action = _controller.Create(1) as RedirectToActionResult;

            Assert.Equal("Index", action?.ActionName);
            Assert.Equal("Session", action?.ControllerName);
        }
    }
}
