using LudyCakeShop.Domain;
using System.Collections.Generic;

namespace LudyCakeShop.Services
{
    public interface IBulkOrdersService
    {
        public string CreateBulkOrder(BulkOrder bulkOrder);
        public bool UpdateBulkOrder(string orderID, BulkOrder bulkOrder);
        public IEnumerable<BulkOrder> GetBulkOrders();
        public BulkOrder GetBulkOrder(string orderID);
    }
}
