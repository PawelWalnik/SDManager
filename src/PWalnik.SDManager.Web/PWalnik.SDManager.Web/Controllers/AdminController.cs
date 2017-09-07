using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PWalnik.SDManager.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using PWalnik.SDManager.DataModel.Enums;
using PWalnik.SDManager.DataModel.Models;
using PWalnik.SDManager.Web.Models.AccountViewModels;

namespace PWalnik.SDManager.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ILogger _logger;

        public AdminController(IAdminService adminService,
            ILoggerFactory loggerFactory)
        {
            _adminService = adminService;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        public async Task<IActionResult> Index()
        {
            return View(await _adminService.GetAllEmployeeAsync());
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.CreateAsync(model.Email, model.Email, model.Password, model.Role);
                if (result.Succeeded)
                {
                    await _adminService.SaveAsync();
                    _logger.LogInformation(5,"Admin added new app user");
                    return RedirectToAction(nameof(Index));
                }
                AddErrors(result);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            return View(await _adminService.GetEmployeebyId(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _adminService.UpdateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult>  DeleteUser(string id)
        {
            return View(await _adminService.GetEmployeebyId(id));
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  DeleteUserConfirmed(string id)
        {
            await _adminService.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}