using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PWalnik.SDManager.DataModel.Models;
using PWalnik.SDManager.DataModel.ViewModels;

namespace PWalnik.SDManager.Interfaces
{
    public interface IClientService
    {
        Task<PaginatedList<Customer>> GetAllClientsAsync(string searchString, int? page);
        Task CreateAsync(Customer customer);
        Task<Customer> GetClientByIdAsync(int id);
        Task UpdateClientAsync(Customer customer);
        Task DeleteClient(int id);
    }
}
