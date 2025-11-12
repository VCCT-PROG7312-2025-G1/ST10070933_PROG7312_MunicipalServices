using System.ComponentModel.DataAnnotations;

namespace ST10070933_PROG7312_MunicipalServices.Models
{
    public class ServiceRequest
    {
        public string? RequestId { get; set; } 

        public int NumericId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000)]
        public string Description { get; set; }

        public string? Status { get; set; }  

        public DateTime Created { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(200)]
        public string Location { get; set; }

        [Required(ErrorMessage = "Priority is required")]
        [Range(1, 3, ErrorMessage = "Please select a priority between 1-3")]
        public int Priority { get; set; }
    }
}