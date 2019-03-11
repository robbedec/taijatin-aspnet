using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G10_ProjectDotNet.Models.CourseViewModel;
using G10_ProjectDotNet.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace G10_ProjectDotNet.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseModuleRepository _courseModuleRepository;
        private readonly IMemberRepository _memberRepository;

        public CourseController(ICourseRepository courseRepository, ICourseModuleRepository courseModuleRepository, IMemberRepository memberRepository)
        {
            _courseRepository = courseRepository;
            _courseModuleRepository = courseModuleRepository;
            _memberRepository = memberRepository;
        }

        [AllowAnonymous]
        public IActionResult Index(int memberId, int? courseId)
        {
            var viewModel = new IndexViewModel();
            viewModel.MemberId = memberId;
            viewModel.Courses = _courseRepository.GetByMinGrade(_memberRepository.GetById(memberId).Grade);
            if (courseId.HasValue)
            {
                viewModel.Modules = _courseModuleRepository.GetByCourse(courseId.Value);
            }
            
            return View(viewModel);
        }
    }
}