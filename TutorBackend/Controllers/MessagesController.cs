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
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessagesController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost("Conversation")]
        [Authorize]
        public async Task<IActionResult> CreateConversation([FromBody] CreateConversationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.UserId = User.GetUserId();
            var result = await messageService.CreateConversation(request);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateMessage([FromBody] CreateMessageRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.AuthorId = User.GetUserId();
            var result = await messageService.CreateMessage(request);

            if (result == false)
                return BadRequest("Invalid data.");

            return Ok();
        }

        [HttpGet("Conversations")]
        [Authorize]
        public async Task<IActionResult> GetConversation()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await messageService.GetConversationList(User.GetUserId());

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetConversation([FromQuery] string username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await messageService.GetConversation(username, User.GetUserId());

            return Ok(result);
        }
    }
}
