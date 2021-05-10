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
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<CreateScheduleRequest, ScheduleDay>()
                .ForMember(x => x.TutorId, opt => opt.MapFrom(s => s.UserId));

            CreateMap<ScheduleDay, ScheduleDayDto>();
        }
    }
}
