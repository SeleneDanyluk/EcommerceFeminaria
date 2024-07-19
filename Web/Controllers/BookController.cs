using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using Domain.Entities;

namespace Web.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? titulo = null, [FromQuery] string? autor = null)
        {

            var books = _bookService.GetAllBooks(titulo, autor);

            if (books == null || !books.Any())
            {
                return NotFound(new { message = "No se encontraron libros con los criterios especificados." });
            }

            return Ok(books);
        }

        [HttpPost("/librosDelCarrito")]

        public IActionResult GetBooksByTitle([FromBody] List<string> books) 
        {
            return Ok(_bookService.GetBooksByTitle(books));
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            return Ok(_bookService.GetBookById(id));
        }

        [HttpGet("/tittle")]
        public IActionResult GetByTittle([FromQuery]string tittle)
        {
            return Ok(_bookService.GetBookByTittle(tittle));
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] BookCreateRequest book)
        {
            var userTypeString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (userTypeString == "admin")
            {
                return Ok(_bookService.AddNewBook(book));
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut]

        public IActionResult UpdateBook([FromQuery] string title, float price)
        {
            var userTypeString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (userTypeString == "admin")
            {
                _bookService.UpdateBook(title, price);
                return Ok(new { message = "Book updated successfully." });
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete]
        public IActionResult DeleteBook([FromQuery]int id) 
        {
            var userTypeString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (userTypeString == "admin")
            { 
                _bookService.DeleteBook(id);
                return Ok(new { message = "Book deleted successfully." });
            }
            else
            {
                return Forbid();
            }

        }
    }
}
