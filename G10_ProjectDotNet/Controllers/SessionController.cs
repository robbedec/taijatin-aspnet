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
            var sessions = _sessionRepository.GetSessionsToday();
            if (sessions != null)
            {
                viewModel.Sessions = sessions;
            }
            return View(viewModel);
        }

        [AllowAnonymous]
        public IActionResult Register(int formulaId)
        {
            return View(GetMembersAsList(formulaId));   
        }

        [AllowAnonymous]
        public IActionResult RegisterAttendancy(int memberId)
        {
            Member updatedMember = _memberRepository.UpdateAttendancy(memberId);
            _memberRepository.SaveChanges();
            if (updatedMember.Attendancy)
                TempData["message"] = "Je bent succesvol geregistreerd";
            else
                TempData["error"] = "Member " + updatedMember.UserName + " succesvol ongeregistreerd.";

            return RedirectToAction("Register", "Session", new { formulaId = updatedMember.FormulaId });
        }

        [Authorize(Policy = "Teacher")]
        public IActionResult Create()
        {
            ViewData["Formulas"] = GetFormulasAsSelectList();
            ViewData["Members"] = GetAllMembers();
            ViewData["Weekdays"] = GetWeekdays();
            return View(new CreateSessionViewModel());
        }

        [Authorize(Policy = "Teacher")]
        [HttpPost]
        public IActionResult Create(CreateSessionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var startTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, viewModel.StartTime, 0, 0);
                Session session = new Session { StartDate = startTime, EndDate = startTime.AddHours(viewModel.Duration), Day = viewModel.Day, Formula = _formulaRepository.GetByName(viewModel.Formula) };
                
                _sessionRepository.Add(session);
                _sessionRepository.SaveChanges();
                TempData["message"] = $"Je niewe sessie is succesvol ingepland.";
            } 
            return RedirectToAction("Index", "Session", new { area = "" });
        }

        private List<Member> GetMembersAsList(int formulaId)
        {
            return _memberRepository.GetMembersFromFormula(formulaId);
        }

        private List<Member> GetAllMembers()
        {
            return _memberRepository.GetAll();
        }

        private SelectList GetFormulasAsSelectList()
        {
            return new SelectList(_formulaRepository.GetAll().OrderBy(l => l.FormulaId).Select(l => l.FormulaName), nameof(Formula.Days));
        }

        private SelectList GetWeekdays() 
        {
            List<Weekday> weekdays = new List<Weekday>{ Weekday.Maandag, Weekday.Dinsdag, Weekday.Woensdag, Weekday.Donderdag, Weekday.Zaterdag, Weekday.Zondag };
            return new SelectList(weekdays);
        }
    }
}