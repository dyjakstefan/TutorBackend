using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Requests;

namespace TutorBackend.Infrastructure.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<bool> CreateScheduleDay(CreateScheduleRequest request);

        Task<IList<ScheduleDayDto>> GetAllSchedulesForTutor(string username);

        Task<IList<ScheduleDayDto>> GetActiveSchedulesForTutor(string username);

        Task<bool> UpdateScheduleDay(UpdateScheduleRequest request);

        Task<bool> DeleteScheduleDay(Guid scheduleId, Guid userId);
    }
}
