using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PWalnik.SDManager.DataModel.Enums;
using PWalnik.SDManager.DataModel.Models;

namespace PWalnik.SDManager.Interfaces
{
    public interface IAdminService
    {
        Task<IdentityResult> CreateAsync(string userName, string email, string password, Role role);
        Task SaveAsync();
        Task<List<Employee>> GetAllEmployeeAsync();
        Task<Employee> GetEmployeebyId(string id);
        Task UpdateEmployee(Employee employee);
        Task DeleteUser(string id);
    }
}
