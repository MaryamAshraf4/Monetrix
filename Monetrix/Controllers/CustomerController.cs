using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monetrix.InterFaces;
using Monetrix.IRepository;
using Monetrix.Models;

namespace Monetrix.Controllers
{
    [Authorize(Roles = "Admin, CustomerService")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUploadFile _uploadFile;
        public CustomerController(ICustomerRepository customerRepository, IUploadFile uploadFile)
        {
            _customerRepository = customerRepository;
            _uploadFile = uploadFile;
        }
        [AllowAnonymous]
        public async Task<ActionResult> Index(string? FullName)
        {
            ViewBag.FullName = FullName;
            var customers = await _customerRepository.GetAllCustomersAsync(FullName);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("~/Views/PartialViews/_CustomerTable.cshtml", customers);
            }

            return View(customers);
        }
        [AllowAnonymous]
        public async Task<ActionResult> Details(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdWithAccountsAndLoansAsync(id); ;
            if (customer == null)
                return NotFound();
            return View(customer);
        }

        public  ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (customer.ImageFile != null)
                    {
                        string filePath = await _uploadFile.UploadFileAsync("\\Images\\Customers\\", customer.ImageFile);
                        customer.Image = filePath;
                    }

                    await _customerRepository.AddCustomerAsync(customer);
                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (customer.ImageFile != null)
                    {
                        string filePath = await _uploadFile.UploadFileAsync("\\Images\\Customers\\", customer.ImageFile);
                        customer.Image = filePath;
                    }
                    await _customerRepository.UpdateCustomerAsync(customer);
                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _customerRepository.DeleteCustomerAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
