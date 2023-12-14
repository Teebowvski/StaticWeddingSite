using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OusMarieWedding.Data;
using OusMarieWedding.Models;
using OusMarieWedding.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OusMarieWedding.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string firstName, string lastName , string phoneNumber , string email )
        {


          



            
            var testimonial = _context.Testimonials.ToList();
            var speakers = await _context.Speakers.ToListAsync();
            var model = new RsvpViewModel
            {
                 FirstName = firstName,
                 LastName = lastName,
                 PhoneNumber = phoneNumber,
                
                  Speakers = speakers,
                  Testimonials = testimonial,
                 

            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PhoneNumber")] Reservations reservations)
        {

            
            if (ModelState.IsValid)
            {
                _context.Add(reservations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservations);
        }
    }
}
