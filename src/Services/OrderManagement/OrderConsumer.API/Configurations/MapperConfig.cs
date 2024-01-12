using AutoMapper;
using EventBus.Messages.Events;

namespace OrderConsumer.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<EquityOrderEvent, Domain.Entities.Order>().ReverseMap();
            CreateMap<DeskOrderEvent, Domain.Entities.Order>().ReverseMap();
        }
    }
}
