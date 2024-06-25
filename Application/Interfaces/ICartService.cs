using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICartService
    {
        public List<Cart> GetCarts();

        public UserDto GetCartByUserId(int UserId);

        public CartDto AddBookToCart(int userId, int bookId);
    }
}
