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

        // GET: Group
        public IActionResult Index(int? id)
        {
            var viewModel = new IndexViewModel();
            viewModel.Groups = _groupRepository.GetAll();

            if(id != null)
            {
                ViewBag.id = id.Value;
                viewModel.UserGroups = _groupRepository.GetLinkedUserGroups(id.Value);
            }
            return View(viewModel);
        }

        // GET: Group/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Group/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Group/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Group/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Group/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Group/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}