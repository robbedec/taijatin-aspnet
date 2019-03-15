using G10_ProjectDotNet.Models.CourseViewModel;
using G10_ProjectDotNet.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.ViewComponents
{
    public class DetailComponent : ViewComponent
    {
        private readonly ICourseModuleRepository _courseModuleRepository;
        private readonly ICourseModuleViewerRepository _courseModuleViewerRepository;

        public DetailComponent(ICourseModuleRepository courseModuleRepository, ICourseModuleViewerRepository courseModuleViewerRepository)
        {
            _courseModuleRepository = courseModuleRepository;
            _courseModuleViewerRepository = courseModuleViewerRepository;
        }

        public IViewComponentResult Invoke(int courseModuleId, int memberId)
        {
            var viewModel = new CourseModuleViewModel();
            var courseModule = _courseModuleRepository.GetById(courseModuleId);
            if (courseModule != null)
            {
                viewModel.MemberId = memberId;
                viewModel.CourseModule = courseModule;
                _courseModuleViewerRepository.AddViewer(new CourseModuleViewer { CourseModuleId = courseModuleId, MemberId = memberId });
                _courseModuleViewerRepository.SaveChanges();
            }
            return View("Detail", viewModel);
        }
    }
}
