using LudyCakeShop.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LudyCakeShop.Web.Controllers
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
            return StatusCode(200, _ordersService.GetOrdersByCustomerContactNumber(customerContactNumber));
        }
    }
}
