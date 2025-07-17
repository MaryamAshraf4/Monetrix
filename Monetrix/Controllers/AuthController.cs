using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Monetrix.Models;
using Monetrix.ViewModels;
using System.Threading.Tasks;

namespace Monetrix.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel userVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppUser appUser = await _userManager.FindByNameAsync(userVm.UserName);
                    if (appUser != null)
                    {
                        bool found = await _userManager.CheckPasswordAsync(appUser, userVm.Password);
                        if (found)
                        {
                            await _signInManager.SignInAsync(appUser, userVm.RememberMe);
                            return RedirectToAction("Index", "Home");
                        }
                    }

                    ModelState.AddModelError("", "UserName and Password invalid");
                }
                return View(userVm);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
