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
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public LessonService(ILessonRepository lessonRepository, IScheduleRepository scheduleRepository, IMapper mapper, ITutorRepository tutorRepository, IUserRepository userRepository)
        {
            this.lessonRepository = lessonRepository;
            this.scheduleRepository = scheduleRepository;
            this.tutorRepository = tutorRepository;
            this.userRepository = userRepository;
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
            lesson.TutorId = tutor.Id;

            var result = await lessonRepository.ReserveLesson(lesson);

            return result;
        }

        public async Task<IList<LessonDto>> GetPlannedLessons(Guid userId)
        {
            var lessons = await lessonRepository.GetPlannedLessons(userId);

            var lessonsDto = mapper.Map<IList<LessonDto>>(lessons);

            return lessonsDto;
        }

        public async Task<IList<LessonDto>> GetHistoryLessons(Guid userId)
        {
            var lessons = await lessonRepository.GetHistoryLessons(userId);

            var lessonsDto = mapper.Map<IList<LessonDto>>(lessons);

            return lessonsDto;
        }

        public async Task<bool> AcceptLesson(AcceptLessonRequest request)
        {
            var tutor = await tutorRepository.GetById(request.TutorId);
            var lesson = await lessonRepository.GetById(request.LessonId);

            if (tutor == null || lesson == null || tutor.Id != lesson.TutorId)
            {
                return false;
            }

            return await lessonRepository.AcceptLesson(lesson);
        }

        public async Task<bool> RejectLesson(RejectLessonRequest request)
        {
            var user = await userRepository.GetById(request.UserId);
            var lesson = await lessonRepository.GetById(request.LessonId);

            if (user == null || lesson == null || !(user.Id == lesson.TutorId || user.Id == lesson.UserId))
            {
                return false;
            }

            return await lessonRepository.RejectLesson(lesson);
        }
    }
}
