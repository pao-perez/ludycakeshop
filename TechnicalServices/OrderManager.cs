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
            IEnumerable<OrderItem> orderItems = GetOrderItems();
            foreach (Order order in resultOrders)
            {
                order.OrderItems = orderItems.Where(orderItem => orderItem.OrderID == order.OrderID);
                orders.Add(order);
            }

            return orders;
        }

        public IEnumerable<Order> GetOrdersByCustomerContactNumber(string customerContactNumber)
        {
            SQLManager sqlManager = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerContactNumber", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = customerContactNumber });

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetOrdersByCustomerContactNumber",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(Order)
            };

            IEnumerable<Order> resultOrders = sqlManager.SelectAll<Order>(datasourceParameter);

            List<Order> orders = new();
            IEnumerable<OrderItem> orderItems = GetOrderItems();
            foreach (Order order in resultOrders)
            {
                order.OrderItems = orderItems.Where(orderItem => orderItem.OrderID == order.OrderID);
                orders.Add(order);
            }

            return orders;
        }

        private static IEnumerable<OrderItem> GetOrderItems()
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

        private static IEnumerable<OrderItem> GetOrderItems(string orderID)
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
            if (order != null)
            {
                order.OrderItems = GetOrderItems(order.OrderID);
            }
            return order;
        }

        public bool CreateOrder(Order order)
        {
            SQLManager sqlManager = new();
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

            return sqlManager.UpsertTransaction(datasourceParameters);
        }

        public bool UpdateOrder(string orderID, Order order)
        {
            SQLManager sqlManager = new();
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

            return sqlManager.UpsertTransaction(datasourceParameters);
        }


        public IEnumerable<BulkOrder> GetBulkOrders()
        {
            SQLManager sqlManager = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetBulkOrders",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(BulkOrder)
            };

            IEnumerable<BulkOrder> resultBulkOrders = sqlManager.SelectAll<BulkOrder>(datasourceParameter);

            List<BulkOrder> bulkOrders = new();
            IEnumerable<OrderItem> bulkOrderItems = GetBulkOrderItems();
            foreach (BulkOrder bulkOrder in resultBulkOrders)
            {
                bulkOrder.OrderItems = bulkOrderItems.Where(bulkOrderItem => bulkOrderItem.OrderID == bulkOrder.OrderID);
                bulkOrders.Add(bulkOrder);
            }

            return bulkOrders;
        }

        private static IEnumerable<OrderItem> GetBulkOrderItems()
        {
            SQLManager sqlManager = new();

            List<StoredProcedureParameter> storedProcedureParameters = new();
            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetBulkOrderItems",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(OrderItem)
            };

            return sqlManager.SelectAll<OrderItem>(datasourceParameter);
        }

        private static IEnumerable<OrderItem> GetBulkOrderItems(string orderID)
        {
            SQLManager sqlManager = new();

            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetBulkOrderItemsByOrderID",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(OrderItem)
            };

            return sqlManager.SelectAll<OrderItem>(datasourceParameter);
        }

        public BulkOrder GetBulkOrder(string orderID)
        {
            SQLManager sqlManager = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetBulkOrder",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(BulkOrder)
            };

            BulkOrder bulkOrder = sqlManager.Select<BulkOrder>(datasourceParameter);
            if (bulkOrder != null)
            {
                bulkOrder.OrderItems = GetBulkOrderItems(bulkOrder.OrderID);
            }
            return bulkOrder;
        }

        public bool CreateBulkOrder(BulkOrder bulkOrder)
        {
            SQLManager sqlManager = new();
            List<DatasourceParameter> datasourceParameters = new();

            List<StoredProcedureParameter> orderStoredProcedureParameters = new();
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.OrderID });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyName });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyAddress", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyAddress });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyEmail", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyEmail });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyContactNumber", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyContactNumber });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyContactPerson", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyContactPerson });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@BulkOrderStatus", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.BulkOrderStatus });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@GST", ParameterSqlDbType = SqlDbType.Money, ParameterValue = bulkOrder.GST });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SubTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = bulkOrder.SubTotal });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SaleTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = bulkOrder.SaleTotal });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@InvoiceNumber", ParameterSqlDbType = SqlDbType.Int, ParameterValue = bulkOrder.InvoiceNumber });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@Note", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.Note });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "CreateBulkOrder",
                StoredProcedureParameters = orderStoredProcedureParameters
            });

            foreach (OrderItem bulkOrderItem in bulkOrder.OrderItems)
            {
                List<StoredProcedureParameter> orderItemStoredProcedureParameters = new();
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.OrderID });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrderItem.ProductID });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ItemQuantity", ParameterSqlDbType = SqlDbType.Int, ParameterValue = bulkOrderItem.ItemQuantity });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ItemTotal", ParameterSqlDbType = SqlDbType.Decimal, ParameterValue = bulkOrderItem.ItemTotal });
                datasourceParameters.Add(new()
                {
                    StoredProcedure = "CreateBulkOrderItem",
                    StoredProcedureParameters = orderItemStoredProcedureParameters
                });
            }

            return sqlManager.UpsertTransaction(datasourceParameters);
        }

        public bool UpdateBulkOrder(string orderID, BulkOrder bulkOrder)
        {
            SQLManager sqlManager = new();
            List<DatasourceParameter> datasourceParameters = new();

            List<StoredProcedureParameter> orderStoredProcedureParameters = new();
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyName });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyAddress", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyAddress });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyEmail", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyEmail });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyContactNumber", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyContactNumber });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyContactPerson", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyContactPerson });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@BulkOrderStatus", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.BulkOrderStatus });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@GST", ParameterSqlDbType = SqlDbType.Money, ParameterValue = bulkOrder.GST });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SubTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = bulkOrder.SubTotal });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SaleTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = bulkOrder.SaleTotal });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@InvoiceNumber", ParameterSqlDbType = SqlDbType.Int, ParameterValue = bulkOrder.InvoiceNumber });
            orderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@Note", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.Note });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "UpdateBulkOrder",
                StoredProcedureParameters = orderStoredProcedureParameters
            });

            foreach (OrderItem bulkOrderItem in bulkOrder.OrderItems)
            {
                List<StoredProcedureParameter> orderItemStoredProcedureParameters = new();
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrderItem.ProductID });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ItemQuantity", ParameterSqlDbType = SqlDbType.Int, ParameterValue = bulkOrderItem.ItemQuantity });
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ItemTotal", ParameterSqlDbType = SqlDbType.Decimal, ParameterValue = bulkOrderItem.ItemTotal });
                datasourceParameters.Add(new()
                {
                    StoredProcedure = "UpdateBulkOrderItem",
                    StoredProcedureParameters = orderItemStoredProcedureParameters
                });
            }

            return sqlManager.UpsertTransaction(datasourceParameters);
        }
    }
}
