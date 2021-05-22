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
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<CreateRatingRequest, Rating>(); 
        } 
    }
}
