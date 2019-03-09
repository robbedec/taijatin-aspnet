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

        public CourseController(ICourseRepository courseRepository, ICourseModuleRepository courseModuleRepository)
        {
            _courseRepository = courseRepository;
            _courseModuleRepository = courseModuleRepository;
        }

        public IActionResult Courses()
        {
            var viewModel = new CourseViewModel();
            var courses = _courseRepository.GetAll();
            if (courses != null)
            {
                viewModel.Courses = courses;
            }
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