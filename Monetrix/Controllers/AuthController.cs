using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                            if (appUser.IsFirstLogin)
                            {
                                return RedirectToAction("ChangePassword");
                            }
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

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound();

            return View(user);
        }


        public async Task<ActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel changePasswordVm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                    return RedirectToAction("Login");

                var result = await _userManager.ChangePasswordAsync(user, changePasswordVm.CurrentPassword, changePasswordVm.NewPassword);

                if (result.Succeeded)
                {
                    user.IsFirstLogin = false;
                    await _userManager.UpdateAsync(user);
                    await _signInManager.RefreshSignInAsync(user);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(changePasswordVm);
        }
    }
}
