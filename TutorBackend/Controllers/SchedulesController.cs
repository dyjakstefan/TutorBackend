
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TutorBackend.Core.Requests;
using TutorBackend.Infrastructure.Extensions;
using TutorBackend.Infrastructure.Services.Interfaces;

namespace TutorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService scheduleService;

        public SchedulesController(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateScheduleDay(CreateScheduleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.UserId = User.GetUserId();
            var result = await scheduleService.CreateScheduleDay(request);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok();
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetAllSchedulesForTutor(string username)
        {
            var result = await scheduleService.GetAllSchedulesForTutor(username);

            if (result == null)
                return BadRequest("Invalid data.");

            return Ok(result);
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> UpdateSchedule(UpdateScheduleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.UserId = User.GetUserId();
            var result = await scheduleService.UpdateScheduleDay(request);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok();
        }

        [HttpDelete("Delete/{scheduleId}")]
        [Authorize]
        public async Task<IActionResult> DeleteSchedule(Guid scheduleId)
        {
            var userId = User.GetUserId();
            var result = await scheduleService.DeleteScheduleDay(scheduleId, userId);

            if (result == null)
                return BadRequest("Invalid data.");

            return Ok(result);
        }

    }
}
