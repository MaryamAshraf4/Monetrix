using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Monetrix.IRepository;
using Monetrix.Models;
using Monetrix.ViewModels;

namespace Monetrix.Controllers
{
    public class AppUserController : Controller
    {
        private readonly IAppUserRepository _AppUserRepository;
        private readonly UserManager<AppUser> _userManager;

        public AppUserController(IAppUserRepository appUserRepository, UserManager<AppUser> userManager)
        {
            _AppUserRepository = appUserRepository;
            _userManager = userManager;
        }
        public async Task<ActionResult> Index(string? FullName)
        {
            ViewData["FullName"] = FullName;
            var appUsers = await _AppUserRepository.GetAllAppUsersAsync(FullName);
            return View(appUsers);
        }

        public async Task<ActionResult> Details(string id)
        {
            var appUser = await _AppUserRepository.GetAppUserByIdAsync(id);
            return View(appUser);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel appUserVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var appUser = new AppUser
                    {
                        UserName = appUserVm.UserName,
                        FullName = appUserVm.FullName,
                        Email = appUserVm.Email,
                        NationalId = appUserVm.NationalId,
                        Position = appUserVm.Position,
                        Salary = appUserVm.Salary,
                        HireDate = appUserVm.HireDate
                    };

                    var result = await _userManager.CreateAsync(appUser, appUserVm.Password);

                    if (result.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(appUser, appUser.Position.ToString());

                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction(nameof(Index));
                        }

                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                    return View(appUserVm);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            var appUser = await _AppUserRepository.GetAppUserByIdAsync(id);
            if (appUser == null)
                return NotFound();
            return View(appUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AppUser appUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _AppUserRepository.UpdateAppUserAsync(appUser);
                    return RedirectToAction(nameof(Index));
                }
                return View(appUser);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            var appUser = await _AppUserRepository.GetAppUserByIdAsync(id);
            if (appUser == null)
                return NotFound();
            return View(appUser); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                await _AppUserRepository.DeleteAppUserAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
