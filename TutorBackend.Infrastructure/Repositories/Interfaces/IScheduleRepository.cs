using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;

namespace TutorBackend.Infrastructure.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        Task<bool> CreateScheduleDay(ScheduleDay schedule);

        Task<IList<ScheduleDay>> GetAllForTutor(string username);

        Task<IList<ScheduleDay>> GetActiveForTutor(string username);

        Task<ScheduleDay> GetScheduleById(Guid scheduleId);

        Task<bool> DeleteScheduleDay(Guid scheduleId, Guid userId);

        Task<bool> UpdateScheduleDay(UpdateScheduleRequest request);
    }
}
