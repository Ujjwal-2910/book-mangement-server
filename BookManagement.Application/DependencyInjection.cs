using BookManagement.Application.Interfaces.Authentication;
using BookManagement.Application.Mappings.Authentication;
using BookManagement.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace BookManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddAutoMapper(cfg => { }, typeof(AuthMappingProfile).Assembly);

            return services;
        }
    }
}
