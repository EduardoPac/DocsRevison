using System.Collections.Generic;
using DocsAPI.Builders;
using DocsAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BurgerMonkeys.Tools;
using LiteDB;
using DocsAPI.Services;
using Microsoft.Extensions.Logging;
using System.Linq;
using Bogus;

namespace DocsAPI.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDocumentService _documentService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, IDocumentService documentService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _documentService = documentService;
            _logger = logger;
        }

        // GET: /api/users
        [HttpGet]
        public IEnumerable<User> Get(string email = "")
        {
            List<User> list = new List<User>();

            if (string.IsNullOrWhiteSpace(email))
                list = _userService.FindAll().ToList();
            else
                list.Add(_userService.FindOne(email));

            return list;
        }

        //Post: /api/users?email=&name=
        [HttpPost]
        public async Task Post(string email, string name)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(name))
                return;

            var user = new User()
            {
                Id = Generator.GetId(8),
                Email = email,
                Name = name,
            };

            //Cria uma lista de documentos para o usuario aqui
            var listDocuments = DocumentBuilder.New().WithCreator(user.Id).BuildList(new Faker().Random.Int(1, 10));

            await _userService.Insert(user);
            await _documentService.InsertAll(listDocuments);
        }
    }
}
