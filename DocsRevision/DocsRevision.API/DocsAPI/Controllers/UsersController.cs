using System.Collections.Generic;
using DocsAPI.Builders;
using DocsAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DocsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        // GET: /api/users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var list = UserBuilder.New().BuildList(5);
            return list;
        }

        // POST: /api/users //Cria um usuario

        // GET: /api/users/{email} //Pega um usuario pelo email
    }
}
