using G10_ProjectDotNet.Controllers;
using G10_ProjectDotNet.Models.Domain;
using G10_ProjectDotNet.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
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
            _controller = new GroupController(_groupRepository.Object);
        }

        [Fact]
        public void Index_PassesOrderdListOfGroups()
        {
            _groupRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Groups);
            IActionResult actionResult = _controller.Index(1);

            IList<Group> groupsInModel = (actionResult as ViewResult)?.Model as IList<Group>;
            //Assert.Equal(2, groupsInModel?.Count);
            Assert.Equal("Teacher", groupsInModel?[0].Teacher.UserName);
        }
    }
}
