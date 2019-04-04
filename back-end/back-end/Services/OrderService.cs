using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Controllers;
using back_end.Models;
using back_end.Repositories;

namespace back_end.Services
{
    public class OrderService
    {
        public OrderRepository orderRepository { get; set; }
        public CartRepository cartRepository;
        public CustomerRepository customerRepository;

        public OrderService(OrderRepository orderRepository, CartRepository cartRepository, CustomerRepository customerRepository)
        {
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
            this.customerRepository = customerRepository;
        }

        public List<Order> Get()
        {
            return orderRepository.Get();
        }

        public Order Get(int id)
        {
            var order = orderRepository.Get(id);

            order.Cart = cartRepository.Get(order.CartId);
            order.Customer = customerRepository.Get(order.CustomerId);

            return order;
        }

        public Order Create(int id, Customer customer)
        {
            var cart = cartRepository.Get(id);

            var orderId = orderRepository.Create(id, customer);

            var order = orderRepository.Get(orderId);

            order.Cart = cart;

            order.Customer = customerRepository.Get(order.CustomerId);

            return order;
        }


    }
}