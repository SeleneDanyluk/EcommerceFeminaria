using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public Book? GetByTittle(string tittle) 
        {
            return _context.Books.FirstOrDefault(u => u.Title == tittle);
        }

    }
}
