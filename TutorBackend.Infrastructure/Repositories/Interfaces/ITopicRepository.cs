using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Entities;

namespace TutorBackend.Infrastructure.Repositories.Interfaces
{
    public interface ITopicRepository
    {
        Task<bool> AddAll(IList<Topic> topics);

        Task<IList<string>> GetAll();

        Task<bool> RemoveFromTutor(Guid userId, string topicName);

        Task<bool> AddToTutor(Guid userId, string topicName);
    }
}
