using BookManagement.Application.Configuration;
using BookManagement.Application.Interfaces.Repositories;
using BookManagement.Application.Interfaces.Security;
using BookManagement.Domain.Entities;
using BookManagement.Infrastructure.Identity;
using BookManagement.Infrastructure.Persistence;
using BookManagement.Infrastructure.Repositories;
using BookManagement.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Connection string for SQL Server database
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddOptions<JwtSettings>()
                .Bind(configuration.GetSection("Jwt"))
                .Validate(settings =>
                    !string.IsNullOrWhiteSpace(settings.Issuer) &&
                    !string.IsNullOrWhiteSpace(settings.Audience) &&
                    !string.IsNullOrWhiteSpace(settings.SecretKey) &&
                    IsValidSecretKey(settings.SecretKey) &&
                    settings.ExpirationInMinutes > 0,
                    "JWT configuration is invalid.")
                .ValidateOnStart();

            services.AddScoped<ITokenService, JwtTokenService>();

            return services;
        }

        private static bool IsValidSecretKey(string secretKey)
        {
            try
            {
                return Convert.FromBase64String(secretKey).Length >= 32;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
