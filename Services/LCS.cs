using LudyCakeShop.Domain;
using LudyCakeShop.TechnicalServices;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public bool AddProduct(Product product)
        {
            return _productManager.AddProduct(product);
        }

        public bool UpdateProduct(Product product)
        {
            return _productManager.UpdateProduct(product);
        }

        public Product GetProduct(int id)
        {
            return _productManager.GetProduct(id);
        }

        public bool DeleteProduct(int id)
        {
            return _productManager.DeleteProduct(id);
        }

        public bool AddOrder(Order order)
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
