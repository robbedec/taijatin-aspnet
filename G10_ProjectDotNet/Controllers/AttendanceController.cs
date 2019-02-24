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
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceController(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public IActionResult Create(int groupId, int memberId)
        {
            _attendanceRepository.Add(new Attendance { MemberId = memberId });
            _attendanceRepository.SaveChanges();
            TempData["message"] = $"Gebruiker met id: {memberId} is succesvol geregistreerd";

            return RedirectToAction("Index", "Group", new { area = "" });
        }
    }
}