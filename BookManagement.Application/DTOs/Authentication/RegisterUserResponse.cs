using System.ComponentModel.DataAnnotations;

namespace BookManagement.Application.DTOs.Authentication
{
    public class RegisterUserResponse
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }
    }
}
