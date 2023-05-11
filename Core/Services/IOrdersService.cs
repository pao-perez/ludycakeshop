using LudyCakeShop.Core.Domain.Data;
using System.Collections.Generic;

namespace LudyCakeShop.Core.Services
{
    public interface IOrdersService
    {
        public IEnumerable<Order> GetOrdersByCustomerContactNumber(string customerContactNumber);
        public string CreateOrder(Order order);
        public bool UpdateOrder(string orderID, Order order);
        public IEnumerable<Order> GetOrders();
        public Order GetOrder(string orderID);
    }
}
