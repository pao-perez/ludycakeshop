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
    public class ProductsController : ControllerBase
    {
        private readonly LCS _requestDirector;

        public ProductsController()
        {
            _requestDirector = new();
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get()
        {
            // TODO: create Custom object binder
            List<ProductsDTO> result = new List<ProductsDTO>();
            foreach (var kp in _requestDirector.GetProductsByCategories())
            {
                ProductsDTO val = new ProductsDTO();
                val._id = kp.Key.CategoryID;
                val.name = kp.Key.CategoryName;
                val.products = kp.Value;
                result.Add(val);
            }

            return Ok(result);
        }
    }
}
