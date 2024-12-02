using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutShop.Models;
using WorkoutShop.Models;
using WorkoutShop.Repositories.OrderRepository; // Додано
using WorkoutShop.Services.ShoppingCartService;
using WorkoutShop.Repositories.OrderRepository;
using Microsoft.EntityFrameworkCore;
using WorkoutShop.DbContext;

namespace WorkoutShop.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IShoppingCartService _cartService;
        private readonly IOrderRepository _orderRepository;
        private readonly ApplicationDbContext _context;

        public OrderService(IShoppingCartService cartService, IOrderRepository orderRepository, ApplicationDbContext context)
        {
            _cartService = cartService;
            _orderRepository = orderRepository;
            _context = context;

        }

        public async Task CreateOrderAsync(string userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart != null && cart.CartItems.Any())
            {
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.UtcNow, // Використовуємо DateTime.UtcNow
                    Status = "Pending",
                    TotalPrice = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity),
                    CreatedAt = DateTime.UtcNow,
                    OrderItems = cart.CartItems.Select(ci => new OrderItem
                    {
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity,
                        Price = ci.Product.Price,
                        CreatedAt = DateTime.UtcNow
                    }).ToList()
                };

                await _orderRepository.AddOrderAsync(order);

                // Очистити кошик після створення замовлення
                cart.CartItems.Clear();
                await _cartService.SaveChangesAsync();

                await _orderRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _orderRepository.GetOrdersByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(string status)
        {
            return await _orderRepository.GetAllOrdersAsync(status);
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<Order> GetOrderByIdAsync(int orderId, string userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId && o.UserId == userId);
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return false;
            }
            order.Status = status;
            await _orderRepository.SaveChangesAsync();
            return true;
        }
    }
}
