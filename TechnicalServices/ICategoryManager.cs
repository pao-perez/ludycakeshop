using LudyCakeShop.Domain;
using System.Collections.Generic;

namespace LudyCakeShop.TechnicalServices
{
    public interface ICategoryManager
    {
        public IEnumerable<Category> GetCategories();
        public Category GetCategory(string categoryID);
        public string CreateCategory(Category category);
        public bool DeleteCategory(string categoryID);
        public bool UpdateCategory(string categoryID, Category category);
    }
}
