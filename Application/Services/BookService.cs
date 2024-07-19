using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
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
        public List<Book> GetAllBooks(string? titulo = null, string? autor = null)
        {
            return _bookRepository.GetAllBooks(titulo, autor);
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

        public BookDto RemoveBookStock(int bookId)
        {
            Book book = _bookRepository.Get(bookId);

            return (BookDto.ToDto(_bookRepository.RemoveBookStock(book)));
        }
    }
}
