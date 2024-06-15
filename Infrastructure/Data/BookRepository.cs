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
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly ApplicationContext _context;
        public BookRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        //obtener todos los libros de la base de datos
        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        //agregar un nuevo libro
        public Book AddBook(Book newBook)
        {
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return newBook;
        }
    }
}
