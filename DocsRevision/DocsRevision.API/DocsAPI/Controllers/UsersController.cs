using System.Collections.Generic;
using DocsAPI.Builders;
using DocsAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BurgerMonkeys.Tools;
using LiteDB;
using DocsAPI.Services;
using Microsoft.Extensions.Logging;

namespace DocsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // GET: /api/users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var list = _userService.FindAll();
            return list;
        }

        // GET: /api/users?email=
        [HttpGet]
        public User Get(string email)
        {
            var user = _userService.FindOne(email);
            return user;
        }

        //Post: /api/users?email=&name=
        [HttpPost]
        public User Post(string email, string name)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(name))
                return null;

            var user = new User()
            {
                Id = Generator.GetId(8),
                Email = email,
                Name = name,
            };

            var result = _userService.Insert(user);

            if (result != 0)
                return user;

            return null;
        }
    }
}
