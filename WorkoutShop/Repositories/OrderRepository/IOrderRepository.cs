using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutShop.Models;

namespace WorkoutShop.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task SaveChangesAsync();
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task<IEnumerable<Order>> GetAllOrdersAsync(string status);
        Task<Order> GetOrderByIdAsync(int orderId);
    }
}
