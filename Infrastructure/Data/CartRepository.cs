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

        public User? GetCartByUserId(int userId)
        {
            User u = _context.Users.Include(c => c.Carts).FirstOrDefault(u => u.Id == userId);
          
            return u; //retorna el usuario con su primer carrito 
        }

        public Cart AddBookToUserCart(User user, Book book)
        {

            _context.Users.Include(c => c.Carts).FirstOrDefault(u => u.Id == user.Id).Carts.FirstOrDefault().Books.Add(book);
            _context.SaveChangesAsync();

            return _context.Carts.Include(b => b.Books).FirstOrDefault(u => u.UserId == user.Id);

        }

    }
}
