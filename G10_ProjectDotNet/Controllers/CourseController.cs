using G10_ProjectDotNet.Models.CourseViewModel;
using G10_ProjectDotNet.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace G10_ProjectDotNet.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseModuleRepository _courseModuleRepository;
        private readonly ICourseModuleViewerRepository _courseModuleViewerRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public CourseController(ICourseRepository courseRepository, ICourseModuleRepository courseModuleRepository, ICourseModuleViewerRepository courseModuleViewerRepository, IMemberRepository memberRepository, IApplicationUserRepository applicationUserRepository, UserManager<IdentityUser> userManager)
        {
            _courseRepository = courseRepository;
            _courseModuleRepository = courseModuleRepository;
            _courseModuleViewerRepository = courseModuleViewerRepository;
            _memberRepository = memberRepository;
            _applicationUserRepository = applicationUserRepository;
            _userManager = userManager;
        }

        // Het scherm toont altijd een treemenu met alle graden + modules die beschikbaar zijn voor de gebruiker die het lesmateriaal raadpleegt
        // Bij het kiezen van een module wordt de indexpagina opnieuw geladen met een courseModuleId waar je een overzicht krijgt van het lesmateriaal
        // (video / tekst / foto's)
        [AllowAnonymous]
        public IActionResult Index(int memberId, int? courseModuleId)
        {
            var id = 0;
            var viewModel = new IndexViewModel();
            if (memberId == 0)
            {
                id = setMemberId();
                viewModel = buildUpViewModel(id, courseModuleId);
            }
            else
            {
                id = memberId;
                viewModel = buildUpViewModel(id, courseModuleId);
            }
            return View(viewModel);
        }

        [AllowAnonymous]
        public IActionResult GoToIndex()
        {
            return View();
        }

        // Aan de huidige module is het mogelijk om commentaar te geven
        // De comment wordt toegevoegd aan de module en gepersisteerd
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

        // Een comment van de huidige module verwijderen
        // Werkt enkel bij je eigen comments
        public IActionResult RemoveComment(int courseModuleId, int commentId, int memberId)
        {
            Comment commentToRemove = _courseModuleRepository.GetComment(courseModuleId, commentId);
            _courseModuleRepository.RemoveComment(courseModuleId, commentToRemove);
            _courseModuleRepository.SaveChanges();
            
            return RedirectToAction("Index", new { memberId = memberId, courseModuleId =  courseModuleId });
        }


        [HttpPost]
        public IActionResult ReplyToComment(int courseModuleId, string reply,int commentId, int memberId)
        {
            Comment comment = _courseModuleRepository.GetComment(courseModuleId, commentId);
            Member member = _memberRepository.GetById(memberId);
            CommentReply replyToAdd = new CommentReply { ReplyText = reply, Comment = comment, Member = member };
            _courseModuleRepository.AddCommentReply(replyToAdd, commentId, courseModuleId);
            _courseModuleRepository.SaveChanges();

            return RedirectToAction("Index", new { memberId = memberId, courseModuleId =  courseModuleId });
        }

        private int setMemberId()
        {
            var username = _userManager.GetUserName(User);
            var id = 5;
            Trace.WriteLine(username);
            if (username != null || username != "")
            {
                Member appUser;
                try
                {
                    appUser = _memberRepository.GetByUserName(username.ToString());
                    id = appUser.Id;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error");
                }
            }
            return id;
        }

        private IndexViewModel buildUpViewModel(int id, int? courseModuleId)
        {

            var viewModel = new IndexViewModel
            {
                MemberId = id,
                Courses = _courseRepository.GetByMinGrade(_memberRepository.GetById(id).Grade),
                CourseModuleId = courseModuleId
            };

            if (courseModuleId.HasValue)
            {
                viewModel.CourseModule = _courseModuleRepository.GetById(courseModuleId.Value);
                _courseModuleViewerRepository.AddViewer(new CourseModuleViewer { CourseModuleId = courseModuleId.Value, MemberId = id });
                _courseModuleViewerRepository.SaveChanges();
            }
            return viewModel;
        }
    }
}