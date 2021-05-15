using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;

namespace TutorBackend.Infrastructure.MappingProfile
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            CreateMap<ReserveLessonRequest, Lesson>()
                .ForMember(x => x.ScheduleDayId, opt => opt.MapFrom(x => x.ScheduleId));

            CreateMap<Lesson, LessonDto>()
                .ForMember(x => x.TutorUsername, opt => opt.MapFrom(x => x.ScheduleDay.Tutor.Username))
                .ForMember(x => x.StudentUsername, opt => opt.MapFrom(x => x.User.Username));
        }
    }
}
