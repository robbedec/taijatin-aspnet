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
    [Authorize]
    public class SessionController : Controller
    {
        private readonly IFormulaRepository _formulaRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IMemberRepository _memberRepository;

        public SessionController(IFormulaRepository formulaRepository, ISessionRepository sessionRepository, IMemberRepository memberRepository)
        {
            _formulaRepository = formulaRepository;
            _sessionRepository = sessionRepository;
            _memberRepository = memberRepository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            var session = _sessionRepository.GetByDateToday();
            if (session != null)
            {
                viewModel.Session = session;
                viewModel.Members = _formulaRepository.GetByWeekDay((int)session.Day).SelectMany(b => b.Members);
            }
            return View(viewModel);
        }

        [Authorize(Policy = "Teacher")]
        public IActionResult Create()
        {
            ViewData["Members"] = GetAllMembers();
            return View(new CreateSessionViewModel());
        }

        [Authorize(Policy = "Teacher")]
        [HttpPost]
        public IActionResult Create(CreateSessionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //_sessionRepository.Add(session);
                _sessionRepository.SaveChanges();
                TempData["message"] = $"Je nieuwe sessie is succesvol ingepland.";
            } 
            return RedirectToAction("Index", "Session", new { area = "" });
        }

        private List<Member> GetAllMembers()
        {
            return _memberRepository.GetAll();
        }
    }
}