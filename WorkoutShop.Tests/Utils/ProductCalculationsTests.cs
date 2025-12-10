using Xunit;
using WorkoutShop.Models;
using System.Collections.Generic;

namespace WorkoutShop.Tests.Utils
{
    public class ProductCalculationsTests
    {
        [Theory]
        [InlineData(100, 0.1, 110)]
        [InlineData(50, 0.2, 60)]
        [InlineData(200, 0.15, 230)]
        public void CalculatePriceWithTax_ValidInputs_ReturnsCorrectTotal(decimal price, decimal taxRate, decimal expected)
        {
            // Act
            var result = price * (1 + taxRate);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 10, 1000)]
        [InlineData(50.5, 2, 101)]
        [InlineData(25.99, 3, 77.97)]
        public void CalculateTotalPrice_PriceAndQuantity_ReturnsCorrectTotal(decimal price, int quantity, decimal expected)
        {
            // Act
            var result = price * quantity;

            // Assert
            Assert.Equal(expected, result, 2);
        }

        [Fact]
        public void Product_PriceCalculation_WithMultipleItems()
        {
            // Arrange
            var products = new List<(decimal price, int quantity)>
            {
                (50.00m, 2),
                (75.50m, 1),
                (30.25m, 3)
            };

            // Act
            decimal total = 0;
            foreach (var (price, quantity) in products)
            {
                total += price * quantity;
            }

            // Assert
            Assert.Equal(266.25m, total);
        }

        [Theory]
        [InlineData(100, 10, 90)]
        [InlineData(50, 20, 40)]
        [InlineData(200, 15, 170)]
        public void CalculateDiscount_ValidInputs_ReturnsDiscountedPrice(decimal price, decimal discountPercent, decimal expected)
        {
            // Act
            var discount = price * (discountPercent / 100);
            var result = price - discount;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsProductInStock_PositiveQuantity_ReturnsTrue()
        {
            // Arrange
            int stockQuantity = 10;

            // Act
            bool inStock = stockQuantity > 0;

            // Assert
            Assert.True(inStock);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void IsProductInStock_ZeroOrNegativeQuantity_ReturnsFalse(int stockQuantity)
        {
            // Act
            bool inStock = stockQuantity > 0;

            // Assert
            Assert.False(inStock);
        }

        [Theory]
        [InlineData(5, true)]
        [InlineData(10, false)]
        [InlineData(3, true)]
        [InlineData(15, false)]
        public void IsLowStock_ChecksThreshold_ReturnsCorrectStatus(int quantity, bool expected)
        {
            // Arrange
            const int threshold = 10;

            // Act
            bool isLowStock = quantity < threshold;

            // Assert
            Assert.Equal(expected, isLowStock);
        }

        [Fact]
        public void Product_ListFiltering_ByPriceRange()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product1", Price = 50m, Description = "Test", CategoryId = 1, StockQuantity = 10 },
                new Product { ProductId = 2, Name = "Product2", Price = 150m, Description = "Test", CategoryId = 1, StockQuantity = 10 },
                new Product { ProductId = 3, Name = "Product3", Price = 250m, Description = "Test", CategoryId = 1, StockQuantity = 10 }
            };
            decimal minPrice = 100m;
            decimal maxPrice = 200m;

            // Act
            var filtered = products.FindAll(p => p.Price >= minPrice && p.Price <= maxPrice);

            // Assert
            Assert.Single(filtered);
            Assert.Equal("Product2", filtered[0].Name);
        }

    }
}
