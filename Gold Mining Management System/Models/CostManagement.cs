using System.ComponentModel.DataAnnotations;

namespace Gold_Mining_Management_System.Models
{
    public class CostManagement
    {
        [Key]
        public int CostId { get; set; }

        [Required(ErrorMessage = "Site ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Site Id must be a valid integer greater than 0.")]
        public int SiteId { get; set; } // Foreign key to Site

        [Required(ErrorMessage = "Expense Type is required.")]
        [StringLength(100, ErrorMessage = "Expense Type cannot exceed 100 characters.")]
        public string ExpenseType { get; set; } // "Labor", "Equipment Maintenance" etc.

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public decimal Amount { get; set; } 

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date format.")]
        public DateTime Date { get; set; }

        public int? ResponsiblePerson { get; set; } 

        public Users? Manager { get; set; }

        public Sites Site { get; set; }
    }
}
