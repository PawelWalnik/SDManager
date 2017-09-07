using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWalnik.SDManager.DataModel.Models;
using PWalnik.SDManager.Interfaces;
using PWalnik.SDManager.Services.Requirements;

namespace PWalnik.SDManager.Web.Controllers
{
    [Authorize(Roles = "SalesRepresentative, Manager")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IAuthorizationService _authorizationService;

        public OrderController(IOrderService orderService, IAuthorizationService authorizationService)
        {
            _orderService = orderService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id, int? page)
        {
            if (!await _orderService.IsClientExists(id))
                return NotFound();

            ViewData["CustomerId"] = id;
            return View(await _orderService.GetAllOrdersAsync(id, page));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return NotFound();

            if (!await _orderService.IsClientExists(id))
                return NotFound();

            Order order = new Order {CustomerId = id, EmployeeId = userId};
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.AddNewOrderAsync(order);
                return RedirectToAction("Index", new {id = order.CustomerId});
            }
            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!await _orderService.IsOrderExists(id))
                return NotFound();

            var order = await _orderService.GetOrderByIdAsync(id);

            if (await _authorizationService.AuthorizeAsync(User, order, new EditOrderRequirement()))
            {
                return View(order);
            }

            return new ChallengeResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.UpdateOrderAsync(order);
                return RedirectToAction("Index", new {id = order.CustomerId});
            }
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _orderService.IsOrderExists(id))
                return NotFound();

            var order = await _orderService.GetOrderByIdAsync(id);

            if (await _authorizationService.AuthorizeAsync(User, order, new EditOrderRequirement()))
            {
                await _orderService.DeleteOrderAsync(order);
                return RedirectToAction("Index", new { id = order.CustomerId });
            }

            return new ChallengeResult();
        }
    }
}