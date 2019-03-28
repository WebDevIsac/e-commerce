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

        public Cart Get(int id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var singleCart = connection.QuerySingleOrDefault<Cart>("SELECT * FROM Cart WHERE Id = @id", new { id });

                if (singleCart == null)
                {
                    return null;
                }

                singleCart.Products = connection.Query<Product>("SELECT * FROM CartItems c INNER JOIN Products p ON c.ProductId = p.Id WHERE c.CartId = @id", new { id }).ToList();

                return singleCart;
            }
        }

        public int Create()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute("INSERT INTO Cart(TotalPrice) VALUES(0)");

                var CartId = connection.QuerySingleOrDefault<int>("SELECT Id FROM Cart ORDER BY Id DESC LIMIT 1");

                return CartId;
            }
        }

        public bool Add(CartItem cartItem)
        {

            using (var connection = new SqlConnection(this.connectionString))
            {
                var checkCart = connection.QuerySingleOrDefault<Cart>("SELECT * FROM CartItems WHERE CartId = @CartId", new { cartItem.CartId });

                if (checkCart == null)
                {
                    connection.Execute("INSERT INTO CartItems (CartId, ProductId, Quantity) VALUES(@CartId, @ProductId, @Quantity)", cartItem);
                    return true;
                }

                return false;
            }

        }

        public void Delete(int cartId, int productId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute("DELETE FROM CartItems WHERE CartId = @cartId AND ProductId = @productId", new { cartId, productId });
            }
        }

        public void Update(int cartId, int productId, int quantity)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute("UPDATE CartItems SET Quantity = @quantity WHERE CartId = @cartId AND ProductId = @productId", new { quantity, cartId, productId});
            }
        }
    }
}
