using Application.Models.Requests;
using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookService
    {
        List<Book> GetAllBooks();

        BookDto AddNewBook(BookCreateRequest bookDto);

        public BookDto GetBookById(int id);

        public BookDto GetBookByTittle(string tittle);

        public BookDto UpdateBook(string title, float price);

        public void DeleteBook(int id);

        public List<BookDto> GetBooksByTitle(List<string> titles);
    }
}
