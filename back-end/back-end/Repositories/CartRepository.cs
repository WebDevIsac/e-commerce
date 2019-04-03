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
                int cartId = connection.QuerySingleOrDefault<int>(
                                        @"INSERT INTO Cart(TotalPrice, CustomerId) VALUES(0, 1)
                                            SELECT SCOPE_IDENTITY()");

                return cartId;
            }
        }

        public bool Add(CartItem cartItem)
        {

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute("INSERT INTO CartItems (CartId, ProductId, Quantity) VALUES(@CartId, @ProductId, @Quantity)", cartItem);

                return true;
            }

        }

        public void Delete(CartItem cartItem)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute("DELETE FROM CartItems WHERE CartId = @CartId AND ProductId = @ProductId", cartItem);
            }
        }

        public bool Update(CartItem cartItem)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var cart = this.Get(cartItem.CartId);
                var product = cart.Products.SingleOrDefault(item => item.Id == cartItem.ProductId);
                cartItem.Quantity = cartItem.Quantity + product.Quantity;

                connection.Execute("UPDATE CartItems SET Quantity = @quantity WHERE CartId = @cartId AND ProductId = @productId", cartItem);
                return true;
            }
        }
    }
}
