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
    public class CartController : Controller
    {
        private readonly string connectionString;
        private readonly CartService cartService;

        public CartController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");
            this.cartService = new CartService(new CartRepository(connectionString));
        }

        [HttpGet("{CartId}")]
        [ProducesResponseType(typeof(List<Cart>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int CartId)
        {
            var result = this.cartService.Get(CartId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody]Cart cart)
        {
            var result = this.cartService.Add(cart);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{CartId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int CartId)
        {
            var result = this.cartService.Delete(CartId);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}