using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public float Total { get; set; }

        public SaleState SaleState { get; set; } = SaleState.draft;

        public ICollection<Book> Books { get; set; } = new List<Book>();

        public int UserId { get; set; }

    }
}
