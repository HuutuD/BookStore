using BookShopBusiness;
using BookShopDataAccess;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository
{
    public class BookRepository : IBookRepository
    {
        public async Task DeleteBookAsync(Book b) => await BookDAO.DeleteBookAsync(b);
        public async Task<List<Category>> GetCategoriesAsync() => await CategoryDAO.GetCategoriesAsync();
        public async Task<Book> GetBookByIdAsync(int id) => await BookDAO.FindBookByIdAsync(id);
        public async Task<List<BookDTO>> GetBooksAsync() => await BookDAO.GetBooksAsync();
        public async Task SaveBookAsync(Book b) => await BookDAO.SaveBookAsync(b);
        public async Task UpdateBookAsync(Book b) => await BookDAO.UpdateBookAsync(b);

        
    }

}
