using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PWalnik.SDManager.DataModel.Data;
using PWalnik.SDManager.DataModel.Models;
using PWalnik.SDManager.DataModel.ViewModels;
using PWalnik.SDManager.Interfaces;

namespace PWalnik.SDManager.Services
{
    public class ClientService :IClientService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly int pageSize = 3;

        public ClientService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
        }


        public async Task<PaginatedList<Customer>> GetAllClientsAsync(string searchString, int? page)
        {
            int numberOfItems = await _context.Customers.CountAsync();

            var clients = _context.Customers.AsQueryable();

            if (String.IsNullOrEmpty(searchString))
            {               
                return await PaginatedList<Customer>.CreateAsync(clients.AsNoTracking(), page ?? 1, pageSize,
                    numberOfItems);
            }

            clients =  clients.Where(c=>c.CompanyName.Contains(searchString));
            return await PaginatedList<Customer>.CreateAsync(clients.AsNoTracking(), page ?? 1, pageSize, numberOfItems);
        }

        public async Task CreateAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetClientByIdAsync(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.CustomerId == id);
            if(customer == null)
                throw new Exception();
            return customer;
        }

        public async Task UpdateClientAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClient(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.CustomerId == id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
