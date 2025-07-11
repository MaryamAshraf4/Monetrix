using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monetrix.IRepository;
using Monetrix.Models;

namespace Monetrix.Controllers
{
    public class AppUserController : Controller
    {
        private readonly IAppUserRepository _AppUserRepository;

        public AppUserController(IAppUserRepository appUserRepository)
        {
            _AppUserRepository = appUserRepository;
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
        public async Task<ActionResult> Create(AppUser appUser)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    await _AppUserRepository.CreateAppUserAsync(appUser);
                    return RedirectToAction(nameof(Index));
                }               
                return View(appUser);
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
