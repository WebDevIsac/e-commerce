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

            return cartRepository.Get(CartId);
        }

        public Cart Create(CartItem cartItem)
        {
            var cartId = cartRepository.Create();

            cartItem.CartId = cartId;
            cartRepository.Add(cartItem);

            var newCart = cartRepository.Get(cartId);

            return newCart;
        }

        public Cart Add(CartItem cartItem)
        {
            cartRepository.Add(cartItem);

            var cart = cartRepository.Get(cartItem.CartId);

            return cart;
        }

        public void Delete(CartItem cartItem)
        {
            if (cartItem.CartId >= 1 && cartItem.ProductId >= 1)
            {
                cartRepository.Delete(cartItem);
            }
        }

        public Cart Update(CartItem cartItem)
        {
            cartRepository.Update(cartItem);

            var cart = cartRepository.Get(cartItem.CartId);

            return cart;
        }
    }
}
