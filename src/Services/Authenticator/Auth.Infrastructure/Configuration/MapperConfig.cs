using Auth.Infrastructure.Entities;
using Auth.Infrastructure.Models;
using AutoMapper;

namespace Auth.Infrastructure.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ApiUser, ApiUserDto>().ReverseMap();
        }
    }
}
