using domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class BookCreateRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public float Price { get; set; }

        public int Stock {  get; set; }

        public static Book ToEntity(BookCreateRequest bookDto)
        {
            Book book = new Book();
            book.Title = bookDto.Title;
            book.Description = bookDto.Description;
            book.Author = bookDto.Author;
            book.Price = bookDto.Price;
            book.Stock = bookDto.Stock; 

            return book;
          
        }
    }
}
