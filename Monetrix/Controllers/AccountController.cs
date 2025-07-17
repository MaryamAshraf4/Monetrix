using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monetrix.IRepository;
using Monetrix.Models;
using System.Threading.Tasks;

namespace Monetrix.Controllers
{
    [Authorize(Roles = "Admin, Accountant")]

    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [Authorize(Roles = "Admin, Teller, Accountant, Auditor")]
        public async Task<ActionResult> Index()
        {
            var account = await _accountRepository.GetAllAccountsAsync();
            return View(account);
        }
        [Authorize(Roles = "Admin, Teller, Accountant, Auditor")]
        public async Task<ActionResult> Details(int id)
        {
            var account = await _accountRepository.GetAccouuntByIdWithTransactionAsync(id);

            if (account == null)
                return NotFound();

            return View(account);
        }
        public ActionResult Create(int customerId)
        {
            var account = new Account
            {
                CustomerId = customerId
            };
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Account account)
        {
            try
            {
                ModelState.Remove("Customer");
                if (ModelState.IsValid)
                {                 
                    await _accountRepository.CreateAccountAsync(account);
                    return RedirectToAction("Details", "Customer", new { id = account.CustomerId });
                }
                return View(account);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);

            if (account == null)
                return NotFound();

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Account account)
        {
            try
            {
                ModelState.Remove("Customer");
                if (ModelState.IsValid)
                {
                    await _accountRepository.UpdateAccountAsync(account);
                    return RedirectToAction("Details", "Customer", new { id = account.CustomerId });
                }
                return View(account);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);

            if (account == null)
                return NotFound();

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var account = await _accountRepository.GetAccountByIdAsync(id);

                await _accountRepository.DeleteAccountAsync(id);
                return RedirectToAction("Details", "Customer", new { id = account.CustomerId });
            }
            catch
            {
                return View();
            }
        }
    }
}
