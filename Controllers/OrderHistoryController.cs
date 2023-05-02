using LudyCakeShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LudyCakeShop.Controllers
{
    [Authorize]
    [Route("api/orders/history")]
    [ApiController]
    public class OrderHistoryController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrderHistoryController(IOrdersService ordersService)
        {
            this._ordersService = ordersService;
        }

        [HttpGet("{customerContactNumber}")]
        [Produces("application/json")]
        public IActionResult Get(string customerContactNumber)
        {
            try
            {
                return StatusCode(200, _ordersService.GetOrdersByCustomerContactNumber(customerContactNumber));
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
        }
    }
}
