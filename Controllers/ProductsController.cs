using LudyCakeShop.Domain;
using LudyCakeShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IActionResult GetAll()
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

        [HttpGet("{_id}")]
        [Produces("application/json")]
        public IActionResult GetByID(int _id)
        {
            return Ok(_requestDirector.GetProduct(_id));
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(ProductsDTO productsDTO)
        {
            // TODO: Change Ok to Created
            Product product = new();
            product.CategoryID = productsDTO._id;

            return Ok(_requestDirector.AddProduct(product));
        }

        [HttpPut]
        [Consumes("application/json")]
        public IActionResult Put(ProductsDTO productsDTO)
        {
            Product product = new();
            product.CategoryID = productsDTO._id;

            return Ok(_requestDirector.UpdateProduct(product));
        }

        [HttpDelete("{_id}")]
        [Consumes("application/json")]
        public IActionResult Delete(int _id)
        {
            _requestDirector.DeleteProduct(_id);
            return NoContent();
        }
    }
}
