using AutoMapper;
using Domain.Entities;
using Domain.Models;
using EventBus.Messages.Events;

namespace OrderManagement.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<EquityOrderEvent, OrderDto>().ReverseMap();
            CreateMap<DeskOrderEvent, OrderDto>().ReverseMap();
        }
    }
}
