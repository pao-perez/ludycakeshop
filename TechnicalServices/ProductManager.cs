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
            SQLManager sQLManager = new();

            List<SqlParameter> SqlParameters = new();

            return sQLManager.SelectAll<Product>("GetProducts", SqlParameters, typeof(Product));
        }
    }
}
