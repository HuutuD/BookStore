using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopBusiness;
using Microsoft.EntityFrameworkCore;

namespace BookShopDataAccess
{
    public class CategoryDAO
    {
        public static async Task<List<Category>> GetCategoriesAsync()
        {
            List<Category> Categorys = new List<Category>();
            try
            {
                using (var context = new BookDbContext())
                {
                    Categorys = await context.Categories.AsNoTracking().ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Categorys;
        }
        private readonly BookDbContext _context;

        public CategoryDAO(BookDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.Find(id);
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }


}
