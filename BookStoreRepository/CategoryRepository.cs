using BookShopBusiness;
using BookShopDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        async Task<List<Category>> ICategoryRepository.GetCategoriesAsync() => await CategoryDAO.GetCategoriesAsync();

        private readonly CategoryDAO _categoryDAO;

        public CategoryRepository(BookDbContext context)
        {
            _categoryDAO = new CategoryDAO(context);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryDAO.GetAllCategories();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryDAO.GetCategoryById(id);
        }

        public void AddCategory(Category category)
        {
            _categoryDAO.AddCategory(category);
        }

        public void UpdateCategory(Category category)
        {
            _categoryDAO.UpdateCategory(category);
        }

        public void DeleteCategory(int id)
        {
            _categoryDAO.DeleteCategory(id);
        }
    }

}
