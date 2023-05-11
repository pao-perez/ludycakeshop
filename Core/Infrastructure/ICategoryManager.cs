using LudyCakeShop.Core.Domain.Data;
using System.Collections.Generic;

namespace LudyCakeShop.Core.Infrastructure
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
