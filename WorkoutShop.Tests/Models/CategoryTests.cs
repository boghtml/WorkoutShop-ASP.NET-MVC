using System.ComponentModel.DataAnnotations;
using WorkoutShop.Models;
using Xunit;

namespace WorkoutShop.Tests.Models
{
    public class CategoryTests
    {
        [Fact]
        public void Category_ValidData_CreatesSuccessfully()
        {
            // Arrange & Act
            var category = new Category
            {
                CategoryId = 1,
                Name = "Weights",
                Description = "All weight equipment",
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(1, category.CategoryId);
            Assert.Equal("Weights", category.Name);
            Assert.Equal("All weight equipment", category.Description);
            Assert.NotEqual(default(DateTime), category.CreatedAt);
        }

        [Fact]
        public void Category_NameMaxLength_ValidationFails()
        {
            // Arrange
            var category = new Category
            {
                Name = new string('A', 51), // Exceeds 50 character limit
                Description = "Test description"
            };

            var validationContext = new ValidationContext(category);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(category, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Name"));
        }

        [Theory]
        [InlineData("Weights")]
        [InlineData("Cardio")]
        [InlineData("Yoga & Pilates")]
        [InlineData("Supplements")]
        public void Category_ValidNames_CreatesSuccessfully(string name)
        {
            // Arrange & Act
            var category = new Category
            {
                Name = name,
                Description = $"{name} category"
            };

            // Assert
            Assert.Equal(name, category.Name);
        }

        [Fact]
        public void Category_RequiredName_CannotBeNull()
        {
            // Arrange
            var category = new Category
            {
                Name = null, // Required field
                Description = "Test description"
            };

            var validationContext = new ValidationContext(category);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(category, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Name"));
        }

        [Fact]
        public void Category_UpdatedAt_CanBeNull()
        {
            // Arrange & Act
            var category = new Category
            {
                CategoryId = 1,
                Name = "Test Category",
                Description = "Test",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            // Assert
            Assert.Null(category.UpdatedAt);
        }

        [Fact]
        public void Category_UpdatedAt_CanBeSet()
        {
            // Arrange
            var createdDate = DateTime.UtcNow.AddDays(-5);
            var updatedDate = DateTime.UtcNow;

            // Act
            var category = new Category
            {
                CategoryId = 1,
                Name = "Test Category",
                Description = "Test",
                CreatedAt = createdDate,
                UpdatedAt = updatedDate
            };

            // Assert
            Assert.NotNull(category.UpdatedAt);
            Assert.True(category.UpdatedAt > category.CreatedAt);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Category_EmptyOrWhitespaceName_ValidationFails(string name)
        {
            // Arrange
            var category = new Category
            {
                Name = name,
                Description = "Test"
            };

            var validationContext = new ValidationContext(category);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(category, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void Category_Description_CanBeLong()
        {
            // Arrange
            var longDescription = new string('A', 500);

            // Act
            var category = new Category
            {
                Name = "Test",
                Description = longDescription
            };

            // Assert
            Assert.Equal(500, category.Description.Length);
        }
    }
}
