using System.ComponentModel.DataAnnotations;

namespace BookManagement.Application.DTOs.Authentication
{
    public class RegisterUserRequest
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [MaxLength(256)]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MinLength(8)]
        [Required]
        [RegularExpression(
            @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; } = string.Empty;

        [MinLength(8)]
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
