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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var jwt = await userService.CreateUser(request);

            if (jwt == null)
                return BadRequest("Invalid data.");

            return Ok(jwt);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var jwt = await userService.LoginUser(request);

            if (jwt == null)
                return BadRequest("Invalid credentials.");

            return Ok(jwt);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var username = User.Identity.Name;
            var result = await userService.GetUser(username);

            if (result == null)
                return BadRequest("Invalid data.");

            return Ok(result);
        }
    }
}
