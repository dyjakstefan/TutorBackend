using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Entities;
using TutorBackend.Infrastructure.Repositories.Interfaces;
using TutorBackend.Infrastructure.SqlServerContext;

namespace TutorBackend.Infrastructure.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly DatabaseContext dbContext;

        public TopicRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddAll(IList<Topic> topics)
        {
            var newTopics = topics.Where(x => !dbContext.Topics.Any(t => t.Name == x.Name));

            await dbContext.Topics.AddRangeAsync(newTopics);
            var result = await dbContext.SaveChangesAsync();

            return result >= 0;
        }

        public async Task<bool> AddToTutor(Guid userId, string topicName)
        {
            var topic = await dbContext.Topics.FirstOrDefaultAsync(x => x.Name == topicName);

            var tutor = await dbContext.Tutors.Include(x => x.Topics).FirstOrDefaultAsync(x => x.Id == userId);
            tutor.Topics.Add(topic ?? new Topic { Name = topicName });
            var result = await dbContext.SaveChangesAsync();

            return result >= 0;
        }

        public async Task<IList<string>> GetAll()
        {
            return await dbContext.Topics.Select(x => x.Name).ToListAsync();
        }

        public async Task<bool> RemoveFromTutor(Guid userId, string topicName)
        {
            var tutor = await dbContext.Tutors.Include(x => x.Topics).FirstOrDefaultAsync(x => x.Id == userId);
            var topic = tutor.Topics.FirstOrDefault(x => x.Name == topicName);

            if (topic != null)
            {
                tutor.Topics.Remove(topic);
                var result = await dbContext.SaveChangesAsync();

                return result > 0;
            }

            return true;
        }
    }
}
