using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookService _bookService;

        public CartService(ICartRepository cartRepository, IUserRepository userRepository, IBookService bookService)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _bookService = bookService;
        }

        public List<Cart> GetCarts()
        {
            return _cartRepository.Get();
        }

        public Cart CreateCart(int UserId, List<string> booksTitle)
        {
            float total = 0;
          
            foreach (var book in booksTitle)
            {
                var libroExistente = _bookService.GetBookByTittle(book);
                if (libroExistente == null)
                {
                    throw new Exception($"Libro no encontrado.");
                }

                total += libroExistente.Price;
                
            }

            Cart cart = new Cart(); 
            cart.Total = total;
            cart.SaleState = SaleState.draft;
            cart.UserId = UserId;

            return _cartRepository.Create(cart);
           
        }

       
    }
}
