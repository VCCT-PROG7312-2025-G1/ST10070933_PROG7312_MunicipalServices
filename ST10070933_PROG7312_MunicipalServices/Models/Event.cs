using System.ComponentModel.DataAnnotations;

namespace ST10070933_PROG7312_MunicipalServices.Models
{
    public class Event
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = string.Empty;

        public int Priority { get; set; } = 0; // higher = more important
    }
}
