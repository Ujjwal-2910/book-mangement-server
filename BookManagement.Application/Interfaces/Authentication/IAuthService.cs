using BookManagement.Application.DTOs.Authentication;

namespace BookManagement.Application.Interfaces.Authentication
{
    public interface IAuthService
    {
        Task<RegisterUserResponse> RegisterCustomerAsync(RegisterUserRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
