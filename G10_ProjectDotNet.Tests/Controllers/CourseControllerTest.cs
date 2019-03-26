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
            _courseRepository.Setup(c => c.GetByMinGrade((Grade) 0)).Returns((IEnumerable<Course>)new List<Course>());

            var actionResult = _controller.Index(1, null) as ViewResult;
            var viewModel = actionResult?.Model as IndexViewModel;

            Assert.Empty(viewModel.Courses);
        }

        [Fact]
        public void Index_ShowssCourses()
        {
            _courseRepository.Setup(c => c.GetByMinGrade((Grade)3)).Returns(_dummyContext.Courses);

            var actionResult = _controller.Index(1, null) as ViewResult;
            var viewModel = actionResult?.Model as IndexViewModel;

            Assert.NotEmpty(viewModel.Courses);
        }

        [Fact]
        public void Index_DoesntShowListCourseModules()
        {
            _courseModuleRepository.Setup(m => m.GetByCourse(0)).Returns((IEnumerable<CourseModule>) new List<CourseModule>());

            var actionResult = _controller.Index(1, null) as ViewResult;
            var viewModel = actionResult?.Model as IndexViewModel;

            Assert.Empty(viewModel.Modules);
        }

        [Fact]
        public void Index_ShowsListCourseModules()
        {
            _courseModuleRepository.Setup(m => m.GetByCourse(1)).Returns(_dummyContext.CourseModules);

            var actionResult = _controller.Index(1, null) as ViewResult;
            var viewModel = actionResult?.Model as IndexViewModel;

            Assert.NotEmpty(viewModel.Modules);

        }

        [Fact]
        public void Index_DoesntShowCourseModule()
        {
            _courseModuleRepository.Setup(m => m.GetById(null)).Returns((CourseModule)null);

            var actionResult = _controller.Index(1, null) as ViewResult;
            var viewModel = actionResult?.Model as IndexViewModel;

            Assert.Null(viewModel.CourseModule);
        }

        [Fact]
        public void Index_ShowsCourseModule()
        {
            _courseModuleRepository.Setup(m => m.GetById(1)).Returns(_dummyContext.CourseModule);

            var actionResult = _controller.Index(1, 1) as ViewResult;
            var viewModel = actionResult?.Model as IndexViewModel;

            Assert.NotNull(viewModel.CourseModule);
        }

        [Theory]
        [InlineData(5,5)]
        public void Index_ShowCoursesForUser(int memberId, int amount)
        {

        }
    }
}
