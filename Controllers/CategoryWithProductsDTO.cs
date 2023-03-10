using LudyCakeShop.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudyCakeShop.Controllers
{
    public class CategoryWithProductsDTO
    {
        public int _id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public List<Product> products;
    }
}
