using AutoMapper;
using Customer.API.Entities;
using Customer.API.Models;
using System.Runtime;

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
