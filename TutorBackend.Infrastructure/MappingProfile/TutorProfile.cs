using AutoMapper;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            CreateMap<Tutor, TutorDto>()
                .ForMember(x => x.Rating, opt => opt.MapFrom<RatingResolver>());
        }
    }

    public class RatingResolver : IValueResolver<Tutor, TutorDto, double>
    {
        public double Resolve(Tutor tutor, TutorDto tutorDto, double rating, ResolutionContext context)
        {
            if (tutor.Ratings is {Count: > 0})
            {
                return tutor.Ratings.Average(x => x.Score);
            }

            return 0;
        }
    }
}
