using System;
using System.Collections.Generic;
using Core.Domain;
using Core.Repositories;
using System.Linq;
using Infrastructure.Dto;
using Infrastructure.Extensions;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IJwtHandler _jwtHandler;

        public UserService(IUserRepository repository, IJwtHandler jwtHandler)
        {
            _repository = repository;
            _jwtHandler = jwtHandler;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync(string name)
        {
            var users = await _repository.GetAllUsersAsync(name);
            IList<UserDto> usersDto = new List<UserDto>();
            foreach(var user in users)
            {
                usersDto.Add(new UserDto
                {
                    Username = user.Username,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt
                });
            }

            return usersDto;
        }
        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _repository.GetUserAsync(id);
            if(user == null)
            {
                throw new Exception($"There is no user with id: '{id}'.");
            }

            return new UserDto
            {
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserDto> GetAsync(string username)
        {
            var user = await _repository.GetUserAsync(username);
            if(user == null)
            {
                throw new Exception($"There is no user with name: '{username}'.");
            }

            return new UserDto
            {
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task RegisterAsync(Guid userId, string username, string email, string password)
        {
            await _repository.CheckEmailAsync(email);
            var user = await _repository.GetUserAsync(username);
            if(user != null)
            {
                throw new Exception($"User with name: '{username}.'");
            }
            user = new User(userId, username, email, password);
            await Task.FromResult(_repository.RegisterAsync(user));
        }

        public async Task<TokenDto> LoginUserAsync(string username, string password)
        {
            var user = await _repository.GetUserAsync(username);
            if (user == null)
            {
                throw new Exception("Invalid credentials.");
            }
            if (user.Password != password)
            {
                throw new Exception("Invalid credentials.");
            }
            var jwt = _jwtHandler.CreateToken(user.Id);

            return new TokenDto
            {
                Token = jwt.Token,
                Expires = jwt.Expires
            };
        }

        public async Task UpdateAsync(Guid userId, string name, string email, string password)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid userId)
        {
            await Task.FromResult(_repository.DeleteAsync(userId));
        }
    }
}