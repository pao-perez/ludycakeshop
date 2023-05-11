using LudyCakeShop.Core.Domain.Data;
using System.Collections.Generic;

namespace LudyCakeShop.Core.Services
{
    public interface IBulkOrdersService
    {
        public string CreateBulkOrder(BulkOrder bulkOrder);
        public bool UpdateBulkOrder(string orderID, BulkOrder bulkOrder);
        public IEnumerable<BulkOrder> GetBulkOrders();
        public BulkOrder GetBulkOrder(string orderID);
    }
}
