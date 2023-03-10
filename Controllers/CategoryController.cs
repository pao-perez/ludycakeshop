using LudyCakeShop.Domain;
using LudyCakeShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LudyCakeShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
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
        public IActionResult Get()
        {
            // TODO: create Custom object binder
            List<CategoryWithProductsDTO> result = new List<CategoryWithProductsDTO>();
            foreach (var kp in _requestDirector.GetProductsByCategory())
            {
                CategoryWithProductsDTO val = new CategoryWithProductsDTO();
                val._id = kp.Key.CategoryID;
                val.name = kp.Key.CategoryName;
                val.products = kp.Value;
                result.Add(val);
            }

            return Ok(result);
        }
    }
}
