using System;
using System.Collections.Generic; // Додано для ICollection
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WorkoutShop.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }  // Використовуємо автоінкрементний int

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock Quantity is required.")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }  // Використовуємо int для ForeignKey

        [ForeignKey("CategoryId")]
        [ValidateNever] // Додано атрибут, щоб ігнорувати валідацію цієї властивості
        public Category Category { get; set; }

        // Додано колекцію зображень продукту
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
