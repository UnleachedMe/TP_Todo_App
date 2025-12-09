using GitDemoToDoApp.Services;
using GitDemoToDoApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GitDemoToDoApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService; // Injection du service

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.Authenticate(model.Username, model.Password);

                if (user != null)
                {
                    HttpContext.Session.SetString("username", user.Username);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Utilisateur ou mot de passe incorrect");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Append("LastLogout", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            return RedirectToAction("Login");
        }
    }
}
