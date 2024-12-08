using System.ComponentModel.DataAnnotations;

namespace Gold_Mining_Management_System.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(255, ErrorMessage = "Password hash cannot exceed 255 characters.")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression("^(Admin|Mine Manager|Geologist|Engineer|Safety Officer|Field Worker)$",
            ErrorMessage = "Role must be one of: Admin, Mine Manager, Geologist, Engineer, Safety Officer, or Field Worker.")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
