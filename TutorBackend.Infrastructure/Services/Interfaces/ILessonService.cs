using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Requests;

namespace TutorBackend.Infrastructure.Services.Interfaces
{
    public interface ILessonService
    {
        Task<bool> ReserveLesson(ReserveLessonRequest request);

        Task<IList<LessonDto>> GetPlannedLessons(Guid userId);

        Task<IList<LessonDto>> GetHistoryLessons(Guid userId);

        Task<bool> AcceptLesson(AcceptLessonRequest request);

        Task<bool> RejectLesson(RejectLessonRequest request);
    }
}
