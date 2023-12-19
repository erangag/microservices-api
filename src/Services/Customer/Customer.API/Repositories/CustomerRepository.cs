using Core.Helpers;
using Dapper;
using Domain.Entities;

namespace Customer.API.Repositories
{
    /// <summary>
    /// Represents a repository for interacting with customer data.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {        
        private readonly IDatabaseHelper _databaseHelper;

        public CustomerRepository(IConfiguration configuration, IDatabaseHelper databaseHelper)
        {            
            _databaseHelper = databaseHelper;
        }

        /// <summary>
        /// Retrieves a customer profile by the specified customer ID asynchronously.
        /// Returns null if no customer is found with the given ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        public async Task<CustomerProfile> GetCustomerByIdAsync(int id)
        {
            CustomerProfile result;

            using (var connection = _databaseHelper.CreateAndOpenConnection())
            {
                result = await connection.QueryFirstOrDefaultAsync<CustomerProfile>("SELECT * FROM Customer WHERE Id = @Id", new { Id = id });
            }

            return result;
        }

        /// <summary>
        /// Retrieves a list of all customer profiles asynchronously.
        /// </summary>
        public async Task<List<CustomerProfile>> GetCustomersAsync()
        {
            List<CustomerProfile> result;

            using (var connection = _databaseHelper.CreateAndOpenConnection())
            {                
                result = (await connection.QueryAsync<CustomerProfile>("SELECT * FROM Customer")).ToList();
            }

            return result;
        }        
    }
}
