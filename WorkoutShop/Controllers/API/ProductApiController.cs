using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutShop.DTOs;
using WorkoutShop.Services.ProductService;

namespace WorkoutShop.Controllers.API
{
    /// <summary>
    /// Public API for reading product data (for AI agents and external services)
    /// </summary>
    [Route("api/products")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductApiController> _logger;

        public ProductApiController(IProductService productService, ILogger<ProductApiController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Get all products or filter by search criteria
        /// </summary>
        /// <param name="searchTerm">Optional search term for product name/description</param>
        /// <param name="categoryId">Optional category filter</param>
        /// <param name="minPrice">Optional minimum price filter</param>
        /// <param name="maxPrice">Optional maximum price filter</param>
        /// <param name="sortOrder">Optional sort order (price_asc, price_desc, name_asc, name_desc)</param>
        /// <returns>List of products</returns>
        /// <response code="200">Returns list of products</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ProductDto>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<IActionResult> GetProducts(
            [FromQuery] string searchTerm = null,
            [FromQuery] int? categoryId = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] string sortOrder = null)
        {
            try
            {
                IEnumerable<WorkoutShop.Models.Product> products;

                if (!string.IsNullOrEmpty(searchTerm) || categoryId.HasValue || minPrice.HasValue || maxPrice.HasValue || !string.IsNullOrEmpty(sortOrder))
                {
                    products = await _productService.GetFilteredProductsAsync(searchTerm, categoryId, minPrice, maxPrice, sortOrder);
                }
                else
                {
                    products = await _productService.GetAllProductsAsync();
                }

                var productDtos = products.Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category?.Name,
                    ImageUrls = p.ProductImages?.Select(img => img.ImageUrl).ToList() ?? new List<string>()
                }).ToList();

                _logger.LogInformation("ProductAPI: Returned {Count} products", productDtos.Count);
                return Ok(productDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProductAPI: Error fetching products");
                return StatusCode(500, new ErrorResponse
                {
                    Error = "InternalServerError",
                    Message = "An error occurred while fetching products",
                    StatusCode = 500
                });
            }
        }

        /// <summary>
        /// Get a single product by ID
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product details</returns>
        /// <response code="200">Returns product details</response>
        /// <response code="404">Product not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);

                if (product == null)
                {
                    _logger.LogWarning("ProductAPI: Product not found with ID {ProductId}", id);
                    return NotFound(new ErrorResponse
                    {
                        Error = "ProductNotFound",
                        Message = $"Product with ID {id} not found",
                        StatusCode = 404
                    });
                }

                var productDto = new ProductDto
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    StockQuantity = product.StockQuantity,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category?.Name,
                    ImageUrls = product.ProductImages?.Select(img => img.ImageUrl).ToList() ?? new List<string>()
                };

                _logger.LogInformation("ProductAPI: Returned product {ProductId}", id);
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProductAPI: Error fetching product {ProductId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    Error = "InternalServerError",
                    Message = "An error occurred while fetching the product",
                    StatusCode = 500
                });
            }
        }
    }
}
