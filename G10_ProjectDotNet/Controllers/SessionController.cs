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

        // We bouwen een viewmodel op dat bestaat uit de sessie en alle deelnemers uit die formule
        // RegistrationState: het scherm toont enkel de leden die nog niet geregistreerd zijn, bij het registreren verdwijnt zijn naam van het scherm
        // RegistrationEndedState: het scherm toont de geregistreerde leden die nu lesmateriaal kunnen raadplegen of hun registratie ongedaan maken, het is niet mogelijk om nu nog te registreren
        // Als er geen sessie bezig is heb je de mogelijkheid om een niewe sessie te starten
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

        // Via het ID van de huidige dag kijk je als er een formule is die vandaag moet plaatsvinden
        // Er wordt een nieuwe sessie aangemaakt (Weekday, is de belangrijke link) met de start state die we persisteren
        // Om te kijken als de sessie van vandaag al is afgelopen, nemen we de laatst aangemaakte sessie en vergelijken we die datum met vandaag
        // Bij succes tonen we een overzicht van de ingeschreven members in de formule, bij falen terug naar het home scherm
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

        // Verandert de sessionstate zodat het niet meer mogelijk is om te registreren
        // De state wordt geserialiseerd en opgeslagen in de databank
        [HttpPost]
        public IActionResult EndRegistration()
        {
            var session = _sessionRepository.GetByDateToday();
            session.EndRegistration();
            _sessionRepository.SaveChanges();

            return RedirectToAction("Index", "Session");
        }

        // Verandert de sessionstate zodat de sessie officieel is afgelopen
        // De state wordt geserialiseerd en opgeslagen in de databank
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