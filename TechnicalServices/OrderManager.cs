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

        public Order GetOrder(int id)
        {
            SQLManager sqlManager = new();

            List<SqlParameter> sqlParameters = new();
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@OrderNumber", SqlDbType.Int, id));

            return sqlManager.Select<Order>("GetOrdersByOrderNumber", sqlParameters, typeof(Order));
        }

        public bool CreateOrder(Order order)
        {
            SQLManager sqlManager = new();

            List<SqlParameter> sqlParameters = new();

            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@CustomerName", SqlDbType.VarChar, order.CustomerName));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@Address", SqlDbType.VarChar, order.Address));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@Email", SqlDbType.VarChar, order.Email));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@Phone", SqlDbType.VarChar, order.Phone));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@Description", SqlDbType.VarChar, order.Description));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@OrderTotal", SqlDbType.Money, order.OrderTotal));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@OrderStatus", SqlDbType.VarChar, order.OrderStatus));

            return sqlManager.Upsert("AddOrder", sqlParameters);
        }

        public bool UpdateOrder(Order order)
        {
            SQLManager sqlManager = new();

            List<SqlParameter> sqlParameters = new();

            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@OrderNumber", SqlDbType.VarChar, order.OrderNumber));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@CustomerName", SqlDbType.VarChar, order.CustomerName));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@Address", SqlDbType.VarChar, order.Address));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@Email", SqlDbType.VarChar, order.Email));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@Phone", SqlDbType.VarChar, order.Phone));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@Description", SqlDbType.VarChar, order.Description));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@OrderTotal", SqlDbType.Money, order.OrderTotal));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@OrderStatus", SqlDbType.VarChar, order.OrderStatus));

            return sqlManager.Upsert("UpdateOrder", sqlParameters);
        }
    }
}
