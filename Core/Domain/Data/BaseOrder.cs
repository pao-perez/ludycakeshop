using System;
using System.Collections.Generic;

namespace LudyCakeShop.Core.Domain.Data
{
    public abstract class BaseOrder
    {
        public string OrderID { get; set; }
        public int OrderNumber { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal GST { get; set; }
        public decimal SubTotal { get; set; }
        public decimal SaleTotal { get; set; }
        public string Note { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
