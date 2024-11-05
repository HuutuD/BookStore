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
    public class UsersController : ODataController
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: odata/Users
        [EnableQuery]
        [HttpGet]
        public IQueryable<User> GetUsers()
        {
            return _userRepository.GetAllUsers().AsQueryable();
        }

        // GET: odata/Users/{id}
        [EnableQuery]
        [HttpGet("{id}")]
        public SingleResult<User> GetUser([FromODataUri] int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return SingleResult.Create(Enumerable.Empty<User>().AsQueryable());
            }

            return SingleResult.Create(new[] { user }.AsQueryable());
        }

        // POST: odata/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userRepository.AddUser(user);
            return Created(user);
        }

        // PUT: odata/Users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromODataUri] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            try
            {
                _userRepository.UpdateUser(user);
            }
            catch
            {
                if (_userRepository.GetUserById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(user);
        }

        // DELETE: odata/Users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromODataUri] int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.DeleteUser(id);
            return NoContent();
        }
    }
}
