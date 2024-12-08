using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gold_Mining_Management_System.Models
{
    public class Sites
    {
        [Key]
        public int SiteId { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Total area is required.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Total area must be greater than 0.")]
        public double TotalArea { get; set; } // Area must be in square kilometers

        [Required(ErrorMessage = "Resource type is required.")]
        [StringLength(50, ErrorMessage = "Resource type cannot exceed 50 characters.")]
        public string ResourceType { get; set; } // "Gold", "Silver", "Copper", "Platinum", "Quartz" etc.

        [Required(ErrorMessage = "Yield estimate is required.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Yield estimate must be 0 or greater.")]
        public double YieldEstimate { get; set; } // Estimated yield in kilograms

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("^(Active|Inactive)$", ErrorMessage = "Status must be 'Active' or 'Inactive'.")]
        public string Status { get; set; } // "Active", "Inactive", "Under Maintenance", "Reclamation", "Closed"

        public int? ManagerId { get; set; } // Foreign key to User

        public Users? Manager { get; set; }
    }
}
