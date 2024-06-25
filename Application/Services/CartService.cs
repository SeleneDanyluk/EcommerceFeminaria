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
        private readonly IBookRepository _bookRepository;

        public CartService(ICartRepository cartRepository, IUserRepository userRepository, IBookRepository bookRepository)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        public List<Cart> GetCarts()
        {
            return _cartRepository.Get();
        }

      

       public UserDto GetCartByUserId(int UserId)
        {
            User u = _userRepository.Get(UserId);

            if (u == null)
            {
                throw new Exception($"User incorrecto.");
            }

            return (UserDto.ToDto(_cartRepository.GetCartByUserId(UserId)));
        }

        public CartDto AddBookToCart(int userId, int bookId)
        {
            User u = _userRepository.Get(userId);

            Book b = _bookRepository.Get(bookId);

            return (CartDto.ToDto(_cartRepository.AddBookToUserCart(u, b)));
        }
    }
}
