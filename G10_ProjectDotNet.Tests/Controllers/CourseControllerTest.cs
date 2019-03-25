using G10_ProjectDotNet.Controllers;
using G10_ProjectDotNet.Models.CourseViewModel;
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
    public class CourseControllerTest
    {
        private readonly CourseController _controller;
        private readonly Mock<ICourseRepository> _courseRepository;
        private readonly Mock<ICourseModuleRepository> _courseModuleRepository;
        private readonly Mock<ICourseModuleViewerRepository> _courseModuleViewerRepository;
        private readonly Mock<IMemberRepository> _memberRepository;
        private readonly DummyApplicationDbContext _dummyContext;

        public CourseControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _courseRepository = new Mock<ICourseRepository>();
            _courseModuleRepository = new Mock<ICourseModuleRepository>();
            _courseModuleViewerRepository = new Mock<ICourseModuleViewerRepository>();
            _memberRepository = new Mock<IMemberRepository>();
            _controller = new CourseController(_courseRepository.Object, _courseModuleRepository.Object, _courseModuleViewerRepository.Object, _memberRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }

        [Fact]
        public void Index_DoesntShowCourses()
        {

            var actionResult = _controller.Index(1, null) as ViewResult;
            var viewModel = actionResult?.Model as IndexViewModel;
        }

        [Fact]
        public void Index_DoesntShowCourseModule()
        {

        }

        [Theory]
        [InlineData(5,5)]
        public void Index_ShowCoursesForUser(int memberId, int amount)
        {

        }
    }
}
