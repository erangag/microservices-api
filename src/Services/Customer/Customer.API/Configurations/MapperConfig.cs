using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace Customer.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<CustomerProfile, CustomerProfileDto>().ReverseMap();
        }
    }
}
