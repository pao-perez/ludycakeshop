using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LudyCakeShop.Domain;

namespace LudyCakeShop.Infrastructure
{
    /**
     * Built as a utility convenience for our courses at school which repeatedly
     * required using ADO.NET and stored procedures.
     * This has evolved and improved upon since.
     */
    public class SQLManager : ISQLManager
    {
        private readonly SQLConfiguration _sqlConfig;

        public SQLManager(SQLConfiguration sqlConfig)
        {
            this._sqlConfig = sqlConfig;
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
            // We don't expect a lot of table columns from all tables, only a few table row cell
            for (int index = 0; index < dataReader.FieldCount; index++)
            {
                PropertyInfo storedProp = props[index];
                if (storedProp == null)
                {
                    continue;
                }
                Action<T, object> setterDelegate = PropertyManager<T>.GetOrCreateSetter(storedProp);
                object val = dataReader[index];
                if (val != DBNull.Value)
                {
                    setterDelegate(obj, val);
                }
            }
        }

        public T Select<T>(DatasourceParameter datasourceParameter)
        {
            SqlConnection sqlConnection = new();
            sqlConnection.ConnectionString = _sqlConfig.ConnectionString;
            SqlDataReader dataReader = null;
            T obj;

            try
            {
                sqlConnection.Open();
                SqlCommand command = CreateSqlCommand(sqlConnection, datasourceParameter.StoredProcedure);

                foreach (StoredProcedureParameter storedProcedureParameter in datasourceParameter.StoredProcedureParameters)
                {
                    command.Parameters.Add(CreateSqlCommandInputParameter(storedProcedureParameter.ParameterName, storedProcedureParameter.ParameterSqlDbType, storedProcedureParameter.ParameterValue));
                }

                dataReader = command.ExecuteReader();

                if (!dataReader.HasRows)
                {
                    return default;
                }
                Func<T> constructorDelegate = PropertyManager<T>.GetOrCreateConstructor(datasourceParameter.ClassType);
                obj = constructorDelegate();
                List<PropertyInfo> props = GetPropertyFields(dataReader, datasourceParameter);
                while (dataReader.Read())
                {
                    SetObjectProperties(dataReader, props, obj);
                }
            }
            finally
            {
                dataReader.Close();
                sqlConnection.Close();
            }

            return obj;
        }

        public IEnumerable<T> SelectAll<T>(DatasourceParameter datasourceParameter)
        {
            SqlConnection sqlConnection = new();
            sqlConnection.ConnectionString = _sqlConfig.ConnectionString;
            SqlDataReader dataReader = null;
            List<T> objects = new();

            try
            {
                sqlConnection.Open();
                SqlCommand command = CreateSqlCommand(sqlConnection, datasourceParameter.StoredProcedure);

                foreach (StoredProcedureParameter storedProcedureParameter in datasourceParameter.StoredProcedureParameters)
                {
                    command.Parameters.Add(CreateSqlCommandInputParameter(storedProcedureParameter.ParameterName, storedProcedureParameter.ParameterSqlDbType, storedProcedureParameter.ParameterValue));
                }

                dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    List<PropertyInfo> props = GetPropertyFields(dataReader, datasourceParameter);
                    Func<T> constructorDelegate = PropertyManager<T>.GetOrCreateConstructor(datasourceParameter.ClassType);
                    while (dataReader.Read()) // iterate table rows
                    {
                        T obj = constructorDelegate();
                        SetObjectProperties(dataReader, props, obj);
                        objects.Add(obj);
                    }
                }
            } 
            finally
            {
                dataReader.Close();
                sqlConnection.Close();
            }

            return objects;
        }

        public bool DeleteTransaction(IEnumerable<DatasourceParameter> datasourceParameters)
        {
            bool success = false;
            SqlConnection sqlConnection = new();
            sqlConnection.ConnectionString = _sqlConfig.ConnectionString;
            SqlTransaction sqlDatasourceTransaction = null;

            try
            {
                sqlConnection.Open();
                sqlDatasourceTransaction = sqlConnection.BeginTransaction();

                IEnumerable<SqlCommand> sqlCommands = ((List<DatasourceParameter>)datasourceParameters).Select(datasourceParameter =>
                {
                    SqlCommand command = CreateSqlCommand(sqlConnection, datasourceParameter.StoredProcedure, sqlDatasourceTransaction);

                    foreach (StoredProcedureParameter storedProcedureParameter in datasourceParameter.StoredProcedureParameters)
                    {
                        command.Parameters.Add(CreateSqlCommandInputParameter(storedProcedureParameter.ParameterName, storedProcedureParameter.ParameterSqlDbType, storedProcedureParameter.ParameterValue));
                    }

                    return command;
                });

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
            sqlConnection.ConnectionString = _sqlConfig.ConnectionString;

            try
            {
                sqlConnection.Open();
                SqlCommand command = CreateSqlCommand(sqlConnection, storedProcedure);
                success = command.ExecuteNonQuery() > 0;
            }
            finally
            {
                sqlConnection.Close();
            }

            return success;
        }

        public bool UpsertTransaction(IEnumerable<DatasourceParameter> datasourceParameters)
        {
            bool success = false;
            SqlConnection sqlConnection = new();
            sqlConnection.ConnectionString = _sqlConfig.ConnectionString;
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
