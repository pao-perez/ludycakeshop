using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudyCakeShop.Controllers
{
    public class OrderDTO
    {
        public string note { get; set; }
        public int _id { get; set; }
        public string customerName { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public DateTime orderDate { get; set; }
        public decimal orderTotal { get; set; }
        public string status { get; set; }
    }
}
