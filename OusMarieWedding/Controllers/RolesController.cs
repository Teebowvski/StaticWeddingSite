
using OusMarieWedding.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OusMarieWedding.Controllers
{
    public class RolesController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _roleManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
        public RolesController(Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager, Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        // GET: RolesController
        public ActionResult Index(string sort, string search_data, string Filter_Value, int page = 1)
        {
            var data = _roleManager.Roles.ToList();

            int pageSize = 5;
            int totalItems = data.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var pagedData = data.Skip((page - 1) * pageSize).Take(pageSize);
            ViewBag.TotalItems = totalItems;
            ViewBag.PagedData = pagedData;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            var model = new RolesViewModel
            {
                Roles = pagedData
            };

                return View(model);
        }

        // GET: RolesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RolesController/Create
        public ActionResult Create()
        {

           

            return View();
        }

        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IdentityRole identityRole)
        {
            if (ModelState.IsValid)
            {
              await  _roleManager.CreateAsync(identityRole);
                
                return RedirectToAction(nameof(Index));
            }

            return View(identityRole);
        }

        // GET: RolesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RolesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // Role ID is passed from the URL to the action
        
        public async Task<IActionResult> UsersInRole(string id)
        {
            // Find the role by Role ID
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                
            };
            // Retrieve all the Users
            var users = _userManager.Users.ToList();

            // Retrieve all the Users
            foreach (var user in users)
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        // This action responds to HttpPost and receives EditRoleViewModel
        [HttpPost]
        public async Task<IActionResult> UsersInRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }



      
        public async Task<IActionResult> EditUsersInRole(string Id, string sort, string search_data, string Filter_Value, int page = 1, string searchString = null
               , string currentFilter = null)
        {

            ViewBag.CurrentSort = sort;


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            ViewBag.roleId = Id;
            
            var roles = _roleManager.Roles.ToList();
            var role = await _roleManager.FindByIdAsync(Id);
            ViewBag.roleId = role.Name;

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {Id} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();
            // Retrieve all the Users
            var users = _userManager.Users.ToList();

            int pageSize = 10000000;
            int totalItems = users.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var pagedData = users.Skip((page - 1) * pageSize).Take(pageSize);
            ViewBag.TotalItems = totalItems;
            ViewBag.PagedData = pagedData;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            foreach (var user in ViewBag.PagedData)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName
    };
            

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }





            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            ViewBag.roleId = role.Name;
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {Id} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                Microsoft.AspNetCore.Identity.IdentityResult result = null;

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditUsersInRole", new { Id = Id });
                }
            }

            return RedirectToAction("EditUsersInRole", new { Id = Id });
        }
    }
}
