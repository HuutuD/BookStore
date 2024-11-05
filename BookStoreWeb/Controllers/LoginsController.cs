using BookShopBusiness;
using BookStoreRepository;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using BookStoreRepository;
using System.Threading.Tasks;

namespace eStoreClient.Controllers
{
    public class LoginsController : Controller
    {
        private readonly ApiService<User> _userRepository;
        private readonly string _usersAPIUrl;
        private readonly IConfiguration _configuration;

        public LoginsController(ApiService<User> userRepository,
                                IOptions<ApiUrls> apiUrls,
                                IConfiguration configuration)
        {
            _userRepository = userRepository;
            _usersAPIUrl = apiUrls.Value.UsersAPIUrl;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [BindProperty]
        public string? UserName { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            var defaultUserName = _configuration["Login:user"];
            var defaultPassword = _configuration["Login:pass"];

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (UserName == defaultUserName && Password == defaultPassword) 
            {
                HttpContext.Session.SetString("UserName", "Admin");
                HttpContext.Session.SetString("Type", "0");
                return Redirect("/");
            }

            List<User> users = await _userRepository.GetAllAsync(_usersAPIUrl);

            var user = users.FirstOrDefault(m => m.Username == UserName && m.Password == Password);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Invalid login attempt";
                return View();
            }

            HttpContext.Session.SetString("UserName", UserName);
            HttpContext.Session.SetString("Type", "1");
            return Redirect("/");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
