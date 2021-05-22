using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TutorBackend.Core.Requests;
using TutorBackend.Infrastructure.Services.Interfaces;

namespace TutorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService ratingService;

        public RatingController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateRatingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.Issuer = User.Identity.Name;
            var result = await ratingService.CreateRating(request);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews([FromQuery] string username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await ratingService.GetRating(username);

            return Ok(result);
        }
    }
}
