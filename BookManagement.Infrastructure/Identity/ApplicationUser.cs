using Microsoft.AspNetCore.Identity;

namespace BookManagement.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
