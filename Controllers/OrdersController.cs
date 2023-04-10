using LudyCakeShop.Domain;
using LudyCakeShop.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
            IEnumerable<Order> orders;
            try
            {
                orders = _requestDirector.GetOrders();
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }

            IEnumerable<OrderDTO> ordersDTO = Mapper.MaptoDTOs(orders);
            return StatusCode(200, ordersDTO);
        }

        [HttpGet("{orderID}")]
        [Produces("application/json")]
        public IActionResult GetByID(string orderID)
        {
            Order order;
            try
            {
                order = _requestDirector.GetOrder(orderID);
            } catch(Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
            if (order == null)
            {
                return StatusCode(404, "OrderID not found.");
            }

            OrderDTO orderDTO = Mapper.MaptoDTO(order);
            return StatusCode(200, orderDTO);
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(OrderDTO orderDTO)
        {
            string orderID;
            try
            {
                orderID = _requestDirector.CreateOrder(Mapper.MaptoDomain(orderDTO));
            } catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }

            string path = HttpContext.Request.Path;
            string createdURI = path + "/" + orderID;
            return StatusCode(201, createdURI);
        }

        [HttpPut("{orderID}")]
        [Consumes("application/json")]
        public IActionResult Put(string orderID, OrderDTO ordersDTO)
        {
            try
            {
                _requestDirector.UpdateOrder(orderID, Mapper.MaptoDomain(ordersDTO));
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
