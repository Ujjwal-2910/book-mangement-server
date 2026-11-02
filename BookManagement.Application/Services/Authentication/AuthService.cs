using AutoMapper;
using BookManagement.Application.DTOs.Authentication;
using BookManagement.Application.Exceptions;
using BookManagement.Application.Interfaces.Authentication;
using BookManagement.Application.Interfaces.Repositories;
using BookManagement.Application.Interfaces.Security;
using BookManagement.Domain.Entities;

namespace BookManagement.Application.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            IPasswordHasherService passwordHasher,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<RegisterUserResponse> RegisterCustomerAsync(RegisterUserRequest request)
        {
            var normalizedEmail = request.Email.Trim().ToUpperInvariant();
            var user = await _userRepository.GetByEmailAsync(normalizedEmail);
            if (user is not null)
            {
                throw new DuplicateEmailException("An account with this email already exists.");
            }

            var customerRole = await _roleRepository.GetByNameAsync("Customer");
            if (customerRole is null)
            {
                throw new InvalidOperationException("Customer role not found");
            }

            var newUser = new User
            {
                Name = request.Name.Trim(),
                Email = normalizedEmail,
                PasswordHash = string.Empty,
                RoleId = customerRole.Id,
                CreatedAt = DateTimeOffset.UtcNow,
                Role = customerRole,
                IsActive = true,
            };
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, request.Password);
            newUser.MustChangePassword = false;
            await _userRepository.AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<RegisterUserResponse>(newUser);
        }
    }
}
