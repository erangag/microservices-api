using Customer.API.Entities;

namespace Customer.API.Repositories
{
    public interface ICustomerRepository
    {
        Task<CustomerProfile> GetCustomerByIdAsync(int id);

        Task<List<CustomerProfile>> GetCustomersAsync();

    }
}
