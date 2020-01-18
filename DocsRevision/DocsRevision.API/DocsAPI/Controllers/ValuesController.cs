﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
namespace DocsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "TA FUNFANDO", "BORA LÁ" };
        }
    }
}
