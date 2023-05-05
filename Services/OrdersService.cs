using LudyCakeShop.Domain;
using LudyCakeShop.Infrastructure;
using System;
using System.Collections.Generic;

namespace LudyCakeShop.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersManager ordersManager;
        private readonly IEmailManager _emailManager;

        public OrdersService(IOrdersManager ordersManager, IEmailManager emailManager)
        {
            this.ordersManager = ordersManager;
            this._emailManager = emailManager;
        }

        public IEnumerable<Order> GetOrdersByCustomerContactNumber(string customerContactNumber)
        {
            return ordersManager.GetOrdersByCustomerContactNumber(customerContactNumber);
        }

        public string CreateOrder(Order order)
        {
            order.OrderStatus = OrderStatus.SUBMITTED;
            order.OrderID = Guid.NewGuid().ToString();
            ordersManager.CreateOrder(order);
            _emailManager.SendEmail(new OrderEmailMessage(order));

            return order.OrderID;
        }

        public bool UpdateOrder(string orderID, Order order)
        {
            return ordersManager.UpdateOrder(orderID, order);
        }

        public IEnumerable<Order> GetOrders()
        {
            return (List<Order>)ordersManager.GetOrders();
        }

        public Order GetOrder(string orderID)
        {
            return ordersManager.GetOrder(orderID);
        }
    }
}
