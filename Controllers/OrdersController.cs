using LudyCakeShop.Domain;
using LudyCakeShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LudyCakeShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly LCS _requestDirector;

        public OrdersController() =>
            _requestDirector = new();

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            try
            {
                return StatusCode(200, _requestDirector.GetOrders());
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
        }

        [HttpGet("{orderID}")]
        [Produces("application/json")]
        public IActionResult GetByID(string orderID)
        {
            try
            {
                Order order = _requestDirector.GetOrder(orderID);
                if (order == null)
                {
                    return StatusCode(404, "OrderID not found.");
                }

                return StatusCode(200, order);
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(Order order)
        {
            try
            {
                string orderID = _requestDirector.CreateOrder(order);
                string path = HttpContext.Request.Path;
                string createdURI = path + "/" + orderID;
                return StatusCode(201, createdURI);
            } catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
        }

        [HttpPut("{orderID}")]
        [Consumes("application/json")]
        public IActionResult Put(string orderID, Order orders)
        {
            try
            {
                _requestDirector.UpdateOrder(orderID, orders);
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
