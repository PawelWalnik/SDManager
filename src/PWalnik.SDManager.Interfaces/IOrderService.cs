using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PWalnik.SDManager.DataModel.Models;
using PWalnik.SDManager.DataModel.ViewModels;

namespace PWalnik.SDManager.Interfaces
{
    public interface IOrderService
    {
        Task<PaginatedList<Order>> GetAllOrdersAsync(int customerId, int? page);
        Task AddNewOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Order order);
        Task<Customer> GetClientByIdAsync(int id);
        Task<bool> IsClientExists(int id);
        Task<bool> IsOrderExists(int id);
    }
}
