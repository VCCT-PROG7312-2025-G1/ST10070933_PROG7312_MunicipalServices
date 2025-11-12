using ST10070933_PROG7312_MunicipalServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10070933_PROG7312_MunicipalServices.Services
{
    public class InMemoryDataService : IDataService
    {
        // === Part 1: Issues ===
        public List<Issue> Issues { get; } = new List<Issue>();

        // === Part 2: Events ===
        public SortedDictionary<DateTime, List<Event>> EventsByDate { get; } = new SortedDictionary<DateTime, List<Event>>();
        public HashSet<string> EventCategories { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public Queue<Event> UpcomingEventQueue { get; } = new Queue<Event>();
        private readonly Dictionary<string, int> _searchCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        private readonly object _lock = new();

        // === Part 3: Service Requests ===
        private readonly List<ServiceRequest> _serviceRequests = new List<ServiceRequest>();
        public ServiceRequestManager RequestManager { get; }

        public InMemoryDataService()
        {
            // Seed sample data
            SeedSampleEvents();
            RequestManager = new ServiceRequestManager();

            // Seed sample service requests
            SeedSampleServiceRequests();
        }

        // === Part 1: Issues ===
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

        // === Part 2: Events ===
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

        // === Part 3: Service Requests ===
        public void AddServiceRequest(ServiceRequest request)
        {
            lock (_lock)
            {
                _serviceRequests.Add(request);
                RequestManager.AddRequest(request);
            }
        }

        public List<ServiceRequest> GetAllRequests()
        {
            lock (_lock)
            {
                return _serviceRequests.ToList();
            }
        }

        // === Seed Sample Service Requests For part 3  ===
        private void SeedSampleServiceRequests()
        {
            var req1 = new ServiceRequest
            {
                RequestId = Guid.NewGuid().ToString(),
                NumericId = 1001,
                Title = "Streetlight Malfunction",
                Description = "Several streetlights are not working along Main Street.",
                Status = "In Progress",
                Created = DateTime.Now.AddDays(-2),
                Location = "Main Street, Zone 3",
                Priority = 2
            };

            var req2 = new ServiceRequest
            {
                RequestId = Guid.NewGuid().ToString(),
                NumericId = 1002,
                Title = "Water Leakage Report",
                Description = "Water leak detected near the community park fountain.",
                Status = "Submitted",
                Created = DateTime.Now.AddDays(-1),
                Location = "Central Park, Zone 5",
                Priority = 3
            };

            var req3 = new ServiceRequest
            {
                RequestId = Guid.NewGuid().ToString(),
                NumericId = 1003,
                Title = "Road Pothole Repair",
                Description = "Large potholes need urgent attention on Elm Avenue.",
                Status = "Completed",
                Created = DateTime.Now.AddDays(-5),
                Location = "Elm Avenue, Zone 2",
                Priority = 1
            };

            AddServiceRequest(req1);
            AddServiceRequest(req2);
            AddServiceRequest(req3);
        }

        // === Sample Events for Part 2 ===
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