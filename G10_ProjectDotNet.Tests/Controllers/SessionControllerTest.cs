using G10_ProjectDotNet.Controllers;
using G10_ProjectDotNet.Models.Domain;
using G10_ProjectDotNet.Models.SessionViewModels;
using G10_ProjectDotNet.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace G10_ProjectDotNet.Tests.Controllers
{
    public class SessionControllerTest
    {
        private readonly SessionController _controller;
        private readonly Mock<IFormulaRepository> _formulaRepository;
        private readonly Mock<ISessionRepository> _sessionRepository;
        private readonly DummyApplicationDbContext _dummyContext;

        public SessionControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _formulaRepository = new Mock<IFormulaRepository>();
            _sessionRepository = new Mock<ISessionRepository>();
            _controller = new SessionController(_formulaRepository.Object, _sessionRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }

        [Fact]
        public void Index_NoSessionInProgress()
        {
            _sessionRepository.Setup(m => m.GetByDateToday()).Returns((Session) null);

            var actionResult = _controller.Index() as ViewResult;
            var viewModel = actionResult?.Model as IndexViewModel;

            Assert.Null(viewModel.Session);
        }

        [Fact]
        public void Index_PassesInformationInViewmodel()
        {
            _sessionRepository.Setup(m => m.GetByDateToday()).Returns(_dummyContext.Session);

            var actionResult = _controller.Index() as ViewResult;
            var viewModel = actionResult?.Model as IndexViewModel;

            Assert.Equal(2, viewModel.Session.Attendances.Count);
        }

        [Fact]
        public void Create_ValidSession_RedirectsToSessionIndex()
        {
            int day = ((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek;
            _formulaRepository.Setup(m => m.GetByWeekDay(day)).Returns(_dummyContext.Formulas);
            _sessionRepository.Setup(m => m.GetLatest()).Returns(_dummyContext.SessionLastWeek);

            RedirectToActionResult action = _controller.Create() as RedirectToActionResult;

            Assert.Equal("Index", action?.ActionName);
            Assert.Equal("Session", action?.ControllerName);
        }

        [Fact]
        public void Create_ValidSession_CreatesAndPersistsSession()
        {
            _sessionRepository.Setup(m => m.Add(It.IsAny<Session>()));
            int day = ((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek;
            _formulaRepository.Setup(m => m.GetByWeekDay(day)).Returns(_dummyContext.Formulas);
            _sessionRepository.Setup(m => m.GetLatest()).Returns(_dummyContext.SessionLastWeek);

            _controller.Create();

            _sessionRepository.Verify(m => m.Add(It.IsAny<Session>()), Times.Once());
            _sessionRepository.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Create_NoSessionToday_RedirectsToActionIndex()
        {
            int day = ((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek;
            _formulaRepository.Setup(m => m.GetByWeekDay(day)).Returns((new List<Formula>()));
            
            RedirectToActionResult action = _controller.Create() as RedirectToActionResult;

            Assert.Equal("Index", action?.ActionName);
            Assert.Equal("Home", action?.ControllerName);
        }

        [Fact]
        public void Create_NoSessionToday_DoesNotCreateNorPersistSession()
        {
            int day = ((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek;
            _formulaRepository.Setup(m => m.GetByWeekDay(day)).Returns(new List<Formula>());
            _sessionRepository.Setup(m => m.Add(It.IsAny<Session>()));

            RedirectToActionResult action = _controller.Create() as RedirectToActionResult;

            _sessionRepository.Verify(m => m.Add(It.IsAny<Session>()), Times.Never());
            _sessionRepository.Verify(m => m.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Create_SessionAlreadyDone_RedirectsToActionIndex()
        {
            int day = ((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek;
            _formulaRepository.Setup(m => m.GetByWeekDay(day)).Returns(_dummyContext.Formulas);
            _sessionRepository.Setup(m => m.GetLatest()).Returns(_dummyContext.Session);

            RedirectToActionResult action = _controller.Create() as RedirectToActionResult;

            Assert.Equal("Index", action?.ActionName);
            Assert.Equal("Home", action?.ControllerName);
        }

        [Fact]
        public void Create_SessionAlreadyDone_DoesNotCreateNorPersistSession()
        {
            int day = ((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek;
            _formulaRepository.Setup(m => m.GetByWeekDay(day)).Returns(_dummyContext.Formulas);
            _sessionRepository.Setup(m => m.GetLatest()).Returns(_dummyContext.Session);
            _sessionRepository.Setup(m => m.Add(It.IsAny<Session>()));

            _controller.Create();

            _sessionRepository.Verify(m => m.Add(It.IsAny<Session>()), Times.Never());
            _sessionRepository.Verify(m => m.SaveChanges(), Times.Never());
        }

        [Fact]
        public void EndSession_RedirectsToActionIndex()
        {
            _sessionRepository.Setup(m => m.GetLatest()).Returns(_dummyContext.Session);

            RedirectToActionResult action = _controller.EndSession() as RedirectToActionResult;

            Assert.Equal("Index", action?.ActionName);
            Assert.Equal("Home", action?.ControllerName);
        }

        [Fact]
        public void EndSession_EndsAndPersistSession()
        {
            _sessionRepository.Setup(m => m.GetLatest()).Returns(_dummyContext.Session);

            RedirectToActionResult action = _controller.EndSession() as RedirectToActionResult;

            _sessionRepository.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}