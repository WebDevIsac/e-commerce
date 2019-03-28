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

        public int Create(Cart cart, Customer customer)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute("INSERT INTO Customers (Name, Country, Address, City, Zipcode) VALUES (@Name, @Country, @Address, @City, @Zipcode)", customer);

                var customerId = connection.QuerySingleOrDefault<int>("SELECT Id FROM Customers ORDER BY Id DESC LIMIT 1");
                
                connection.Execute("INSERT INTO Orders (CartId, CustomerId, TotalPrice) VALUES (@cartId, @customerId, 0)", new { cart.Id, customerId });

                var orderId = connection.QuerySingleOrDefault<int>("SELECT Id FROM Orders ORDER BY DESC LIMIT 1");

                return orderId;
            }
        }
    }
}