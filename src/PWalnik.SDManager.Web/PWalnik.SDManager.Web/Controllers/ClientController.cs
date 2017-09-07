using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWalnik.SDManager.DataModel.Models;
using PWalnik.SDManager.Interfaces;

namespace PWalnik.SDManager.Web.Controllers
{
    [Authorize(Roles = "SalesRepresentative, Manager")]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult>  Index(string currentFilter, string searchString, int? page)
        {
            if (searchString == null)
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewData["CurrentFilter"] = searchString;

            return View(await _clientService.GetAllClientsAsync(searchString, page));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyName, Country, Adress, City, PostalCode, Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _clientService.CreateAsync(customer);
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult>  Edit(int customerId)
        {
            return View(await _clientService.GetClientByIdAsync(customerId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _clientService.UpdateClientAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _clientService.GetClientByIdAsync(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clientService.DeleteClient(id);
            return RedirectToAction(nameof(Index));
        }
    }
}