using System.ComponentModel.DataAnnotations;

namespace Gold_Mining_Management_System.Models
{
    public class Production
    {
        [Key]
        public int ProductionId { get; set; }

        [Required(ErrorMessage = "Site ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Site Id must be a valid integer greater than 0.")]
        public int SiteId { get; set; } // Foreign key to Site

        [Required(ErrorMessage = "Ore Extracted is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Ore Extracted must be a non-negative value.")]
        public double OreExtracted { get; set; } // In kilograms

        [Required(ErrorMessage = "Gold Produced is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Gold Produced must be a non-negative value.")]
        public double GoldProduced { get; set; } // In kilograms

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date format.")]
        public DateTime Date { get; set; }

        public int? ShiftSupervisor { get; set; } // Foreign key to User

        public Users? Supervisor { get; set; }

        [Required]
        public Sites Site { get; set; }
    }
}
