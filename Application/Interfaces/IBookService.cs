using Application.Models.Requests;
using Application.Models;
using domain.Entities;
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

        public void UpdateBook(BookCreateRequest bookDto);

        public void DeleteBook(int id);
    }
}
