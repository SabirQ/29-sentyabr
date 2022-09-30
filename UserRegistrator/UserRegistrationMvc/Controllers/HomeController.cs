using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserRegistrationMvc.Filters;
using UserRegistrationMvc.Models;

namespace UserRegistrationMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Custom(RoleEnum.Member)]
        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username)) return NotFound();
            
            return View("Index",username);
        }
        [Custom(RoleEnum.Member)]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}