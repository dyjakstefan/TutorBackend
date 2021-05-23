using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TutorBackend.Core.Entities;
using TutorBackend.Infrastructure.Repositories.Interfaces;
using TutorBackend.Infrastructure.SqlServerContext;

namespace TutorBackend.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DatabaseContext dbContext;

        public MessageRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateMessage(Message message)
        {
            message.CreatedAt = DateTime.Now;
            await dbContext.Messages.AddAsync(message);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> CreateConversation(Conversation conversation)
        {
            var conversationFromDb = await GetConversation(conversation.UserId, conversation.TutorId);

            if (conversationFromDb != null)
            {
                return false;
            }

            await dbContext.Conversations.AddAsync(conversation);
            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Conversation> GetConversation(Guid userId, Guid tutorId)
        {
            var conversation = await dbContext.Conversations
                .Include(x => x.Messages.OrderByDescending(m => m.CreatedAt))
                .Include(x => x.Tutor)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.TutorId == tutorId && x.UserId == userId);

            return conversation;
        }

        public async Task<IList<Conversation>> GetConversationList(Guid userId)
        {
            var conversation = await dbContext.Conversations.Include(x => x.Tutor).Include(x => x.User).Where(x => x.UserId == userId || x.TutorId == userId).ToListAsync();

            return conversation;
        }
    }
}
