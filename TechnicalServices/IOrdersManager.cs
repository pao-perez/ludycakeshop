﻿using LudyCakeShop.Domain;
using System.Collections.Generic;

namespace LudyCakeShop.TechnicalServices
{
    public interface IOrdersManager
    {
        public IEnumerable<Order> GetOrders();
        public IEnumerable<Order> GetOrdersByCustomerContactNumber(string customerContactNumber);
        public Order GetOrder(string orderID);
        public bool CreateOrder(Order order);
        public bool UpdateOrder(string orderID, Order order);
    }
}
