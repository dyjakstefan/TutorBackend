using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorBackend.Infrastructure.Extensions;
using TutorBackend.Infrastructure.Services.Interfaces;

namespace TutorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService topicService;

        public TopicsController(ITopicService topicService)
        {
            this.topicService = topicService;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var result = await topicService.GetAllTopics();

            if (result == null)
                return BadRequest("Invalid data.");

            return Ok(result);
        }

        [HttpPost("Add")]
        [Authorize]
        public async Task<IActionResult> AddTopicToTutor([FromBody] string topic)
        {
            var userId = User.GetUserId();
            var result = await topicService.AddTopicToTutor(userId, topic);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok(result);
        }


        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteTopicFromTutor([FromBody] string topic)
        {
            var userId = User.GetUserId();
            var result = await topicService.RemoveTopicFromTutor(userId, topic);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok(result);
        }
    }
}
