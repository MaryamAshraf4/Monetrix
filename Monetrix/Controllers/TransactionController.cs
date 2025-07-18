using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Monetrix.Enums;
using Monetrix.IRepository;
using Monetrix.Models;
using Monetrix.ViewModels;

namespace Monetrix.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionController(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }
        [Authorize(Roles = "Admin, Teller, Accountant, Auditor")]
        public async Task<ActionResult> Index()
        {
            var transaction = await _transactionRepository.GetAllTransactionsAsync();
            return View(transaction);
        }
        [Authorize(Roles = "Admin, Teller, Accountant, Auditor")]
        public async Task<ActionResult> Details(int id)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);

            if (transaction == null)
                return NotFound();

            return View(transaction);
        }
        [Authorize(Roles = "Admin, Teller")]
        public ActionResult Create(int accountId)
        {
            var transactionvm = new TransactionViewModel
            {
                SenderAccountId = accountId
            };
            return View(transactionvm);
        }
        [Authorize(Roles = "Admin, Teller")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TransactionViewModel transactionVm)
        {
            try
            {
                ModelState.Remove("SenderAccount");
                ModelState.Remove("ReceiverAccount");
                ModelState.Remove("AppUser");
                if (ModelState.IsValid)
                {
                    string? userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                    if (string.IsNullOrEmpty(userId))
                    {
                        return Unauthorized(); 
                    }

                    var success = await _transactionRepository.CreateTransactionAsync(transactionVm, userId);

                    if (!success)
                    {
                        ModelState.AddModelError("", "Transaction failed. Please check the data or account balance.");
                        return View(transactionVm);
                    }

                    return RedirectToAction("Details","Account", new { id = transactionVm.SenderAccountId});
                }
                return View(transactionVm);
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin, Teller")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reverse(int id)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound();

            string? userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var success = await _transactionRepository.ReverseTransactionAsync(id, userId);
            if (!success)
                return BadRequest("Failed to reverse transaction");

            return RedirectToAction("Details", "Account", new { id = transaction.SenderAccountId });
        }
    }
}
