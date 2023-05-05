using LudyCakeShop.Domain;
using LudyCakeShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LudyCakeShop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this._ordersService = ordersService;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            return StatusCode(200, _ordersService.GetOrders());
        }

        [HttpGet("{orderID}")]
        [Produces("application/json")]
        public IActionResult GetByID(string orderID)
        {

            Order order = _ordersService.GetOrder(orderID);
            if (order == null)
            {
                return StatusCode(404, "OrderID not found.");
            }

            return StatusCode(200, order);
        }

        [AllowAnonymous]
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(Order order)
        {

            string orderID = _ordersService.CreateOrder(order);
            string path = HttpContext.Request.Path;
            string createdURI = path + "/" + orderID;
            return StatusCode(201, createdURI);
        }

        [HttpPut("{orderID}")]
        [Consumes("application/json")]
        public IActionResult Put(string orderID, Order orders)
        {
            _ordersService.UpdateOrder(orderID, orders);
            return StatusCode(204);
        }
    }
}
