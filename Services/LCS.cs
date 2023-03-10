using LudyCakeShop.Domain;
using LudyCakeShop.TechnicalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudyCakeShop.Services
{
    public class LCS
    {
        private readonly CategoryManager _categoryManager;
        private readonly ProductManager _productManager;

        public LCS()
        {
            _categoryManager = new();
            _productManager = new();
        }

        public Dictionary<Category, List<Product>> GetProductsByCategory()
        {
            List<Category> categories = (List<Category>)_categoryManager.GetCategories();

            List<Product> products = (List<Product>)_productManager.GetProducts();

            return products.GroupBy(product => product.CategoryID).ToDictionary(p => categories.Where(c => c.CategoryID == p.Key).FirstOrDefault(), p => p.ToList());
        }
    }
}
