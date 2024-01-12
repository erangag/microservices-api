using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Ordering.Infrastructure.Repositories;

namespace OrderConsumer.API.EventBusConsumer
{
    public class EquityOrderConsumer : IConsumer<EquityOrderEvent>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public EquityOrderConsumer(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task Consume(ConsumeContext<EquityOrderEvent> context)
        {
            try
            {
                var order = _mapper.Map<Domain.Entities.Order>(context.Message);
                await _orderRepository.CreateOrderAsync(order);
            }
            catch (Exception ex)
            {

            }
        }       
    }
}
