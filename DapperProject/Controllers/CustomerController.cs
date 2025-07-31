using System.Threading.Tasks;
using DapperProject.Dtos.CustomerDtos;
using DapperProject.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DapperProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> CustomerList()
        {
            var values = await _customerService.GetAllCustomerAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            await _customerService.CreateCustomer(createCustomerDto);
            return RedirectToAction("CustomerList");
        }
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomer(id);
            return RedirectToAction("CustomerList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var values = await _customerService.GetCustomerById(id);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            await _customerService.UpdateCustomer(updateCustomerDto);
            return RedirectToAction("CustomerList");
        }
    }
}
