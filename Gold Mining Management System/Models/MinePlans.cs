using System.ComponentModel.DataAnnotations;

namespace Gold_Mining_Management_System.Models
{
    public class MinePlans
    {
        [Key]
        public int PlanId { get; set; }

        [Required(ErrorMessage = "Site ID is required.")]
        public int SiteId { get; set; } // Foreign key to Site

        [Required(ErrorMessage = "Activity Type is required.")]
        [StringLength(100, ErrorMessage = "Activity Type cannot exceed 100 characters.")]
        public string ActivityType { get; set; } // "Excavation", "Blasting", "Survey", etc.

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Start Date format.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid End Date format.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Assigned Engineer is required.")]
        public int AssignedEngineer { get; set; } // Foreign key to User

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; } // "Planned", "In Progress", "Completed"

        [Required(ErrorMessage = "Engineer is required.")]
        public Users Engineer { get; set; }

        public Sites Site { get; set; }
    }
}
