using AutoMapper;
using System;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;

namespace TutorBackend.Infrastructure.MappingProfile
{
    public class TutorProfile : Profile
    {
        public TutorProfile()
        {
            CreateMap<string, Topic>()
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s));

            CreateMap<Topic, string>()
                .ConvertUsing(x => x.Name);

            CreateMap<CreateTutorProfileRequest, Tutor>()
                .ForMember(x => x.Topics, opt => opt.MapFrom(s => s.Topics))
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.UserId));

            CreateMap<UpdateTutorProfileRequest, Tutor>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.UserId));

            CreateMap<User, Tutor>();

            CreateMap<Tutor, TutorDto>();
        }
    }
}
