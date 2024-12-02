using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkoutShop.Models;
using WorkoutShop.Services.ProductService;
using WorkoutShop.Services.CategoryService;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace WorkoutShop.Areas.Admin.Controllers
{
    [Area("Admin")] // Переконайтеся, що цей атрибут присутній

    public class ProductController : AdminBaseController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ICategoryService categoryService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _categoryService = categoryService;
            _logger = logger;
        }

        // GET: /Admin/Product
        public async Task<IActionResult> Index(string searchTerm, int? categoryId, decimal? minPrice, decimal? maxPrice, string sortOrder)
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            var products = await _productService.GetFilteredProductsAsync(searchTerm, categoryId, minPrice, maxPrice, sortOrder);
            return View(products);
        }


        // GET: /Admin/Product/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View();
        }

        // POST: /Admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, string ImageUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Attempting to create a new product.");
                    await _productService.CreateProductAsync(product, ImageUrl);
                    _logger.LogInformation("Product created successfully.");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating product.");
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
                }
            }
            else
            {
                _logger.LogWarning("Model state is invalid.");

                // Логування помилок ModelState
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        _logger.LogWarning($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }
            }
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View(product);
        }




        // GET: /Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, string ImageUrl, int[] ImagesToDelete)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Завантажуємо існуючий продукт з бази даних
                    var existingProduct = await _productService.GetProductByIdAsync(product.ProductId);
                    if (existingProduct == null)
                    {
                        return NotFound();
                    }

                    // Оновлюємо властивості продукту
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.StockQuantity = product.StockQuantity;
                    existingProduct.CategoryId = product.CategoryId;

                    // Оновлюємо продукт через сервіс
                    await _productService.UpdateProductAsync(existingProduct, ImageUrl, ImagesToDelete);

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_productService.ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View(product);
        }


        // GET: /Admin/Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: /Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
