using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookShopBusiness;
using BookStoreRepository;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BookStroreOdata.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class CategoriesController : ODataController
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var Categories = await _categoryRepository.GetCategoriesAsync();
            if (Categories == null || !Categories.Any())
            {
                return BadRequest();
            }
            return Ok(Categories);
        }
        //// GET: odata/Categories
        //[HttpGet]
        //public ActionResult<IEnumerable<Category>> GetCategories()
        //{
        //    var categories = _categoryRepository.GetAllCategories();
        //    return Ok(categories);
        //}

        // GET: odata/Categories/{id}
        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // POST: odata/Categories
        [HttpPost]
        public IActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _categoryRepository.AddCategory(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }

        // PUT: odata/Categories/{id}
        [HttpPut("{id}")]
        public IActionResult PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            try
            {
                _categoryRepository.UpdateCategory(category);
            }
            catch
            {
                if (_categoryRepository.GetCategoryById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: odata/Categories/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            _categoryRepository.DeleteCategory(id);
            return NoContent();
        }
    }
}
