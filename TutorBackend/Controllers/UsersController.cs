using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorBackend.Core.Requests;
using TutorBackend.Infrastructure.Services.Interfaces;
using TutorBackend.Infrastructure.SqlServerContext;

namespace TutorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var jwt = await userService.CreateUser(request);

            if (jwt == null)
            {
                return BadRequest("Invalid data.");
            }

            return Ok(jwt);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var jwt = await userService.LoginUser(request);

            if (jwt == null)
            {
                return BadRequest("Invalid credentials.");
            }

            return Ok(jwt);
        }

        [Authorize]
        [HttpPost("test")]
        public async Task<IActionResult> Test()
        {
            return Ok("You are authorized.");
        }
    }
}
