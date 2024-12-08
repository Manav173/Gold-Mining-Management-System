using System.ComponentModel.DataAnnotations;

namespace Gold_Mining_Management_System.Models
{
    public class SafetyIncident
    {
        [Key]
        public int IncidentId { get; set; }

        [Required(ErrorMessage = "Site ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Site Id must be a valid integer greater than 0.")]
        public int SiteId { get; set; } // Foreign key to Site

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date format.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Severity is required.")]
        [StringLength(20, ErrorMessage = "Severity cannot exceed 20 characters.")]
        public string Severity { get; set; } // "Low", "Medium", "High"

        [Required(ErrorMessage = "Resolution Status is required.")]
        [StringLength(20, ErrorMessage = "Resolution Status cannot exceed 20 characters.")]
        public string ResolutionStatus { get; set; } // "Resolved", "Pending"

        [Required(ErrorMessage = "Reported By is required.")]
        public int ReportedBy { get; set; } // Foreign key to User

        public Users Reporter { get; set; }
    }
}
