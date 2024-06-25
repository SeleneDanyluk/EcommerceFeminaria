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
        public float Total { get; set; }

        public SaleState SaleState { get; set; } = SaleState.draft;

        public ICollection<Book> Books { get; set; } = new List<Book>();

        public static CartDto ToDto(Cart cart)
        {
            CartDto dto = new CartDto();
            dto.Total = cart.Total;
            dto.SaleState = cart.SaleState;
            dto.Books = cart.Books;

            return dto;

        }

    }
}
