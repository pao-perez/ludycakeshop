using LudyCakeShop.Domain;
using LudyCakeShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LudyCakeShop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BulkOrdersController : ControllerBase
    {
        private readonly IBulkOrdersService _bulkOrdersService;

        public BulkOrdersController(IBulkOrdersService bulkOrdersService)
        {
            this._bulkOrdersService = bulkOrdersService;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            return StatusCode(200, _bulkOrdersService.GetBulkOrders());
        }

        [HttpGet("{orderID}")]
        [Produces("application/json")]
        public IActionResult GetByID(string orderID)
        {
            BulkOrder bulkOrder = _bulkOrdersService.GetBulkOrder(orderID);
            if (bulkOrder == null)
            {
                return StatusCode(404, "OrderID not found.");
            }

            return StatusCode(200, bulkOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(BulkOrder bulkOrder)
        {
            string orderID = _bulkOrdersService.CreateBulkOrder(bulkOrder);
            string path = HttpContext.Request.Path;
            string createdURI = path + "/" + orderID;
            return StatusCode(201, createdURI);
        }

        [HttpPut("{orderID}")]
        [Consumes("application/json")]
        public IActionResult Put(string orderID, BulkOrder bulkOrder)
        {
            _bulkOrdersService.UpdateBulkOrder(orderID, bulkOrder);
            return StatusCode(204);
        }
    }
}
