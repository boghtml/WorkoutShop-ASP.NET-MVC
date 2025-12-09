namespace WorkoutShop.DTOs
{
    /// <summary>
    /// Response DTO for AI add to cart operation
    /// </summary>
    public class AiAddToCartResponse
    {
        /// <summary>
        /// Indicates if the operation was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Human-readable message about the operation result
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Updated cart item count after adding the product
        /// </summary>
        public int CartItemCount { get; set; }

        /// <summary>
        /// User ID (for reference)
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Product ID that was added
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Quantity that was added
        /// </summary>
        public int Quantity { get; set; }
    }
}
