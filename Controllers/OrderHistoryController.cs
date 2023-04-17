using LudyCakeShop.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LudyCakeShop.Controllers
{
    [Route("api/orders/history")]
    [ApiController]
    public class OrderHistoryController : ControllerBase
    {
        private readonly LCS _requestDirector;

        public OrderHistoryController() =>
            _requestDirector = new();

        [HttpGet("{customerContactNumber}")]
        [Produces("application/json")]
        public IActionResult Get(string customerContactNumber)
        {
            try
            {
                return StatusCode(200, _requestDirector.GetOrdersByCustomerContactNumber(customerContactNumber));
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
        }
    }
}
