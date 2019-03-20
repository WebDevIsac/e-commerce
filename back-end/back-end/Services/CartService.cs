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

        public Cart Get(int CartId)
        {
            if (CartId < 1)
            {
                return null;
            }

            return this.cartRepository.Get(CartId);
        }

        public Cart Create(int ProductId, int Quantity)
        {
            var CartId = cartRepository.Create();
            cartRepository.Add(CartId, ProductId, Quantity);

            var NewCart = cartRepository.Get(CartId);

            return NewCart;
        }

        public Cart Add(int CartId, int ProductId, int Quantity)
        {
            cartRepository.Add(CartId, ProductId, Quantity);

            var Cart = cartRepository.Get(CartId);

            return Cart;
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
