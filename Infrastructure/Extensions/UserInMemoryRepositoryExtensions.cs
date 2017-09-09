using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using Core.Repositories;

namespace Infrastructure.Extensions
{
    public static class UserInMemoryRepositoryExtensions
    {
        public static async Task CheckEmailAsync(this IUserRepository repository, string email)
        {
            IEnumerable<User> users = await repository.GetAllUsersAsync();
            if (users.Any(user => user.Email == email))
            {
                throw new Exception($"User with email '{email} already exists.");
            }
        }
    }
}