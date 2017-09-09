using System;

namespace Infrastructure.Dto
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}