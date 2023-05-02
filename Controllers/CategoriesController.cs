﻿using LudyCakeShop.Domain;
using LudyCakeShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LudyCakeShop.Controllers
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
            try
            {
                return StatusCode(200, _categoryService.GetCategories());
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
        }

        [AllowAnonymous]
        [HttpGet("{categoryID}")]
        [Produces("application/json")]
        public IActionResult GetByID(string categoryID)
        {
            try
            {
                Category category = _categoryService.GetCategory(categoryID);
                if (category == null)
                {
                    return StatusCode(404, "CategoryID not found.");
                }

                return StatusCode(200, category);
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(Category category)
        {
            try
            {
                string categoryID = _categoryService.CreateCategory(category);
                string path = HttpContext.Request.Path;
                string createdURI = path + "/" + categoryID;
                return StatusCode(201, createdURI);
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
        }

        [HttpPut("{categoryID}")]
        [Consumes("application/json")]
        public IActionResult Put(string categoryID, Category category)
        {
            try
            {
                _categoryService.UpdateCategory(categoryID, category);
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
                _categoryService.DeleteCategory(categoryID);
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
