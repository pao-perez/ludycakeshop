using LudyCakeShop.Core.Domain.Data;
using LudyCakeShop.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;

namespace LudyCakeShop.Infrastructure
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ISQLManager _sqlManager;
        public CategoryManager(ISQLManager sqlManager)
        {
            this._sqlManager = sqlManager;
        }

        public IEnumerable<Category> GetCategories()
        {
            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetCategories",
                StoredProcedureParameters = new List<StoredProcedureParameter>(),
                ClassType = typeof(Category)
            };

            return _sqlManager.SelectAll<Category>(datasourceParameter);
        }

        public Category GetCategory(string categoryID)
        {
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = categoryID });
            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetCategory",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(Category)
            };

            return _sqlManager.Select<Category>(datasourceParameter);
        }

        public string CreateCategory(Category category)
        {
            List<DatasourceParameter> datasourceParameters = new();

            string categoryID = Guid.NewGuid().ToString();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = categoryID });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = category.CategoryName });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryDescription", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = category.CategoryDescription });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryImage", ParameterSqlDbType = SqlDbType.Image, ParameterValue = category.CategoryImage });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "CreateCategory",
                StoredProcedureParameters = storedProcedureParameters
            });

            _sqlManager.UpsertTransaction(datasourceParameters);

            return categoryID;
        }

        public bool DeleteCategory(string categoryID)
        {
            List<DatasourceParameter> datasourceParameters = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = categoryID });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "DeleteCategory",
                StoredProcedureParameters = storedProcedureParameters
            });

            return _sqlManager.DeleteTransaction(datasourceParameters);
        }

        public bool UpdateCategory(string categoryID, Category category)
        {
            List<DatasourceParameter> datasourceParameters = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryID", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = categoryID });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryName", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = category.CategoryName });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryDescription", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = category.CategoryDescription });
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@CategoryImage", ParameterSqlDbType = SqlDbType.Image, ParameterValue = category.CategoryImage });
            datasourceParameters.Add(new()
            {
                StoredProcedure = "UpdateCategory",
                StoredProcedureParameters = storedProcedureParameters
            });

            return _sqlManager.UpsertTransaction(datasourceParameters);
        }
    }
}
