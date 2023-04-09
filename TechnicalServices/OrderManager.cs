using LudyCakeShop.Domain;
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

            //Dictionary<int, List<OrderItem>> orderItemsByOrderNumber = ((List<OrderItem>)GetOrderItems()).ToDictionary(orderItem => orderItem.OrderNumber, orderItem => new List<OrderItem>());
            List<Order> orders = new();
            IEnumerable<OrderItem> orderItems = GetOrderItems(); // get all order Items from DB
            foreach (Order order in resultOrders)
            {
                order.OrderItems = orderItems.Where(order => order.OrderNumber == order.OrderNumber);
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

        public IEnumerable<OrderItem> GetOrderItems(int orderNumber)
        {
            SQLManager sqlManager = new();

            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderNumber", ParameterSqlDbType = SqlDbType.Int, ParameterValue = orderNumber });

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetOrderItems",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(OrderItem)
            };

            return sqlManager.SelectAll<OrderItem>(datasourceParameter);
        }

        public Order GetOrder(int orderNumber)
        {
            SQLManager sqlManager = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderNumber", ParameterSqlDbType = SqlDbType.Int, ParameterValue = orderNumber });

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetOrder",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(Order)
            };

            Order order = sqlManager.Select<Order>(datasourceParameter);
            order.OrderItems = GetOrderItems(order.OrderNumber);
            return order;
        }

        public bool CreateOrder(Order order)
        {
            SQLManager sqlManager = new();
            List<DatasourceParameter> datasourceParameters = new();

            List<StoredProcedureParameter> createOrderStoredProcedureParameters = new();
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerName });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerAddress", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerAddress });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerEmail", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerEmail });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerContactNumber", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerContactNumber });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderStatus", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.OrderStatus });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@GST", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.GST });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SubTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.SubTotal });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SaleTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.SaleTotal });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@InvoiceNumber", ParameterSqlDbType = SqlDbType.Int, ParameterValue = order.InvoiceNumber });
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
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderNumber", ParameterSqlDbType = SqlDbType.Int, ParameterValue = orderItem.OrderNumber });
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

        public bool UpdateOrder(int orderNumber, Order order)
        {
            SQLManager sqlManager = new();
            List<DatasourceParameter> datasourceParameters = new();

            // update order
            List<StoredProcedureParameter> updateOrderStoredProcedureParameters = new();
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderNumber", ParameterSqlDbType = SqlDbType.Int, ParameterValue = orderNumber });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerName });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerAddress", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerAddress });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerEmail", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerEmail });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerContactNumber", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerContactNumber });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderStatus", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.OrderStatus });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@GST", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.GST });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SubTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.SubTotal });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SaleTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = order.SaleTotal });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@InvoiceNumber", ParameterSqlDbType = SqlDbType.Int, ParameterValue = order.InvoiceNumber });
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
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderNumber", ParameterSqlDbType = SqlDbType.Int, ParameterValue = orderItem.OrderNumber });
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
