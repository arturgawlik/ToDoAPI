using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class UserInMemoryRepository : IUserRepository
    {
        private static readonly ISet<User> _users = new HashSet<User>
        {
            new User(Guid.NewGuid(), "hufcio", "artur.gawlik@outlook.com", "mypassword"),
            new User(Guid.NewGuid(), "kurkam", "kamil.kural@outlook.com", "myreallypassword")
        };

        public async Task<IEnumerable<User>> GetAllUsersAsync(string name = "")
        {
            var users = _users.AsEnumerable();
            if(!string.IsNullOrWhiteSpace(name))
            {
                await Task.FromResult(users = users.Where(p => p.Username.ToLowerInvariant().Contains(name.ToLowerInvariant())));
            }
            return users;
        }
        public async Task<User> GetUserAsync(Guid userId)
            => await Task.FromResult(_users.FirstOrDefault(p => p.Id == userId));

        public async Task<User> GetUserAsync(string userName)
            => await Task.FromResult(_users.FirstOrDefault(p => p.Username == userName));

        public async Task RegisterAsync(User user)
            => await Task.FromResult(_users.Add(user));

        public async Task UpdateAsync(Guid userId, User user)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid userId)
        {
            var user = await Task.FromResult(_users.FirstOrDefault(p => p.Id == userId));
            if(user == null)
            {
                return;
            }

            _users.Remove(user);
        }
    }
}