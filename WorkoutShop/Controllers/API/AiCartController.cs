using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkoutShop.DTOs;
using WorkoutShop.Models;
using WorkoutShop.Services.ShoppingCartService;

namespace WorkoutShop.Controllers.API
{
    /// <summary>
    /// AI Integration API Controller for external AI agents (e.g., Python LangGraph)
    /// Provides HTTP endpoints for AI to interact with user carts without cookie authentication
    /// </summary>
    [Route("api/ai/cart")]
    [ApiController]
    public class AiCartController : ControllerBase
    {
        private readonly IShoppingCartService _cartService;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AiCartController> _logger;

        public AiCartController(
            IShoppingCartService cartService,
            UserManager<User> userManager,
            IConfiguration configuration,
            ILogger<AiCartController> logger)
        {
            _cartService = cartService;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Adds a product to a user's cart via AI agent
        /// Uses email as user identifier and API key for authentication
        /// </summary>
        /// <param name="request">Request containing email, productId, quantity, and apiKey</param>
        /// <returns>Success/failure response with cart details</returns>
        /// <response code="200">Product successfully added to cart</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="401">Invalid or missing API key</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(AiAddToCartResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 401)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<IActionResult> AddToCart([FromBody] AiAddToCartRequest request)
        {
            try
            {
                // 1. Validate model state
                if (!ModelState.IsValid)
                {
                    var errors = string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

                    _logger.LogWarning("AI AddToCart: Invalid model state - {Errors}", errors);
                    return BadRequest(new ErrorResponse
                    {
                        Error = "ValidationError",
                        Message = $"Invalid request data: {errors}",
                        StatusCode = 400
                    });
                }

                // 2. Validate API Key
                var configuredApiKey = _configuration["AiIntegration:ApiKey"];
                if (string.IsNullOrEmpty(configuredApiKey))
                {
                    _logger.LogError("AI AddToCart: AiIntegration:ApiKey not configured in appsettings");
                    return StatusCode(500, new ErrorResponse
                    {
                        Error = "ConfigurationError",
                        Message = "AI integration is not properly configured",
                        StatusCode = 500
                    });
                }

                if (request.ApiKey != configuredApiKey)
                {
                    _logger.LogWarning("AI AddToCart: Invalid API key provided for email {Email}", request.Email);
                    return Unauthorized(new ErrorResponse
                    {
                        Error = "UnauthorizedError",
                        Message = "Invalid API key. Access denied.",
                        StatusCode = 401
                    });
                }

                // 3. Find user by email
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    _logger.LogWarning("AI AddToCart: User not found with email {Email}", request.Email);
                    return NotFound(new ErrorResponse
                    {
                        Error = "UserNotFound",
                        Message = $"User with email '{request.Email}' not found",
                        StatusCode = 404
                    });
                }

                // 4. Add product to cart using existing cart service
                await _cartService.AddToCartAsync(user.Id, request.ProductId, request.Quantity);

                // 5. Get updated cart item count
                var cartItemCount = await _cartService.GetCartItemCountAsync(user.Id);

                _logger.LogInformation(
                    "AI AddToCart: Successfully added ProductId={ProductId} Quantity={Quantity} to cart for User={Email}",
                    request.ProductId, request.Quantity, request.Email);

                // 6. Return success response
                return Ok(new AiAddToCartResponse
                {
                    Success = true,
                    Message = $"Successfully added {request.Quantity} item(s) of product {request.ProductId} to cart",
                    CartItemCount = cartItemCount,
                    UserId = user.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                });
            }
            catch (ArgumentException ex)
            {
                // Product not found or validation error from service layer
                _logger.LogWarning(ex, "AI AddToCart: Argument exception - {Message}", ex.Message);
                return BadRequest(new ErrorResponse
                {
                    Error = "InvalidArgument",
                    Message = ex.Message,
                    StatusCode = 400
                });
            }
            catch (InvalidOperationException ex)
            {
                // Business logic error (e.g., out of stock)
                _logger.LogWarning(ex, "AI AddToCart: Operation exception - {Message}", ex.Message);
                return BadRequest(new ErrorResponse
                {
                    Error = "OperationFailed",
                    Message = ex.Message,
                    StatusCode = 400
                });
            }
            catch (Exception ex)
            {
                // Unexpected error
                _logger.LogError(ex, "AI AddToCart: Unexpected error while adding to cart");
                return StatusCode(500, new ErrorResponse
                {
                    Error = "InternalServerError",
                    Message = "An unexpected error occurred while processing your request",
                    StatusCode = 500
                });
            }
        }
    }
}
