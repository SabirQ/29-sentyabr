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
            return View();
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