using BookShopBusiness;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookShopDataAccess
{
    public class BookDAO
    {
        private readonly BookDbContext _context;

        public BookDAO(BookDbContext context)
        {
            _context = context;
        }
        public static async Task<List<BookDTO>> GetBooksAsync()
        {

            try
            {
                using (var context = new BookDbContext())
                {
                    var books = await context.Books
                        .AsNoTracking()
                        .Select(p => new BookDTO
                        {
                            BookId = p.BookId,
                            BookName = p.BookName,
                            CategoryName = p.Category.CategoryName,
                            CategoryId = p.CategoryId,
                            Price = p.Price,

                        })
                        .ToListAsync();

                    return books;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static async Task<Book> FindBookByIdAsync(int bookdId)
        {
            Book book = null;
            try
            {
                using (var context = new BookDbContext())
                {
                    book = await context.Books.SingleOrDefaultAsync(x => x.BookId == bookdId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return book;
        }

        public static async Task SaveBookAsync(Book book)
        {
            try
            {
                using (var context = new BookDbContext())
                {
                    await context.Books.AddAsync(book);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task UpdateBookAsync(Book book)
        {
            try
            {
                using (var context = new BookDbContext())
                {
                    context.Entry(book).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task DeleteBookAsync(Book book)
        {
            try
            {
                using (var context = new BookDbContext())
                {
                    var existingBook = await context.Books.SingleOrDefaultAsync(c => c.BookId == book.BookId);
                    if (existingBook != null)
                    {
                        context.Books.Remove(existingBook);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
