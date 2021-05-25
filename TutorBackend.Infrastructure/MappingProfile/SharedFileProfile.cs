using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Entities;

namespace TutorBackend.Infrastructure.MappingProfile
{
    public class SharedFileProfile : Profile
    {
        public SharedFileProfile()
        {
            CreateMap<SharedFile, SharedFileDto>();
        }
    }
}
