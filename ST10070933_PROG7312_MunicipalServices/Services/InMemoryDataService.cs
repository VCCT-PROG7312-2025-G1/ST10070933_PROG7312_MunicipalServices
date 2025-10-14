using ST10070933_PROG7312_MunicipalServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;

namespace ST10070933_PROG7312_MunicipalServices.Services
{
    public class InMemoryDataService : IDataService
    {
        // ======= Part 1: Issues =======
        public List<Issue> Issues { get; } = new List<Issue>();

        // ======= Part 2: Events =======
        // SortedDictionary keyed by StartDate for chronological listing
        public SortedDictionary<DateTime, List<Event>> EventsByDate { get; } = new SortedDictionary<DateTime, List<Event>>();

        // Unique categories
        public HashSet<string> EventCategories { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Queue for upcoming events demo
        public Queue<Event> UpcomingEventQueue { get; } = new Queue<Event>();

        // Record search terms and counts for recommendations
        private readonly Dictionary<string, int> _searchCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        private readonly object _lock = new();

        public InMemoryDataService()
        {
            // Seed sample events
            SeedSampleEvents();
        }

        // ======= Issues =======
        public void AddIssue(Issue issue)
        {
            lock (_lock)
            {
                Issues.Add(issue);
            }
        }

        public List<Issue> GetAllIssues()
        {
            return Issues;
        }

        // ======= Events =======
        public void AddEvent(Event ev)
        {
            lock (_lock)
            {
                var key = ev.StartDate.Date;
                if (!EventsByDate.ContainsKey(key)) EventsByDate[key] = new List<Event>();
                EventsByDate[key].Add(ev);

                EventCategories.Add(ev.Category ?? string.Empty);
                UpcomingEventQueue.Enqueue(ev);
            }
        }

        public List<Event> GetAllEvents()
        {
            // Flatten SortedDictionary to a list, ordered by StartDate
            return EventsByDate.SelectMany(kv => kv.Value).OrderBy(e => e.StartDate).ToList();
        }

        public IEnumerable<Event> SearchEvents(string? category = null, DateTime? date = null)
        {
            IEnumerable<Event> results = EventsByDate.SelectMany(kv => kv.Value);

            if (!string.IsNullOrWhiteSpace(category))
            {
                results = results.Where(e => e.Category?.Equals(category, StringComparison.OrdinalIgnoreCase) == true);
            }

            if (date.HasValue)
            {
                var key = date.Value.Date;
                if (EventsByDate.ContainsKey(key))
                    results = results.Where(e => e.StartDate.Date == key);
                else
                    results = Enumerable.Empty<Event>();
            }

            return results.OrderBy(e => e.StartDate);
        }

        public void RecordSearch(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return;
            lock (_lock)
            {
                if (!_searchCounts.ContainsKey(query)) _searchCounts[query] = 0;
                _searchCounts[query]++;
            }
        }

        public List<string> GetTopSearchTerms(int top = 3)
        {
            lock (_lock)
            {
                return _searchCounts.OrderByDescending(kv => kv.Value).Take(top).Select(kv => kv.Key).ToList();
            }
        }

        private void SeedSampleEvents()
        {
            var e1 = new Event { Title = "Community Clean-up", Description = "Neighborhood clean-up and recycling day.", StartDate = DateTime.UtcNow.AddDays(3), EndDate = DateTime.UtcNow.AddDays(3).AddHours(3), Category = "Community", Priority = 2 };
            var e2 = new Event { Title = "Water Safety Seminar", Description = "How to conserve water and report leaks.", StartDate = DateTime.UtcNow.AddDays(7), EndDate = DateTime.UtcNow.AddDays(7).AddHours(2), Category = "Utilities", Priority = 3 };
            var e3 = new Event { Title = "Local Market", Description = "Weekend local producers market.", StartDate = DateTime.UtcNow.AddDays(1), EndDate = DateTime.UtcNow.AddDays(1).AddHours(5), Category = "Business", Priority = 1 };
            var e4 = new Event { Title = "Charity Fun Run", Description = "5km run to raise funds for local schools.", StartDate = DateTime.UtcNow.AddDays(5), EndDate = DateTime.UtcNow.AddDays(5).AddHours(4), Category = "Health", Priority = 2 };
            var e5 = new Event { Title = "Youth Coding Workshop", Description = "Intro to programming for high school students.", StartDate = DateTime.UtcNow.AddDays(10), EndDate = DateTime.UtcNow.AddDays(10).AddHours(3), Category = "Education", Priority = 3 };
            var e6 = new Event { Title = "Food Drive", Description = "Collecting non-perishable food items for those in need.", StartDate = DateTime.UtcNow.AddDays(4), EndDate = DateTime.UtcNow.AddDays(4).AddHours(6), Category = "Charity", Priority = 1 };
            var e7 = new Event { Title = "Blood Donation Day", Description = "Community blood drive in partnership with Red Cross.", StartDate = DateTime.UtcNow.AddDays(8), EndDate = DateTime.UtcNow.AddDays(8).AddHours(5), Category = "Health", Priority = 3 };
            var e8 = new Event { Title = "Garden Workshop", Description = "Learn to grow your own vegetables.", StartDate = DateTime.UtcNow.AddDays(2), EndDate = DateTime.UtcNow.AddDays(2).AddHours(3), Category = "Environment", Priority = 2 };
            var e9 = new Event { Title = "Neighborhood Watch Meeting", Description = "Discuss safety and security in the area.", StartDate = DateTime.UtcNow.AddDays(6), EndDate = DateTime.UtcNow.AddDays(6).AddHours(2), Category = "Safety", Priority = 3 };
            var e10 = new Event { Title = "Community Movie Night", Description = "Outdoor movie screening for all ages.", StartDate = DateTime.UtcNow.AddDays(9), EndDate = DateTime.UtcNow.AddDays(9).AddHours(3), Category = "Entertainment", Priority = 1 };
            var e11 = new Event { Title = "Art in the Park", Description = "Local artists showcase their work.", StartDate = DateTime.UtcNow.AddDays(12), EndDate = DateTime.UtcNow.AddDays(12).AddHours(5), Category = "Culture", Priority = 2 };
            var e12 = new Event { Title = "Recycling Awareness Day", Description = "Learn more about reducing waste and recycling.", StartDate = DateTime.UtcNow.AddDays(11), EndDate = DateTime.UtcNow.AddDays(11).AddHours(3), Category = "Environment", Priority = 2 };
            var e13 = new Event { Title = "Elderly Care Workshop", Description = "Training for caring for senior citizens.", StartDate = DateTime.UtcNow.AddDays(13), EndDate = DateTime.UtcNow.AddDays(13).AddHours(4), Category = "Community", Priority = 3 };
            var e14 = new Event { Title = "Local Talent Show", Description = "Showcase of community music and dance talent.", StartDate = DateTime.UtcNow.AddDays(14), EndDate = DateTime.UtcNow.AddDays(14).AddHours(5), Category = "Entertainment", Priority = 1 };
            var e15 = new Event { Title = "Tree Planting Day", Description = "Join us to plant trees and beautify the park.", StartDate = DateTime.UtcNow.AddDays(15), EndDate = DateTime.UtcNow.AddDays(15).AddHours(4), Category = "Environment", Priority = 2 };

            AddEvent(e1); AddEvent(e2); AddEvent(e3); AddEvent(e4); AddEvent(e5); AddEvent(e6);
            AddEvent(e7); AddEvent(e8); AddEvent(e9); AddEvent(e10); AddEvent(e11); AddEvent(e12);
            AddEvent(e13); AddEvent(e14); AddEvent(e15);
        }
    }
}