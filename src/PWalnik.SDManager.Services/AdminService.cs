using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using PWalnik.SDManager.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PWalnik.SDManager.DataModel.Data;
using PWalnik.SDManager.DataModel.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PWalnik.SDManager.DataModel.Enums;

namespace PWalnik.SDManager.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateAsync(string userName, string email, string password, Role role)
        {
            IdentityRole positionRole = new IdentityRole();
            if (role == Role.SalesRepresentative)
            {
                 positionRole = await _roleManager.FindByNameAsync("SalesRepresentative");
            }
            else
            {
                 positionRole = await _roleManager.FindByNameAsync("Manager");
            }


            var userRole = new IdentityUserRole<string>{RoleId = positionRole.Id};

            var user = new ApplicationUser() {UserName = userName, Email = email};
            user.Roles.Add(userRole);

            var employee = new Employee() {EmployeeId = user.Id, LastName = email};
            _context.Employyes.Add(employee);
            return await _userManager.CreateAsync(user, password);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            return await _context.Employyes.ToListAsync();
        }

        public async  Task<Employee> GetEmployeebyId(string id)
        {
            return await _context.Employyes.SingleOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            _context.Employyes.Update(employee);
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == employee.EmployeeId);
            if (user != null)
            {
                user.UserName = employee.FirstName;
                _context.Users.Update(user);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
