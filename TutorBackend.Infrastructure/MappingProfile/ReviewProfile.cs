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
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<CreateReviewRequest, Review>();

            CreateMap<Review, ReviewDto>()
                .ForMember(x => x.TutorUsername, opt => opt.MapFrom(x => x.Tutor.Username));
        }
    }
}
