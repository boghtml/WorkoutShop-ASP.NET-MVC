using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutShop.Models;

namespace WorkoutShop.Services.OrderService
{
    public interface IOrderService
    {
        Task CreateOrderAsync(string userId);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId); 

        Task<IEnumerable<Order>> GetAllOrdersAsync(string status = null);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<Order> GetOrderByIdAsync(int orderId, string userId);

        Task<bool> UpdateOrderStatusAsync(int orderId, string status);
    }
}
