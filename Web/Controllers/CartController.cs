using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var userTypeString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (userTypeString == "superAdmin")
            {
                return Ok(_cartService.GetCarts());
            }
            else
            {
                return Forbid();
            }
        }

        [HttpGet("/my-cart")]
        public IActionResult GetCartByUserId() 
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UnauthorizedException("User ID claim not found.");
            }
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new BadRequestException("Invalid user ID format.");
            }
            return Ok(_cartService.GetCartByUserId(userId));
        }

        [HttpGet("/myPurchases")]
        public IActionResult GetClientPurchases()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UnauthorizedException("User ID claim not found.");
            }
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new BadRequestException("Invalid user ID format.");
            }
                return Ok(_cartService.GetClientPurchases(userId));
        }

        [HttpPost("/addItem")]
        public IActionResult AddBookToCart([FromQuery] int bookId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UnauthorizedException("User ID claim not found.");
            }
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new BadRequestException("Invalid user ID format.");
            }
            return Ok(_cartService.AddBookToCart(userId,bookId));
        }

        [HttpDelete("/removeItem")]
        public IActionResult RemoveBookFromCart([FromQuery] int bookId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UnauthorizedException("User ID claim not found.");
            }
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new BadRequestException("Invalid user ID format.");
            }
            return Ok(_cartService.RemoveBookFromCart(userId, bookId));
        }

        [HttpPut("/purchase")]
        public IActionResult CreatePurchase()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UnauthorizedException("User ID claim not found.");
            }
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new BadRequestException("Invalid user ID format.");
            }
            return Ok(_cartService.ChangeCartState(userId));
        }
    }
}
