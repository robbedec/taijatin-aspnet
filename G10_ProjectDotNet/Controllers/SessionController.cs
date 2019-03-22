using System;
using System.Collections.Generic;
using System.Linq;
using G10_ProjectDotNet.Models.Domain;
using G10_ProjectDotNet.Models.SessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace G10_ProjectDotNet.Controllers
{
    [Authorize(Policy = "Teacher")]
    public class SessionController : Controller
    {
        private readonly IFormulaRepository _formulaRepository;
        private readonly ISessionRepository _sessionRepository;


        public SessionController(IFormulaRepository formulaRepository, ISessionRepository sessionRepository)
        {
            _formulaRepository = formulaRepository;
            _sessionRepository = sessionRepository;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            var session = _sessionRepository.GetByDateToday();
            viewModel.Session = session;
            if (session != null)
            {
                viewModel.Session = session;
                if (JsonConvert.DeserializeObject<Type>(session.StateSerialized) == typeof(RegistrationState))
                {
                    foreach (Member member in _formulaRepository.GetByWeekDay((int)session.Day).SelectMany(b => b.Members))
                    {
                        if (!session.AlreadyRegistered(member.Id))
                        {
                            viewModel.Members.Add(member);
                        }
                    }
                }
                else
                {
                    viewModel.RegistrationEnded = true;
                    foreach (Member member in _formulaRepository.GetByWeekDay((int)session.Day).SelectMany(b => b.Members))
                    {
                        if (session.AlreadyRegistered(member.Id))
                        {
                            viewModel.Members.Add(member);
                        }
                    }
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create()
        {
            int weekday = ((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek;
            if (!_formulaRepository.GetByWeekDay(weekday).Any())
            {
                TempData["error"] = $"Er zijn geen formules gevonden die vandaag plaatsvinden!";
                return RedirectToAction("Index", "Home");
            }
            if (_sessionRepository.GetLatest() != null && _sessionRepository.GetLatest().Date == DateTime.Now.Date)
            {
                TempData["error"] = $"De sessie van vandaag is al gedaan!";
                return RedirectToAction("Index", "Home");
            }
            Session sessie = new Session { Day = (Weekday)weekday, Date = DateTime.Now.Date, Attendances = new List<Attendance>() };
            sessie.StateSerialized = JsonConvert.SerializeObject(new RegistrationState(sessie).GetType());
            _sessionRepository.Add(sessie);

            _sessionRepository.SaveChanges();
            return RedirectToAction("Index", "Session");
        }

        public IActionResult EndRegistration()
        {
            var session = _sessionRepository.GetByDateToday();
            session.EndRegistration();
            _sessionRepository.SaveChanges();

            return RedirectToAction("Index", "Session");
        }

        [HttpPost]
        public IActionResult EndSession()
        {
            _sessionRepository.GetLatest().EndSession();
            _sessionRepository.SaveChanges();
            TempData["message"] = $"De sessie is succesvol beëindigd";
            return RedirectToAction("Index", "Home");
        }
    }
}