using Application.Interfaces;
using domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService (IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public Book AddNewBook(string title, string description, string author, float price, int stock)
        {
            var newBook = new Book
            {
                Title = title,
                Description = description,
                Author = author,
                Price = price,
                Stock = stock
            };

            _bookRepository.AddBook(newBook);

            return(newBook);
        }

    }
}
