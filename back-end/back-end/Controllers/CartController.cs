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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<Cart>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var result = this.cartService.Get(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody]CartItem cartItem)
        {
            var result = this.cartService.Get(cartItem.CartId);
            if (result == null)
            {
                var cart = cartService.Create(cartItem);
                return Ok(cart);
            }
            
            else
            {
                var cart = cartService.Add(cartItem);

                return Ok(cart);
            }
        }

        [HttpDelete("{CartId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromBody]CartItem cartItem)
        {
            cartService.Delete(cartItem.ProductId, cartItem.CartId);
            return Ok();
            
        }
    }
}