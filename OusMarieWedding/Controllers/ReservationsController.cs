using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OusMarieWedding.Data;
using OusMarieWedding.Models;
using OusMarieWedding.ViewModels;

namespace OusMarieWedding.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        
            public async Task<IActionResult> Index(string firstName, string lastName, string phoneNumber, string email, string searchString = null
               , string currentFilter = null, string sort = null, int page = 1)
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



            var reservations = await _context.Reservations.OrderBy(c=>c.dateTime).ToListAsync();


            if (!String.IsNullOrEmpty(searchString))
            {
                reservations = reservations.Where(s => s.FirstName.Contains(searchString)
                                       || s.LastName.Contains(searchString) || s.PhoneNumber.Contains(searchString) ).ToList();
            }


            int pageSize = 25;
            int totalItems = reservations.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var pagedData = reservations.Skip((page - 1) * pageSize).Take(pageSize).ToList();




            ViewBag.TotalItems = totalItems;
            ViewBag.PagedData = pagedData;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            ViewData["PlusOneId"] = new SelectList(_context.PlusOnes.ToList(), "Id", "Name");
                var model = new RsvpViewModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    
                    Reservationss = pagedData,
                   

                };
                return View(model);
            }
       

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = await _context.Reservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservations == null)
            {
                return NotFound();
            }

            return View(reservations);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PhoneNumber")] Reservations reservations)
        {
            if (ModelState.IsValid)

            {
                reservations.dateTime = DateTime.Now;
                _context.Add(reservations);
              
                await _context.SaveChangesAsync();

                TempData["ReservationCreated"] = true;

                // Handle success, maybe return a different view or redirect
                return RedirectToAction("Index", "Home");
                

              
            }
            
            return View("Home/Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModalCreate([Bind("Id,FirstName,LastName,PhoneNumber")] Reservations reservations)
        {
            if (ModelState.IsValid)
            {
                reservations.dateTime = DateTime.Now;
                _context.Add(reservations);

                await _context.SaveChangesAsync();



                // Handle success, maybe return a different view or redirect
                return RedirectToAction("Index");



            }

            return View("Index");
        }



        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["PlusOneId"] = new SelectList(_context.PlusOnes.ToList(), "Id", "Name");
            if (id == null)
            {
                return NotFound();
            }

            var reservations = await _context.Reservations.FindAsync(id);
            if (reservations == null)
            {
                return NotFound();
            }
            return View(reservations);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PhoneNumber")] Reservations reservations)
        {
           
            if (id != reservations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    
             
                    _context.Update(reservations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationsExists(reservations.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reservations);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = await _context.Reservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservations == null)
            {
                return NotFound();
            }

            return View(reservations);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservations = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationsExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
