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

        public string CreateProduct(Product product)
        {
            return _productManager.CreateProduct(product);
        }

        public bool UpdateProduct(string productID, Product product)
        {
            return _productManager.UpdateProduct(productID, product);
        }

        public Product GetProduct(string productID)
        {
            return _productManager.GetProduct(productID);
        }

        public IEnumerable<Product> GetProducts()
        {
            return (List<Product>)_productManager.GetProducts();
        }

        public bool DeleteProduct(string productID)
        {
            return _productManager.DeleteProduct(productID);
        }

        public string CreateOrder(Order order)
        {
            //TODO: compute subTotal, saleTotal, and GST
            order.OrderStatus = OrderStatus.SUBMITTED;
            return _orderManager.CreateOrder(order);
        }

        public bool UpdateOrder(string orderID, Order order)
        {
            return _orderManager.UpdateOrder(orderID, order);
        }

        public IEnumerable<Order> GetOrders()
        {
            return (List<Order>)_orderManager.GetOrders();
        }

        public Order GetOrder(string orderID)
        {
            return _orderManager.GetOrder(orderID);
        }

        public bool DeleteCategory(string categoryID)
        {
            return _categoryManager.DeleteCategory(categoryID);
        }

        public bool UpdateCategory(string categoryID, Category category)
        {
            return _categoryManager.UpdateCategory(categoryID, category);
        }

        public string CreateCategory(Category category)
        {
            return _categoryManager.CreateCategory(category);
        }

        public Category GetCategory(string categoryID)
        {
            return _categoryManager.GetCategory(categoryID);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryManager.GetCategories();
        }

        public Dictionary<Category, List<Product>> GetProductsByCategories()
        {
            IEnumerable<Category> categories = _categoryManager.GetCategories();
            IEnumerable<Product> products = _productManager.GetProducts();

            return products.GroupBy(product => product.CategoryID).ToDictionary(p => categories.Where(c => c.CategoryID == p.Key).FirstOrDefault(), p => p.ToList());
        }
    }
}
