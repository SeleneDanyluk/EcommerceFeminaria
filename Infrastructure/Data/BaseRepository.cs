using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BaseRepository<T> where T : class
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public List<T> Get()
        {
            return _context.Set<T>().ToList();
        }

        public T? Get<Tid>(Tid id)
        {
            return _context.Set<T>().Find(new object[] {id});
        }
    }
}
