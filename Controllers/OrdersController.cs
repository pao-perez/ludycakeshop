using LudyCakeShop.Domain;
using LudyCakeShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudyCakeShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly LCS _requestDirector;

        public OrdersController()
        {
            _requestDirector = new();
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            return Ok(_requestDirector.GetOrders());
        }

        [HttpGet("{_id}")]
        [Produces("application/json")]
        public IActionResult GetByID(int _id)
        {
            return Ok(_requestDirector.GetOrder(_id));
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(OrderDTO orderDTO)
        {
            // TODO: Change Ok to Created
            Order order = new();
            order.Description = orderDTO.note;
            order.Address = orderDTO.address;
            order.CustomerName = orderDTO.customerName;
            order.Email = orderDTO.email;
            order.Phone = orderDTO.phone;
            order.OrderDate = orderDTO.orderDate;
            order.OrderTotal = orderDTO.orderTotal;
            order.OrderStatus = orderDTO.status;

            return Ok(_requestDirector.AddOrder(order));
        }

        [HttpPut]
        [Consumes("application/json")]
        public IActionResult Put(OrderDTO orderDTO)
        {
            Order order = new();
            order.OrderNumber = orderDTO._id;
            order.Address = orderDTO.address;
            order.CustomerName = orderDTO.customerName;
            order.Email = orderDTO.email;
            order.Phone = orderDTO.phone;
            order.OrderDate = orderDTO.orderDate;
            order.OrderTotal = orderDTO.orderTotal;
            order.Description = orderDTO.note;
            order.OrderStatus = orderDTO.status;

            return Ok(_requestDirector.UpdateOrder(order));
        }
    }
}
