using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G10_ProjectDotNet.Models.Domain;
using G10_ProjectDotNet.Models.SessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace G10_ProjectDotNet.Controllers
{
    [Authorize(Policy = "Teacher")]
    public class SessionController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ISessionRepository _sessionRepository;

        public SessionController(IGroupRepository groupRepository, ISessionRepository sessionRepository)
        {
            _groupRepository = groupRepository;
            _sessionRepository = sessionRepository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            var session = _sessionRepository.GetCurrentSession();
            if (session != null)
            {
                viewModel.Session = session;
                viewModel.UserGroups = _groupRepository.GetLinkedUserGroups(session.Group.GroupId);
            }
            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewData["Groups"] = GetGroupsAsSelectList();
            return View(new CreateSessionViewModel());
        }

        [HttpPost]
        public IActionResult Create(CreateSessionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (_sessionRepository.GetCurrentSession() != null)
                {
                    TempData["error"] = $"Er is al een sessie bezig!";
                    return RedirectToAction("Index", "Session");
                }
                var startTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, viewModel.StartTime, 0, 0);
                Session session = new Session { StartDate = startTime, EndDate = startTime.AddHours(viewModel.Duration), Group = _groupRepository.GetById(viewModel.Group) };
                _sessionRepository.Add(session);
                _sessionRepository.SaveChanges();
                TempData["message"] = $"Je niewe sessie is succesvol ingepland.";
            }
            return RedirectToAction("Index", "Session", new { area = "" });
        }

        private SelectList GetGroupsAsSelectList()
        {
            return new SelectList(_groupRepository.GetAll().OrderBy(l => l.GroupId).Select(l => l.GroupId), nameof(Group.Day));
        }
    }
}