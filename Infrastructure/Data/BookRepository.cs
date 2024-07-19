using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Reflection.Metadata.BlobBuilder;

namespace Infrastructure.Data
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly ApplicationContext _context;
        public BookRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public Book? GetByTittle(string tittle) 
        {
            return _context.Books.FirstOrDefault(u => u.Title == tittle);
        }
        public List<Book> GetAllBooks(string? titulo = null, string? autor = null)
        {
            if (!string.IsNullOrWhiteSpace(titulo))
            {
                return _context.Books.Where(b => b.Title.ToLower().Contains(titulo.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(autor))
            {
                return _context.Books.Where(b => b.Author.ToLower().Contains(autor.ToLower())).ToList();
            }
            return _context.Books.ToList();
        }

    }
}
