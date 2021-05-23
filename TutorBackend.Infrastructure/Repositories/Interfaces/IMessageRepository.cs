using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Entities;

namespace TutorBackend.Infrastructure.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<bool> CreateMessage(Message message);

        Task<bool> CreateConversation(Conversation conversation);

        Task<Conversation> GetConversation(Guid userId, Guid tutorId);

        Task<IList<Conversation>> GetConversationList(Guid conversationId);
    }
}
