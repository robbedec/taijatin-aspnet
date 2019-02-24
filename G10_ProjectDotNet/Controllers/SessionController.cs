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

        public SessionController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
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
                
            }
            return RedirectToAction("Index", "Group", new { id = viewModel.Group ,area = "" });
        }

        private SelectList GetGroupsAsSelectList()
        {
            return new SelectList(_groupRepository.GetAll().OrderBy(l => l.GroupId).Select(l => l.GroupId), nameof(Group.Day));
        }
    }
}