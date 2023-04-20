using LudyCakeShop.Domain;
using System.Collections.Generic;
using System.Data;

namespace LudyCakeShop.TechnicalServices
{
    public class AuthManager
    {
        public UserAccount GetAuth(string username)
        {
            SQLManager sqlManager = new();
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@Username", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = username });
            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetUserAccount",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(UserAccount)
            };

            return sqlManager.Select<UserAccount>(datasourceParameter);
        }
    }
}
