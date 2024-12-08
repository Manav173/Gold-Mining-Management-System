using System.ComponentModel.DataAnnotations;

namespace Gold_Mining_Management_System.Models
{
    public class Notifications
    {
        [Key]
        public int NotificationId { get; set; }

        [Required(ErrorMessage = "Notification Type is required.")]
        [StringLength(100, ErrorMessage = "Notification Type cannot exceed 100 characters.")]
        public string Type { get; set; } // "Safety", "Maintenance Due", etc.

        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; } 

        [Required(ErrorMessage = "Timestamp is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Timestamp format.")]
        public DateTime Timestamp { get; set; }

        [Required(ErrorMessage = "Receiver Id is required.")]
        public int SendTo { get; set; } // Foreign key to User

        [Required(ErrorMessage = "Sender Id is required.")]
        public int SendFrom { get; set; } // Foreign key to User

        public Users Receiver { get; set; }

        public Users Sender { get; set; }
    }
}
