using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;

namespace TutorBackend.Infrastructure.Repositories.Interfaces
{
    public interface ILessonRepository
    {
        Task<bool> ReserveLesson(Lesson lesson);

        Task<IList<Lesson>> GetPlannedLessons(Guid userId);

        Task<IList<Lesson>> GetHistoryLessons(Guid userId);

        Task<Lesson> GetById(Guid id);

        Task<bool> AcceptLesson(Lesson lesson);

        Task<bool> RejectLesson(Lesson lesson);
    }
}
