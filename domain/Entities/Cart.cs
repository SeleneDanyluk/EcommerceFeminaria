using domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        public List<Book> Books { get; set; }

        public float Total { get; set; }

        public SaleState SaleState { get; set; }

    }
}
