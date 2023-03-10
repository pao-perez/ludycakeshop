using LudyCakeShop.Domain;
using LudyCakeShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudyCakeShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly LCS _requestDirector;

        public CategoriesController(ILogger<CategoriesController> logger)
        {
            _logger = logger;
            _requestDirector = new();
        }

        [HttpGet]
        public Dictionary<Category, List<Product>> Get()
        {
            return _requestDirector.GetProductsByCategory();
        }
    }
}
