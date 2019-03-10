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
        public IActionResult ShowCategories(int memberId)
        {
            var viewModel = new CategoryViewModel();
            var member = _memberRepository.GetById(memberId);
            var grade = member.Grade;
            var types = new[] { TypeOfExcersise.Afbeelding, TypeOfExcersise.Audio, TypeOfExcersise.Tekst, TypeOfExcersise.Video };
            viewModel.Grade = grade;
            viewModel.Types = types;
            
            return View(viewModel);
        }

        public IActionResult CourseModuleList()
        {
            var viewModel = new CourseModuleListViewModel();
            var courseModules = _courseModuleRepository.GetAll();
            if(courseModules != null)
            {
                viewModel.CourseModules = courseModules;
            }
            return View(viewModel);
        }
    }
}