using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Models;
using System.Data.SqlClient;
using Dapper;

namespace back_end.Repositories
{
    public class OrderRepository
    {
        private readonly string connectionString;

        public OrderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Order> Get()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var orders = connection.Query<Order>("SELECT * FROM Orders").ToList();

                return orders;
            }
        }

        public Order Get(int id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var order = connection.QuerySingleOrDefault<Order>("SELECT * FROM Orders WHERE Id = @id", new { id });

                return order;
            }
        }

        public int Create(int id, Customer customer)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                int customerId = connection.QuerySingleOrDefault<int>(@"INSERT INTO Customers (Name, Country, Address, City, Zipcode) VALUES (@Name, @Country, @Address, @City, @Zipcode)
                 SELECT SCOPE_IDENTITY()", customer);
                
                int orderId = connection.QuerySingleOrDefault<int>(@"INSERT INTO Orders (CartId, CustomerId, TotalPrice) VALUES (@id, @customerId, 0)
                    SELECT SCOPE_IDENTITY()", new { id, customerId });

                return orderId;
            }
        }
    }
}