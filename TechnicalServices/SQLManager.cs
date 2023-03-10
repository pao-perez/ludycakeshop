using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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

            string sqlConnectionString = DatabaseUsersConfiguration.GetConnectionString("SQLServerConn");

            _sqlDatasourceConnection = new()
            {
                ConnectionString = sqlConnectionString
            };
        }

        public static SqlCommand CreateSqlCommand(SqlConnection datasourceConnection, string storedProcedure)
        {
            SqlCommand command = new();
            command.Connection = datasourceConnection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = storedProcedure;

            return command;
        }

        public static SqlParameter CreateSqlCommandInputParameter(string storedProcedureParameter, SqlDbType sqlDbType, object sqlValue)
        {
            SqlParameter commandParameter = new SqlParameter
            {
                ParameterName = storedProcedureParameter,
                SqlDbType = sqlDbType,
                Direction = ParameterDirection.Input,
                SqlValue = sqlValue
            };

            return commandParameter;
        }

        public T Select<T>(string storedProcedure, IEnumerable<SqlParameter> sqlParameters, Type classType)
        {
            _sqlDatasourceConnection.Open();

            SqlCommand command = CreateSqlCommand(_sqlDatasourceConnection, storedProcedure);

            foreach (SqlParameter sqlParameter in sqlParameters)
            {
                command.Parameters.Add(sqlParameter);
            }

            SqlDataReader dataReader = command.ExecuteReader();

            T obj = (T)Activator.CreateInstance(null, classType.FullName).Unwrap();
            if (dataReader.HasRows)
            {
                PropertyInfo prop = null;
                List<PropertyInfo> props = new List<PropertyInfo>();
                for (int index = 0; index < dataReader.FieldCount; index++)
                {
                    prop = classType.GetProperty(dataReader.GetName(index));
                    props.Add(prop);
                }
                while (dataReader.Read())
                {
                    for (int index = 0; index < dataReader.FieldCount; index++)
                    {
                        PropertyInfo storedProp = props[index];
                        if (storedProp != null)
                        {
                            storedProp.SetValue(obj, dataReader[index]);
                        }
                    }
                }
            }

            dataReader.Close();
            _sqlDatasourceConnection.Close();

            return obj;
        }

        public IEnumerable<T> SelectAll<T>(string storedProcedure, IEnumerable<SqlParameter> sqlParameters, Type classType)
        {
            _sqlDatasourceConnection.Open();

            SqlCommand command = CreateSqlCommand(_sqlDatasourceConnection, storedProcedure);

            foreach (SqlParameter sqlParameter in sqlParameters)
            {
                command.Parameters.Add(sqlParameter);
            }

            SqlDataReader dataReader = command.ExecuteReader();

            List<T> objects = new List<T>();
            if (dataReader.HasRows)
            {
                PropertyInfo prop = null;
                List<PropertyInfo> props = new List<PropertyInfo>();
                for (int Index = 0; Index < dataReader.FieldCount; Index++)
                {
                    prop = classType.GetProperty(dataReader.GetName(Index));
                    props.Add(prop);
                }
                while (dataReader.Read())
                {
                    T obj = (T)Activator.CreateInstance(null, classType.FullName).Unwrap();
                    for (int Index = 0; Index < dataReader.FieldCount; Index++)
                    {
                        PropertyInfo storedProp = props[Index];
                        var val = dataReader[Index];
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

        public bool Upsert(string storedProcedure, IEnumerable<SqlParameter> sqlParameters)
        {
            bool success = false;
            _sqlDatasourceConnection.Open();

            SqlCommand command = CreateSqlCommand(_sqlDatasourceConnection, storedProcedure);

            foreach (SqlParameter sqlParameter in sqlParameters)
            {
                command.Parameters.Add(sqlParameter);
            }

            success = command.ExecuteNonQuery() > 0;

            _sqlDatasourceConnection.Close();

            return success;
        }

        public bool Delete(string storedProcedure, SqlParameter sqlParameter)
        {
            bool success = false;
            _sqlDatasourceConnection.Open();

            SqlCommand command = CreateSqlCommand(_sqlDatasourceConnection, storedProcedure);

            command.Parameters.Add(sqlParameter);

            success = command.ExecuteNonQuery() > 0;

            _sqlDatasourceConnection.Close();

            return success;
        }
    }
}
