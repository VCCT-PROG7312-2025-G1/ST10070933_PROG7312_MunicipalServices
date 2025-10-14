using Microsoft.AspNetCore.Mvc;
using ST10070933_PROG7312_MunicipalServices.Models;
using ST10070933_PROG7312_MunicipalServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10070933_PROG7312_MunicipalServices.Controllers
{
    public class EventsController : Controller
    {
        private readonly IDataService _dataService;

        // Track user search categories for recommendations
        private static List<string> _recentSearchCategories = new List<string>();

        public EventsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: Events
        public IActionResult Index(string searchCategory = "", DateTime? searchDate = null, string sortOption = "")
        {
            // ✅ Get all events
            var events = _dataService.GetAllEvents();

            // ✅ Use a SortedDictionary to organize events by StartDate
            var sortedEvents = new SortedDictionary<DateTime, List<Event>>();
            foreach (var ev in events)
            {
                if (!sortedEvents.ContainsKey(ev.StartDate))
                    sortedEvents[ev.StartDate] = new List<Event>();

                sortedEvents[ev.StartDate].Add(ev);
            }

            // ✅ Use a Queue for upcoming events
            Queue<Event> upcomingEvents = new Queue<Event>();
            foreach (var ev in events.OrderBy(e => e.StartDate))
            {
                if (ev.StartDate >= DateTime.Now)
                    upcomingEvents.Enqueue(ev);
                if (upcomingEvents.Count > 5) break; // Keep only next 5
            }

            // ✅ Filter by category if provided
            if (!string.IsNullOrWhiteSpace(searchCategory))
            {
                events = events
                    .Where(e => e.Category.Equals(searchCategory, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                _recentSearchCategories.Add(searchCategory);
            }

            // ✅ Filter by date if provided
            if (searchDate.HasValue)
            {
                events = events
                    .Where(e => e.StartDate.Date == searchDate.Value.Date)
                    .ToList();
            }

            // ✅ Sort results if a sort option is selected
            if (!string.IsNullOrEmpty(sortOption))
            {
                switch (sortOption)
                {
                    case "Title":
                        events = events.OrderBy(e => e.Title).ToList();
                        break;
                    case "Category":
                        events = events.OrderBy(e => e.Category).ToList();
                        break;
                    case "Date":
                        events = events.OrderBy(e => e.StartDate).ToList();
                        break;
                }
            }

            // ✅ HashSet for unique categories
            var uniqueCategories = new HashSet<string>(_dataService.GetAllEvents().Select(e => e.Category));

            // ✅ Recommendation logic (based on most-searched category)
            var recommendedEvents = new List<Event>();
            if (_recentSearchCategories.Count > 0)
            {
                var topCategory = _recentSearchCategories
                    .GroupBy(c => c)
                    .OrderByDescending(g => g.Count())
                    .First().Key;

                recommendedEvents = _dataService.GetAllEvents()
                    .Where(e => e.Category.Equals(topCategory, StringComparison.OrdinalIgnoreCase))
                    .Take(3)
                    .ToList();
            }

            // ✅ ViewBags for front-end
            ViewBag.Categories = uniqueCategories;
            ViewBag.RecommendedEvents = recommendedEvents;
            ViewBag.UpcomingEvents = upcomingEvents;
            ViewBag.Message = events.Any()
                ? $"{events.Count} event(s) found."
                : "No matching events found.";

            return View(events);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event newEvent)
        {
            if (ModelState.IsValid)
            {
                _dataService.AddEvent(newEvent);
                TempData["SuccessMessage"] = "Event added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(newEvent);
        }
    }
}