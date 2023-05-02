﻿using LudyCakeShop.Domain;
using LudyCakeShop.TechnicalServices;
using System.Collections.Generic;

namespace LudyCakeShop.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductManager _productManager;

        public ProductService(IProductManager productManager)
        {
            this._productManager = productManager;
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
    }
}
