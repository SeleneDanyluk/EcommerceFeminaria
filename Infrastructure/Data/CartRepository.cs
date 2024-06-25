using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        private readonly ApplicationContext _context;

        public CartRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public Cart? GetCartByUserId(int userId)
        {
            Cart u = _context.Carts.Include(b => b.Books).FirstOrDefault(u => u.UserId == userId);
          
            return u; //retorna el usuario con su primer carrito 
        }

        public Cart AddBookToUserCart(User user, Book book)
        {

            var cart = _context.Carts.Include(b => b.Books).FirstOrDefault(u => u.UserId == user.Id);
            cart.Books.Add(book);
            _context.SaveChangesAsync();

            return _context.Carts.Include(b => b.Books).FirstOrDefault(u => u.UserId == user.Id);

        }

    }
}
