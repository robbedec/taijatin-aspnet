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
        private readonly ApplicationDbContext _dbContext;

        public GroupController(IGroupRepository groupRepository, ApplicationDbContext dbContext)
        {
            _groupRepository = groupRepository;
            _dbContext = dbContext;
        }

        // GET: Group
        public ActionResult Index(int? groupId)
        {
            var viewModel = new IndexViewModel();
            viewModel.Groups = _groupRepository.GetAll();

            if(groupId != null)
            {
                ViewBag.GroupId = groupId.Value;
                //viewModel.UserGroups = viewModel.Groups.Where(b => b.GroupId == groupId).Single().UserGroups;
                var selectedGroup = viewModel.Groups.Where(x => x.GroupId == groupId).Single();
                _dbContext.Entry(selectedGroup).Collection(x => x.UserGroups).Load();
                foreach(UserGroup userGroup in selectedGroup.UserGroups)
                {
                    _dbContext.Entry(userGroup).Reference(x => x.Member).Load();
                }
                viewModel.UserGroups = selectedGroup.UserGroups;
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