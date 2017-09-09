using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync(string name = "");
        Task<User> GetUserAsync(Guid userId);
        Task<User> GetUserAsync(string userName);
        Task RegisterAsync(User user);
        Task UpdateAsync(Guid userId, User user);
        Task DeleteAsync(Guid userId);
    }
}