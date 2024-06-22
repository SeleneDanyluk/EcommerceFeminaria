using Application.Interfaces;
using Application.Models.Requests;
using Application.Models;
using domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService (IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.Get();
        }

        public UserDto AddNewUser(UserCreatedRequest userDto)
        {
            return UserDto.ToDto(_userRepository.Create(UserCreatedRequest.ToEntity(userDto)));
        }

        public UserDto GetUserByEmail(string email)
        {
            return UserDto.ToDto(_userRepository.GetByEmail(email));
        }

        public UserAuthenticationRequest GetUserToAuthenticate(string email)
        {
            UserDto entity = GetUserByEmail(email);

            UserAuthenticationRequest entityToAuthenticate = new();
            entityToAuthenticate.Email = entity.Email;
            entityToAuthenticate.UserType = entity.UserType;
            entityToAuthenticate.Password = entity.Password;

            return entityToAuthenticate;
        }

        public void UpdateUser(int id, string password)
        {   
            User? user = _userRepository.Get(id); //guardamos el usuario al que refiere el id
            if (user == null){
                throw new NotFoundException(nameof(User), id);
            }
            user.Password = password;
            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(_userRepository.Get(id));
        }
    }
}
