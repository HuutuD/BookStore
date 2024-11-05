using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopBusiness;
using DTO;

namespace BookStoreRepository
{
    public interface IBookRepository
    {
        Task SaveBookAsync(Book b);
        Task<Book> GetBookByIdAsync(int id);
        Task DeleteBookAsync(Book b);
        Task UpdateBookAsync(Book b);
        Task<List<BookDTO>> GetBooksAsync();
    }

}
