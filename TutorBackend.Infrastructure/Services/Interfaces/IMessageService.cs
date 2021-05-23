using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Requests;

namespace TutorBackend.Infrastructure.Services.Interfaces
{
    public interface IMessageService
    {
        Task<bool> CreateMessage(CreateMessageRequest request);

        Task<bool> CreateConversation(CreateConversationRequest request);

        Task<IList<ConversationDto>> GetConversationList(Guid conversationId);

        Task<ConversationDto> GetConversation(string username, Guid userId);
    }
}
