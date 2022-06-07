using Chat.Models;
using Chat.View_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Chat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

       public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Chat()
        {
            List<AppUser> users = _userManager.Users.ToList();
            return View(users);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task <IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser dbUser = _userManager.FindByNameAsync(loginVM.UserName).Result;
            if (dbUser == null)
            {
                ModelState.AddModelError("", "username or password is not valid");
            }

            SignInResult result = _signInManager.PasswordSignInAsync(dbUser, loginVM.Password, true, true).Result;

            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "username or password is not valid");
            }

            await _signInManager.SignInAsync(dbUser, true);

            return RedirectToAction("chat", "home");
        }

        public IActionResult Register()
        {
            AppUser user1 = new AppUser { UserName = "_fatima", Fullname = "Fatima" };
            AppUser user2 = new AppUser { UserName = "_eli", Fullname = "Eli" };
            AppUser user3 = new AppUser { UserName = "_ayna", Fullname = "Ayna" };


            var result1 = _userManager.CreateAsync(user1, "User123@").Result;
            var result2 = _userManager.CreateAsync(user2, "User123@").Result;
            var result3 = _userManager.CreateAsync(user3, "User123@").Result;

            return Content("userler created oldu");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index","home");
        }
    }
}
