using BookShopBusiness;
using BookStoreWeb.Models;
using BookStoreWeb.Services;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreWeb.Controllers
{
    [AdminOnly]
    public class BooksController : Controller
    {
        private readonly ApiService<BookDTO> _bookDTOService;
        private readonly ApiService<Book> _bookService;
        private readonly ApiService<Category> _categoryService;
        private readonly string BooksAPIUrl;
        private readonly string CategoriesAPIUrl;
        public BooksController(
            ApiService<BookDTO> bookDTOService,
            ApiService<Book> bookService,
            ApiService<Category> categoryService,
            IOptions<ApiUrls> apiUrls
            )
        {
            _bookDTOService = bookDTOService;
            _bookService = bookService;
            _categoryService = categoryService;

            BooksAPIUrl = apiUrls.Value.BooksAPIUrl;
            CategoriesAPIUrl = apiUrls.Value.CategoriesAPIUrl;

            //_httpClient = httpClient;
            //_bookUrl = configuration["ApiSettings:BooksUrl"];  // API URL from appsettings.json
        }

        // GET: Books
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BookDTO> books = await _bookDTOService.GetAllAsync(BooksAPIUrl);

            if (books == null || !books.Any())
            {
                Console.WriteLine("Không có dữ liệu sách nào trả về từ API.");
                
            }
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<Category> categories = await _categoryService.GetAllAsync(CategoriesAPIUrl);
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = await _categoryService.GetAllAsync(CategoriesAPIUrl);
                ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
                return View(book);
            }

            bool isCreated = await _bookService.CreateAsync(BooksAPIUrl, book);
            if (isCreated)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error creating product. Please try again.");
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Book book = await _bookService.GetByIdAsync(BooksAPIUrl, id);
            if (book == null)
            {
                return NotFound();
            }

            List<Category> categories = await _categoryService.GetAllAsync(CategoriesAPIUrl);
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = await _categoryService.GetAllAsync(CategoriesAPIUrl);
                ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
                return View(book);
            }

            bool isUpdated = await _bookService.UpdateAsync(BooksAPIUrl, book, book.BookId);
            if (isUpdated)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error updating product. Please try again.");
            return View(book);
        }

        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _bookService.DeleteAsync(BooksAPIUrl, id);
            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int id)
        {

            Book book = await _bookService.GetByIdAsync(BooksAPIUrl, id);

            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        
    }
}
