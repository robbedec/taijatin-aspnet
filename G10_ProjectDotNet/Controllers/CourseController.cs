using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G10_ProjectDotNet.Models;
using G10_ProjectDotNet.Models.CourseViewModel;
using G10_ProjectDotNet.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace G10_ProjectDotNet.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseModuleRepository _courseModuleRepository;
        private readonly ICourseModuleViewerRepository _courseModuleViewerRepository;
        private readonly IMemberRepository _memberRepository;

        public CourseController(ICourseRepository courseRepository, ICourseModuleRepository courseModuleRepository, ICourseModuleViewerRepository courseModuleViewerRepository, IMemberRepository memberRepository)
        {
            _courseRepository = courseRepository;
            _courseModuleRepository = courseModuleRepository;
            _courseModuleViewerRepository = courseModuleViewerRepository;
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

        //public IActionResult Detail(int courseModuleId, int memberId)
        //{
        //    return ViewComponent("DetailComponent", new { courseModuleId, memberId });
        //}

        public IActionResult CourseModuleMembers()
        {
            return View();
        }
    }
}