using LudyCakeShop.Domain;
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
            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetProducts",
                StoredProcedureParameters = new List<StoredProcedureParameter>(),
                ClassType = typeof(Product)
            };

            return sqlManager.SelectAll<Product>(datasourceParameter);
        }

        public Product GetProduct(string id)
        {
            SQLManager sqlManager = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = id });
            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetProduct",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(Product)
            };

            return sqlManager.Select<Product>(datasourceParameter);
        }

        public string CreateProduct(Product product)
        {
            SQLManager sqlManager = new();
            List<DatasourceParameter> datasourceParameters = new();

            var productID = Guid.NewGuid().ToString();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = productID });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = product.ProductName });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductDescription", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = product.ProductDescription });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@QuantityAvailable", ParameterSqlDbType = SqlDbType.Int, ParameterValue = product.QuantityAvailable });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@UnitPrice", ParameterSqlDbType = SqlDbType.Money, ParameterValue = product.UnitPrice });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@Discontinued", ParameterSqlDbType = SqlDbType.Bit, ParameterValue = product.Discontinued });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@QuantityPerUnit", ParameterSqlDbType = SqlDbType.Int, ParameterValue = product.QuantityPerUnit });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = product.CategoryID });
            //storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductImageID", ParameterSqlDbType = SqlDbType.Int, ParameterValue = product.ProductImageID });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "CreateProduct",
                StoredProcedureParameters = storedProcedureParameters
            });

            sqlManager.UpsertTransaction(datasourceParameters);

            return productID;
        }

        public bool UpdateProduct(string productID, Product product)
        {
            SQLManager sqlManager = new();
            List<DatasourceParameter> datasourceParameters = new();

            //TODO: check if product ID exists in any "active" order
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = productID });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = product.ProductName });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductDescription", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = product.ProductDescription });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@QuantityAvailable", ParameterSqlDbType = SqlDbType.Int, ParameterValue = product.QuantityAvailable });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@UnitPrice", ParameterSqlDbType = SqlDbType.Money, ParameterValue = product.UnitPrice });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@Discontinued", ParameterSqlDbType = SqlDbType.Bit, ParameterValue = product.Discontinued });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@QuantityPerUnit", ParameterSqlDbType = SqlDbType.Int, ParameterValue = product.QuantityPerUnit });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = product.CategoryID });
            //storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductImageID", ParameterSqlDbType = SqlDbType.Int, ParameterValue = product.ProductImageID });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "UpdateProduct",
                StoredProcedureParameters = storedProcedureParameters
            });

            return sqlManager.UpsertTransaction(datasourceParameters);
        }

        public bool DeleteProduct(string id)
        {
            SQLManager sqlManager = new();
            List<DatasourceParameter> datasourceParameters = new();

            //TODO: check if product ID exists in any "active" order
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@ProductID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = id });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "DiscontinueProduct",
                StoredProcedureParameters = storedProcedureParameters
            });

            return sqlManager.UpsertTransaction(datasourceParameters);
        }
    }
}
