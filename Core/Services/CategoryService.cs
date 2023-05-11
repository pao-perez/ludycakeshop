using LudyCakeShop.Core.Domain.Data;
using LudyCakeShop.Core.Infrastructure;
using System.Collections.Generic;

namespace LudyCakeShop.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryService(ICategoryManager categoryManager)
        {
            this._categoryManager = categoryManager;
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
    }
}
