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
        private readonly SqlConnection _sqlDatasourceConnection;
        public SQLManager()
        {
            ConfigurationBuilder DatabaseUsersBuilder = new();
            DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUsersBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUsersConfiguration = DatabaseUsersBuilder.Build();

            _sqlDatasourceConnection = new();
            _sqlDatasourceConnection.ConnectionString =
            DatabaseUsersConfiguration.GetConnectionString("SQLServerConn");
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

        public static SqlParameter CreateSqlCommandInputParameter(string storedProcedureParameter, SqlDbType sqlDbType, object sqlValue)
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

        public T Select<T>(DatasourceParameter datasourceParameter)
        {
            _sqlDatasourceConnection.Open();

            SqlCommand command = CreateSqlCommand(_sqlDatasourceConnection, datasourceParameter.StoredProcedure);

            foreach (StoredProcedureParameter storedProcedureParameter in datasourceParameter.StoredProcedureParameters)
            {
                command.Parameters.Add(CreateSqlCommandInputParameter(storedProcedureParameter.ParameterName, storedProcedureParameter.ParameterSqlDbType, storedProcedureParameter.ParameterValue));
            }

            SqlDataReader dataReader = command.ExecuteReader();

            T obj = (T)Activator.CreateInstance(null, datasourceParameter.ClassType.FullName).Unwrap();
            if (dataReader.HasRows)
            {
                PropertyInfo prop;
                List<PropertyInfo> props = new();
                for (int index = 0; index < dataReader.FieldCount; index++)
                {
                    prop = datasourceParameter.ClassType.GetProperty(dataReader.GetName(index));
                    props.Add(prop);
                }
                while (dataReader.Read())
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
            }

            dataReader.Close();
            _sqlDatasourceConnection.Close();

            return obj;
        }

        public IEnumerable<T> SelectAll<T>(DatasourceParameter datasourceParameter)
        {
            _sqlDatasourceConnection.Open();

            SqlCommand command = CreateSqlCommand(_sqlDatasourceConnection, datasourceParameter.StoredProcedure);

            foreach (StoredProcedureParameter storedProcedureParameter in datasourceParameter.StoredProcedureParameters)
            {
                command.Parameters.Add(CreateSqlCommandInputParameter(storedProcedureParameter.ParameterName, storedProcedureParameter.ParameterSqlDbType, storedProcedureParameter.ParameterValue));
            }

            SqlDataReader dataReader = command.ExecuteReader();

            List<T> objects = new();
            if (dataReader.HasRows)
            {
                PropertyInfo prop;
                List<PropertyInfo> props = new();
                for (int index = 0; index < dataReader.FieldCount; index++)
                {
                    prop = datasourceParameter.ClassType.GetProperty(dataReader.GetName(index));
                    props.Add(prop);
                }
                while (dataReader.Read())
                {
                    T obj = (T)Activator.CreateInstance(null, datasourceParameter.ClassType.FullName).Unwrap();
                    for (int index = 0; index < dataReader.FieldCount; index++)
                    {
                        PropertyInfo storedProp = props[index];
                        var val = dataReader[index];
                        if (storedProp != null && val != DBNull.Value)
                        {
                            storedProp.SetValue(obj, val);
                        }
                    }
                    objects.Add(obj);
                }
            }

            dataReader.Close();
            _sqlDatasourceConnection.Close();

            return objects;
        }

        public bool Delete(IEnumerable<DatasourceParameter> datasourceParameters)
        {
            bool success = false;
            _sqlDatasourceConnection.Open();
            SqlTransaction sqlDatasourceTransaction = _sqlDatasourceConnection.BeginTransaction();

            IEnumerable<SqlCommand> sqlCommands = ((List<DatasourceParameter>)datasourceParameters).Select(datasourceParameter =>
            {
                SqlCommand command = CreateSqlCommand(_sqlDatasourceConnection, datasourceParameter.StoredProcedure, sqlDatasourceTransaction);

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
            }
            catch (Exception)
            {
                sqlDatasourceTransaction.Rollback();
                throw;
            }
            finally
            {
                _sqlDatasourceConnection.Close();
            }

            return success;
        }

        public bool DeleteAll(string storedProcedure)
        {
            bool success;
            _sqlDatasourceConnection.Open();

            SqlCommand command = CreateSqlCommand(_sqlDatasourceConnection, storedProcedure);

            success = command.ExecuteNonQuery() > 0;

            _sqlDatasourceConnection.Close();
            return success;
        }

        public bool UpsertTransaction(IEnumerable<DatasourceParameter> datasourceParameters)
        {
            bool success = true;
            _sqlDatasourceConnection.Open();
            SqlTransaction sqlDatasourceTransaction = _sqlDatasourceConnection.BeginTransaction();

            IEnumerable<SqlCommand> sqlCommands = ((List<DatasourceParameter>)datasourceParameters).Select(datasourceParameter =>
            {
                SqlCommand command = CreateSqlCommand(_sqlDatasourceConnection, datasourceParameter.StoredProcedure, sqlDatasourceTransaction);

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
            }
            catch (Exception)
            {
                sqlDatasourceTransaction.Rollback();
                throw; // TODO: Create a custom DAOException OR Keep this (rethrow while preserving stack info) and handle in Specific Manager or Services
            }
            finally
            {
                _sqlDatasourceConnection.Close();
            }

            return success;
        }
    }
}
