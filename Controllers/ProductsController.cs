using LudyCakeShop.Domain;
using LudyCakeShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LudyCakeShop.Controllers
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
            try
            {
                return StatusCode(200, _productService.GetProducts());
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
        }

        [AllowAnonymous]
        [HttpGet("{productID}")]
        [Produces("application/json")]
        public IActionResult GetByID(string productID)
        {
            try
            {
                Product product = _productService.GetProduct(productID);
                if (product == null)
                {
                    return StatusCode(404, "ProductID not found.");
                }

                return StatusCode(200, product);
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(Product product)
        {
            try
            {
                string productID = _productService.CreateProduct(product);
                string path = HttpContext.Request.Path;
                string createdURI = path + "/" + productID;
                return StatusCode(201, createdURI);
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
        }

        [HttpPut("{productID}")]
        [Consumes("application/json")]
        public IActionResult Put(string productID, Product product)
        {
            try
            {
                _productService.UpdateProduct(productID, product);
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }

            return StatusCode(204);
        }

        [HttpDelete("{productID}")]
        [Consumes("application/json")]
        public IActionResult Delete(string productID)
        {
            try
            {
                _productService.DeleteProduct(productID);
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }

            return StatusCode(204);
        }
    }
}
