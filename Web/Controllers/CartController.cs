using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return Ok(_cartService.GetCarts());
        }

        [HttpGet("/{userId}/my-cart")]
        public IActionResult GetCartByUserId(int userId) 
        {
            return Ok(_cartService.GetCartByUserId(userId));
        }

        [HttpPost("/{userId}/addItem")]
        public IActionResult AddBookToCart([FromRoute]int userId, [FromQuery] int bookId)
        {
            return Ok(_cartService.AddBookToCart(userId,bookId));
        }
       
    }
}
