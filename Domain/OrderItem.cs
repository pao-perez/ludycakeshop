using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudyCakeShop.Domain
{
    public class OrderItem
    {
        public int OrderNumber { get; set; }
        public int ProductID { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemTotal { get; set; }
    }
}
