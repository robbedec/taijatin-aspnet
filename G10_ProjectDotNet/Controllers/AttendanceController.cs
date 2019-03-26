using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G10_ProjectDotNet.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace G10_ProjectDotNet.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMemberRepository _memberRepository;

        public AttendanceController(ISessionRepository sessionRepository, IMemberRepository memberRepository)
        {
            _sessionRepository = sessionRepository;
            _memberRepository = memberRepository;
        }

        // Voegt een nieuwe aanwezigheid toe aan de huidige sessie en geeft het lid 5 of 10 punten (afhankelijk van de formule)
        // Gooit een exception als je al bent geregistreerd of het registreren is afgesloten
        // Toont de indexpagina van session bij het voltooien / falen
        [HttpPost]
        public IActionResult Create(int memberId)
        {
            try
            {
                _sessionRepository.GetByDateToday().AddAttendance(new Attendance { MemberId = memberId });
                _memberRepository.GetById(memberId).AddPoints();
                _sessionRepository.SaveChanges();
                TempData["message"] = $"Je bent succesvol geregistreerd";
            }
            catch(InvalidOperationException)
            {
                TempData["error"] = $"Deze gebruiker is reeds geregistreerd!";
            }
            catch (OperationCanceledException)
            {
                TempData["error"] = $"Het is niet mogelijk om te registreren als je de 1e helft hebt gemist!";
            }
            return RedirectToAction("Index", "Session", new { area = "" });
        }
    }
}