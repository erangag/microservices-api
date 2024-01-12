using AutoMapper;
using Domain.Entities;
using Domain.Models;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderManagementController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public OrderManagementController(IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        [HttpPost]        
        public async Task<ActionResult> PlaceEquityOrder([FromBody] OrderDto orderDto)
        {
            try
            {
                // send checkout event to rabbitmq
                var eventMessage = _mapper.Map<EquityOrderEvent>(orderDto);
                await _publishEndpoint.Publish(eventMessage);

                return Accepted();
            }
            catch (Exception ex)
            {
                return Problem("Fail");
            }
        }

        [HttpPost]
        [Route(nameof(PlaceDeskOrder))]
        public async Task<ActionResult> PlaceDeskOrder([FromBody] OrderDto orderDto)
        {
            try
            {
                // send checkout event to rabbitmq
                var eventMessage = _mapper.Map<DeskOrderEvent>(orderDto);
                await _publishEndpoint.Publish(eventMessage);

                return Accepted();
            }
            catch (Exception ex)
            {
                return Problem("Fail");
            }
        }

        [HttpGet]
        public string Get()
        {
            return "Api Success";
        }
    }
}
