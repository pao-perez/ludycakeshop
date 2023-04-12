using LudyCakeShop.Domain;
using LudyCakeShop.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LudyCakeShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly LCS _requestDirector;

        public ProductsController() =>
            _requestDirector = new();

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            IEnumerable<Product> products;
            try
            {
                products = _requestDirector.GetProducts();
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
            IEnumerable<ProductDTO> productsDTO = Mapper.MaptoDTOs(products);
            return StatusCode(200, productsDTO);
        }

        [HttpGet("{productID}")]
        [Produces("application/json")]
        public IActionResult GetByID(string productID)
        {
            Product product;
            try
            {
                product = _requestDirector.GetProduct(productID);
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
            if (product == null)
            {
                return StatusCode(404, "ProductID not found.");
            }

            ProductDTO productDTO = Mapper.MaptoDTO(product);
            return StatusCode(200, productDTO);
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(ProductDTO productDTO)
        {
            string productID;
            try
            {
                productID = _requestDirector.CreateProduct(Mapper.MaptoDomain(productDTO));
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }

            string path = HttpContext.Request.Path;
            string createdURI = path + "/" + productID;
            return StatusCode(201, createdURI);
        }

        [HttpPut("{productID}")]
        [Consumes("application/json")]
        public IActionResult Put(string productID, ProductDTO productDTO)
        {
            try
            {
                _requestDirector.UpdateProduct(productID, Mapper.MaptoDomain(productDTO));
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
                _requestDirector.DeleteProduct(productID);
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
