using LudyCakeShop.Domain;
using System.Collections.Generic;

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
    }
}
