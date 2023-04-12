using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudyCakeShop.Controllers
{
    public class BulkOrderItemDTO
    {
        public string BulkOrderID { get; set; }
        public string ProductID { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemTotal { get; set; }
    }
}
