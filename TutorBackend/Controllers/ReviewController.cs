using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TutorBackend.Core.Requests;
using TutorBackend.Infrastructure.Extensions;
using TutorBackend.Infrastructure.Services.Interfaces;

namespace TutorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateReviewRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.Username = User.Identity.Name;
            var result = await reviewService.CreateReview(request);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UpdateReviewRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.Username = User.Identity.Name;
            var result = await reviewService.UpdateReview(request);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] DeleteReviewRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.Username = User.Identity.Name;
            var result = await reviewService.DeleteReview(request);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews([FromQuery] string username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await reviewService.GetReviewForTutor(username);

            if (result == null)
                return BadRequest("Invalid data.");

            return Ok(result);
        }
    }
}
