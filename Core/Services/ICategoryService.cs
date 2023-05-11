using LudyCakeShop.Core.Domain.Data;
using System.Collections.Generic;

namespace LudyCakeShop.Core.Services
{
    public interface ICategoryService
    {
        public bool DeleteCategory(string categoryID);
        public bool UpdateCategory(string categoryID, Category category);
        public string CreateCategory(Category category);
        public Category GetCategory(string categoryID);
        public IEnumerable<Category> GetCategories();
    }
}
