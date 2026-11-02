using System.ComponentModel.DataAnnotations;

namespace BookManagement.Application.DTOs.Authentication
{
    public class LoginRequest
    {
        [EmailAddress]
        [Required]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
