namespace WorkoutShop.DTOs
{
    /// <summary>
    /// Standard error response for API endpoints
    /// </summary>
    public class ErrorResponse
    {
        public bool Success { get; set; } = false;
        public string Error { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
