using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TripLog.Data;
using TripLog.Models;

namespace TripLog.Controllers
{
    public class AccommodationsController : Controller
    {
        private readonly TripLogContext _context;

        public AccommodationsController(TripLogContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.SubHeader = "Manage Accommodations";
            var accommodations = _context.Accommodations.ToList();
            return View(accommodations);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new Accommodation());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Accommodation model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Accommodations.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var acc = _context.Accommodations.Find(id);
            if (acc != null)
            {
                bool inUse = _context.Trips.Any(t => t.AccommodationId == id);
                if (!inUse)
                {
                    _context.Accommodations.Remove(acc);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
