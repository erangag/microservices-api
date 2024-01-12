using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;

namespace Order.Consumer
{
    public class OrderConsumer : IConsumer<EquityOrderEvent>
    {
        private readonly IMapper _mapper;        

        public OrderConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<EquityOrderEvent> context)
        {
            var order = _mapper.Map<Domain.Entities.Order> (context.Message);
        }
    }
}
