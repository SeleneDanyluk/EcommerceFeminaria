using domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationContext _context;
        public BookRepository(ApplicationContext context) 
        {
            _context = context;
        }

        //obtener todos los libros de la base de datos
        public List<Book> GetAllBooks()
        {
            IQueryable<Book> query = _context.Books;

            return query.ToList();
        }

        //agregar un nuevo libro
        public void AddBook(Book newBook)
        {
            _context.Books.Add(newBook);
            _context.SaveChanges();
        }
    }
}
