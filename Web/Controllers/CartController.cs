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

        [HttpPost]

        public IActionResult CreeateCart(int UserId, List<string> booksTitle) 
        {  
            return Ok(_cartService.CreateCart(UserId, booksTitle));
        }
    }
}
