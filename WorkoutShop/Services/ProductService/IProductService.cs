using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutShop.Models;
using Microsoft.AspNetCore.Http;

namespace WorkoutShop.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetFilteredProductsAsync(string searchTerm, int? categoryId, decimal? minPrice, decimal? maxPrice, string sortOrder);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task CreateProductAsync(Product product, string imageUrl);
        Task UpdateProductAsync(Product product, string imageUrl, int[] imagesToDelete);
        Task DeleteProductAsync(int id);
        bool ProductExists(int id);
    }
}
