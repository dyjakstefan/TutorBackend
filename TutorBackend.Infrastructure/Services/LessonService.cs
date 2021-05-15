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
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository lessonRepository;
        private readonly IScheduleRepository scheduleRepository;
        private readonly ITutorRepository tutorRepository;
        private readonly IMapper mapper;

        public LessonService(ILessonRepository lessonRepository, IScheduleRepository scheduleRepository, IMapper mapper, ITutorRepository tutorRepository)
        {
            this.lessonRepository = lessonRepository;
            this.scheduleRepository = scheduleRepository;
            this.tutorRepository = tutorRepository;
            this.mapper = mapper;
        }

        public async Task<bool> ReserveLesson(ReserveLessonRequest request)
        {
            var tutor = await tutorRepository.GetByUsername(request.TutorUsername);
            var schedule = await scheduleRepository.GetScheduleById(request.ScheduleId);

            if (tutor == null || schedule == null)
            {
                return false;
            }

            var isDateReserved = schedule.Lessons.Any(x =>
                (x.StartAt >= request.StartAt && x.EndAt <= request.EndAt) ||
                (x.StartAt <= request.StartAt && x.EndAt >= request.EndAt) ||
                (x.EndAt >= request.StartAt && x.StartAt <= request.EndAt));

            if (isDateReserved)
            {
                return false;
            }

            var lesson = mapper.Map<Lesson>(request);
            
            var result = await lessonRepository.ReserveLesson(lesson);

            return result;
        }

        public async Task<IList<LessonDto>> GetIncomingLessons(Guid userId)
        {
            var lessons = await lessonRepository.GetIncomingLessons(userId);

            var lessonsDto = mapper.Map<IList<LessonDto>>(lessons);

            return lessonsDto;
        }

        public async Task<IList<LessonDto>> GetHistoryLessons(Guid userId)
        {
            var lessons = await lessonRepository.GetHistoryLessons(userId);

            var lessonsDto = mapper.Map<IList<LessonDto>>(lessons);

            return lessonsDto;
        }
    }
}
