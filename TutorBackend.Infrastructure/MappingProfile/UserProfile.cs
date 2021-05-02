using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;

namespace TutorBackend.Infrastructure.MappingProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserRequest, User>()
                .ForMember(x => x.Id, opt => opt.MapFrom(o => Guid.NewGuid()))
                .ForMember(x => x.Password, opt => opt.Ignore());
            //CreateMap<User, UserDto>();
        }
    }
}
