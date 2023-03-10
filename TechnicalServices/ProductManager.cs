using LudyCakeShop.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudyCakeShop.TechnicalServices
{
    public class ProductManager
    {
        public IEnumerable<Product> GetProducts()
        {
            SQLManager sqlManager = new();

            List<SqlParameter> sqlParameters = new();

            return sqlManager.SelectAll<Product>("GetProducts", sqlParameters, typeof(Product));
        }
    }
}
