using System.ComponentModel.DataAnnotations;

namespace WorkoutShop.DTOs
{
    /// <summary>
    /// Request DTO for AI agent to add products to a user's cart
    /// </summary>
    public class AiAddToCartRequest
    {
        /// <summary>
        /// User's email address (used as identifier since AI doesn't have access to Identity)
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        /// <summary>
        /// Product ID to add to cart
        /// </summary>
        [Required(ErrorMessage = "ProductId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ProductId must be greater than 0")]
        public int ProductId { get; set; }

        /// <summary>
        /// Quantity of the product to add
        /// </summary>
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }

        /// <summary>
        /// Secret API key for AI integration authentication
        /// </summary>
        [Required(ErrorMessage = "ApiKey is required")]
        public string ApiKey { get; set; }
    }
}
