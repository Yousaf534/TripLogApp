using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TripLog.Data;
using TripLog.Models;

namespace TripLog.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly TripLogContext _context;

        public DestinationsController(TripLogContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.SubHeader = "Manage Destinations";
            var destinations = _context.Destinations.ToList();
            return View(destinations);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new Destination());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Destination model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Destinations.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var dest = _context.Destinations.Find(id);
            if (dest != null)
            {
                bool inUse = _context.Trips.Any(t => t.DestinationId == id);
                if (!inUse)
                {
                    _context.Destinations.Remove(dest);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
