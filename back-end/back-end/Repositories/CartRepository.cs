using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Models;
using System.Data.SqlClient;
using Dapper;

namespace back_end.Repositories
{
    public class CartRepository
    {
        private readonly string connectionString;

        public CartRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Cart Get(int CartId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var singleCart = connection.QuerySingleOrDefault<Cart>("SELECT * FROM Cart WHERE CartId = @CartId", new { CartId });

                if (singleCart == null)
                {
                    return null;
                }

                singleCart.Products = connection.Query<Product>("SELECT * FROM CartItems c INNER JOIN Products p ON c.ProductId = p.ProductId WHERE c.CartId = @CartId", new { CartId }).ToList();

                return singleCart;
            }
        }

        public int Create()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute("INSERT INTO Cart(TotalPrice) VALUES(0)");
                var CartId = connection.QuerySingleOrDefault<int>("SELECT CartId FROM Cart ORDER BY CardId DESC LIMIT 1");
                return CartId;
            }
        }

        public bool Add(Cart cart)
        {

            using (var connection = new SqlConnection(this.connectionString))
            {
                var checkCart = connection.QuerySingleOrDefault<Cart>("SELECT * FROM Cart WHERE CartId = @CartId", new { cart.CartId });

                if (checkCart == null)
                {
                    connection.Execute("INSERT INTO Cart (TotalPrice, CustomerId) VALUES(@TotalPrice, @CustomerId)", cart);
                    return true;
                }

                return false;
            }

        }

        public bool Delete(int CartId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var singleCart = connection.QuerySingleOrDefault<Cart>("SELECT * FROM Cart WHERE CartId = @CartId", new { CartId });

                if (singleCart == null)
                {
                    return false;
                }

                connection.Execute("DELETE FROM Cart WHERE CartId = @CartId", new { CartId });

                return true;
            }
        }
    }
}
