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
            // TODO: check product quantity count if enough for each orderItem ItemQuantity
            //TODO: compute subTotal, saleTotal, and GST
            order.OrderStatus = OrderStatus.SUBMITTED;
            order.OrderID = Guid.NewGuid().ToString();
            ordersManager.CreateOrder(order);
            _emailManager.SendEmail(new OrderEmailMessage(order));

            return order.OrderID;
        }

        public bool UpdateOrder(string orderID, Order order)
        {
            // TODO: check product quantity count if enough for each orderItem ItemQuantity
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
