using Customer.API.Entities;
using Dapper;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Customer.API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;        

        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CustomerDBConnectionStrings") ?? string.Empty;
        }

        public async Task<CustomerProfile> GetCustomerByIdAsync(int id)
        {
            CustomerProfile result;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                result = await connection.QueryFirstOrDefaultAsync<CustomerProfile>("SELECT * FROM Customer WHERE Id = @Id", new { Id = id });
            }

            return result;
        }

        public async Task<List<CustomerProfile>> GetCustomersAsync()
        {
            List<CustomerProfile> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                result = (await connection.QueryAsync<CustomerProfile>("SELECT * FROM Customer")).ToList();
            }

            return result;
        }        
    }
}
