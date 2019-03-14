using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G10_ProjectDotNet.Models;
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

        public SessionController(IFormulaRepository formulaRepository, ISessionRepository sessionRepository)
        {
            _formulaRepository = formulaRepository;
            _sessionRepository = sessionRepository;
            _memberRepository = memberRepository;
        }

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

        [AllowAnonymous]
        public IActionResult Register(int formulaId)
        {
            int weekday = ((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek;
            if (_formulaRepository.GetByWeekDay(weekday) == null)
            {
                TempData["error"] = $"Er zijn geen formules gevonden die vandaag plaatsvinden!";
                return RedirectToAction("Index", "Home");
            }
            if (_sessionRepository.GetLatest() != null && _sessionRepository.GetLatest().Date == DateTime.Now.Date)
            {
                TempData["error"] = $"De sessie van vandaag is al gedaan!";
                return RedirectToAction("Index", "Home");
            }
            _sessionRepository.Add(new Session { Day = (Weekday)weekday, SessionEnded = false, Date = DateTime.Now.Date });
            _sessionRepository.SaveChanges();
            return RedirectToAction("Index", "Session");
        }

        public IActionResult EndSession()
        {
            _sessionRepository.EndSession();
            _sessionRepository.SaveChanges();
            TempData["message"] = $"De sessie is succesvol beëindigd";
            return RedirectToAction("Index", "Home");
        }

    }
}