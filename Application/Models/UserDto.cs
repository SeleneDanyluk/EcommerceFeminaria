using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UserDto
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public UserType UserType { get; set; }


        public static UserDto ToDto(User user)
        {
            UserDto userDto = new();
            userDto.Name = user.Name;
            userDto.Email = user.Email;
            userDto.Password = user.Password;
            userDto.UserType = user.UserType;//Ver con roles super admin y client
      
            return userDto;

        }

    }
}
