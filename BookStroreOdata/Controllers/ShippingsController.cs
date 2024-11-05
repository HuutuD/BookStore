using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookShopBusiness;
using BookStoreRepository;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Results;

namespace BookStroreOdata.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class ShippingsController : ODataController
    {
        private readonly IShippingRepository _shippingRepository;

        public ShippingsController(IShippingRepository shippingRepository)
        {
            _shippingRepository = shippingRepository;
        }

        // GET: odata/Shippings
        [EnableQuery]
        [HttpGet]
        public IQueryable<Shipping> GetShippings()
        {
            return _shippingRepository.GetAllShippings().AsQueryable();
        }

        // GET: odata/Shippings/{id}
        [EnableQuery]
        [HttpGet("{id}")]
        public SingleResult<Shipping> GetShipping([FromODataUri] int id)
        {
            var shipping = _shippingRepository.GetShippingById(id);
            if (shipping == null)
            {
                return SingleResult.Create(Enumerable.Empty<Shipping>().AsQueryable());
            }

            return SingleResult.Create(new[] { shipping }.AsQueryable());
        }

        // POST: odata/Shippings
        [HttpPost]
        public async Task<IActionResult> PostShipping([FromBody] Shipping shipping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _shippingRepository.AddShipping(shipping);
            return Created(shipping);
        }

        // PUT: odata/Shippings/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipping([FromODataUri] int id, [FromBody] Shipping shipping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shipping.ShippingId)
            {
                return BadRequest();
            }

            try
            {
                _shippingRepository.UpdateShipping(shipping);
            }
            catch
            {
                if (_shippingRepository.GetShippingById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(shipping);
        }

        // DELETE: odata/Shippings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipping([FromODataUri] int id)
        {
            var shipping = _shippingRepository.GetShippingById(id);
            if (shipping == null)
            {
                return NotFound();
            }

            _shippingRepository.DeleteShipping(id);
            return NoContent();
        }
    }
}
