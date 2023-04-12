using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudyCakeShop.Domain
{
    public class BulkOrder
    {
        public string BulkOrderID { get; set; }
        public int BulkOrderNumber { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime BulkOrderDate { get; set; }
        public string BulkOrderStatus { get; set; }
        public decimal GST { get; set; }
        public decimal SubTotal { get; set; }
        public decimal SaleTotal { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyContactNumber { get; set; }
        public string CompanyContactPerson { get; set; }
        public string Note { get; set; }
        public IEnumerable<BulkOrderItem> BulkOrderItems { get; set; }
    }
}
