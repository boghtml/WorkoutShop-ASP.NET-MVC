using System.Threading.Tasks;
using WorkoutShop.Models;

namespace WorkoutShop.Repositories.ShoppingCartRepository
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetCartByUserIdAsync(string userId);
        Task AddCartAsync(ShoppingCart cart);
        Task AddCartItemAsync(CartItem cartItem);
        Task<CartItem> GetCartItemAsync(int cartItemId);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task RemoveCartItemAsync(CartItem cartItem);
        Task SaveChangesAsync();
    }
}
