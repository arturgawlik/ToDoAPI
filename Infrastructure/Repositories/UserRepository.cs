using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain;
using Core.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }


        public async Task<IEnumerable<User>> GetAllUsersAsync(string name = "")
            => await Users.AsQueryable().ToListAsync();

        public async Task<User> GetUserAsync(Guid userId)
            => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == userId);

        public async Task<User> GetUserAsync(string userName)
            => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Username == userName);

        public async Task RegisterAsync(User user)
            => await Users.InsertOneAsync(user);

        public async Task UpdateAsync(Guid userId, User user)
            => await Users.ReplaceOneAsync(x => x.Id == userId, user);
        public async Task DeleteAsync(Guid userId)
            => await Users.DeleteOneAsync(x => x.Id == userId);

        private IMongoCollection<User> Users => _database.GetCollection<User>("Users");

    }
}