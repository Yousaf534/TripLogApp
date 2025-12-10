using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TripLog.Data;
using TripLog.Models;

namespace TripLog.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly TripLogContext _context;

        public ActivitiesController(TripLogContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.SubHeader = "Manage Activities";
            var activities = _context.Activities.ToList();
            return View(activities);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new Activity());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Activity model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Activities.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var act = _context.Activities.Find(id);
            if (act != null)
            {
                bool inUse = _context.TripActivities.Any(ta => ta.ActivityId == id);
                if (!inUse)
                {
                    _context.Activities.Remove(act);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
