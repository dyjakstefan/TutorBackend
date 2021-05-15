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
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService lessonService;

        public LessonsController(ILessonService ILessonService)
        {
            this.lessonService = ILessonService;
        }

        [HttpPost("Reserve")]
        [Authorize]
        public async Task<IActionResult> ReserveLesson(ReserveLessonRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.UserId = User.GetUserId();
            var result = await lessonService.ReserveLesson(request);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok();
        }

        [HttpGet("Planned")]
        [Authorize]
        public async Task<IActionResult> GetPlannedLessons()
        {
            var userId = User.GetUserId();
            var result = await lessonService.GetPlannedLessons(userId);

            if (result == null)
                return BadRequest("Invalid data.");

            return Ok(result);
        }


        [HttpGet("History")]
        [Authorize]
        public async Task<IActionResult> GetHistoryLessons()
        {
            var userId = User.GetUserId();
            var result = await lessonService.GetHistoryLessons(userId);

            if (result == null)
                return BadRequest("Invalid data.");

            return Ok(result);
        }

        [HttpPost("Accept")]
        [Authorize]
        public async Task<IActionResult> AcceptLesson([FromBody] AcceptLessonRequest request)
        {
            request.TutorId = User.GetUserId();
            var result = await lessonService.AcceptLesson(request);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok();
        }

        [HttpDelete("Reject")]
        [Authorize]
        public async Task<IActionResult> RejectLesson([FromBody] RejectLessonRequest request)
        {
            request.UserId = User.GetUserId();
            var result = await lessonService.RejectLesson(request);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok();
        }
    }
}
