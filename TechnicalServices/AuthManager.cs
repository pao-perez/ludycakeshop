using LudyCakeShop.Domain;
using System.Collections.Generic;
using System.Data;

namespace LudyCakeShop.TechnicalServices
{
    public class AuthManager : IAuthManager
    {
        private readonly ISQLManager _sqlManager;
        public AuthManager(ISQLManager sqlManager)
        {
            this._sqlManager = sqlManager;
        }

        public UserAccount GetAuth(string username)
        {
            List<StoredProcedureParameter> storedProcedureParameters = new();
            storedProcedureParameters.Add(new StoredProcedureParameter() { ParameterName = "@Username", ParameterSqlDbType = SqlDbType.VarChar, ParameterValue = username });
            DatasourceParameter datasourceParameter = new()
            {
                StoredProcedure = "GetUserAccount",
                StoredProcedureParameters = storedProcedureParameters,
                ClassType = typeof(UserAccount)
            };

            return _sqlManager.Select<UserAccount>(datasourceParameter);
        }
    }
}
