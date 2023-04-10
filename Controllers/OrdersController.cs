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

        [HttpGet("{orderID}")]
        [Produces("application/json")]
        public IActionResult GetByID(string orderID)
        {
            OrderDTO orderDTO = Mapper.MaptoDTO(_requestDirector.GetOrder(orderID));

            return Ok(orderDTO);
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(OrderDTO orderDTO)
        {
            Order order = Mapper.MaptoDomain(orderDTO);
            string orderID = _requestDirector.CreateOrder(order);
            string path = HttpContext.Request.Path;
            string createdURI = path + "/" + orderID;

            return Created(createdURI, orderID);
        }

        [HttpPut("{orderID}")]
        [Consumes("application/json")]
        public IActionResult Put(string orderID, OrderDTO ordersDTO)
        {
            Order order = Mapper.MaptoDomain(ordersDTO);
            _requestDirector.UpdateOrder(orderID, order);

            return NoContent();
        }
    }
}
