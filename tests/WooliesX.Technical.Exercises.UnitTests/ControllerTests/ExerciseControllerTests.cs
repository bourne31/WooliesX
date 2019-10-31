using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesX.Technical.Exercises.Controllers;
using WooliesX.Technical.Exercises.External.Contracts.Requests;
using WooliesX.Technical.Exercises.Models;
using WooliesX.Technical.Exercises.Services;
using Xunit;

namespace WooliesX.Technical.Exercises.UnitTests.ControllerTests
{
    public class ExerciseControllerTests
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly ITrolleyService _trolleyService;
        private readonly ExerciseController _controller;

        public ExerciseControllerTests()
        {
            _userService = Substitute.For<IUserService>();
            _productService = Substitute.For<IProductService>();
            _trolleyService = Substitute.For<ITrolleyService>();

            _controller = new ExerciseController(_userService, _productService, _trolleyService);
        }

        [Fact]
        public void GetReturnsUser()
        {
            // Arrange
            var user = new User { Name = "Test", Token = "abc123" };
            _userService.GetUser().Returns(user);

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsAssignableFrom<User>(result.Value);
            Assert.Equal(user.Name, result.Value.Name);
            Assert.Equal(user.Token, result.Value.Token);
        }

        [Theory()]
        [InlineData("")]
        [InlineData("Low")]
        [InlineData("High")]
        [InlineData("Ascending")]
        [InlineData("Descending")]
        [InlineData("Recommended")]
        public async Task SortProducts(string sortOption)
        {
            // Arrange
            var products = new List<Product>()
            {
                new Product
                {
                    Name = "Test Product A",
                    Price = 99,
                    Quantity = 1
                },
                new Product
                {
                    Name = "Test Product B",
                    Price = 80,
                    Quantity = 1
                }
            };
            _productService.GetProductsAsync(sortOption).Returns(Task.FromResult(products.AsEnumerable()));

            // Act
            var actionResult = await _controller.SortProducts(sortOption);

            var result = actionResult.Result as OkObjectResult;
            var productsResult = (IEnumerable<Product>)result.Value;

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Product>>(productsResult);
            Assert.Equal(productsResult.FirstOrDefault().Name, productsResult.FirstOrDefault().Name);
            Assert.Equal(productsResult.FirstOrDefault().Price, productsResult.FirstOrDefault().Price);
            Assert.Equal(productsResult.FirstOrDefault().Quantity, productsResult.FirstOrDefault().Quantity);
        }

        [Fact]
        public async Task GetTrolleyTotal()
        {
            // Arrange
            var trolleyRequest = new TrolleyRequest
            {
                Products = new List<ProductRequest>
                {
                    new ProductRequest
                    {
                        Name = "Test A",
                        Price = 14
                    }
                },
                Specials = new List<SpecialRequest>
                {
                    new SpecialRequest
                    {
                        Quantities = new List<QuantityRequest>
                        {
                            new QuantityRequest
                            {
                                Name = "Test B",
                                Quantity = 1
                            }
                        }
                    }
                },
                Quantities = new List<QuantityRequest>
                {
                    new QuantityRequest
                    {
                        Name = "Test C"
                        ,Quantity = 1
                    }
                }
            };
            _trolleyService.GetTrolleyTotalAsync(trolleyRequest).Returns(Task.FromResult(14m));

            // Act
            var actionResult = await _controller.GetTrolleyTotal(trolleyRequest);

            var result = actionResult.Result as OkObjectResult;
            var total = (decimal)result.Value;

            // Assert
            Assert.IsAssignableFrom<decimal>(total);
            Assert.Equal(14, total);
        }
    }
}
