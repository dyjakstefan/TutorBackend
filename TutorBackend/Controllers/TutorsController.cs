using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using TutorBackend.Core.Requests;
using TutorBackend.Infrastructure.Extensions;
using TutorBackend.Infrastructure.Services.Interfaces;

namespace TutorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorsController : ControllerBase
    {
        private readonly ITutorService tutorService;

        public TutorsController(ITutorService tutorService)
        {
            this.tutorService = tutorService;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateTutorProfile(CreateTutorProfileRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.UserId = User.GetUserId();
            var result = await tutorService.CreateTutorProfile(request);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok();
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetTutorProfile(string username)
        {
            var result = await tutorService.GetTutorProfile(username);

            if (result == null)
                return BadRequest("Invalid data.");

            return Ok(result);
        }

        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteTutorProfile()
        {
            var userId = User.GetUserId();
            var result = await tutorService.DeleteTutorProfile(userId);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok();
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> UpdateTutorProfile(UpdateTutorProfileRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.UserId = User.GetUserId();
            var result = await tutorService.UpdateTutorProfile(request);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok();
        }
    }
}
