using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesX.Technical.Exercises.Models;
using WooliesX.Technical.Exercises.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WooliesX.Technical.Exercises.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public ExerciseController(IUserService userService,
            IProductService productService)
        {
            _userService = userService;
            _productService = productService;
        }

        public ActionResult<User> Get()
        {
            return GetUser();
        }

        [Route("user")]
        public ActionResult<User> GetUser()
        {
            var user = _userService.GetUser();
            if (user == null)
            {
                NotFound();
            }

            return user;
        }

        [Route("sort")]
        public async Task<ActionResult<IEnumerable<Product>>> SortProducts(string sortOption)
        {
            var products = await _productService.GetProductsAsync(sortOption);
            if (products == null)
            {
                NotFound();
            }

            return Ok(products);
        }
    }
}
