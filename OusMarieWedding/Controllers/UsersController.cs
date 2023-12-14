


using OusMarieWedding.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OusMarieWedding.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly  RoleManager<IdentityRole>_roleManager;

        public UsersController( Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
           
        }
        // GET: UsersController
        public ActionResult Index(int id, string sort, string search_data, string Filter_Value, int page = 1)
        {
            var data = _userManager.Users.ToList();
        
           

            int pageSize = 5;
            int totalItems = data.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var pagedData = data.Skip((page - 1) * pageSize).Take(pageSize);
            ViewBag.TotalItems = totalItems;
            ViewBag.PagedData = pagedData;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            var model = new UserViewModel
            {
                Users = pagedData
            };


            return View(model);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
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

        // GET: UsersController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var data = await _userManager.FindByIdAsync(id);
            if (data == null) return View("Not Found");
            return View(data);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var data = await _userManager.FindByIdAsync(id);
            if (data == null) return View("Not Found");
            await _userManager.DeleteAsync(data);
            return RedirectToAction(nameof(Index));
        }
    }
}
