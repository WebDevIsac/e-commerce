using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using back_end.Models;
using back_end.Repositories;
using back_end.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly string connectionString;
        private readonly OrderService orderService;

        public OrderController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");
            this.orderService = new OrderService(
                    new OrderRepository(connectionString),
                    new CartRepository(connectionString),
                    new CustomerRepository(connectionString)
                );
        }

        [HttpPost("{id}")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Order), StatusCodes.Status400BadRequest)]
        public IActionResult Post(int id,[FromBody]Customer customer)
        {
            var result = this.orderService.Create(id, customer);
            return Ok(result);
        }
    }
}