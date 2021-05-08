using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Entities;

namespace TutorBackend.Infrastructure.Services.Interfaces
{
    public interface ITopicService
    {
        Task<IList<string>> GetAllTopics();

        Task<bool> AddTopicToTutor(Guid id, string topic);

        Task<bool> RemoveTopicFromTutor(Guid id, string topic);
    }
}
