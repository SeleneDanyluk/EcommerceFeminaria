using Domain.Entities;
using Domain.Interfaces;
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


    }
}
