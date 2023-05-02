using LudyCakeShop.Domain;
using System.Collections.Generic;

namespace LudyCakeShop.Services
{
    public interface IProductService
    {
        public string CreateProduct(Product product);
        public bool UpdateProduct(string productID, Product product);
        public Product GetProduct(string productID);
        public IEnumerable<Product> GetProducts();
        public bool DeleteProduct(string productID);
    }
}
