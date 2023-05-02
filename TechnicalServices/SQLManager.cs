using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace LudyCakeShop.TechnicalServices
{
    public class SQLManager
    {
        private readonly string _sqlConnectionString;
        private readonly string settingFile = "appsettings.json";

        public SQLManager()
        {
            _sqlConnectionString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(settingFile)
                .Build()
                .GetConnectionString("SQLServer");
        }

        private static SqlCommand CreateSqlCommand(SqlConnection datasourceConnection, string storedProcedure)
        {
            SqlCommand command = new();
            command.Connection = datasourceConnection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = storedProcedure;

            return command;
        }

        private static SqlCommand CreateSqlCommand(SqlConnection datasourceConnection, string storedProcedure, SqlTransaction sqlTransaction)
        {
            SqlCommand command = new();
            command.Connection = datasourceConnection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = storedProcedure;
            command.Transaction = sqlTransaction;

            return command;
        }

        private static SqlParameter CreateSqlCommandInputParameter(string storedProcedureParameter, SqlDbType sqlDbType, object sqlValue)
        {
            SqlParameter commandParameter = new()
            {
                ParameterName = storedProcedureParameter,
                SqlDbType = sqlDbType,
                Direction = ParameterDirection.Input,
                SqlValue = sqlValue
            };

            return commandParameter;
        }

        private static List<PropertyInfo> GetPropertyFields(SqlDataReader dataReader, DatasourceParameter datasourceParameter)
        {
            PropertyInfo prop;
            List<PropertyInfo> props = new();
            for (int index = 0; index < dataReader.FieldCount; index++)
            {
                prop = datasourceParameter.ClassType.GetProperty(dataReader.GetName(index));
                props.Add(prop);
            }

            return props;
        }

        private static void SetObjectProperties<T>(SqlDataReader dataReader, List<PropertyInfo> props, T obj)
        {
            for (int index = 0; index < dataReader.FieldCount; index++)
            {
                PropertyInfo storedProp = props[index];
                var val = dataReader[index];
                if (storedProp != null && val != DBNull.Value)
                {
                    storedProp.SetValue(obj, val);
                }
            }
        }

        public T Select<T>(DatasourceParameter datasourceParameter)
        {
            SqlConnection sqlConnection = new();
            sqlConnection.ConnectionString = _sqlConnectionString;
            sqlConnection.Open();

            SqlCommand command = CreateSqlCommand(sqlConnection, datasourceParameter.StoredProcedure);

            foreach (StoredProcedureParameter storedProcedureParameter in datasourceParameter.StoredProcedureParameters)
            {
                command.Parameters.Add(CreateSqlCommandInputParameter(storedProcedureParameter.ParameterName, storedProcedureParameter.ParameterSqlDbType, storedProcedureParameter.ParameterValue));
            }

            SqlDataReader dataReader = command.ExecuteReader();

            if (!dataReader.HasRows)
            {
                return default;
            }
            T obj = (T)Activator.CreateInstance(null, datasourceParameter.ClassType.FullName).Unwrap();
            List<PropertyInfo> props = GetPropertyFields(dataReader, datasourceParameter);
            while (dataReader.Read())
            {
                SetObjectProperties(dataReader, props, obj);
            }

            dataReader.Close();
            sqlConnection.Close();

            return obj;
        }

        public IEnumerable<T> SelectAll<T>(DatasourceParameter datasourceParameter)
        {
            SqlConnection sqlConnection = new();
            sqlConnection.ConnectionString = _sqlConnectionString;
            sqlConnection.Open();

            SqlCommand command = CreateSqlCommand(sqlConnection, datasourceParameter.StoredProcedure);

            foreach (StoredProcedureParameter storedProcedureParameter in datasourceParameter.StoredProcedureParameters)
            {
                command.Parameters.Add(CreateSqlCommandInputParameter(storedProcedureParameter.ParameterName, storedProcedureParameter.ParameterSqlDbType, storedProcedureParameter.ParameterValue));
            }

            SqlDataReader dataReader = command.ExecuteReader();

            List<T> objects = new();
            if (dataReader.HasRows)
            {
                List<PropertyInfo> props = GetPropertyFields(dataReader, datasourceParameter);
                while (dataReader.Read())
                {
                    T obj = (T)Activator.CreateInstance(null, datasourceParameter.ClassType.FullName).Unwrap();
                    SetObjectProperties(dataReader, props, obj);
                    objects.Add(obj);
                }
            }

            dataReader.Close();
            sqlConnection.Close();

            return objects;
        }

        public bool DeleteTransaction(IEnumerable<DatasourceParameter> datasourceParameters)
        {
            bool success = false;
            SqlConnection sqlConnection = new();
            sqlConnection.ConnectionString = _sqlConnectionString;
            sqlConnection.Open();
            SqlTransaction sqlDatasourceTransaction = sqlConnection.BeginTransaction();

            IEnumerable<SqlCommand> sqlCommands = ((List<DatasourceParameter>)datasourceParameters).Select(datasourceParameter =>
            {
                SqlCommand command = CreateSqlCommand(sqlConnection, datasourceParameter.StoredProcedure, sqlDatasourceTransaction);

                foreach (StoredProcedureParameter storedProcedureParameter in datasourceParameter.StoredProcedureParameters)
                {
                    command.Parameters.Add(CreateSqlCommandInputParameter(storedProcedureParameter.ParameterName, storedProcedureParameter.ParameterSqlDbType, storedProcedureParameter.ParameterValue));
                }

                return command;
            });

            try
            {
                foreach (SqlCommand command in sqlCommands)
                {
                    command.ExecuteNonQuery();
                }
                sqlDatasourceTransaction.Commit();
                success = true;
            }
            catch (Exception)
            {
                sqlDatasourceTransaction.Rollback();
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

            return success;
        }

        public bool DeleteAll(string storedProcedure)
        {
            bool success;
            SqlConnection sqlConnection = new();
            sqlConnection.ConnectionString = _sqlConnectionString;
            sqlConnection.Open();

            SqlCommand command = CreateSqlCommand(sqlConnection, storedProcedure);

            success = command.ExecuteNonQuery() > 0;

            sqlConnection.Close();
            return success;
        }

        public bool UpsertTransaction(IEnumerable<DatasourceParameter> datasourceParameters)
        {
            bool success = false;
            SqlConnection sqlConnection = new();
            sqlConnection.ConnectionString = _sqlConnectionString;
            sqlConnection.Open();
            SqlTransaction sqlDatasourceTransaction = sqlConnection.BeginTransaction();

            IEnumerable<SqlCommand> sqlCommands = ((List<DatasourceParameter>)datasourceParameters).Select(datasourceParameter =>
            {
                SqlCommand command = CreateSqlCommand(sqlConnection, datasourceParameter.StoredProcedure, sqlDatasourceTransaction);

                foreach (StoredProcedureParameter storedProcedureParameter in datasourceParameter.StoredProcedureParameters)
                {
                    command.Parameters.Add(CreateSqlCommandInputParameter(storedProcedureParameter.ParameterName, storedProcedureParameter.ParameterSqlDbType, storedProcedureParameter.ParameterValue));
                }

                return command;
            });

            try
            {
                foreach (SqlCommand command in sqlCommands)
                {
                    command.ExecuteNonQuery();
                }
                sqlDatasourceTransaction.Commit();
                success = true;
            }
            catch (Exception)
            {
                sqlDatasourceTransaction.Rollback();
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

            return success;
        }
    }
}
