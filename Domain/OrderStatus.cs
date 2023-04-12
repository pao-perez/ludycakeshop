using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudyCakeShop.Domain
{
    public class OrderStatus
    {
        public const string SUBMITTED = "Submitted";
        public const string COMPLETED = "Completed";
        public const string FOR_PICKUP = "For-Pickup";
        public const string PREPARING = "Preparing";
    }
}
