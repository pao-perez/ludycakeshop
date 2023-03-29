using LudyCakeShop.Domain;
using System.Collections.Generic;
using System.Data;

namespace LudyCakeShop.TechnicalServices
{
    public class ProductManager
    {
        public IEnumerable<Product> GetProducts()
        {
            SQLManager sqlManager = new();
            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetProducts",
                StoredProcedureParameters = new List<StoredProcedureParameter>(),
                ClassType = typeof(Product)
            };

            return sqlManager.SelectAll<Product>(datasourceParameter);
        }

        public Product GetProduct(int id)
        {
            SQLManager sqlManager = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductID", ParameterSqlDbType = SqlDbType.Int, ParameterValue = id });
            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetProduct",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(Product)
            };

            return sqlManager.Select<Product>(datasourceParameter);
        }

        public bool CreateProduct(Product product)
        {
            SQLManager sqlManager = new();

            List<DatasourceParameter> datasourceParameters = new();
            List<StoredProcedureParameter> createProductStoredProcedureParameters = new();
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = product.ProductName });
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductDescription", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = product.ProductDescription });
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@QuantityAvailable", ParameterSqlDbType = SqlDbType.Int, ParameterValue = product.QuantityAvailable });
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@UnitPrice", ParameterSqlDbType = SqlDbType.Money, ParameterValue = product.UnitPrice });
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@Discontinued", ParameterSqlDbType = SqlDbType.Bit, ParameterValue = product.Discontinued });
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@QuantityPerUnit", ParameterSqlDbType = SqlDbType.Int, ParameterValue = product.QuantityPerUnit });
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryID", ParameterSqlDbType = SqlDbType.Int, ParameterValue = product.CategoryID });
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductImageID", ParameterSqlDbType = SqlDbType.Int, ParameterValue = product.ProductImageID });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "CreateProduct",
                StoredProcedureParameters = createProductStoredProcedureParameters
            });

            return sqlManager.UpsertTransaction(datasourceParameters);
        }

        public bool UpdateProduct(int productID, Product product)
        {
            SQLManager sqlManager = new();

            List<DatasourceParameter> datasourceParameters = new();
            List<StoredProcedureParameter> createProductStoredProcedureParameters = new();
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductID", ParameterSqlDbType = SqlDbType.Int, ParameterValue = productID });
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = product.ProductName });
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductDescription", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = product.ProductDescription });
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@QuantityAvailable", ParameterSqlDbType = SqlDbType.Int, ParameterValue = product.QuantityAvailable });
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@UnitPrice", ParameterSqlDbType = SqlDbType.Money, ParameterValue = product.UnitPrice });
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@Discontinued", ParameterSqlDbType = SqlDbType.Bit, ParameterValue = product.Discontinued });
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@QuantityPerUnit", ParameterSqlDbType = SqlDbType.Int, ParameterValue = product.QuantityPerUnit });
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryID", ParameterSqlDbType = SqlDbType.Int, ParameterValue = product.CategoryID });
            createProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductImageID", ParameterSqlDbType = SqlDbType.Int, ParameterValue = product.ProductImageID });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "UpdateProduct",
                StoredProcedureParameters = createProductStoredProcedureParameters
            });

            return sqlManager.UpsertTransaction(datasourceParameters);
        }

        public bool DeleteProduct(int id)
        {
            SQLManager sqlManager = new();
            //TODO: check if product ID exists in any "active" order
            List<DatasourceParameter> datasourceParameters = new();
            List<StoredProcedureParameter> deleteProductStoredProcedureParameters = new();
            deleteProductStoredProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductID", ParameterSqlDbType = SqlDbType.Int, ParameterValue = id });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "DeleteProduct",
                StoredProcedureParameters = deleteProductStoredProcedureParameters
            });

            return sqlManager.Delete(datasourceParameters);
        }
    }
}
