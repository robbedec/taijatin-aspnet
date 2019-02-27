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
        private readonly Mock<IGroupRepository> _groupRepository;
        private readonly Mock<ISessionRepository> _sessionRepository;
        private readonly DummyApplicationDbContext _dummyContext;

        public SessionControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _groupRepository = new Mock<IGroupRepository>();
            _sessionRepository = new Mock<ISessionRepository>();
            _controller = new SessionController(_groupRepository.Object, _sessionRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }

        [Fact]
        public void Index_NoSessionInProgress()
        {
            _sessionRepository.Setup(m => m.GetCurrentSession()).Returns((Session)null);

            var actionResult = _controller.Index() as ViewResult;
            var viewModel = actionResult?.Model as IndexViewModel;

            Assert.Null(viewModel.Session);
            Assert.Null(viewModel.UserGroups);
        }

        //[Fact]
        //public void Index_PassesListOfUserGroupsInViewmodel()
        //{
        //    _sessionRepository.Setup(m => m.GetCurrentSession()).Returns(_dummyContext.Session);
        //    _groupRepository.Setup(m => m.GetLinkedUserGroups(2)).Returns(_dummyContext.UserGroups);

        //    var actionResult = _controller.Index() as ViewResult;
        //    var userGroupsInModel = actionResult?.Model as IndexViewModel;

        //    Assert.Single(userGroupsInModel.UserGroups);
        //}

        // -- CREATE GET --

        [Fact]
        public void Create_GroupsInViewData()
        {
            _groupRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Groups);

            var actionResult = _controller.Create();
            var viewResult = actionResult as ViewResult;
            var groupsInViewData = viewResult?.ViewData["Groups"] as SelectList;

            Assert.Equal(2, groupsInViewData.Count());
        }

        // -- CREATE POST --

        [Fact]
        public void Create_ValidSession_RedirectsToSessionIndex()
        {
            CreateSessionViewModel sessionViewmodel = new CreateSessionViewModel
            {
                Group = _dummyContext.Groups.FirstOrDefault().GroupId,
                StartTime = 14,
                Duration = 2
            };

            RedirectToActionResult action = _controller.Create(sessionViewmodel) as RedirectToActionResult;

            Assert.Equal("Index", action?.ActionName);
            Assert.Equal("Session", action?.ControllerName);
        }

        [Fact]
        public void Create_ValidSession_CreatesAndPersistsSession()
        {
            _sessionRepository.Setup(m => m.Add(It.IsAny<Session>()));
            _sessionRepository.Setup(m => m.GetCurrentSession()).Returns((Session)null);
            CreateSessionViewModel viewModel = new CreateSessionViewModel
            {
                Group = _dummyContext.Groups.FirstOrDefault().GroupId,
                StartTime = 14,
                Duration = 2
            };

            _controller.Create(viewModel);

            _sessionRepository.Verify(m => m.Add(It.IsAny<Session>()), Times.Once());
            _sessionRepository.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Create_InvalidSession_RedirectsToActionIndex()
        {
            _sessionRepository.Setup(m => m.GetCurrentSession()).Returns(_dummyContext.Session);
            CreateSessionViewModel viewModel = new CreateSessionViewModel
            {
                Group = _dummyContext.Groups.FirstOrDefault().GroupId,
                StartTime = 14,
                Duration = 2
            };

            RedirectToActionResult action = _controller.Create(viewModel) as RedirectToActionResult;

            Assert.Equal("Index", action?.ActionName);
            Assert.Equal("Session", action?.ControllerName);

        }

        [Fact]
        public void Create_InvalidSession_DoesNotCreateNorPersistSession()
        {
            _sessionRepository.Setup(m => m.Add(It.IsAny<Session>()));
            _sessionRepository.Setup(m => m.GetCurrentSession()).Returns(_dummyContext.Session);
            CreateSessionViewModel viewModel = new CreateSessionViewModel
            {
                Group = _dummyContext.Groups.FirstOrDefault().GroupId,
                StartTime = 14,
                Duration = 2
            };

            RedirectToActionResult action = _controller.Create(viewModel) as RedirectToActionResult;

            _sessionRepository.Verify(m => m.Add(It.IsAny<Session>()), Times.Never());
            _sessionRepository.Verify(m => m.SaveChanges(), Times.Never());
        }
    }
}