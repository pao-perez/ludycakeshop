using LudyCakeShop.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudyCakeShop.TechnicalServices
{
    public class CategoryManager
    {
        public IEnumerable<Category> GetCategories()
        {
            SQLManager datasourceManager = new();

            List<SqlParameter> sqlParameters = new();

            return datasourceManager.SelectAll<Category>("GetCategories", sqlParameters, typeof(Category));
        }
    }
}
