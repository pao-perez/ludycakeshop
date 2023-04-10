using LudyCakeShop.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LudyCakeShop.TechnicalServices
{
    public class OrderManager
    {
        public IEnumerable<Order> GetOrders()
        {
            SQLManager sqlManager = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetOrders",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(Order)
            };

            IEnumerable<Order> resultOrders = sqlManager.SelectAll<Order>(datasourceParameter);

            List<Order> orders = new();
            IEnumerable<OrderItem> orderItems = GetOrderItems(); // get all order Items from DB
            foreach (Order order in resultOrders)
            {
                order.OrderItems = orderItems.Where(orderItem => orderItem.OrderID == order.OrderID);
                orders.Add(order);
            }

            return orders;
        }

        public IEnumerable<OrderItem> GetOrderItems()
        {
            SQLManager sqlManager = new();

            List<StoredProcedureParameter> storedProcedureParameters = new();
            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetOrderItems",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(OrderItem)
            };

            return sqlManager.SelectAll<OrderItem>(datasourceParameter);
        }

        public IEnumerable<OrderItem> GetOrderItems(string orderID)
        {
            SQLManager sqlManager = new();

            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetOrderItemsByOrderID",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(OrderItem)
            };

            return sqlManager.SelectAll<OrderItem>(datasourceParameter);
        }

        public Order GetOrder(string orderID)
        {
            SQLManager sqlManager = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetOrder",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(Order)
            };

            Order order = sqlManager.Select<Order>(datasourceParameter);
            order.OrderItems = GetOrderItems(order.OrderID);
            return order;
        }

        public string CreateOrder(Order order)
        {
            SQLManager sqlManager = new();
            List<DatasourceParameter> datasourceParameters = new();

            var orderID = Guid.NewGuid().ToString();
            List<StoredProcedureParameter> createOrderStoredProcedureParameters = new();
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerName });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerAddress", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerAddress });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerEmail", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerEmail });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerContactNumber", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerContactNumber });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@GST", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.GST });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SubTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.SubTotal });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SaleTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.SaleTotal });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@InvoiceNumber", ParameterSqlDbType = SqlDbType.Int, ParameterValue = order.InvoiceNumber });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@Note", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.Note });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "CreateOrder",
                StoredProcedureParameters = createOrderStoredProcedureParameters
            });

            // create order items
            foreach (OrderItem orderItem in order.OrderItems)
            {
                // TODO: check product quantity count if enough for order ItemQuantity
                List<StoredProcedureParameter> orderItemStoredProcedureParameters = new();
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductID", ParameterSqlDbType = SqlDbType.Int, ParameterValue = orderItem.ProductID });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ItemQuantity", ParameterSqlDbType = SqlDbType.Int, ParameterValue = orderItem.ItemQuantity });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ItemTotal", ParameterSqlDbType = SqlDbType.Decimal, ParameterValue = orderItem.ItemTotal });
                datasourceParameters.Add(new()
                {
                    StoredProcedure = "CreateOrderItem",
                    StoredProcedureParameters = orderItemStoredProcedureParameters
                });
            }

            bool success = sqlManager.UpsertTransaction(datasourceParameters);

            return success ? orderID : null;
        }

        public bool UpdateOrder(string orderID, Order order)
        {
            SQLManager sqlManager = new();
            List<DatasourceParameter> datasourceParameters = new();

            // update order
            List<StoredProcedureParameter> updateOrderStoredProcedureParameters = new();
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerName });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerAddress", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerAddress });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerEmail", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerEmail });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerContactNumber", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerContactNumber });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderStatus", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.OrderStatus });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@GST", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.GST });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SubTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.SubTotal });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SaleTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.SaleTotal });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@InvoiceNumber", ParameterSqlDbType = SqlDbType.Int, ParameterValue = order.InvoiceNumber });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@Note", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.Note });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "UpdateOrder",
                StoredProcedureParameters = updateOrderStoredProcedureParameters
            });

            // update order items
            foreach (OrderItem orderItem in order.OrderItems)
            {
                // TODO: check product quantity count if enough for order ItemQuantity
                List<StoredProcedureParameter> orderItemStoredProcedureParameters = new();
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductID", ParameterSqlDbType = SqlDbType.Int, ParameterValue = orderItem.ProductID });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ItemQuantity", ParameterSqlDbType = SqlDbType.Int, ParameterValue = orderItem.ItemQuantity });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ItemTotal", ParameterSqlDbType = SqlDbType.Decimal, ParameterValue = orderItem.ItemTotal });
                datasourceParameters.Add(new()
                {
                    StoredProcedure = "UpdateOrderItem",
                    StoredProcedureParameters = orderItemStoredProcedureParameters
                });
            }

            return sqlManager.UpsertTransaction(datasourceParameters);
        }
    }
}
