using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Exceptions.NotAllowedExceptions;

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
            var existingBook = _bookRepository.GetByTittle(bookDto.Title);
            if (existingBook != null)
            {
                throw new NotAllowedException("El titulo ya se encuentra en el sistema.");
            }

            var newBook = _bookRepository.Create(BookCreateRequest.ToEntity(bookDto));

            return BookDto.ToDto(newBook);
        }

        //Obtener un libro por Id
        public BookDto GetBookById(int id)
        {
            var book = _bookRepository.Get(id);
            if (book == null)
            {
                throw new NotFoundException(nameof(Book), id);
            }

            return BookDto.ToDto(book);
        }

        //Eliminar un libro por id
        public void DeleteBook(int id)
        {
            var book = _bookRepository.Get(id);
            if (book == null)
            {
                throw new NotFoundException(nameof(Book), id);
            }

            _bookRepository.Delete(book);
        }

        //Obtener un libro por título
        public BookDto GetBookByTittle(string tittle) 
        {
            return BookDto.ToDto(_bookRepository.GetByTittle(tittle));
        }

        //Modificar un libro
        public BookDto UpdateBook(string title, float price)
        {
            var bookToUpdate = _bookRepository.GetByTittle(title);  

           if (bookToUpdate != null)
            {
                bookToUpdate.Price = price;
            }

           return BookDto.ToDto(_bookRepository.Update(bookToUpdate));
        }

        public List<BookDto> GetBooksByTitle(List<string> titles)
        {
            var list = new List<BookDto>();
            foreach (var title in titles)
            {
                var libro = _bookRepository.GetByTittle(title);
                if (libro == null)
                {
                    throw new Exception($"Libro no encontrado.");
                }

                list.Add(BookDto.ToDto(libro));

            }
            return list;
        }
    }
}
