using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Controllers;
using back_end.Models;
using back_end.Repositories;

namespace back_end.Services
{
    public class ProductsService
    {
        private readonly ProductsRepository productsRepository;

        public ProductsService (ProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public List<Product> Get()
        {
            return this.productsRepository.Get();
        }

        public Product Get(int id)
        {
            if (id < 1)
            {
                return null;
            }

            return this.productsRepository.Get(id);
        }

        public bool Add(Product product)
        {
            var result = this.productsRepository.Add(product);

            if (result)
            {
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            if (id >= 1)
            {
                var result = this.productsRepository.Delete(id);

                if (result)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
