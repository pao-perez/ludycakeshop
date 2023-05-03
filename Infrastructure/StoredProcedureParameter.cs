using System;
using System.Data;

namespace LudyCakeShop.Infrastructure
{
    public class StoredProcedureParameter
    {
        public string ParameterName { get; set; }
        public SqlDbType ParameterSqlDbType { get; set; }
        public Object ParameterValue { get; set; }
    }
}
