using System;
using System.Threading.Tasks;
using Infrastructure.Commands.Users;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Controllers;

namespace API.Controllers
{
    [Route("[controller]")]
    public class UserController : ApIBaseController
    {
        private readonly IUserService _userservice;

        public UserController(IUserService userService)
        {
            _userservice = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string name)
        {
            var users = await _userservice.GetAllAsync(name);

            return Json(users);
        }

        //[HttpGet("{userId}")]
        [HttpGet("myAccount")]
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userservice.GetAsync(UserId);

            return Json(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterUser command)
        {
            command.UserId = Guid.NewGuid();
            await _userservice.RegisterAsync(command.UserId, command.Username, command.Email, command.Password);

            return Created($"/user/{command.UserId}", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand command)
            => Json(await _userservice.LoginUserAsync(command.Username, command.Password));
        
        
        [HttpPut]
        public Task<IActionResult> UpdateAsync()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteAsync(Guid userId)
        {
            await _userservice.DeleteAsync(userId);

            return NoContent();
        }

    }
}