using System;
using WooliesX.Technical.Exercises.Models;

namespace WooliesX.Technical.Exercises.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {

        }

        public User GetUser() => new User { Name = "Roberto Suner", Token = new Guid().ToString() };
    }
}
