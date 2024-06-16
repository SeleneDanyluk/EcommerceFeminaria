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

        //Obtener todos los libros
        public List<Book> GetAllBooks()
        {
            return _bookRepository.Get();
        }

        //Agregar un nuevo libro
        public BookDto AddNewBook(BookCreateRequest bookDto)
        {
            return BookDto.ToDto(_bookRepository.Create(BookCreateRequest.ToEntity(bookDto)));
        }

        //Obtener un libro por Id
        public BookDto GetBookById(int id)
        {

            return BookDto.ToDto(_bookRepository.Get(id));
        }

        //Eliminar un libro por id
        public void DeleteBook(int id)
        {
            _bookRepository.Delete(_bookRepository.Get(id));
        }

        //Obtener un libro por título
        public BookDto GetBookByTittle(string tittle) 
        {
            return BookDto.ToDto(_bookRepository.GetByTittle(tittle));
        }

        //Modificar un libro
        public void UpdateBook(BookCreateRequest bookDto)
        {
            BookDto.ToDto(_bookRepository.Update(BookCreateRequest.ToEntity(bookDto)));
        }

    }
}
