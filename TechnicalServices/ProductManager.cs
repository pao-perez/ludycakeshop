using LudyCakeShop.Domain;
using Microsoft.Data.SqlClient;
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

            return sqlManager.Select<Product>("GetProduct", sqlParameters, typeof(Product));
        }

        public bool CreateProduct(Product product)
        {
            SQLManager sqlManager = new();

            List<SqlParameter> sqlParameters = new();

            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@ProductName", SqlDbType.VarChar, product.ProductName));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@ProductDescription", SqlDbType.VarChar, product.ProductDescription));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@QuantityAvailable", SqlDbType.Int, product.QuantityAvailable));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@UnitPrice", SqlDbType.Money, product.UnitPrice));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@Discontinued", SqlDbType.Bit, product.Discontinued));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@QuantityPerUnit", SqlDbType.VarChar, product.QuantityPerUnit));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@CategoryID", SqlDbType.Int, product.CategoryID));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@ProductImageID", SqlDbType.Int, product.ProductImageID));

            return sqlManager.Upsert("CreateProduct", sqlParameters);
        }

        public bool UpdateProduct(int productID, Product product)
        {
            SQLManager sqlManager = new();

            List<SqlParameter> sqlParameters = new();

            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@ProductID", SqlDbType.VarChar, productID));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@ProductName", SqlDbType.VarChar, product.ProductName));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@ProductDescription", SqlDbType.VarChar, product.ProductDescription));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@QuantityAvailable", SqlDbType.Int, product.QuantityAvailable));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@UnitPrice", SqlDbType.Money, product.UnitPrice));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@Discontinued", SqlDbType.Bit, product.Discontinued));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@QuantityPerUnit", SqlDbType.VarChar, product.QuantityPerUnit));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@CategoryID", SqlDbType.Int, product.CategoryID));
            sqlParameters.Add(SQLManager.CreateSqlCommandInputParameter("@ProductImageID", SqlDbType.Int, product.ProductImageID));

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
