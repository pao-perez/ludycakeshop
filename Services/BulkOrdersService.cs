using LudyCakeShop.Domain;
using LudyCakeShop.Infrastructure;
using System;
using System.Collections.Generic;

namespace LudyCakeShop.Services
{
    public class BulkOrdersService : IBulkOrdersService
    {
        private readonly IBulkOrdersManager _bulkOrdersManager;
        private readonly IEmailManager _emailManager;

        public BulkOrdersService(IBulkOrdersManager bulkOrdersManager, IEmailManager emailManager)
        {
            this._bulkOrdersManager = bulkOrdersManager;
            this._emailManager = emailManager;
        }

        public string CreateBulkOrder(BulkOrder bulkOrder)
        {
            // TODO: check product quantity count if enough for each orderItem ItemQuantity
            //TODO: compute subTotal, saleTotal, and GST
            bulkOrder.BulkOrderStatus = BulkOrderStatus.SUBMITTED;
            bulkOrder.OrderID = Guid.NewGuid().ToString();
            _bulkOrdersManager.CreateBulkOrder(bulkOrder);
            _emailManager.SendEmail(new BulkOrderEmailMessage(bulkOrder));

            return bulkOrder.OrderID;
        }

        public bool UpdateBulkOrder(string orderID, BulkOrder bulkOrder)
        {
            // TODO: check product quantity count if enough for each orderItem ItemQuantity
            return _bulkOrdersManager.UpdateBulkOrder(orderID, bulkOrder);
        }

        public IEnumerable<BulkOrder> GetBulkOrders()
        {
            return (List<BulkOrder>)_bulkOrdersManager.GetBulkOrders();
        }

        public BulkOrder GetBulkOrder(string orderID)
        {
            return _bulkOrdersManager.GetBulkOrder(orderID);
        }
    }
}
