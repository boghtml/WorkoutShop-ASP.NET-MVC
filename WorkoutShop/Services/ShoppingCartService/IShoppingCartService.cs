using System.Threading.Tasks;
using WorkoutShop.Models;

namespace WorkoutShop.Services.ShoppingCartService
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetCartByUserIdAsync(string userId);
        Task AddToCartAsync(string userId, int productId, int quantity);
        Task RemoveFromCartAsync(string userId, int cartItemId);
        Task UpdateCartItemQuantityAsync(string userId, int cartItemId, int quantity);
        Task<int> GetCartItemCountAsync(string userId);
        Task SaveChangesAsync();
    }
}
