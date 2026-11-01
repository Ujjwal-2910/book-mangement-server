using BookManagement.Domain.Entities;

namespace BookManagement.Application.Interfaces.Security
{
    public interface IPasswordHasherService
    {
        string HashPassword(User user, string password);

        bool VerifyPassword(
            User user,
            string hashedPassword,
            string providedPassword);
    }
}
