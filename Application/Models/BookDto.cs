using domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BookDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public float Price { get; set; }

        public static BookDto BookToDto(Book book)
        {
            BookDto bookDto = new();
            bookDto.Title = book.Title;
            bookDto.Description = book.Description;
            bookDto.Author = book.Author;
            bookDto.Price = book.Price;

            return bookDto;

        }
    }
}
