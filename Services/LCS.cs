using LudyCakeShop.Domain;
using LudyCakeShop.TechnicalServices;
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

        public bool CreateProduct(Product product)
        {
            return _productManager.CreateProduct(product);
        }

        public bool UpdateProduct(int productID, Product product)
        {
            return _productManager.UpdateProduct(productID, product);
        }

        public Product GetProduct(int id)
        {
            return _productManager.GetProduct(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return (List<Product>)_productManager.GetProducts();
        }

        public bool DeleteProduct(int id)
        {
            return _productManager.DeleteProduct(id);
        }

        public bool CreateOrder(Order order)
        {
            return _orderManager.CreateOrder(order);
        }

        public bool UpdateOrder(int orderNumber, Order order)
        {
            return _orderManager.UpdateOrder(orderNumber, order);
        }

        public IEnumerable<Order> GetOrders()
        {
            return (List<Order>)_orderManager.GetOrders();
        }

        public Order GetOrder(int orderNumber)
        {
            return _orderManager.GetOrder(orderNumber);
        }
    }
}
