﻿using domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        public Book? GetByTittle(string tittle);
    }
}
