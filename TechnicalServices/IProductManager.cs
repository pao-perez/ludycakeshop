using LudyCakeShop.Domain;
using System.Collections.Generic;

namespace LudyCakeShop.TechnicalServices
{
    public interface IProductManager
    {
        public IEnumerable<Product> GetProducts();
        public Product GetProduct(string productID);
        public string CreateProduct(Product product);
        public bool UpdateProduct(string productID, Product product);
        public bool DeleteProduct(string productID);
    }
}
