using AutoMapper;
using nytEksamensprojekt.Entities;
using nytEksamensprojekt.Models;

namespace nytEksamensprojekt.AutoMapperProfiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}