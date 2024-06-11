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

        public Book AddNewBook(Book book);
    }
}
