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

        public AttendanceController(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public IActionResult Create(int sessionId, int memberId)
        {
            try
            {
                _sessionRepository.GetBy(sessionId).AddAttendance(new Attendance { MemberId = memberId });
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