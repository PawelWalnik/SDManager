using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PWalnik.SDManager.DataModel.Data;
using PWalnik.SDManager.DataModel.Models;
using PWalnik.SDManager.DataModel.ViewModels;
using PWalnik.SDManager.Interfaces;

namespace PWalnik.SDManager.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly int pageSize = 3;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Order>> GetAllOrdersAsync(int customerId, int? page)
        {
            int numberOfItems = await _context.Orders.CountAsync();

            var orders = _context.Orders.AsQueryable();
            orders = orders.Where(o => o.CustomerId == customerId).OrderByDescending(o => o.OrderDate);

            return await PaginatedList<Order>.CreateAsync(orders.AsNoTracking(), page ?? 1, pageSize, numberOfItems);
        }

        public async Task AddNewOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders.SingleOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetClientByIdAsync(int id)
        {
            return await _context.Customers.SingleOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<bool> IsClientExists(int id)
        {
            var customer = await GetClientByIdAsync(id);

            if (customer == null)
                return false;

            return true;
        }

        public async Task<bool> IsOrderExists(int id)
        {
            var order = await GetOrderByIdAsync(id);

            if (order == null)
                return false;

            return true;
        }
    }
}
