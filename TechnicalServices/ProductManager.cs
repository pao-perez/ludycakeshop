using LudyCakeShop.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace LudyCakeShop.TechnicalServices
{
    public class ProductManager
    {
        public IEnumerable<Product> GetProducts()
        {
            SQLManager sqlManager = new();

            List<SqlParameter> sqlParameters = new();

            return sqlManager.SelectAll<Product>("GetProducts", sqlParameters, typeof(Product));
        }

        public Product GetProduct(int id)
        {
            SQLManager sqlManager = new();

            List<SqlParameter> sqlParameters = new();
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@ProductID", SqlDbType.Int, id));

            return sqlManager.Select<Product>("GetProductByID", sqlParameters, typeof(Product));
        }

        public bool AddProduct(Product product)
        {
            SQLManager sqlManager = new();

            List<SqlParameter> sqlParameters = new();

            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@ProductName", SqlDbType.VarChar, product.ProductName));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@ProductDescription", SqlDbType.VarChar, product.ProductDescription));

            return sqlManager.Upsert("AddProduct", sqlParameters);
        }

        public bool UpdateProduct(Product product)
        {
            SQLManager sqlManager = new();

            List<SqlParameter> sqlParameters = new();

            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@ProductID", SqlDbType.VarChar, product.ProductID));

            return sqlManager.Upsert("UpdateProduct", sqlParameters);
        }

        public bool DeleteProduct(int id)
        {
            SQLManager sqlManager = new();

            SqlParameter sqlParamter = SQLManager.CreateSqlCommandInputParameter("@ProductID", SqlDbType.Int, id);

            return sqlManager.Delete("DeleteProduct", sqlParamter);
        }
    }
}
