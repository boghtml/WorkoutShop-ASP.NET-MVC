using Moq;
using WorkoutShop.Models;
using WorkoutShop.Repositories.ProductRepository;
using WorkoutShop.Services.ProductService;
using Xunit;

namespace WorkoutShop.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetFilteredProductsAsync_ReturnsAllProducts()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { ProductId = 1, Name = "Dumbbell", Price = 50.00m, CategoryId = 1, Description = "Test" },
                new Product { ProductId = 2, Name = "Barbell", Price = 150.00m, CategoryId = 1, Description = "Test" },
                new Product { ProductId = 3, Name = "Treadmill", Price = 1200.00m, CategoryId = 2, Description = "Test" }
            };
            _mockRepository.Setup(repo => repo.GetFilteredProductsAsync(null, null, null, null, null))
                .ReturnsAsync(expectedProducts);

            // Act
            var result = await _productService.GetFilteredProductsAsync(null, null, null, null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Equal(expectedProducts, result);
            _mockRepository.Verify(repo => repo.GetFilteredProductsAsync(null, null, null, null, null), Times.Once);
        }

        [Fact]
        public async Task GetProductByIdAsync_ExistingId_ReturnsProduct()
        {
            // Arrange
            var expectedProduct = new Product 
            { 
                ProductId = 1, 
                Name = "Dumbbell", 
                Price = 50.00m, 
                CategoryId = 1,
                Description = "Premium quality dumbbell",
                StockQuantity = 10
            };
            _mockRepository.Setup(repo => repo.GetProductByIdAsync(1))
                .ReturnsAsync(expectedProduct);

            // Act
            var result = await _productService.GetProductByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ProductId);
            Assert.Equal("Dumbbell", result.Name);
            Assert.Equal(50.00m, result.Price);
            _mockRepository.Verify(repo => repo.GetProductByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetProductByIdAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetProductByIdAsync(999))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _productService.GetProductByIdAsync(999);

            // Assert
            Assert.Null(result);
            _mockRepository.Verify(repo => repo.GetProductByIdAsync(999), Times.Once);
        }

        [Fact]
        public async Task GetFilteredProductsAsync_WithCategoryFilter_ReturnsFilteredProducts()
        {
            // Arrange
            var categoryId = 1;
            var expectedProducts = new List<Product>
            {
                new Product { ProductId = 1, Name = "Dumbbell", Price = 50.00m, CategoryId = 1, Description = "Test" },
                new Product { ProductId = 2, Name = "Barbell", Price = 150.00m, CategoryId = 1, Description = "Test" }
            };
            _mockRepository.Setup(repo => repo.GetFilteredProductsAsync(null, categoryId, null, null, null))
                .ReturnsAsync(expectedProducts);

            // Act
            var result = await _productService.GetFilteredProductsAsync(null, categoryId, null, null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, p => Assert.Equal(categoryId, p.CategoryId));
            _mockRepository.Verify(repo => repo.GetFilteredProductsAsync(null, categoryId, null, null, null), Times.Once);
        }

        [Fact]
        public async Task GetFilteredProductsAsync_WithSearchTerm_ReturnsMatchingProducts()
        {
            // Arrange
            var searchTerm = "bell";
            var expectedProducts = new List<Product>
            {
                new Product { ProductId = 1, Name = "Dumbbell", Description = "Weight", Price = 50.00m, CategoryId = 1 },
                new Product { ProductId = 2, Name = "Barbell", Description = "Long bar", Price = 150.00m, CategoryId = 1 }
            };
            _mockRepository.Setup(repo => repo.GetFilteredProductsAsync(searchTerm, null, null, null, null))
                .ReturnsAsync(expectedProducts);

            // Act
            var result = await _productService.GetFilteredProductsAsync(searchTerm, null, null, null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _mockRepository.Verify(repo => repo.GetFilteredProductsAsync(searchTerm, null, null, null, null), Times.Once);
        }

        [Fact]
        public async Task GetFilteredProductsAsync_WithPriceRange_ReturnsFilteredProducts()
        {
            // Arrange
            decimal? minPrice = 50m;
            decimal? maxPrice = 200m;
            var expectedProducts = new List<Product>
            {
                new Product { ProductId = 1, Name = "Dumbbell", Price = 50.00m, CategoryId = 1, Description = "Test" },
                new Product { ProductId = 2, Name = "Barbell", Price = 150.00m, CategoryId = 1, Description = "Test" }
            };
            _mockRepository.Setup(repo => repo.GetFilteredProductsAsync(null, null, minPrice, maxPrice, null))
                .ReturnsAsync(expectedProducts);

            // Act
            var result = await _productService.GetFilteredProductsAsync(null, null, minPrice, maxPrice, null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, p => Assert.InRange(p.Price, minPrice.Value, maxPrice.Value));
            _mockRepository.Verify(repo => repo.GetFilteredProductsAsync(null, null, minPrice, maxPrice, null), Times.Once);
        }

        [Fact]
        public void ProductExists_ExistingId_ReturnsTrue()
        {
            // Arrange
            var productId = 1;
            _mockRepository.Setup(repo => repo.ProductExists(productId))
                .Returns(true);

            // Act
            var result = _productService.ProductExists(productId);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(repo => repo.ProductExists(productId), Times.Once);
        }

        [Fact]
        public void ProductExists_NonExistingId_ReturnsFalse()
        {
            // Arrange
            var productId = 999;
            _mockRepository.Setup(repo => repo.ProductExists(productId))
                .Returns(false);

            // Act
            var result = _productService.ProductExists(productId);

            // Assert
            Assert.False(result);
            _mockRepository.Verify(repo => repo.ProductExists(productId), Times.Once);
        }
    }
}
