namespace ST10070933_PROG7312_MunicipalServices.Models
{
    public class Issue
 {
public Guid Id { get; set; } = Guid.NewGuid();
    public string Location { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime ReportedAt { get; set; } = DateTime.UtcNow;
    public List<string> Attachments { get; set; } = new List<string>();
    public string Status { get; set; } = "Reported"; // could be Reported, InProgress, Resolved
}
}
