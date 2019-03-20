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
        public IActionResult Index(int memberId, int? courseModuleId, int? courseId)
        {
            var viewModel = new IndexViewModel();
            viewModel.MemberId = memberId;
            CourseModule courseModule = null;
            viewModel.Courses = _courseRepository.GetByMinGrade(_memberRepository.GetById(memberId).Grade);
            if (courseId.HasValue)
            {
                viewModel.Modules = _courseModuleRepository.GetByCourse(courseId.Value);
            }
            if(courseModuleId != null)
            {
                viewModel.CourseModuleId = courseModuleId;
                courseModule = _courseModuleRepository.GetById(courseModuleId);
            }
            if(courseModule != null)
            {
                viewModel.CourseModule = courseModule;
                _courseModuleViewerRepository.AddViewer(new CourseModuleViewer { CourseModuleId = courseModuleId, MemberId = memberId });
                _courseModuleViewerRepository.SaveChanges();
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddComment(int memberId, string comment, int courseModuleId)
        {
            CourseModule courseModule = _courseModuleRepository.GetById(courseModuleId); 
            Member member = _memberRepository.GetById(memberId);
            Comment commentToAdd = new Comment { CommentText = comment, CourseModule = courseModule, Member = member };
            _courseModuleRepository.AddComment(commentToAdd, courseModuleId);
            _courseModuleRepository.SaveChanges();
            
            return RedirectToAction("Index", new { memberId = memberId, courseModuleId =  courseModuleId });
        }


        // [HttpPost]
        // public IActionResult ReplyToComment(int courseModuleId, int commentId, string reply, int memberId)
        // {
            
        // }

        //public IActionResult Detail(int? courseModuleId, int memberId)
        //{
        //    return ViewComponent("DetailComponent", new { courseModuleId, memberId });
        //}

        public IActionResult CourseModuleMembers()
        {
            return View();
        }
    }
}