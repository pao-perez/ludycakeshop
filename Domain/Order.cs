using System;
using System.Collections.Generic;

namespace LudyCakeShop.Domain
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public decimal GST { get; set; }
        public decimal SubTotal { get; set; }
        public decimal SaleTotal { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContactNumber { get; set; }
        public string Note { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
