using AutoMapper;
using Semesterprojekt.Core.Entites;
using Semesterprojekt.Presentation.Models;

namespace Semesterprojekt.Infrastructure.AutoMapperProfiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}