using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutShop.Models;
using WorkoutShop.Repositories.CategoryRepository;

namespace WorkoutShop.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task CreateCategoryAsync(Category category)
        {
            category.CreatedAt = DateTime.UtcNow;
            await _categoryRepository.AddCategoryAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            category.UpdatedAt = DateTime.UtcNow;
            _categoryRepository.UpdateCategory(category);
            await _categoryRepository.SaveChangesAsync();
        }


        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category != null)
            {
                _categoryRepository.DeleteCategory(category);
                await _categoryRepository.SaveChangesAsync();
            }
        }
    }
}
