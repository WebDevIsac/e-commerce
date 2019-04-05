using System;
using System.Collections.Generic;
using System.Text;
using back_end.Services;
using System.Linq;
using NUnit.Framework;
using back_end.Repositories;
using System.Transactions;
using back_end.Models;
using FluentAssertions;

namespace back_end.IntegrationTests.Services
{
    class ProductServiceTests
    {
        private ProductsService productsService;

        [SetUp]
        public void SetUp()
        {
            this.productsService = new ProductsService(new ProductsRepository("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=E-Commerce;Integrated Security=True;Pooling=True"));
        }

        [Test] 
        public void Get_ReturnsResultFromDatabase()
        {
            // Act

            var result = productsService.Get();

            // Assert

            Assert.That(result.Count, Is.AtLeast(5));
            Assert.That(result[0].Id, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("T-Shirt Black"));
            Assert.That(result[0].Info, Is.EqualTo("Black basic t-shirt"));
            Assert.That(result[0].Price, Is.EqualTo(199));
            Assert.That(result[0].Image, Is.EqualTo("https://ean-images.booztcdn.com/mango-man/1300x1700/man43070455_cblack_v99.jpg"));
        }

        [Test]
        public void Get_GivenId_ReturnsResultFromDatabase()
        {
            // Arrange 

            var id = 1;

            var product = new Product
            {
                Id = id,
                Name = "T-Shirt Black",
                Info = "Black basic t-shirt",
                Price = 199,
                Image = "https://ean-images.booztcdn.com/mango-man/1300x1700/man43070455_cblack_v99.jpg"
            };

            // Act

            var result = productsService.Get(id);

            // Assert

            result.Should().BeEquivalentTo(product);
        }

        [Test]
        public void Add_GivenValidProductsItem_SavesIt()
        {
            // Arrange
            var product = new Product
            {
                Name = "T-Shirt Grey",
                Info = "Grey basic t-shirt",
                Price = 399,
                Image = "https://thisisnotavalid.link"
            };

            // Act 

            Product addedItem;

            using (new TransactionScope())
            {
                this.productsService.Add(product);

                addedItem = this.productsService.Get().Last();
            }

            // Assert 

            Assert.That(addedItem, !Is.Null);
            Assert.That(addedItem.Id, Is.GreaterThanOrEqualTo(5));
            Assert.That(addedItem.Name, Is.EqualTo(product.Name));
            Assert.That(addedItem.Info, Is.EqualTo(product.Info));
            Assert.That(addedItem.Price, Is.EqualTo(product.Price));
            Assert.That(addedItem.Image, Is.EqualTo(product.Image));
        }

        [Test]
        public void Delete_GivenValidId_DeletesId()
        {
            // Arrange 

            var id = 1;

            // Act 

            Product result;

            using (new TransactionScope())
            {
                this.productsService.Delete(id);

                result = this.productsService.Get(id);
            }

            // Assert 

            Assert.That(result, Is.EqualTo(null));
        }
    }
}

