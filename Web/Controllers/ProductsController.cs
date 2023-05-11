using LudyCakeShop.Core.Domain.Data;
using LudyCakeShop.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LudyCakeShop.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            this._productService = productService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            return StatusCode(200, _productService.GetProducts());
        }

        [AllowAnonymous]
        [HttpGet("{productID}")]
        [Produces("application/json")]
        public IActionResult GetByID(string productID)
        {
            Product product = _productService.GetProduct(productID);
            if (product == null)
            {
                return StatusCode(404, "ProductID not found.");
            }

            return StatusCode(200, product);
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(Product product)
        {
            string productID = _productService.CreateProduct(product);
            string path = HttpContext.Request.Path;
            string createdURI = path + "/" + productID;
            return StatusCode(201, createdURI);
        }

        [HttpPut("{productID}")]
        [Consumes("application/json")]
        public IActionResult Put(string productID, Product product)
        {
            _productService.UpdateProduct(productID, product);
            return StatusCode(204);
        }

        [HttpDelete("{productID}")]
        [Consumes("application/json")]
        public IActionResult Delete(string productID)
        {
            _productService.DeleteProduct(productID);
            return StatusCode(204);
        }
    }
}
