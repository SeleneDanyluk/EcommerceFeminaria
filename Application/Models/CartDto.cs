using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CartDto
    {
        public List<Book> Books { get; set; }

        public float Total { get; set; }

        public SaleState SaleState { get; set; }

        public int UserId { get; set; }

        public List<BookDto> BooksList { get; set; }

        public static CartDto ToDto(Cart cart)
        {
            CartDto cartDto = new();
            cartDto.Books = cart.Books;
            cartDto.SaleState = cart.SaleState;
            cartDto.Total = cart.Total;

            return cartDto;

        }
    }
}
