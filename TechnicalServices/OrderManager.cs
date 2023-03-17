using LudyCakeShop.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LudyCakeShop.TechnicalServices
{
    public class OrderManager
    {
        public IEnumerable<Order> GetOrders()
        {
            SQLManager sqlManager = new();

            List<SqlParameter> sqlParameters = new();

            return sqlManager.SelectAll<Order>("GetOrders", sqlParameters, typeof(Order));
        }

        public Order GetOrder(int orderNumber)
        {
            SQLManager sqlManager = new();

            List<SqlParameter> sqlParameters = new();
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@OrderNumber", SqlDbType.Int, orderNumber));

            return sqlManager.Select<Order>("GetOrder", sqlParameters, typeof(Order));
        }

        public bool CreateOrder(Order order)
        {
            SQLManager sqlManager = new();

            List<SqlParameter> sqlParameters = new();

            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@CustomerName", SqlDbType.VarChar, order.CustomerName));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@CustomerAddress", SqlDbType.VarChar, order.CustomerAddress));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@CustomerEmail", SqlDbType.VarChar, order.CustomerEmail));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@CustomerContactNumber", SqlDbType.VarChar, order.CustomerContactNumber));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@OrderStatus", SqlDbType.VarChar, order.OrderStatus));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@GST", SqlDbType.Money, order.GST));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@SubTotal", SqlDbType.Money, order.SubTotal));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@SaleTotal", SqlDbType.Money, order.SaleTotal));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@InvoiceNumber", SqlDbType.Int, order.InvoiceNumber));

            return sqlManager.Upsert("CreateOrder", sqlParameters);
        }

        public bool UpdateOrder(int orderNumber, Order order)
        {
            SQLManager sqlManager = new();

            List<SqlParameter> sqlParameters = new();

            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@OrderNumber", SqlDbType.Int, orderNumber));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@CustomerName", SqlDbType.VarChar, order.CustomerName));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@CustomerAddress", SqlDbType.VarChar, order.CustomerAddress));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@CustomerEmail", SqlDbType.VarChar, order.CustomerEmail));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@CustomerContactNumber", SqlDbType.VarChar, order.CustomerContactNumber));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@OrderStatus", SqlDbType.VarChar, order.OrderStatus));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@GST", SqlDbType.Money, order.GST));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@SubTotal", SqlDbType.Money, order.SubTotal));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@SaleTotal", SqlDbType.Money, order.SaleTotal));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@InvoiceNumber", SqlDbType.Int, order.InvoiceNumber));

            return sqlManager.Upsert("UpdateOrder", sqlParameters);
        }
    }
}
