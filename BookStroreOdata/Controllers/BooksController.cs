using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BookShopBusiness;
using BookStoreRepository;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using DTO;

namespace BookStroreOdata.Controllers
{
    [Route("odata/[controller]")]
    public class BooksController : ODataController
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: odata/Books
        [EnableQuery] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var books = await _bookRepository.GetBooksAsync();
            return Ok(books);
        }

        // GET: odata/Books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return BadRequest();
            }

            return Ok(book);
        }

        // POST: odata/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
        {
            await _bookRepository.SaveBookAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
        }

        // PUT: odata/Books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, [FromBody] Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            try
            {
                await _bookRepository.UpdateBookAsync(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BookExists(id))
                {
                    return BadRequest();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: odata/Books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return BadRequest();
            }

            await _bookRepository.DeleteBookAsync(book);
            return NoContent();
        }
        private async Task<bool> BookExists(int id)
        {
            var Book = await _bookRepository.GetBookByIdAsync(id);
            return Book != null;
        }
    }
}
