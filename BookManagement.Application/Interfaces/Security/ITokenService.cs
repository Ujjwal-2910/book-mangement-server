using BookManagement.Application.DTOs.Security;
using BookManagement.Domain.Entities;

namespace BookManagement.Application.Interfaces.Security
{
    public interface ITokenService
    {
        TokenResult GenerateAccessToken(User user);
    }
}
