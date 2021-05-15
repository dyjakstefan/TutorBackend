using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Entities;

namespace TutorBackend.Infrastructure.Repositories.Interfaces
{
    public interface ILessonRepository
    {
        Task<bool> ReserveLesson(Lesson lesson);

        Task<IList<Lesson>> GetIncomingLessons(Guid userId);

        Task<IList<Lesson>> GetHistoryLessons(Guid userId);

    }
}
