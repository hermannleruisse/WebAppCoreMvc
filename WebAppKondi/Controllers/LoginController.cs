using Microsoft.AspNetCore.Mvc;
using WebAppCoreMVC.Helpers;
using WebAppCoreMVC.Models;
using WebAppCoreMVC.Services;
using BCryptNet = BCrypt.Net.BCrypt;

namespace WebAppCoreMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Connexion(AuthenticateRequest auth)
        {
            if (ModelState.IsValid)
            {
                string pass = BCryptNet.HashPassword(auth.Password);
                var us = _userService.ObtenirElement(auth.Username).Result;
                if (us != null && BCryptNet.Verify(auth.Password, us.Password))
                {
                    //Session["UserId"] = us.Id;
                    //Session["Username"] = us.Username;
                    //Session["Role"] = us.Role;
                    //Session.Timeout = 2;
                    return RedirectToAction("Index", "Dashboard", new { area = "ADMIN" });
                }
            }
            ViewBag.Message = "Login ou mots de passe incorrecte";
            return View("Index");
        }
    }
}
