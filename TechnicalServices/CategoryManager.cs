using LudyCakeShop.Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace LudyCakeShop.TechnicalServices
{
    public class CategoryManager
    {
        public IEnumerable<Category> GetCategories()
        {
            SQLManager sqlManager = new();
            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetCategories",
                StoredProcedureParameters = new List<StoredProcedureParameter>(),
                ClassType = typeof(Category)
            };

            return sqlManager.SelectAll<Category>(datasourceParameter);
        }

        public Category GetCategory(string categoryID)
        {
            SQLManager sqlManager = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = categoryID });
            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetCategory",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(Category)
            };

            return sqlManager.Select<Category>(datasourceParameter);
        }

        public string CreateCategory(Category category)
        {
            SQLManager sqlManager = new();
            List<DatasourceParameter> datasourceParameters = new();

            var categoryID = Guid.NewGuid().ToString();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = categoryID });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = category.CategoryName });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryDescription", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = category.CategoryDescription });
            //storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryImage", ParameterSqlDbType = SqlDbType.Image, ParameterValue = category.CategoryImage });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "CreateCategory",
                StoredProcedureParameters = storedProcedureParameters
            });

            sqlManager.UpsertTransaction(datasourceParameters);

            return categoryID;
        }

        public bool DeleteCategory(string categoryID)
        {
            SQLManager sqlManager = new();
            List<DatasourceParameter> datasourceParameters = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = categoryID });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "DeleteCategory",
                StoredProcedureParameters = storedProcedureParameters
            });

            return sqlManager.DeleteTransaction(datasourceParameters);
        }

        public bool UpdateCategory(string categoryID, Category category)
        {
            SQLManager sqlManager = new();
            List<DatasourceParameter> datasourceParameters = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = categoryID });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = category.CategoryName });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryDescription", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = category.CategoryDescription });
            //storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryImage", ParameterSqlDbType = SqlDbType.Image, ParameterValue = category.CategoryImage });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "UpdateCategory",
                StoredProcedureParameters = storedProcedureParameters
            });

            return sqlManager.UpsertTransaction(datasourceParameters);
        }
    }
}
