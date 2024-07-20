using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Enum;
using Domain.Exceptions;
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

       public CartDto GetCartByUserId(int userId)
        {
            User user = _userRepository.Get(userId);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }
            return (CartDto.ToDto(_cartRepository.GetCartByUserId(userId)));
        }

        public CartDto AddBookToCart(int userId, int bookId)
        {
            User user = _userRepository.Get(userId);
             if (user == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }

            Book book = _bookRepository.Get(bookId);

            if (book == null)
            {
                throw new NotFoundException(nameof(Book), bookId);
            }

            return (CartDto.ToDto(_cartRepository.AddBookToUserCart(user, book)));
        }

        public CartDto RemoveBookFromCart(int userId, int bookId)
        {
            User user = _userRepository.Get(userId);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }

            Book book = _bookRepository.Get(bookId);

            if (book == null)
            {
                throw new NotFoundException(nameof(Book), bookId);
            }

            return (CartDto.ToDto(_cartRepository.RemoveBookFromUserCart(user,book)));
        }

        public CartDto ChangeCartState(int userId)
        {
            User user = _userRepository.Get(userId);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }

            return (CartDto.ToDto(_cartRepository.ChangeCartState(user)));
        }

        public List<Cart> GetClientPurchases(int userId)
        {
            User user = _userRepository.Get(userId);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }

            return _cartRepository.GetClientPurchases(user).ToList();
        }
    }
}
