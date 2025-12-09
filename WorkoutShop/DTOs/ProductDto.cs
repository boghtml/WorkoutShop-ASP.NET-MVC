using System.Collections.Generic;

namespace WorkoutShop.DTOs
{
    /// <summary>
    /// Simplified Product DTO for API responses
    /// </summary>
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
