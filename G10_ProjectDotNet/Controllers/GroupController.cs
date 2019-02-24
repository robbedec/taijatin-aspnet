using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G10_ProjectDotNet.Data;
using G10_ProjectDotNet.Models.Domain;
using G10_ProjectDotNet.Models.GroupViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace G10_ProjectDotNet.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        // GET: Group/Index/1
        // Shows every user in the group with groupId = 1
        public IActionResult Index(int? id)
        {
            var viewModel = new IndexViewModel();
            if(id != null)
            {
                ViewBag.id = id.Value;
                viewModel.UserGroups = _groupRepository.GetLinkedUserGroups(id.Value);
            }
            return View(viewModel);
        }
    }
}