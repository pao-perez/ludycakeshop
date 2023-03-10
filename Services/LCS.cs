using LudyCakeShop.Controllers;
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
        private readonly OrderManager _orderManager;

        public LCS()
        {
            _categoryManager = new();
            _productManager = new();
            _orderManager = new();
        }

        public Dictionary<Category, List<Product>> GetProductsByCategories()
        {
            List<Category> categories = (List<Category>)_categoryManager.GetCategories();

            List<Product> products = (List<Product>)_productManager.GetProducts();

            return products.GroupBy(product => product.CategoryID).ToDictionary(p => categories.Where(c => c.CategoryID == p.Key).FirstOrDefault(), p => p.ToList());
        }

        public bool SubmitOrder(Order order)
        {
            return _orderManager.AddOrder(order);
        }

        public bool UpdateOrder(Order order)
        {
            return _orderManager.UpdateOrder(order);
        }

        public IEnumerable<Order> GetOrders()
        {
            return (List<Order>)_orderManager.GetOrders();
        }

        public Order GetOrder(int id)
        {
            return _orderManager.GetOrder(id);
        }
    }
}
