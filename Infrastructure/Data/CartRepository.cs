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

        public Cart ChangeCartState(User user)
        {
            Cart c = _context.Carts.FirstOrDefault(u => u.UserId == user.Id && u.SaleState == Domain.Enum.SaleState.draft);
            if (c != null)
            {
                c.SaleState = Domain.Enum.SaleState.confirmed;
  
            }
            _context.Users.FirstOrDefault(u => u.Id == user.Id).Carts.Add(new Cart());
            _context.SaveChanges();

            return _context.Carts.Include(b => b.Books).FirstOrDefault(u => u.UserId == user.Id && u.SaleState == Domain.Enum.SaleState.draft);
        }

        public Cart? GetCartByUserId(int userId)
        {
            Cart u = _context.Carts
                .Include(b => b.Books)
                .FirstOrDefault(u => u.UserId == userId && u.SaleState == Domain.Enum.SaleState.draft);

          
            return u;  
        }

        public Cart AddBookToUserCart(User user, Book book)
        {
            var cart = _context.Carts.Include(b => b.Books).FirstOrDefault(u => u.UserId == user.Id && u.SaleState == Domain.Enum.SaleState.draft);
            cart.Books.Add(book);
            if (cart != null)
            {
                cart.Total += book.Price;
                book.Stock = book.Stock - 1;
            }
            _context.SaveChangesAsync();

            return _context.Carts.Include(b => b.Books).FirstOrDefault(u => u.UserId == user.Id && u.SaleState == Domain.Enum.SaleState.draft);
        }

        public Cart RemoveBookFromUserCart(User user, Book book)
        {

            var cart = _context.Carts.Include(b => b.Books).FirstOrDefault(u => u.UserId == user.Id && u.SaleState == Domain.Enum.SaleState.draft);
            cart.Books.Remove(book);
            if (cart != null)
            {
                cart.Total -= book.Price;
                book.Stock = book.Stock + 1;
            }
            _context.SaveChangesAsync();

            return _context.Carts.Include(b => b.Books).FirstOrDefault(u => u.UserId == user.Id && u.SaleState == Domain.Enum.SaleState.draft);

        }

        public List<Cart> GetClientPurchases(User user)
        {
            var carts = _context.Carts
                .Where(u => u.UserId == user.Id && u.SaleState == Domain.Enum.SaleState.confirmed)
                .Include(b => b.Books)
                .OrderByDescending(c => c.Id)
                .ToList();
            return carts;
        }
    }
}
