using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesX.Technical.Exercises.External.Contracts.Requests;
using WooliesX.Technical.Exercises.Models;
using WooliesX.Technical.Exercises.Services;

namespace WooliesX.Technical.Exercises.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly ITrolleyService _trolleyService;

        public ExerciseController(IUserService userService,
            IProductService productService,
            ITrolleyService trolleyService)
        {
            _userService = userService;
            _productService = productService;
            _trolleyService = trolleyService;
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
                return NotFound();
            }

            return user;
        }

        [Route("sort")]
        public async Task<ActionResult<IEnumerable<Product>>> SortProducts(string sortOption)
        {
            if (string.IsNullOrWhiteSpace(sortOption))
            {
                return BadRequest("SortOption parameter is required.");
            }

            var products = await _productService.GetProductsAsync(sortOption);
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpPost]
        [Route("trolleytotal")]
        public async Task<ActionResult<decimal>> GetTrolleyTotal([FromBody] TrolleyRequest trolleyRequest)
        {
            var total = await _trolleyService.GetTrolleyTotalAsync(trolleyRequest);
            if (total <= 0)
            {
                return BadRequest();
            }

            return Ok(total);
        }
    }
}
