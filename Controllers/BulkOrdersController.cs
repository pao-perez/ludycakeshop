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
    public class BulkOrdersController : ControllerBase
    {
        private readonly LCS _requestDirector;

        public BulkOrdersController() =>
            _requestDirector = new();

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BulkOrder))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                return StatusCode(200, _requestDirector.GetBulkOrders());
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
        }

        [HttpGet("{bulkOrderID}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BulkOrder))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByID(string bulkOrderID)
        {
            try
            {
                BulkOrder bulkOrder = _requestDirector.GetBulkOrder(bulkOrderID);
                if (bulkOrder == null)
                {
                    return StatusCode(404, "BulkOrderID not found.");
                }

                return StatusCode(200, bulkOrder);
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(BulkOrder bulkOrder)
        {
            try
            {
                string bulkOrderID = _requestDirector.CreateBulkOrder(bulkOrder);
                string path = HttpContext.Request.Path;
                string createdURI = path + "/" + bulkOrderID;
                return StatusCode(201, createdURI);
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
        }

        [HttpPut("{bulkOrderID}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(string bulkOrderID, BulkOrder bulkOrder)
        {
            try
            {
                _requestDirector.UpdateBulkOrder(bulkOrderID, bulkOrder);
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
