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
        private readonly EmailManager _emailManager;

        public LCS()
        {
            _categoryManager = new();
            _productManager = new();
            _orderManager = new();
            _emailManager = new();
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

        public IEnumerable<Order> GetOrdersByCustomerContactNumber(string customerContactNumber)
        {
            return _orderManager.GetOrdersByCustomerContactNumber(customerContactNumber);
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
            // TODO: check product quantity count if enough for each orderItem ItemQuantity
            //TODO: compute subTotal, saleTotal, and GST
            order.OrderStatus = OrderStatus.SUBMITTED;
            order.OrderID = Guid.NewGuid().ToString();
            _orderManager.CreateOrder(order);
            _emailManager.SendEmail(new OrderEmailMessage(order));

            return order.OrderID;
        }

        public bool UpdateOrder(string orderID, Order order)
        {
            // TODO: check product quantity count if enough for each orderItem ItemQuantity
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

        public string CreateBulkOrder(BulkOrder bulkOrder)
        {
            // TODO: check product quantity count if enough for each orderItem ItemQuantity
            //TODO: compute subTotal, saleTotal, and GST
            bulkOrder.BulkOrderStatus = BulkOrderStatus.SUBMITTED;
            bulkOrder.OrderID = Guid.NewGuid().ToString();
            _orderManager.CreateBulkOrder(bulkOrder);
            _emailManager.SendEmail(new BulkOrderEmailMessage(bulkOrder));

            return bulkOrder.OrderID;
        }

        public bool UpdateBulkOrder(string orderID, BulkOrder bulkOrder)
        {
            // TODO: check product quantity count if enough for each orderItem ItemQuantity
            return _orderManager.UpdateBulkOrder(orderID, bulkOrder);
        }

        public IEnumerable<BulkOrder> GetBulkOrders()
        {
            return (List<BulkOrder>)_orderManager.GetBulkOrders();
        }

        public BulkOrder GetBulkOrder(string orderID)
        {
            return _orderManager.GetBulkOrder(orderID);
        }
    }
}
