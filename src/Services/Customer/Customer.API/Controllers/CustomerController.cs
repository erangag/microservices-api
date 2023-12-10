using AutoMapper;
using Customer.API.Models;
using Customer.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository,IMapper mapper)
        {
            _repository = customerRepository;
            _mapper = mapper;
        }

        //Q: How to get maultiple filtering are incluted
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerProfileDto>> GetCustomer(int id)
        {
            var customer = await _repository.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CustomerProfileDto>(customer));
        }

        [HttpGet]        
        public async Task<ActionResult<IEnumerable<CustomerProfileDto>>> GetCustomers()
        {
            var customers = await _repository.GetCustomersAsync();
            return Ok(_mapper.Map<IEnumerable<CustomerProfileDto>>(customers));
        }
    }
}
