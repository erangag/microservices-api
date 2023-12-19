using System.Data.SqlClient;

namespace Core.Helpers
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseHelper"/> class with the provided connection string.
        /// </summary>
        /// <param name="connectionString">The connection string for the database.</param>
        public DatabaseHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Creates and opens a new SQL database connection using the configured connection string.
        /// </summary>
        /// <returns>An open SqlConnection instance.</returns>
        public SqlConnection CreateAndOpenConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
