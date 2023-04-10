using LudyCakeShop.Domain;
using LudyCakeShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LudyCakeShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly LCS _requestDirector;

        public CategoriesController() =>
            _requestDirector = new();

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            IEnumerable<Category> categories;
            try
            {
                categories = _requestDirector.GetCategories();
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
            IEnumerable<CategoryDTO> categoriesDTO = Mapper.MaptoDTOs(categories);
            return StatusCode(200, categoriesDTO);
        }

        [HttpGet("{categoryID}")]
        [Produces("application/json")]
        public IActionResult GetByID(string categoryID)
        {
            Category category;
            try
            {
                category = _requestDirector.GetCategory(categoryID);
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
            if (category == null)
            {
                return StatusCode(404, "CategoryID not found.");
            }

            CategoryDTO categoryDTO = Mapper.MaptoDTO(category);
            return StatusCode(200, categoryDTO);
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(CategoryDTO categoryDTO)
        {
            string categoryID;
            try
            {
                categoryID = _requestDirector.CreateCategory(Mapper.MaptoDomain(categoryDTO));
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }

            string path = HttpContext.Request.Path;
            string createdURI = path + "/" + categoryID;
            return StatusCode(201, createdURI);
        }

        [HttpPut("{categoryID}")]
        [Consumes("application/json")]
        public IActionResult Put(string categoryID, CategoryDTO categoryDTO)
        {
            try
            {
                _requestDirector.UpdateCategory(categoryID, Mapper.MaptoDomain(categoryDTO));
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }

            return StatusCode(204);
        }

        [HttpDelete("{categoryID}")]
        [Consumes("application/json")]
        public IActionResult Delete(string categoryID)
        {
            try
            {
                _requestDirector.DeleteCategory(categoryID);
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
