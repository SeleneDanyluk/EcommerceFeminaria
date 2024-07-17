using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class UserCreateRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserType UserType { get; set; }
        public static User ToEntity(UserCreateRequest userDto)
        {
            User user = new User();
            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.Password = userDto.Password;
            user.UserType = userDto.UserType;
            Cart cart = new Cart();
            user.Carts.Add(cart);
     
            return user;

        }
    }
}
