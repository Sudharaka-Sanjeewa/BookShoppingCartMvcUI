using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShoppingCartMvcUI.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _customerRepo.GetCustomers();
            return View(customers);
        }

        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerDTO customer)
        {
            if(!ModelState.IsValid)
            {
                return View(customer);
            }
            try
            {
                var customerToAdd = new Customer { Email = customer.Email, Mobile = customer.Mobile, Id = customer.Id };
                await _customerRepo.AddCustomer(customerToAdd);
                TempData["successMessage"] = "Customer added successfully";
                return RedirectToAction(nameof(AddCustomer));
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = "Customer could not added!";
                return View(customer);
            }

        }

        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var customer = await _customerRepo.GetCustomerById(id);
            if (customer is null)
                throw new InvalidOperationException($"Customer with id: {id} does not found");
            var customerToUpdate = new CustomerDTO
            {
                Id = customer.Id,
                Email = customer.Email,
                Mobile = customer.Mobile
            };
            return View(customerToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(CustomerDTO customerToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(customerToUpdate);
            }
            try
            {
                var customer = new Customer { Email = customerToUpdate.Email, Mobile = customerToUpdate.Mobile, Id = customerToUpdate.Id };
                await _customerRepo.UpdateCustomer(customer);
                TempData["successMessage"] = "Customer is updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Customer could not updated!";
                return View(customerToUpdate);
            }

        }

        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerRepo.GetCustomerById(id);
            if (customer is null)
                throw new InvalidOperationException($"Customer with id: {id} does not found");
            await _customerRepo.DeleteCustomer(customer);
            return RedirectToAction(nameof(Index));

        }

    }
}
