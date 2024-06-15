using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
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
            return _bookRepository.Get();
        }

        public BookDto AddNewBook(BookCreateRequest bookDto)
        {
            return BookDto.ToDto(_bookRepository.AddBook(BookCreateRequest.ToEntity(bookDto)));
        }

    }
}
