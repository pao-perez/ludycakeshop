using LudyCakeShop.Domain;
using System.Collections.Generic;

namespace LudyCakeShop.Infrastructure
{
    public interface IBulkOrdersManager
    {
        public IEnumerable<BulkOrder> GetBulkOrders();
        public BulkOrder GetBulkOrder(string orderID);
        public bool CreateBulkOrder(BulkOrder bulkOrder);
        public bool UpdateBulkOrder(string orderID, BulkOrder bulkOrder);
    }
}
