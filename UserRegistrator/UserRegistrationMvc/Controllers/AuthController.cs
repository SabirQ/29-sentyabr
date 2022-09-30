using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UserRegistrationMvc.DataContext;
using UserRegistrationMvc.Services;
using UserRegistrationMvc.ViewModels;

namespace UserRegistrationMvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private const string LOGIN_SESSION_KEY = "login";
        private const string LOGIN_SESSION_USERNAME = "username";
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _authService.GetUsers());
        }
        public IActionResult Register()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(LOGIN_SESSION_KEY)))
                return RedirectToAction("Index");
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM registerVM)
        {
            var result = await _authService.Register(registerVM);
            if (result != "success")
            {
                ModelState.AddModelError("", result);
                return View(registerVM);
            }
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM loginVM)
        {
            var result = await _authService.Login(loginVM);
            if (!result)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View(loginVM);
            }
            var user = await _authService.GetUserAsync(loginVM);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View(loginVM);
            }

            HttpContext.Session.SetString(LOGIN_SESSION_KEY,user.Role.Name);
            HttpContext.Session.SetString(LOGIN_SESSION_USERNAME, user.Username);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove(LOGIN_SESSION_KEY);
            HttpContext.Session.Remove(LOGIN_SESSION_USERNAME);
            return RedirectToAction(nameof(Login));
        }
    }
}
