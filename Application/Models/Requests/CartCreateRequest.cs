using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class CartCreateRequest
    {
        public List<Book> Books { get; set; }

        public float Total { get; set; }

        public SaleState SaleState { get; set; }

        public int IdUser { get; set; }

        public static Cart ToEntity(CartCreateRequest cartDto)
        {
           Cart cart = new Cart();
            cart.Total = cartDto.Total;
            cart.SaleState = cartDto.SaleState;
            cart.UserId = cartDto.IdUser;
            cart.Books = cartDto.Books;

            return cart;
     
        }
    }
}
