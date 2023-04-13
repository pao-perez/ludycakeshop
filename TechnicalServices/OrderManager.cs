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

        public IEnumerable<Order> GetOrdersByCustomerEmail(string customerEmail)
        {
            SQLManager sqlManager = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerEmail", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = customerEmail });

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetOrdersByCustomerEmail",
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

            List<StoredProcedureParameter> createOrderStoredProcedureParameters = new();
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.OrderID });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerName });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerAddress", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerAddress });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerEmail", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerEmail });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CustomerContactNumber", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.CustomerContactNumber });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderStatus", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = order.OrderStatus });
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

            foreach (OrderItem orderItem in order.OrderItems)
            {
                // TODO: check product quantity count if enough for order ItemQuantity
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

            foreach (OrderItem orderItem in order.OrderItems)
            {
                // TODO: check product quantity count if enough for order ItemQuantity
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
            IEnumerable<BulkOrderItem> bulkOrderItems = GetBulkOrderItems(); // get all bulk order Items from DB
            foreach (BulkOrder bulkOrder in resultBulkOrders)
            {
                bulkOrder.BulkOrderItems = bulkOrderItems.Where(bulkOrderItem => bulkOrderItem.BulkOrderID == bulkOrder.BulkOrderID);
                bulkOrders.Add(bulkOrder);
            }

            return bulkOrders;
        }

        public IEnumerable<BulkOrderItem> GetBulkOrderItems()
        {
            SQLManager sqlManager = new();

            List<StoredProcedureParameter> storedProcedureParameters = new();
            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetBulkOrderItems",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(BulkOrderItem)
            };

            return sqlManager.SelectAll<BulkOrderItem>(datasourceParameter);
        }

        public IEnumerable<BulkOrderItem> GetBulkOrderItems(string bulkOrderID)
        {
            SQLManager sqlManager = new();

            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@BulkOrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrderID });

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetBulkOrderItemsByBulkOrderID",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(BulkOrderItem)
            };

            return sqlManager.SelectAll<BulkOrderItem>(datasourceParameter);
        }

        public BulkOrder GetBulkOrder(string bulkOrderID)
        {
            SQLManager sqlManager = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@BulkOrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrderID });

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetBulkOrder",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(BulkOrder)
            };

            BulkOrder bulkOrder = sqlManager.Select<BulkOrder>(datasourceParameter);
            if (bulkOrder != null)
            {
                bulkOrder.BulkOrderItems = GetBulkOrderItems(bulkOrder.BulkOrderID);
            }
            return bulkOrder;
        }

        public bool CreateBulkOrder(BulkOrder bulkOrder)
        {
            SQLManager sqlManager = new();
            List<DatasourceParameter> datasourceParameters = new();

            List<StoredProcedureParameter> createOrderStoredProcedureParameters = new();
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@BulkOrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.BulkOrderID });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyName });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyAddress", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyAddress });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyEmail", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyEmail });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyContactNumber", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyContactNumber });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyContactPerson", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyContactPerson });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@BulkOrderStatus", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.BulkOrderStatus });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@GST", ParameterSqlDbType = SqlDbType.Money, ParameterValue = bulkOrder.GST });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SubTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = bulkOrder.SubTotal });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SaleTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = bulkOrder.SaleTotal });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@InvoiceNumber", ParameterSqlDbType = SqlDbType.Int, ParameterValue = bulkOrder.InvoiceNumber });
            createOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@Note", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.Note });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "CreateBulkOrder",
                StoredProcedureParameters = createOrderStoredProcedureParameters
            });

            foreach (BulkOrderItem bulkOrderItem in bulkOrder.BulkOrderItems)
            {
                // TODO: check product quantity count if enough for order ItemQuantity
                List<StoredProcedureParameter> orderItemStoredProcedureParameters = new();
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@BulkOrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.BulkOrderID });
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

        public bool UpdateBulkOrder(string bulkOrderID, BulkOrder bulkOrder)
        {
            SQLManager sqlManager = new();
            List<DatasourceParameter> datasourceParameters = new();

            List<StoredProcedureParameter> updateOrderStoredProcedureParameters = new();
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@BulkOrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrderID });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyName });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyAddress", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyAddress });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyEmail", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyEmail });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyContactNumber", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyContactNumber });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CompanyContactPerson", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.CompanyContactPerson });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@BulkOrderStatus", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.BulkOrderStatus });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@GST", ParameterSqlDbType = SqlDbType.Money, ParameterValue = bulkOrder.GST });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SubTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = bulkOrder.SubTotal });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@SaleTotal", ParameterSqlDbType = SqlDbType.Money, ParameterValue = bulkOrder.SaleTotal });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@InvoiceNumber", ParameterSqlDbType = SqlDbType.Int, ParameterValue = bulkOrder.InvoiceNumber });
            updateOrderStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@Note", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrder.Note });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "UpdateBulkOrder",
                StoredProcedureParameters = updateOrderStoredProcedureParameters
            });

            foreach (BulkOrderItem bulkOrderItem in bulkOrder.BulkOrderItems)
            {
                // TODO: check product quantity count if enough for order ItemQuantity
                List<StoredProcedureParameter> orderItemStoredProcedureParameters = new();
                orderItemStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@BulkOrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = bulkOrderID });
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
