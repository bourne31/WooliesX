using NSubstitute;
using WooliesX.Technical.Exercises.Controllers;
using WooliesX.Technical.Exercises.Models;
using WooliesX.Technical.Exercises.Services;
using Xunit;

namespace WooliesX.Technical.Exercises.UnitTests.ControllerTests
{
    public class ExerciseControllerTests
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly ExerciseController _controller;

        public ExerciseControllerTests()
        {
            _userService = Substitute.For<IUserService>();
            _productService = Substitute.For<IProductService>();

            _controller = new ExerciseController(_userService, _productService);
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
    }
}
