using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Entities
{
    public abstract class User
    {
        public int Id { get; set; }

        public string Name { get; set; }    

        public string Email { get; set; }       

        public string Password { get; set; }

        public UserType UserType { get; set; }

        public List<Cart> Purchase {  get; set; } 

    }
}
