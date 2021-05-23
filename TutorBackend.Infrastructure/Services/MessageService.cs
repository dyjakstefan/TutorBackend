using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TutorBackend.Core;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Enums;
using TutorBackend.Core.Requests;
using TutorBackend.Infrastructure.Repositories.Interfaces;
using TutorBackend.Infrastructure.Services.Interfaces;

namespace TutorBackend.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<bool> CreateMessage(CreateMessageRequest request)
        {
            var user = await userRepository.GetByUsername(request.Username);
            var author = await userRepository.GetById(request.AuthorId);
            Conversation conversation;

            if (user.UserType == Constants.Tutor)
            {
                conversation = await messageRepository.GetConversation(request.AuthorId, user.Id);
            }
            else
            {
                conversation = await messageRepository.GetConversation(user.Id, request.AuthorId);
            }

            if (conversation == null)
            {
                return false;
            }

            var message = new Message
            {
                Text = request.Text,
                ConversationId = conversation.Id,
                Author = author.Username
            };

            return await messageRepository.CreateMessage(message);
        }

        public async Task<bool> CreateConversation(CreateConversationRequest request)
        {
            var issuer = await userRepository.GetById(request.UserId);
            var receiver = await userRepository.GetByUsername(request.Username);

            if (issuer == null || receiver == null)
            {
                return false;
            }

            if ((issuer.UserType != Constants.User || receiver.UserType != Constants.Tutor) &&
                (issuer.UserType != Constants.Tutor || receiver.UserType != Constants.User))
            {
                return false;
            }

            var conversation = new Conversation {Id = Guid.NewGuid()};

            if (issuer.UserType == Constants.Tutor)
            {
                conversation.TutorId = issuer.Id;
                conversation.UserId = receiver.Id;
            }
            else
            {
                conversation.TutorId = receiver.Id;
                conversation.UserId = issuer.Id;
            }

            var result = await messageRepository.CreateConversation(conversation);
            return result;
        }

        public async Task<IList<ConversationDto>> GetConversationList(Guid userId)
        {
            var conversation = await messageRepository.GetConversationList(userId);

            return mapper.Map<List<ConversationDto>>(conversation);
        }

        public async Task<ConversationDto> GetConversation(string username, Guid userId)
        {
            var user = await userRepository.GetByUsername(username);
            Conversation conversation;

            if (user.UserType == Constants.Tutor)
            {
                conversation = await messageRepository.GetConversation(userId, user.Id);
            }
            else
            {
                conversation = await messageRepository.GetConversation(user.Id, userId);
            }

            return mapper.Map<ConversationDto>(conversation);
        }
    }
}
