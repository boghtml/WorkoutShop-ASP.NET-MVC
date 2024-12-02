using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutShop.Models;

namespace WorkoutShop.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetFilteredProductsAsync(string searchTerm, int? categoryId, decimal? minPrice, decimal? maxPrice, string sortOrder);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<List<ProductImage>> GetProductImagesByIdsAsync(int[] imageIds);
        Task AddProductAsync(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Task SaveChangesAsync();
        bool ProductExists(int id);
    }
}
