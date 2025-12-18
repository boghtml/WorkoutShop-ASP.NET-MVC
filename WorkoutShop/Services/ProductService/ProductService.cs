using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutShop.Models;
using WorkoutShop.Repositories.ProductRepository;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace WorkoutShop.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(string searchTerm, int? categoryId, decimal? minPrice, decimal? maxPrice, string sortOrder)
        {
            return await _productRepository.GetFilteredProductsAsync(searchTerm, categoryId, minPrice, maxPrice, sortOrder);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task CreateProductAsync(Product product, string imageUrl)
        {
            product.CreatedAt = DateTime.UtcNow;
            if (product.ProductImages == null)
            {
                product.ProductImages = new List<ProductImage>();
            }
            if (!string.IsNullOrEmpty(imageUrl))
            {
                product.ProductImages.Add(new ProductImage
                {
                    ImageUrl = imageUrl,
                    IsPrimary = true,
                    CreatedAt = DateTime.UtcNow,
                    ProductId = product.ProductId 
                });
            }

            await _productRepository.AddProductAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product, string imageUrl, int[] imagesToDelete)
        {
            product.UpdatedAt = DateTime.UtcNow;

            if (imagesToDelete != null && imagesToDelete.Length > 0)
            {
                var images = product.ProductImages.Where(pi => imagesToDelete.Contains(pi.ImageId)).ToList();
                foreach (var image in images)
                {
                    product.ProductImages.Remove(image);

                }
            }

            if (!string.IsNullOrEmpty(imageUrl))
            {
                if (product.ProductImages == null)
                {
                    product.ProductImages = new List<ProductImage>();
                }
                product.ProductImages.Add(new ProductImage
                {
                    ImageUrl = imageUrl,
                    IsPrimary = false,
                    CreatedAt = DateTime.UtcNow
                });
            }

            _productRepository.UpdateProduct(product);
            await _productRepository.SaveChangesAsync();
        }


        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product != null)
            {
                if (product.ProductImages != null)
                {
                    foreach (var image in product.ProductImages)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.ImageUrl.TrimStart('/'));
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                }

                _productRepository.DeleteProduct(product);
                await _productRepository.SaveChangesAsync();
            }
        }

        public bool ProductExists(int id)
        {
            return _productRepository.ProductExists(id);
        }
    }
}
