﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monetrix.IRepository;
using Monetrix.Models;

namespace Monetrix.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanRepository _loanRepository;
        public LoanController(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }
        [Authorize(Roles = "Admin, LoanOfficer, Auditor")]
        public async Task<ActionResult> Index()
        {
            var loan = await _loanRepository.GetAllLoansAsync();
            return View(loan);
        }
        [Authorize(Roles = "Admin, LoanOfficer, Auditor")]
        public async Task<ActionResult> Details(int id)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(id);

            if (loan == null)
                return NotFound();

            return View(loan);
        }
        [Authorize(Roles = "Admin, LoanOfficer")]
        public ActionResult Create(int customerId)
        {
            var loan = new Loan { 
                CustomerId = customerId
            };
            return View(loan);
        }
        [Authorize(Roles = "Admin, LoanOfficer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Loan loan)
        {
            try
            {
                ModelState.Remove("Customer");
                ModelState.Remove("AppUser");
                if (ModelState.IsValid)
                {
                    string? userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                    if (string.IsNullOrEmpty(userId))
                    {
                        return Unauthorized(); 
                    }

                    loan.AppUserId = userId;

                    await _loanRepository.AddLoanAsync(loan);
                    return RedirectToAction(nameof(Details), nameof(Customer), new { id = loan.CustomerId });
                }
                return View(loan);
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin, LoanOfficer")]
        public async Task<ActionResult> Edit(int id)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(id);

            if (loan == null)
                return NotFound();

            return View(loan);
        }
        [Authorize(Roles = "Admin, LoanOfficer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Loan loan)
        {
            try
            {
                ModelState.Remove("Customer");
                ModelState.Remove("AppUser");
                if (ModelState.IsValid)
                {
                    string? userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                    if (string.IsNullOrEmpty(userId))
                    {
                        return Unauthorized(); 
                    }

                    loan.AppUserId = userId;

                    await _loanRepository.UpdateLoanAsync(loan);
                    return RedirectToAction(nameof(Details), nameof(Customer), new { id = loan.CustomerId });
                }
                return View(loan);
            }
            catch
            {
                return View();
            }
        }
 
        [Authorize(Roles = "Admin, LoanOfficer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MarkasCompleted(int id, IFormCollection collection)
        {
            try
            {
                var loan = await _loanRepository.GetLoanByIdAsync(id);

                await _loanRepository.CompletedLoanAsync(id);
                return RedirectToAction(nameof(Details), nameof(Customer), new { id = loan.CustomerId });
            }
            catch
            {
                return View();
            }
        }
    }
}
