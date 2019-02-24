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
            _sessionRepository.GetCurrentSession().Add(new Attendance { SessionId = sessionId, MemberId = memberId });
            _sessionRepository.SaveChanges();
            TempData["message"] = $"Gebruiker met id: {memberId} is succesvol geregistreerd";

            return RedirectToAction("Index", "Session", new { area = "" });
        }
    }
}