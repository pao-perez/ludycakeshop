using LudyCakeShop.Core.Domain.Data;
using LudyCakeShop.Core.Domain.Notification;
using LudyCakeShop.Core.Infrastructure;
using System;
using System.Collections.Generic;

namespace LudyCakeShop.Core.Services
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
            bulkOrder.BulkOrderStatus = BulkOrderStatus.SUBMITTED;
            bulkOrder.OrderID = Guid.NewGuid().ToString();
            _bulkOrdersManager.CreateBulkOrder(bulkOrder);
            _emailManager.SendEmail(new BulkOrderEmailMessage(bulkOrder));

            return bulkOrder.OrderID;
        }

        public bool UpdateBulkOrder(string orderID, BulkOrder bulkOrder)
        {
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
