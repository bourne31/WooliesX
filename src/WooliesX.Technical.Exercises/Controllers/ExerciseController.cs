using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public ExerciseController(IUserService userService)
        {
            _userService = userService;
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
    }
}
