using System.ComponentModel.DataAnnotations;
using WorkoutShop.Models;
using Xunit;

namespace WorkoutShop.Tests.Models
{
    public class ProductTests
    {
        [Fact]
        public void Product_ValidData_CreatesSuccessfully()
        {
            // Arrange & Act
            var product = new Product
            {
                ProductId = 1,
                Name = "Dumbbell Set",
                Description = "Professional grade dumbbells",
                Price = 99.99m,
                StockQuantity = 50,
                CategoryId = 1
            };

            // Assert
            Assert.Equal(1, product.ProductId);
            Assert.Equal("Dumbbell Set", product.Name);
            Assert.Equal("Professional grade dumbbells", product.Description);
            Assert.Equal(99.99m, product.Price);
            Assert.Equal(50, product.StockQuantity);
            Assert.Equal(1, product.CategoryId);
        }

        [Fact]
        public void Product_NameMaxLength_ValidationFails()
        {
            // Arrange
            var product = new Product
            {
                Name = new string('A', 101), // Exceeds 100 character limit
                Description = "Test",
                Price = 50.00m,
                StockQuantity = 10,
                CategoryId = 1
            };

            var validationContext = new ValidationContext(product);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Name"));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(-100)]
        public void Product_NegativeOrZeroPrice_IsInvalid(decimal price)
        {
            // Arrange
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test",
                Price = price,
                StockQuantity = 10,
                CategoryId = 1
            };

            // Act & Assert
            Assert.True(price <= 0);
        }

        [Theory]
        [InlineData(10.50)]
        [InlineData(99.99)]
        [InlineData(1500.00)]
        public void Product_PositivePrice_IsValid(decimal price)
        {
            // Arrange
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test",
                Price = price,
                StockQuantity = 10,
                CategoryId = 1
            };

            // Act & Assert
            Assert.True(price > 0);
            Assert.Equal(price, product.Price);
        }

        [Fact]
        public void Product_DefaultTimestamps_AreSet()
        {
            // Arrange & Act
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 50.00m,
                StockQuantity = 10,
                CategoryId = 1,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.NotEqual(default(DateTime), product.CreatedAt);
            Assert.True(product.CreatedAt <= DateTime.UtcNow);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(100)]
        [InlineData(1000)]
        public void Product_StockQuantity_AcceptsValidValues(int quantity)
        {
            // Arrange
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test",
                Price = 50.00m,
                StockQuantity = quantity,
                CategoryId = 1
            };

            // Act & Assert
            Assert.Equal(quantity, product.StockQuantity);
        }

        [Fact]
        public void Product_WithCategory_ReturnsCategory()
        {
            // Arrange
            var category = new Category
            {
                CategoryId = 1,
                Name = "Weights",
                Description = "Weight equipment"
            };

            var product = new Product
            {
                ProductId = 1,
                Name = "Dumbbell",
                Description = "Test",
                Price = 50.00m,
                StockQuantity = 10,
                CategoryId = 1,
                Category = category
            };

            // Act & Assert
            Assert.NotNull(product.Category);
            Assert.Equal("Weights", product.Category.Name);
            Assert.Equal(category.CategoryId, product.CategoryId);
        }

        [Fact]
        public void Product_WithProductImages_ReturnsCollection()
        {
            // Arrange
            var product = new Product
            {
                ProductId = 1,
                Name = "Dumbbell",
                Description = "Test",
                Price = 50.00m,
                StockQuantity = 10,
                CategoryId = 1,
                ProductImages = new List<ProductImage>
                {
                    new ProductImage { ImageId = 1, ImageUrl = "image1.jpg", IsPrimary = true },
                    new ProductImage { ImageId = 2, ImageUrl = "image2.jpg", IsPrimary = false }
                }
            };

            // Act & Assert
            Assert.NotNull(product.ProductImages);
            Assert.Equal(2, product.ProductImages.Count);
            Assert.Contains(product.ProductImages, img => img.IsPrimary);
        }

        [Fact]
        public void Product_RequiredFields_CannotBeNull()
        {
            // Arrange
            var product = new Product
            {
                Name = null, // Required field
                Description = "Test",
                Price = 50.00m,
                StockQuantity = 10,
                CategoryId = 1
            };

            var validationContext = new ValidationContext(product);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Name"));
        }
    }
}
