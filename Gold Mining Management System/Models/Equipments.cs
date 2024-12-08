using System.ComponentModel.DataAnnotations;

namespace Gold_Mining_Management_System.Models
{
    public class Equipments
    {
        [Key]
        public int EquipmentId { get; set; }

        [Required(ErrorMessage = "Equipment Name is required.")]
        [StringLength(100, ErrorMessage = "Equipment Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Equipment Type is required.")]
        [StringLength(50, ErrorMessage = "Equipment Type cannot be longer than 50 characters.")]
        public string Type { get; set; } // "Excavator", "Drill", etc.

        [Required(ErrorMessage = "Equipment Condition is required.")]
        [StringLength(20, ErrorMessage = "Condition cannot be longer than 20 characters.")]
        public string Condition { get; set; } // "Good", "Needs Repair"

        // Nullable, as some equipment might not be assigned to a site
        [Range(1, int.MaxValue, ErrorMessage = "Assigned Site must be a valid Site Id.")]
        public int? AssignedSite { get; set; }

        public Sites? Site { get; set; }
        public DateTime MaintenanceDate { get; set; }
    }
}
