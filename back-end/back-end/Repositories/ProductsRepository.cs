using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using back_end.Controllers;
using back_end.Models;
using back_end.Services;
using Dapper;

namespace back_end.Repositories
{
    public class ProductsRepository
    {
        private readonly string connectionString;

        public ProductsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Product> Get()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var products = connection.Query<Product>("SELECT * FROM Products").ToList();
                return products;
            }
        }

        public Product Get(int product_id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var productItem = connection.QuerySingleOrDefault<Product>("SELECT * FROM Products WHERE Product_id = @product_id", new { product_id });

                if (productItem == null)
                {
                    return null;
                }
                return productItem;
            }
        }

        public bool Add(Product product)
        {
            if (string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(product.Info) || product.Price < 0)
            {
                return false;
            }

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute("INSERT INTO Products (Name, Info, Price, Image) VALUES(@name, @info, @price, @image)", product);

                return true;
            }

        }

        public bool Delete(int product_id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var productItem = connection.QuerySingleOrDefault<Product>("SELECT * FROM Products WHERE Product_id = @product_id", new { product_id });

                if (productItem == null)
                {
                    return false;
                }

                connection.Execute("DELETE FROM Products WHERE Product_id = @product_id", new { product_id });

                return true;
            }
        }
    }
}
