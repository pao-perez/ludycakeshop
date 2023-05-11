using LudyCakeShop.Core.Domain.Data;
using LudyCakeShop.Core.Infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LudyCakeShop.Infrastructure
{
    public class OrdersManager : IOrdersManager
    {
        private readonly ISQLManager _sqlManager;
        public OrdersManager(ISQLManager sqlManager)
        {
            this._sqlManager = sqlManager;
        }

        public IEnumerable<Order> GetOrders()
        {
            List<StoredProcedureParameter> storedProcedureParameters = new();

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetOrders",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(Order)
            };

            IEnumerable<Order> resultOrders = _sqlManager.SelectAll<Order>(datasourceParameter);

            List<Order> orders = new();
            IEnumerable<OrderItem> orderItems = GetOrderItems();
            foreach (Order order in resultOrders)
            {
                order.OrderItems = orderItems.Where(orderItem => orderItem.OrderID.Equals(order.OrderID));
                orders.Add(order);
            }

            return orders;
        }

        public IEnumerable<Order> GetOrdersByCustomerContactNumber(string customerContactNumber)
        {
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerContactNumber", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = customerContactNumber });

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetOrdersByCustomerContactNumber",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(Order)
            };

            IEnumerable<Order> resultOrders = _sqlManager.SelectAll<Order>(datasourceParameter);

            List<Order> orders = new();
            IEnumerable<OrderItem> orderItems = GetOrderItems();
            foreach (Order order in resultOrders)
            {
                order.OrderItems = orderItems.Where(orderItem => orderItem.OrderID.Equals(order.OrderID));
                orders.Add(order);
            }

            return orders;
        }

        private IEnumerable<OrderItem> GetOrderItems()
        {
            List<StoredProcedureParameter> storedProcedureParameters = new();
            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetOrderItems",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(OrderItem)
            };

            return _sqlManager.SelectAll<OrderItem>(datasourceParameter);
        }

        private IEnumerable<OrderItem> GetOrderItems(string orderID)
        {
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetOrderItemsByOrderID",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(OrderItem)
            };

            return _sqlManager.SelectAll<OrderItem>(datasourceParameter);
        }

        public Order GetOrder(string orderID)
        {
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetOrder",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(Order)
            };

            Order order = _sqlManager.Select<Order>(datasourceParameter);
            if (order != null)
            {
                order.OrderItems = GetOrderItems(order.OrderID);
            }
            return order;
        }

        public bool CreateOrder(Order order)
        {
            List<DatasourceParameter> datasourceParameters = new();

            List<StoredProcedureParameter> orderStoredProcedureParameters = new();
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.OrderID });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerName });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerAddress", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerAddress });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerEmail", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerEmail });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerContactNumber", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerContactNumber });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderStatus", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.OrderStatus });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@GST", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.GST });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SubTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.SubTotal });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SaleTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.SaleTotal });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@InvoiceNumber", ParameterSqlDbType = SqlDbType.Int, ParameterValue = order.InvoiceNumber });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@Note", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.Note });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "CreateOrder",
                StoredProcedureParameters = orderStoredProcedureParameters
            });

            foreach (OrderItem orderItem in order.OrderItems)
            {
                List<StoredProcedureParameter> orderItemStoredProcedureParameters = new();
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.OrderID });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderItem.ProductID });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ItemQuantity", ParameterSqlDbType = SqlDbType.Int, ParameterValue = orderItem.ItemQuantity });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ItemTotal", ParameterSqlDbType = SqlDbType.Decimal, ParameterValue = orderItem.ItemTotal });
                datasourceParameters.Add(new()
                {
                    StoredProcedure = "CreateOrderItem",
                    StoredProcedureParameters = orderItemStoredProcedureParameters
                });
            }

            return _sqlManager.UpsertTransaction(datasourceParameters);
        }

        public bool UpdateOrder(string orderID, Order order)
        {
            List<DatasourceParameter> datasourceParameters = new();

            List<StoredProcedureParameter> orderStoredProcedureParameters = new();
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerName });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerAddress", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerAddress });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerEmail", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerEmail });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerContactNumber", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerContactNumber });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderStatus", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.OrderStatus });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@GST", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.GST });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SubTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.SubTotal });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SaleTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.SaleTotal });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@InvoiceNumber", ParameterSqlDbType = SqlDbType.Int, ParameterValue = order.InvoiceNumber });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@Note", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.Note });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "UpdateOrder",
                StoredProcedureParameters = orderStoredProcedureParameters
            });

            foreach (OrderItem orderItem in order.OrderItems)
            {
                List<StoredProcedureParameter> orderItemStoredProcedureParameters = new();
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderItem.ProductID });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ItemQuantity", ParameterSqlDbType = SqlDbType.Int, ParameterValue = orderItem.ItemQuantity });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ItemTotal", ParameterSqlDbType = SqlDbType.Decimal, ParameterValue = orderItem.ItemTotal });
                datasourceParameters.Add(new()
                {
                    StoredProcedure = "UpdateOrderItem",
                    StoredProcedureParameters = orderItemStoredProcedureParameters
                });
            }

            return _sqlManager.UpsertTransaction(datasourceParameters);
        }
    }
}
