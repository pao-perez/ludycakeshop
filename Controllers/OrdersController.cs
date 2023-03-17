using LudyCakeShop.Domain;
using LudyCakeShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LudyCakeShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly LCS _requestDirector;

        public OrdersController() =>
            _requestDirector = new();

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            IEnumerable<OrderDTO> ordersDTO = Mapper.MaptoDTOs(_requestDirector.GetOrders());
            
            return Ok(ordersDTO);
        }

        [HttpGet("{orderNumber}")]
        [Produces("application/json")]
        public IActionResult GetByID(int orderNumber)
        {
            OrderDTO orderDTO = Mapper.MaptoDTO(_requestDirector.GetOrder(orderNumber));

            return Ok(orderDTO);
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(OrderDTO orderDTO)
        {
            Order order = Mapper.MaptoDomain(orderDTO);

            // TODO: Change Ok to Created
            return Ok(_requestDirector.CreateOrder(order));
        }

        [HttpPut("{orderNumber}")]
        [Consumes("application/json")]
        public IActionResult Put(int orderNumber, OrderDTO ordersDTO)
        {
            Order order = Mapper.MaptoDomain(ordersDTO);

            return Ok(_requestDirector.UpdateOrder(orderNumber, order));
        }
    }
}
