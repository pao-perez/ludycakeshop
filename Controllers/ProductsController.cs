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

        public ProductsController() =>
            _requestDirector = new();

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            IEnumerable<ProductDTO> productsDTO = Mapper.MaptoDTOs(_requestDirector.GetProducts());

            return Ok(productsDTO);
        }

        [HttpGet("{productID}")]
        [Produces("application/json")]
        public IActionResult GetByID(int productID)
        {
            ProductDTO productDTO = Mapper.MaptoDTO(_requestDirector.GetProduct(productID));

            return Ok(productDTO);
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(ProductDTO productDTO)
        {
            Product product = Mapper.MaptoDomain(productDTO);

            // TODO: Change Ok to Created
            return Ok(_requestDirector.CreateProduct(product));
        }

        [HttpPut("{productID}")]
        [Consumes("application/json")]
        public IActionResult Put(int productID, ProductDTO productDTO)
        {
            Product product = Mapper.MaptoDomain(productDTO);

            return Ok(_requestDirector.UpdateProduct(productID, product));
        }

        [HttpDelete("{productID}")]
        [Consumes("application/json")]
        public IActionResult Delete(int productID)
        {
            _requestDirector.DeleteProduct(productID);
            return NoContent();
        }
    }
}
