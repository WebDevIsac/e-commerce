﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Models;
using System.Data.SqlClient;
using Dapper;

namespace back_end.Repositories
{
    public class CustomerRepository
    {
        private readonly string connectionString;

        public CustomerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Customer Get(int id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var customer = connection.QuerySingleOrDefault<Customer>("SELECT * FROM Customers WHERE Id = @id", new { id });

                return customer;
            }
        }
    }
}