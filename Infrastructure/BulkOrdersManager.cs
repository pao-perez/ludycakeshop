using LudyCakeShop.Domain;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LudyCakeShop.Infrastructure
{
    public class BulkOrdersManager : IBulkOrdersManager
    {
        private readonly ISQLManager _sqlManager;
        public BulkOrdersManager(ISQLManager sqlManager)
        {
            this._sqlManager = sqlManager;
        }

        public IEnumerable<BulkOrder> GetBulkOrders()
        {
            List<StoredProcedureParameter> storedProcedureParameters = new();

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetBulkOrders",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(BulkOrder)
            };

            IEnumerable<BulkOrder> resultBulkOrders = _sqlManager.SelectAll<BulkOrder>(datasourceParameter);

            List<BulkOrder> bulkOrders = new();
            IEnumerable<OrderItem> bulkOrderItems = GetBulkOrderItems();
            foreach (BulkOrder bulkOrder in resultBulkOrders)
            {
                bulkOrder.OrderItems = bulkOrderItems.Where(bulkOrderItem => bulkOrderItem.OrderID == bulkOrder.OrderID);
                bulkOrders.Add(bulkOrder);
            }

            return bulkOrders;
        }

        private IEnumerable<OrderItem> GetBulkOrderItems()
        {
            List<StoredProcedureParameter> storedProcedureParameters = new();
            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetBulkOrderItems",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(OrderItem)
            };

            return _sqlManager.SelectAll<OrderItem>(datasourceParameter);
        }

        private IEnumerable<OrderItem> GetBulkOrderItems(string orderID)
        {
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetBulkOrderItemsByOrderID",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(OrderItem)
            };

            return _sqlManager.SelectAll<OrderItem>(datasourceParameter);
        }

        public BulkOrder GetBulkOrder(string orderID)
        {
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@OrderID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = orderID });

            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetBulkOrder",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(BulkOrder)
            };

            BulkOrder bulkOrder = _sqlManager.Select<BulkOrder>(datasourceParameter);
            if (bulkOrder != null)
            {
                bulkOrder.OrderItems = GetBulkOrderItems(bulkOrder.OrderID);
            }
            return bulkOrder;
        }

        public bool CreateBulkOrder(BulkOrder bulkOrder)
        {
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

            return _sqlManager.UpsertTransaction(datasourceParameters);
        }

        public bool UpdateBulkOrder(string orderID, BulkOrder bulkOrder)
        {
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

            return _sqlManager.UpsertTransaction(datasourceParameters);
        }
    }
}
