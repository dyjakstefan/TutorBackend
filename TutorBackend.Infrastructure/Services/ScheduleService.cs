using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;
using TutorBackend.Infrastructure.Repositories.Interfaces;
using TutorBackend.Infrastructure.Services.Interfaces;

namespace TutorBackend.Infrastructure.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly ITutorRepository tutorRepository;
        private readonly IMapper mapper;

        public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper, ITutorRepository tutorRepository)
        {
            this.scheduleRepository = scheduleRepository;
            this.tutorRepository = tutorRepository;
            this.mapper = mapper;
        }

        public async Task<bool> CreateScheduleDay(CreateScheduleRequest request)
        {
            if (request.StartAt.Date != request.EndAt.Date || !await tutorRepository.AnyExists(request.UserId))
            {
                return false;
            }

            var schedule = mapper.Map<ScheduleDay>(request);

            if (request.RepeatAfterWeeks == 0)
            {
                return await scheduleRepository.CreateScheduleDay(schedule);
            }

            var duration = request.EndOfRepetition - request.StartAt;

            for (int i = 0; i < duration.TotalDays / 7; i += request.RepeatAfterWeeks)
            {
                schedule.StartAt = request.StartAt.AddDays(7 * i);
                schedule.EndAt = request.EndAt.AddDays(7 * i);
                await scheduleRepository.CreateScheduleDay(schedule);
            }

            return true;
        }

        public async Task<IList<ScheduleDayDto>> GetAllSchedulesForTutor(string username)
        {
            var schedules = await scheduleRepository.GetAllForTutor(username);

            var schedulesDto = mapper.Map<IList<ScheduleDayDto>>(schedules);

            return schedulesDto;
        }

        public async Task<bool> UpdateScheduleDay(UpdateScheduleRequest request)
        {
            if (request.StartAt.Date != request.EndAt.Date || !await tutorRepository.AnyExists(request.UserId))
            {
                return false;
            }

            return await scheduleRepository.UpdateScheduleDay(request);
        }

        public async Task<bool> DeleteScheduleDay(Guid scheduleId, Guid userId)
        {
            if (!await tutorRepository.AnyExists(userId))
            {
                return false;
            }

            var result = await scheduleRepository.DeleteScheduleDay(scheduleId, userId);

            return result;
        }
    }
}
