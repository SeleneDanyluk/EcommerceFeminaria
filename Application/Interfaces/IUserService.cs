using Application.Models;
using Application.Models.Requests;
using domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
       List<User> GetAllUsers();

        UserDto AddNewUser(UserCreatedRequest userDto);

        UserDto GetUserByEmail(string email);

     

        void UpdateUser(UserCreatedRequest userDto);

        void DeleteUser(int id);
    }
}
