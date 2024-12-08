using System.ComponentModel.DataAnnotations;

namespace Gold_Mining_Management_System.Models
{
    public class EnvironmentalData
    {
        [Key]
        public int AssessmentId { get; set; }

        [Required(ErrorMessage = "Site ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Site Id must be a valid integer greater than 0.")]
        public int SiteId { get; set; } // Foreign key to Site

        [Required(ErrorMessage = "Impact Type is required.")]
        [StringLength(100, ErrorMessage = "Impact Type cannot exceed 100 characters.")]
        public string ImpactType { get; set; } // "Water Pollution", "Deforestation", etc.

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date format.")]
        public DateTime Date { get; set; }

        public string Recommendations { get; set; } 

        
        public int? ConductedBy { get; set; } // Foreign key to User

        public Users? Officer { get; set; }
        public Sites Site { get; set; }
    }
}
