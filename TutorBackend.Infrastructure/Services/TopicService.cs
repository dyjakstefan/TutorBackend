using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Infrastructure.Repositories.Interfaces;
using TutorBackend.Infrastructure.Services.Interfaces;

namespace TutorBackend.Infrastructure.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository topicRepository;

        public TopicService(ITopicRepository topicRepository)
        {
            this.topicRepository = topicRepository;
        }

        public async Task<bool> AddTopicToTutor(Guid id, string topicName)
        {
            return await topicRepository.AddToTutor(id, topicName);
        }

        public async Task<IList<string>> GetAllTopics()
        {
            return await topicRepository.GetAll();
        }

        public async Task<bool> RemoveTopicFromTutor(Guid id, string topicName)
        {
            return await topicRepository.RemoveFromTutor(id, topicName);
        }
    }
}
