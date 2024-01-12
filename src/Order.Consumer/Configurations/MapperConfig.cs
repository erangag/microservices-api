using AutoMapper;
using EventBus.Messages.Events;

namespace Order.Consumer.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<EquityOrderEvent, Domain.Entities.Order>().ReverseMap();
        }
    }
}
