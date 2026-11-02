using AutoMapper;
using BookManagement.Application.DTOs.Authentication;
using BookManagement.Domain.Entities;

namespace BookManagement.Application.Mappings.Authentication
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<User, RegisterUserResponse>()
                .ForMember(
                    destination => destination.Role,
                    options => options.MapFrom(source => source.Role.Name)
                );

            CreateMap<User, LoginResponse>()
                .ForMember(
                    destination => destination.Role,
                    options => options.MapFrom(source => source.Role.Name)
                );
        }
    }
}
