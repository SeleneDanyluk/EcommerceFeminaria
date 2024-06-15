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

        [HttpPost]
        public IActionResult AddBook([FromBody] BookCreateRequest book)
        {
            return Ok(_bookService.AddNewBook(book));
        }
    }
}
