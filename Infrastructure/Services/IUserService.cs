using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain;
using Infrastructure.Dto;

namespace Infrastructure.Services
{
    public interface IUserService
    {
         Task<IEnumerable<UserDto>> GetAllAsync(string name);
         Task<UserDto> GetAsync(Guid id);
         Task<UserDto> GetAsync(string username);
         Task RegisterAsync(Guid userId, string username, string email, string password);
         Task<TokenDto> LoginUserAsync(string username, string password);
         Task UpdateAsync(Guid userId, string name, string email, string password);
         Task DeleteAsync(Guid userId);
    }
}