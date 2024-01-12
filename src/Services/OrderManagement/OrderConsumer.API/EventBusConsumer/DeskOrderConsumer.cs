using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Ordering.Infrastructure.Repositories;

namespace OrderConsumer.API.EventBusConsumer
{
    public class DeskOrderConsumer : IConsumer<DeskOrderEvent>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public DeskOrderConsumer(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task Consume(ConsumeContext<DeskOrderEvent> context)
        {            
            var order = _mapper.Map<Domain.Entities.Order>(context.Message);            
            await _orderRepository.CreateOrderAsync(order);
        }
    }
}
