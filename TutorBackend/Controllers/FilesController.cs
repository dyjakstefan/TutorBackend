using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TutorBackend.Infrastructure.Extensions;
using TutorBackend.Infrastructure.Services.Interfaces;

namespace TutorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly ISharedFileService sharedFileService;

        public FilesController(ISharedFileService sharedFileService)
        {
            this.sharedFileService = sharedFileService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadFile(IFormFile file, string username)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            await using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            var userId = User.GetUserId();

            var result = await sharedFileService.UploadFile(stream, file.FileName, username, userId);

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DownloadFile(Guid fileId)
        {
            if (fileId == Guid.Empty)
            {
                return BadRequest();
            }

            var userId = User.GetUserId();

            var fileDto = await sharedFileService.DownloadFile(fileId, userId);

            return File(fileDto.Stream, fileDto.ContentType, fileDto.FileName);
        }
    }
}
