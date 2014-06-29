using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using AngularStub.Data;
using AngularStub.Domain;
using AngularStub.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AngularStub.Tests.Controllers
{
    /// <summary>
    /// Integration tests for interactions between Web API controller and the repository.
    /// </summary>
    [TestClass]
    public class ProductsControllerTests
    {
        private ProductsController _controller;
        private ProductRepository _pRepo;
        private IEnumerable<Product> _pNumerable;
        private Product _product;
        private int _laptopId;

        [TestInitialize]
        public void Initialize()
        {
            _pRepo = new ProductRepository();
            _product = new Product();
            _pNumerable = Enumerable.Empty<Product>();
            _controller = new ProductsController(_pRepo);
        }
        /// <summary>
        /// Arrange
        /// </summary>
        public void GivenProductsInDb()
        {
            var products = new[]
            {
                new Product {Name = "Speakers", Category = "Peripherals", Price = 40.00M},
                new Product {Name = "Mouse", Category = "Peripherals", Price = 20.00M},
                new Product {Name = "Keyboard", Category = "Peripherals", Price = 30.00M},
                new Product {Name = "Lamp", Category = "Accessories", Price = 10.00M},
                new Product {Name = "Monitor", Category = "Peripherals", Price = 200.00M},
                new Product {Name = "Laptop", Category = "Components", Price = 1200.00M}
            };
            foreach (var product in products)
            {
                _pRepo.Add(product);
                if (product.Name == "Laptop") 
                    _laptopId = product.Id;
            }
        }
        /// <summary>
        /// Arrange
        /// </summary>
        private void GivenNewProduct()
        {
            _product = new Product
            {
                Name = "Binder",
                Category = "Accessories",
                Price = 12.00M
            };
        }
        /// <summary>
        /// Act
        /// </summary>
        public void WhenGetCalled()
        {
            _pNumerable = _controller.Get();
        }
        /// <summary>
        /// Act
        /// </summary>
        private void WhenGetByIdCalled(int id)
        {
            var httpActionResult = _controller.Get(id);
            var okNegotiatedContentResult = httpActionResult as OkNegotiatedContentResult<Product>;
            if (okNegotiatedContentResult == null)
            {
                throw new NullReferenceException("okNegotiatedContentResult");
            }
            var product = okNegotiatedContentResult.Content;
            _product = product;
        }
        /// <summary>
        /// Act
        /// </summary>
        private void WhenPostCalled()
        {
            _controller.Post(_product);
        }
        /// <summary>
        /// Act
        /// </summary>
        private void WhenProductChanges()
        {
            WhenGetByIdCalled(_laptopId);
            _product.Name = "MacBook";
            _product.Price = 12000.00M;
        }
        /// <summary>
        /// Act
        /// </summary>
        private void WhenPutCalled(int id)
        {
            _controller.Put(id, _product);
        }
        /// <summary>
        /// Act
        /// </summary>
        private void WhenDeleteCalledForEachProduct()
        {
            WhenGetCalled();
            foreach (var product in _pNumerable)
                _controller.Delete(product.Id);
        }
        /// <summary>
        /// Assert
        /// </summary>
        public void ThenValuesMatchExpected(string name, decimal price)
        {
            WhenGetCalled();
            var pList = _pNumerable.ToList();
            var pListSingle = pList.Single(p => p.Name == name);
            var priceSingle = pListSingle.Price;
            Assert.AreEqual(priceSingle, price);
        }
        /// <summary>
        /// Assert
        /// </summary>
        private void ThenValuesMatchExpected(int id)
        {
            var product = _pRepo.GetById(id);
            Assert.AreEqual(_product, product);
        }
        /// <summary>
        /// Assert
        /// </summary>
        private void ThenTableIsEmpty()
        {
            var deleted = _pRepo.GetAll();
            Assert.IsFalse(deleted.Any());
        }

        [TestMethod]
        public void Get()
        {
            GivenProductsInDb();
            WhenGetCalled();
            ThenValuesMatchExpected("Laptop", 1200.00M);
        }

        [TestMethod]
        public void GetById()
        {
            GivenProductsInDb();
            WhenGetByIdCalled(_laptopId);
            ThenValuesMatchExpected(_laptopId);
        }

        [TestMethod]
        public void Post()
        {
            GivenProductsInDb();
            GivenNewProduct();
            WhenPostCalled();
            ThenValuesMatchExpected(_product.Id); // Should work
        }

        [TestMethod]
        public void Put()
        {
            GivenProductsInDb();
            WhenProductChanges();
            WhenPutCalled(_laptopId);
            ThenValuesMatchExpected("MacBook", 12000.00M);
        }

        [TestMethod]
        [TestCleanup]
        public void Delete()
        {
            GivenProductsInDb();
            WhenDeleteCalledForEachProduct();
            ThenTableIsEmpty();
        }
    }
}