using LudyCakeShop.Core.Domain.Data;
using LudyCakeShop.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LudyCakeShop.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            return StatusCode(200, _categoryService.GetCategories());
        }

        [AllowAnonymous]
        [HttpGet("{categoryID}")]
        [Produces("application/json")]
        public IActionResult GetByID(string categoryID)
        {
            Category category = _categoryService.GetCategory(categoryID);
            if (category == null)
            {
                return StatusCode(404, "CategoryID not found.");
            }

            return StatusCode(200, category);
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(Category category)
        {
            string categoryID = _categoryService.CreateCategory(category);
            string path = HttpContext.Request.Path;
            string createdURI = path + "/" + categoryID;
            return StatusCode(201, createdURI);
        }

        [HttpPut("{categoryID}")]
        [Consumes("application/json")]
        public IActionResult Put(string categoryID, Category category)
        {
            _categoryService.UpdateCategory(categoryID, category);
            return StatusCode(204);
        }

        [HttpDelete("{categoryID}")]
        [Consumes("application/json")]
        public IActionResult Delete(string categoryID)
        {
            _categoryService.DeleteCategory(categoryID);
            return StatusCode(204);
        }
    }
}
