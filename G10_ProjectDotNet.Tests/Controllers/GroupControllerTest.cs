using G10_ProjectDotNet.Controllers;
using G10_ProjectDotNet.Models.Domain;
using G10_ProjectDotNet.Models.GroupViewModels;
using G10_ProjectDotNet.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace G10_ProjectDotNet.Tests.Controllers
{
    public class GroupControllerTest
    {
        private readonly GroupController _controller;
        private readonly Mock<IGroupRepository> _groupRepository;
        private readonly DummyApplicationDbContext _dummyContext;

        public GroupControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _groupRepository = new Mock<IGroupRepository>();
            _controller = new GroupController(_groupRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }

        [Fact]
        public void Index_PassesListOfGroupsInIndexViewModel()
        {
            _groupRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Groups);
            var actionResult = _controller.Index(null) as ViewResult;
            var groupIndexViewModel = actionResult?.Model as IndexViewModel;
            Assert.Equal(2, groupIndexViewModel.Groups.Count());
        }

        [Fact]
        public void Index_CheckGroupTeacher()
        {
            _groupRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Groups);
            var actionResult = _controller.Index(null) as ViewResult;
            var groupIndexViewModel = actionResult?.Model as IndexViewModel;
            Assert.Equal("Teacher", groupIndexViewModel.Groups.First().Teacher.UserName);
        }

        [Fact]
        public void Index_PassesGroupIdInViewData()
        {
            _groupRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Groups);
            _groupRepository.Setup(m => m.GetLinkedUserGroups(1)).Returns(_dummyContext.UserGroups.Where(m => m.GroupId == 1));
            var actionResult = _controller.Index(1) as ViewResult;
            var groupIndexViewModel = actionResult?.Model as IndexViewModel;
            Assert.Equal(1, (actionResult as ViewResult)?.ViewData["id"]);
        }
    }
}
