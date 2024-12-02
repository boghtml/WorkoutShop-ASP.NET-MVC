using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutShop.Models;

namespace WorkoutShop.Repositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        Task SaveChangesAsync();
    }
}
