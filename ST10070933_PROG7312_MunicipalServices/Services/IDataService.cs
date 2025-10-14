using ST10070933_PROG7312_MunicipalServices.Models;

namespace ST10070933_PROG7312_MunicipalServices.Services
{
    public interface IDataService
    {
        // Issues
        List<Issue> Issues { get; }
        void AddIssue(Issue issue);

        // Events
        SortedDictionary<DateTime, List<Event>> EventsByDate { get; }
        void AddEvent(Event ev);
        IEnumerable<Event> SearchEvents(string? category = null, DateTime? date = null);

        // Add this
        List<Event> GetAllEvents();

        // Search history & recommendations
        void RecordSearch(string query);
        List<string> GetTopSearchTerms(int top = 3);
    }
}
