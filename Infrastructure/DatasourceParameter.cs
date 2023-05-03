using System;
using System.Collections.Generic;

namespace LudyCakeShop.Infrastructure
{
    public class DatasourceParameter
    {
        public string StoredProcedure { get; set; }
        public IEnumerable<StoredProcedureParameter> StoredProcedureParameters { get; set; }
        public Type ClassType { get; set; }
    }
}
