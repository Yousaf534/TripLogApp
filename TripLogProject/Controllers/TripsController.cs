using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TripLog.Data;
using TripLog.Models;
using TripLog.ViewModels;

namespace TripLog.Controllers
{
    public class TripsController : Controller
    {
        private readonly TripLogContext _context;

        public TripsController(TripLogContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.SubHeader = "View Trips";

            var trips = _context.Trips
                .Include(t => t.Destination)
                .Include(t => t.Accommodation)
                .Include(t => t.TripActivities)
                    .ThenInclude(ta => ta.Activity)
                .ToList();

            return View(trips);
        }

        [HttpGet]
        public IActionResult AddStep1()
        {
            ViewBag.SubHeader = "Add Trip Destination and Dates";

            var vm = new TripStep1ViewModel
            {
                Destinations = _context.Destinations
                    .Select(d => new SelectListItem { Value = d.DestinationId.ToString(), Text = d.Name })
                    .ToList(),
                Accommodations = _context.Accommodations
                    .Select(a => new SelectListItem { Value = a.AccommodationId.ToString(), Text = a.Name })
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStep1(TripStep1ViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Destinations = _context.Destinations
                    .Select(d => new SelectListItem { Value = d.DestinationId.ToString(), Text = d.Name })
                    .ToList();
                vm.Accommodations = _context.Accommodations
                    .Select(a => new SelectListItem { Value = a.AccommodationId.ToString(), Text = a.Name })
                    .ToList();
                return View(vm);
            }

            TempData["DestinationId"] = vm.DestinationId;
            TempData["AccommodationId"] = vm.AccommodationId;
            TempData["StartDate"] = vm.StartDate.ToString("o");
            TempData["EndDate"] = vm.EndDate.ToString("o");

            TempData.Keep();

            return RedirectToAction(nameof(AddStep2));
        }

        [HttpGet]
        public IActionResult AddStep2()
        {
            if (!TempData.ContainsKey("DestinationId"))
            {
                return RedirectToAction(nameof(Index));
            }

            int destinationId = int.Parse(TempData["DestinationId"].ToString());
            var dest = _context.Destinations.Find(destinationId);
            string destName = dest != null ? dest.Name : "Destination";

            ViewBag.SubHeader = $"Add Things To Do in {destName}";

            var vm = new TripStep2ViewModel
            {
                Activities = _context.Activities
                    .Select(a => new SelectListItem { Value = a.ActivityId.ToString(), Text = a.Name })
                    .ToList()
            };

            TempData.Keep();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStep2(TripStep2ViewModel vm)
        {
            if (!TempData.ContainsKey("DestinationId") ||
                !TempData.ContainsKey("AccommodationId") ||
                !TempData.ContainsKey("StartDate") ||
                !TempData.ContainsKey("EndDate"))
            {
                return RedirectToAction(nameof(Index));
            }

            int destinationId = int.Parse(TempData["DestinationId"].ToString());
            int accommodationId = int.Parse(TempData["AccommodationId"].ToString());
            DateTime start = DateTime.Parse(TempData["StartDate"].ToString());
            DateTime end = DateTime.Parse(TempData["EndDate"].ToString());

            var trip = new Trip
            {
                DestinationId = destinationId,
                AccommodationId = accommodationId,
                StartDate = start,
                EndDate = end
            };

            if (vm.SelectedActivityIds != null)
            {
                foreach (int actId in vm.SelectedActivityIds)
                {
                    trip.TripActivities.Add(new TripActivity { ActivityId = actId });
                }
            }

            _context.Trips.Add(trip);
            _context.SaveChanges();

            TempData.Clear();

            TempData["Message"] = "Trip added successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var trip = _context.Trips
                .Include(t => t.TripActivities)
                .FirstOrDefault(t => t.TripId == id);

            if (trip != null)
            {
                _context.TripActivities.RemoveRange(trip.TripActivities);
                _context.Trips.Remove(trip);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel()
        {
            TempData.Clear();
            return RedirectToAction(nameof(Index));
        }
    }
}
