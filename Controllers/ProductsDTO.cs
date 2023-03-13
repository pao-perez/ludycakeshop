using LudyCakeShop.Domain;
using System.Collections.Generic;

namespace LudyCakeShop.Controllers
{
    public class ProductsDTO
    {
        public int _id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public List<Product> products;
    }
}
