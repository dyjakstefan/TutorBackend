using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Entities;
using TutorBackend.Infrastructure.Repositories.Interfaces;
using TutorBackend.Infrastructure.Services.Interfaces;

namespace TutorBackend.Infrastructure.Services
{
    public class SharedFileService : ISharedFileService
    {
        private readonly ISharedFileRepository sharedFileRepository;
        private readonly IUserRepository userRepository;
        private readonly IMessageRepository messageRepository;
        private readonly IBlobStorageService blobStorageService;

        public SharedFileService(ISharedFileRepository sharedFileRepository, IUserRepository userRepository, IMessageRepository messageRepository, IBlobStorageService blobStorageService)
        {
            this.sharedFileRepository = sharedFileRepository;
            this.userRepository = userRepository;
            this.messageRepository = messageRepository;
            this.blobStorageService = blobStorageService;
        }

        public async Task<Guid> UploadFile(MemoryStream stream, string filename, string username, Guid userId)
        {
            var author = await userRepository.GetById(userId);
            var user = await userRepository.GetByUsername(username);

            if (author == null || user == null)
            {
                return Guid.Empty;
            }

            Conversation conversation;
            if (user.UserType == Constants.Tutor)
            {
                conversation = await messageRepository.GetConversation(userId, user.Id);
            }
            else
            {
                conversation = await messageRepository.GetConversation(user.Id, userId);
            }

            await blobStorageService.UploadFile(stream, filename, conversation.Id.ToString());

            var file = new SharedFile
            {
                ConversationId = conversation.Id,
                Issuer = author.Username,
                CreatedAt = DateTime.Now,
                FileName = filename,
                ContainerId = ""
            };

            var result = await sharedFileRepository.CreateFile(file);
            return result;
        }

        public async Task<FileDto> DownloadFile(Guid fileId, Guid userId)
        {
            var author = await userRepository.GetById(userId);
            var file = await sharedFileRepository.GetFile(fileId);

            if (author == null || file == null)
            {
                return null;
            }

            if (file.Conversation.UserId != author.Id && file.Conversation.TutorId != author.Id) 
                return null;

            var stream = await blobStorageService.DownloadFile(file.FileName, file.ConversationId.ToString());

            var fileDto = new FileDto
            {
                FileName = file.FileName,
                Stream = stream
            };

            return fileDto;
        }
    }
}
