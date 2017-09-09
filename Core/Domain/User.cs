using System;

namespace Core.Domain
{
    public class User : Entity
    {           
        public string Username { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }    
        public DateTime CreatedAt { get; protected set; }

        protected User()
        {
        }
        
        public User(Guid id, string username, string email, string password)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            CreatedAt = DateTime.UtcNow;
        }
    }
}