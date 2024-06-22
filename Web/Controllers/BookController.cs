using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {
            return Ok(_bookService.GetAllBooks());
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

        [Authorize]
        [HttpPost]
        public IActionResult AddBook([FromBody] BookCreateRequest book)
        {
            return Ok(_bookService.AddNewBook(book));
        }

        [HttpPut]

        public IActionResult UpdateBook([FromBody] string title, float price)
        {
            try
            {
                _bookService.UpdateBook(title, price);
                return Ok(new { message = "Book updated successfully." });
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = "An error occurred while updating the book.", error = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteBook([FromQuery]int id) 
        {
            try
            {
                _bookService.DeleteBook(id);
                return Ok(new { message = "Book deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while deleting the book.", error = ex.Message });
            }

        }
    }
}
