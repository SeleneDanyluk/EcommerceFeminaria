using Application.Interfaces;
using Application.Models.Requests;
using Application.Services;
using domain.Entities;
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
            return Ok(_bookService.AddNewBook(book));
        }

        [HttpPut]

        public IActionResult UpdateBook([FromBody] BookCreateRequest book)
        {
            try
            {
                _bookService.UpdateBook(book);
                return Ok(new { message = "Book updated successfully." });
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = "An error occurred while updating the book.", error = ex.Message });
            }
        }

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
