using System.ComponentModel.DataAnnotations;

namespace Gold_Mining_Management_System.Models
{
    public class Reports
    {
        [Key]
        public int ReportId { get; set; }

        [Required(ErrorMessage = "Report Type is required.")]
        [StringLength(100, ErrorMessage = "Report Type cannot exceed 100 characters.")]
        public string Type { get; set; } // "Production", "Safety", "Environmental", "Survey"

        public DateTime GeneratedDate { get; set; }

        [StringLength(500, ErrorMessage = "Report Data cannot exceed 500 characters.")]
        public string Data { get; set; } // Either Plain Data or Test(path of the report).

        public int? CreatedBy { get; set; } // Foreign key to User

        public Users? Creator { get; set; }

        [Range(0, 100, ErrorMessage = "Site Performance should be between 0 to 100 percent.")]
        public int SitePerformance { get; set; } 

        [Range(0, 100, ErrorMessage = "Efficiency details should be between 0 to 100 percent.")]
        public int Efficiency { get; set; }

        [Range(0, 100, ErrorMessage = "Resource Management  details should be between 0 to 100 percent.")]
        public int ResourceManagement { get; set; } 
    }
}
