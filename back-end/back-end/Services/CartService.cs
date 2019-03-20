using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Controllers;
using back_end.Models;
using back_end.Repositories;

namespace back_end.Services
{
    public class CartService
    {
        private readonly CartRepository cartRepository;

        public CartService(CartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public List<Cart> Get()
        {
            return this.cartRepository.Get();
        }

        public Cart Get(int CartId)
        {
            if (CartId < 1)
            {
                return null;
            }

            return this.cartRepository.Get(CartId);
        }

        public bool Add(Cart cart)
        {
            var result = this.cartRepository.Add(cart);

            if (result)
            {
                return true;
            }

            return false;
        }

        public bool Delete(int CartId)
        {
            if (CartId >= 1)
            {
                var result = this.cartRepository.Delete(CartId);

                if (result)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
